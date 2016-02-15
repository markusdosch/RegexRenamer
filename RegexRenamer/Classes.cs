/* =============================================================================
 * RegexRenamer                                     Copyright (c) 2011 Xiperware
 * http://regexrenamer.sourceforge.net/                      xiperware@gmail.com
 * 
 * This program is free software; you can redistribute it and/or modify it under
 * the terms of the GNU General Public License v2, as published by the Free
 * Software Foundation.
 * 
 * This program is distributed in the hope that it will be useful, but WITHOUT
 * ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS
 * FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
 * =============================================================================
 */


using System.Drawing;
using System.IO;
using System.Windows.Forms;


namespace RegexRenamer
{
  public class RRItem
  {
    public string Filename;   // [subdir\]filename.txt
    public string Basename;   // [subdir\]filename
    public string Extension;  // .txt
    public string Fullpath;   // c:\..\[subdir\]filename.txt
    public string Preview;    // [subdir\]newfilename[.txt]
    public bool Hidden;       // true if hidden file
    public bool Matched;      // true if matches current regex
    public bool PreserveExt;  // true if 'Preserve file extension' checked
    public bool Selected;     // true if row is currently selected

    public RRItem( FileInfo fi, bool hidden, bool preserveext )
    {
      this.Filename = fi.Name;
      this.Basename = fi.Name.Substring( 0, fi.Name.Length - fi.Extension.Length );
      this.Extension = fi.Extension;
      this.Fullpath = fi.FullName;
      this.Preview = fi.Name;
      this.Hidden = hidden;
      this.Matched = false;
      this.PreserveExt = preserveext;
      this.Selected = false;
    }
    public RRItem( DirectoryInfo di, bool hidden, bool preserveext )
    {
      this.Filename = di.Name;
      this.Basename = di.Name;
      this.Extension = "";
      this.Fullpath = di.FullName;
      this.Preview = di.Name;
      this.Hidden = hidden;
      this.Matched = false;
      this.PreserveExt = preserveext;
      this.Selected = false;
    }

    public string Name  // either filename or basename, depending on preserveext
    {
      get
      {
        if( this.PreserveExt )
          return this.Basename;
        else
          return this.Filename;
      }
    }
    public string PreviewExt  // if preserveext, append extension
    {
      get
      {
        if( this.PreserveExt )
          return this.Preview + this.Extension;
        else
          return this.Preview;
      }
    }
  }


  public struct InsertArgs
  {
    public string InsertBefore;       // text to insert before selection/cursor
    public string InsertAfter;        // text to insert after selection/cursor
    public int SelectionStartOffset;  // adjust cursor position (0 = no change, >0 adjust from front, <0 adjust from end)
    public int SelectionLength;       // set selection length (-1 = leave as is)
    public bool GroupSelection;       // [group] if selection, group selection first
    public bool WrapIfSelection;      // [wrap]  if selection, insert InsertBefore before AND after

    public InsertArgs( string ib )
    {
      this.InsertBefore = ib;
      this.InsertAfter = "";
      this.SelectionStartOffset = 0;
      this.SelectionLength = -1;
      this.GroupSelection = false;
      this.WrapIfSelection = false;
    }
    public InsertArgs( string ib, string ia )
    {
      this.InsertBefore = ib;
      this.InsertAfter = ia;
      this.SelectionStartOffset = 0;
      this.SelectionLength = -1;
      this.GroupSelection = false;
      this.WrapIfSelection = false;
    }
    public InsertArgs( string ib, string ia, string flags )
    {
      this.InsertBefore = ib;
      this.InsertAfter = ia;
      this.SelectionStartOffset = 0;
      this.SelectionLength = -1;
      this.GroupSelection = flags.Contains( "group" );
      this.WrapIfSelection = flags.Contains( "wrap" );
    }
    public InsertArgs( string ib, string ia, int sso, int sl )
    {
      this.InsertBefore = ib;
      this.InsertAfter = ia;
      this.SelectionStartOffset = sso;
      this.SelectionLength = sl;
      this.GroupSelection = false;
      this.WrapIfSelection = false;
    }
    public InsertArgs( string ib, string ia, int sso, int sl, string flags )
    {
      this.InsertBefore = ib;
      this.InsertAfter = ia;
      this.SelectionStartOffset = sso;
      this.SelectionLength = sl;
      this.GroupSelection = flags.Contains( "group" );
      this.WrapIfSelection = flags.Contains( "wrap" );
    }

  }


  public class RenameResult
  {
    private int countSuccess = 0;
    private int countErrors = 0;
    private RenameErrorDialog errorDialog = null;

    public bool Cancelled = false;
    public bool RenameToSubfolders = false;

    public bool AnyErrors
    {
      get
      {
        return countErrors != 0;
      }
    }
    public int FilesRenamed
    {
      get
      {
        return countSuccess;
      }
    }

    public void ReportSuccess()
    {
      countSuccess++;
    }
    public void ReportError( string oldname, string newname, string error )
    {
      countErrors++;

      if( errorDialog == null )
        errorDialog = new RenameErrorDialog();

      errorDialog.AddEntry( oldname, newname, error );
    }

    public void ShowErrorDialog( string strFile )
    {
      int countTotal = countSuccess + countErrors;
      errorDialog.Message = "The following " + ( countErrors == 1 ? "error" : "errors" )
                          + " occured during the batch rename.\n" + countSuccess + " of " + countTotal + " " + strFile
                          + ( countTotal == 1 ? " was" : "s were" ) + " renamed successfully.";
      errorDialog.AutoSizeColumns();
      errorDialog.ShowDialog();
    }
  }


  public class FileCount
  {
    public int total;     // num files in active path
    public int shown;     // num files in filelist
    public int filtered;  // num files filtered out
    public int hidden;    // num files hidden (may or may not be shown)

    public void Reset()
    {
      this.total    = 0;
      this.shown    = 0;
      this.filtered = 0;
      this.hidden   = 0;
    }
  }


  public class MyComboBox : ComboBox
  {
    public string newText = "";
    const int WM_PAINT = 0xf;

    protected override void WndProc( ref Message message )
    {
      // workaround to be able to set Text during SelectedIndexChanged event

      if( message.Msg == WM_PAINT && newText != "" )
      {
        this.Text = newText;
        newText = "";
        this.SelectionStart = this.Text.Length;
      }

      base.WndProc( ref message );
    }
  }


  public class MyToolStripSystemRenderer : ToolStripSystemRenderer
  {
    // only draw border on menus (not toolbar itself)
    protected override void OnRenderToolStripBorder( ToolStripRenderEventArgs e )
    {
      if( e.ToolStrip.Name == "" )  // not a toolstrip
        base.OnRenderToolStripBorder( e );
    }

    // draw text labels in image margin
    protected override void OnRenderImageMargin( ToolStripRenderEventArgs e )
    {
      if( e.ToolStrip.Items[0].Name == "txtNumberingStart" )  // numbering menu
      {
        Brush brush = new SolidBrush( SystemColors.WindowText );

        e.Graphics.DrawString( "Start", e.ToolStrip.Font, brush, 3, e.ToolStrip.Items[0].Bounds.Top + 4 );
        e.Graphics.DrawString( "Pad", e.ToolStrip.Font, brush, 3, e.ToolStrip.Items[1].Bounds.Top + 4 );
        e.Graphics.DrawString( "Inc", e.ToolStrip.Font, brush, 3, e.ToolStrip.Items[2].Bounds.Top + 4 );
        e.Graphics.DrawString( "Reset", e.ToolStrip.Font, brush, 3, e.ToolStrip.Items[3].Bounds.Top + 4 );
      }

      base.OnRenderImageMargin( e );
    }
  }


}
