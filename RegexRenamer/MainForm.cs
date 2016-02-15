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


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

using Furty.Windows.Forms;      // ExtractIcons
using Microsoft.Win32;          // Registry
using System.Reflection;        // FieldInfo


namespace RegexRenamer
{
  public partial class MainForm : Form
  {
    #region Consts

    private       int MAX_FILES   = 10000;   // file limit for filelist (was a const)
    private const int MAX_HISTORY = 20;      // number of regex history entries to keep

    #endregion

    #region Variables

    private string activePath = null;     // current path
    private string activeFilter = "*.*";  // current filter

    private List<RRItem> activeFiles = new List<RRItem>();  // files in activePath displayed in filelist
    private Dictionary<string, InactiveReason> inactiveFiles = new Dictionary<string, InactiveReason>();  // files in activePath but not displayed

    private Dictionary<string, Icon> icons = new Dictionary<string, Icon>();  // key = file extension

    private bool validFilter = true;      // file filter is valid
    private bool validMatch  = true;      // regex match expression is valid
    private bool validNumber = true;      // numbering menu options are all valid

    private int countProgLaunches = 1;    // counters for
    private int countFilesRenamed = 0;    // about-dialog stats

    private FileCount fileCount = new FileCount();  // holds file counts (total/shown/filtered/hidden)

    private About aboutForm;

    private enum InactiveReason
    {
      Hidden,
      Filtered
    }

    #endregion

    #region Properties

    // prevent setting to true if rename operation in progress

    private bool enableUpdates = true;
    private bool EnableUpdates
    {
      get
      {
        return enableUpdates;
      }
      set
      {
        if( value && bgwRename.IsBusy ) return;
        enableUpdates = value;
      }
    }


    // when toggling rename folders, update menus, change strings, etc.

    private bool renameFolders = false;
    private bool RenameFolders
    {
      get
      {
        return renameFolders;
      }
      set
      {
        renameFolders = value;


        // update menu

        if( itmRenameFolders.Checked != renameFolders )
        {
          itmRenameFiles.Checked   = !renameFolders;
          itmRenameFolders.Checked =  renameFolders;
        }


        // preserve file extensions disabled when renaming folders

        itmOptionsPreserveExt.Enabled = !renameFolders;


        // can't use Copy To or Backup To while renaming folders

        itmOutputCopyTo.Enabled = itmOutputBackupTo.Enabled = !renameFolders;

        if( renameFolders && ( itmOutputCopyTo.Checked || itmOutputBackupTo.Checked ) )
          itmOutputRenameInPlace.PerformClick();


        // change text "file" <=> "folder"

        string oldFile     = renameFolders ? "file"     : "folder";
        string oldFilename = renameFolders ? "filename" : "folder name";
        string oldCapFile  = renameFolders ? "File"     : "Folder";

        mnuChangeCase.ToolTipText     = mnuChangeCase.ToolTipText.Replace( oldFilename, strFilename );
        txtNumberingInc.ToolTipText   = txtNumberingInc.ToolTipText.Replace( oldFile, strFile );
        txtNumberingReset.ToolTipText = txtNumberingReset.ToolTipText.Replace( oldFile, strFile );
        itmOutputMoveTo.ToolTipText   = itmOutputMoveTo.ToolTipText.Replace( oldCapFile, strCapFile );
        itmOutputCopyTo.ToolTipText   = renameFolders ? "Unavailable during folder rename" : "Files that match are copied and the copies are renamed";
        itmOutputBackupTo.ToolTipText = renameFolders ? "Unavailable during folder rename" : "Files that match are copied and the originals are renamed";
        miRegexReplaceOrigAll.Text    = miRegexReplaceOrigAll.Text.Replace( oldFilename, strFilename );
        itmOptionsShowHidden.Text     = itmOptionsShowHidden.Text.Replace( oldFile, strFile );
        colFilename.HeaderText        = strCapFilename;
      }
    }


    // "file"/"folder" strings used throughout the program

    private string strFile
    {
      get
      {
        return RenameFolders ? "folder" : "file";
      }
    }
    private string strFilename
    {
      get
      {
        return RenameFolders ? "folder name" : "filename";
      }
    }
    private string strCapFile
    {
      get
      {
        return RenameFolders ? "Folder" : "File";
      }
    }
    private string strCapFilename
    {
      get
      {
        return RenameFolders ? "Folder name" : "Filename";
      }
    }


    // used when realtime update is disabled

    private string prevMatch;
    private string prevReplace;
    private bool PreviewNeedsUpdate
    {
      set
      {
        if( !value )  // reset
        {
          prevMatch = cmbMatch.Text;
          prevReplace = txtReplace.Text;
        }
      }
      get
      {
        return cmbMatch.Text != prevMatch || txtReplace.Text != prevReplace;
      }
    }

    #endregion

    #region Constructor

    public MainForm( string initPath )
    {
      this.activePath = initPath;


      // draw form

      InitializeComponent();
      scMain.Panel2MinSize = 301;  // workaround for bug in VS designer
      tsMenu.Renderer    = new MyToolStripSystemRenderer();
      tsOptions.Renderer = new MyToolStripSystemRenderer();

      
      // manually set network folder browser root to My Network Places

      FieldInfo fieldInfo = fbdNetwork.GetType().GetField( "rootFolder", BindingFlags.NonPublic | BindingFlags.Instance );
      fieldInfo.SetValue( fbdNetwork, (Environment.SpecialFolder)0x0012 );  // My Network Places


      // disable help menuitems if files are missing

      if( !File.Exists( Path.Combine( Application.StartupPath, "RegexRenamer.chm" ) ) )
      {
        itmHelpContents.Enabled = false;
        itmHelpContents.ToolTipText = "Not installed";
      }
      if( !File.Exists( Path.Combine( Application.StartupPath, "Regex Quick Reference.chm" ) ) )
      {
        itmHelpRegexReference.Enabled = false;
        itmHelpRegexReference.ToolTipText = "Not installed";
      }

      
      // add insert args to regex context menu items

      miRegexMatchMatchSingleChar.Tag       = new InsertArgs( "." );
      miRegexMatchMatchDigit.Tag            = new InsertArgs( "\\d" );
      miRegexMatchMatchAlpha.Tag            = new InsertArgs( "\\w" );
      miRegexMatchMatchSpace.Tag            = new InsertArgs( "\\s" );
      miRegexMatchMatchMultiChar.BarBreak   = true;
      miRegexMatchMatchMultiChar.Tag        = new InsertArgs( ".*" );
      miRegexMatchMatchNonDigit.Tag         = new InsertArgs( "\\D" );
      miRegexMatchMatchNonAlpha.Tag         = new InsertArgs( "\\W" );
      miRegexMatchMatchNonSpace.Tag         = new InsertArgs( "\\S" );

      miRegexMatchAnchorStart.Tag           = new InsertArgs( "^", "", "group" );
      miRegexMatchAnchorEnd.Tag             = new InsertArgs( "", "$", "group" );
      miRegexMatchAnchorStartEnd.Tag        = new InsertArgs( "^", "$", "group" );
      miRegexMatchAnchorBound.Tag           = new InsertArgs( "\\b", "", "wrap" );
      miRegexMatchAnchorNonBound.Tag        = new InsertArgs( "\\B", "", "wrap" );

      miRegexMatchGroupCapt.Tag             = new InsertArgs( "(", ")" );
      miRegexMatchGroupNonCapt.Tag          = new InsertArgs( "(?:", ")" );
      miRegexMatchGroupAlt.Tag              = new InsertArgs( "(", "|)", -1, 0 );

      miRegexMatchQuantZeroOneG.Tag         = new InsertArgs( "", "?", "group" );
      miRegexMatchQuantOneMoreG.Tag         = new InsertArgs( "", "+", "group" );
      miRegexMatchQuantZeroMoreG.Tag        = new InsertArgs( "", "*", "group" );
      miRegexMatchQuantExactG.Tag           = new InsertArgs( "", "{n}", -2, 1, "group" );
      miRegexMatchQuantAtLeastG.Tag         = new InsertArgs( "", "{n,}", -3, 1, "group" );
      miRegexMatchQuantBetweenG.Tag         = new InsertArgs( "", "{n,m}", -4, 3, "group" );
      miRegexMatchQuantLazy.BarBreak        = true;
      miRegexMatchQuantZeroOneL.Tag         = new InsertArgs( "", "??", "group" );
      miRegexMatchQuantOneMoreL.Tag         = new InsertArgs( "", "+?", "group" );
      miRegexMatchQuantZeroMoreL.Tag        = new InsertArgs( "", "*?", "group" );
      miRegexMatchQuantExactL.Tag           = new InsertArgs( "", "{n}?", -3, 1, "group" );
      miRegexMatchQuantAtLeastL.Tag         = new InsertArgs( "", "{n,}?", -4, 1, "group" );
      miRegexMatchQuantBetweenL.Tag         = new InsertArgs( "", "{n,m}?", -5, 3, "group" );

      miRegexMatchClassPos.Tag              = new InsertArgs( "[", "]" );
      miRegexMatchClassNeg.Tag              = new InsertArgs( "[^", "]" );
      miRegexMatchClassLower.Tag            = new InsertArgs( "[a-z]" );
      miRegexMatchClassUpper.Tag            = new InsertArgs( "[A-Z]" );

      miRegexMatchCaptCreateUnnamed.Tag     = new InsertArgs( "(", ")" );
      miRegexMatchCaptMatchUnnamed.Tag      = new InsertArgs( "\\n", "", 1, 1 );
      miRegexMatchCaptCreateNamed.Tag       = new InsertArgs( "(?<name>", ")", 3, 4 );
      miRegexMatchCaptMatchNamed.Tag        = new InsertArgs( "\\<name>", "", 2, 4 );

      miRegexMatchLookPosAhead.Tag          = new InsertArgs( "(?=", ")" );
      miRegexMatchLookNegAhead.Tag          = new InsertArgs( "(?!", ")" );
      miRegexMatchLookPosBehind.Tag         = new InsertArgs( "(?<=", ")" );
      miRegexMatchLookNegBehind.Tag         = new InsertArgs( "(?<!", ")" );

      miRegexMatchLiteralDot.Tag            = new InsertArgs( "\\." );
      miRegexMatchLiteralQuestion.Tag       = new InsertArgs( "\\?" );
      miRegexMatchLiteralPlus.Tag           = new InsertArgs( "\\+" );
      miRegexMatchLiteralStar.Tag           = new InsertArgs( "\\*" );
      miRegexMatchLiteralCaret.Tag          = new InsertArgs( "\\^" );
      miRegexMatchLiteralDollar.Tag         = new InsertArgs( "\\$" );
      miRegexMatchLiteralBackslash.Tag      = new InsertArgs( "\\\\" );
      miRegexMatchLiteralOpenRound.BarBreak = true;
      miRegexMatchLiteralOpenRound.Tag      = new InsertArgs( "\\(" );
      miRegexMatchLiteralCloseRound.Tag     = new InsertArgs( "\\)" );
      miRegexMatchLiteralOpenSquare.Tag     = new InsertArgs( "\\[" );
      miRegexMatchLiteralCloseSquare.Tag    = new InsertArgs( "\\]" );
      miRegexMatchLiteralOpenCurly.Tag      = new InsertArgs( "\\{" );
      miRegexMatchLiteralCloseCurly.Tag     = new InsertArgs( "\\}" );
      miRegexMatchLiteralPipe.Tag           = new InsertArgs( "\\|" );

      miRegexReplaceCaptureUnnamed.Tag      = new InsertArgs( "$n", "", 1, 1 );
      miRegexReplaceCaptureNamed.Tag        = new InsertArgs( "${name}", "", 2, 4 );

      miRegexReplaceOrigMatched.Tag         = new InsertArgs( "$0" );
      miRegexReplaceOrigBefore.Tag          = new InsertArgs( "$`" );
      miRegexReplaceOrigAfter.Tag           = new InsertArgs( "$'" );
      miRegexReplaceOrigAll.Tag             = new InsertArgs( "$_" );

      miRegexReplaceSpecialNumSeq.Tag       = new InsertArgs( "$#" );
      miRegexReplaceLiteralDollar.Tag       = new InsertArgs( "$$" );

      miGlobMatchSingle.Tag                 = new InsertArgs( "?" );
      miGlobMatchMultiple.Tag               = new InsertArgs( "*" );
    }

    #endregion

    #region Methods

    // load/save settings & regex history

    private void LoadSettings()
    {
#if !DEBUG
      try
      {
#endif

        // general

        using( RegistryKey key = Registry.CurrentUser.OpenSubKey( "Software\\RegexRenamer" ) )
        {
          if( key != null )
          {
            if( activePath == null )
              activePath = (string)key.GetValue( "LastPath", "" );
            fbdMoveCopy.SelectedPath = (string)key.GetValue( "MoveCopyPath", "" );
            RenameFolders = (string)key.GetValue( "RenameFolders" ) == "True";

            try
            {
              int maxFilesOverride = (int)key.GetValue( "MaxFileLimit", 1000 );
              if( maxFilesOverride > MAX_FILES )
                MAX_FILES = maxFilesOverride;
            }
            catch {} // ignore if wrong reg key type

            key.Close();
          }
        }


        // options

        using( RegistryKey key = Registry.CurrentUser.OpenSubKey( "Software\\RegexRenamer\\Options" ) )
        {
          if( key != null )
          {
            EnableUpdates = false;

            itmOptionsShowHidden.Checked = (string)key.GetValue( "ShowHiddenFiles" ) == "True";
            itmOptionsPreserveExt.Checked = (string)key.GetValue( "PreserveExtension" ) == "True";
            itmOptionsRealtimePreview.Checked = (string)key.GetValue( "RealtimePreview", "True" ) == "True";
            itmOptionsAllowRenSub.Checked = (string)key.GetValue( "AllowRenameIntoSubfolders" ) == "True";
            itmOptionsRememberWinPos.Checked = (string)key.GetValue( "RememberWindowPosition", "True" ) == "True";
            itmOptionsRenameSelectedRows.Checked = (string)key.GetValue( "OnlyRenameSelectedRows" ) == "True";

            EnableUpdates = true;

            key.Close();
          }
        }


        // explorer shell context menu

        using( RegistryKey key = Registry.ClassesRoot.OpenSubKey( "Folder\\shell\\RegexRenamer\\command" ) )
        {
          if( key != null )
          {
            if( ((string)key.GetValue( "", "" )).StartsWith( Application.ExecutablePath ) )
              itmOptionsAddContextMenu.Checked = true;

            key.Close();
          }
        }


        // stats

        using( RegistryKey key = Registry.CurrentUser.OpenSubKey( "Software\\RegexRenamer\\Stats" ) )
        {
          if( key != null )
          {
            countProgLaunches = (int)key.GetValue( "ProgramLaunches", 0 ) + 1;
            countFilesRenamed = (int)key.GetValue( "FilesRenamed", 0 );

            key.Close();
          }
        }


        // check for old reg keys (maintain backwards compatibility)

        using( RegistryKey key = Registry.CurrentUser.OpenSubKey( "Software\\RegexRenamer", true ) )
        {
          if( key != null )
          {
            int cpl = (int)key.GetValue( "CountProgLaunches", 0 );
            int cfr = (int)key.GetValue( "CountFilesRenamed", 0 );
            if( cpl > 0 ) countProgLaunches = cpl;
            if( cfr > 0 ) countFilesRenamed = cfr;

            key.DeleteValue( "CountProgLaunches", false );
            key.DeleteValue( "CountFilesRenamed", false );
            key.DeleteValue( "ShowHiddenFiles", false );
            key.DeleteValue( "PreserveExtension", false );
          }
        }


        // window position

        if( !itmOptionsRememberWinPos.Checked ) return;  // skip

        using( RegistryKey key = Registry.CurrentUser.OpenSubKey( "Software\\RegexRenamer\\WindowPosition" ) )
        {
          if( key != null )
          {
            if( (string)key.GetValue( "WindowState" ) == "Maximized" )
            {
              this.WindowState = FormWindowState.Maximized;
            }
            else // not maximized
            {
              // get size and offset from registry

              object oWinX = key.GetValue( "WindowX", "0" ); // used to be a dword, now a string
              object oWinY = key.GetValue( "WindowY", "0" );
              int winx = oWinX is int ? (int)oWinX : int.Parse( (string)oWinX );
              int winy = oWinY is int ? (int)oWinY : int.Parse( (string)oWinY );

              int height = (int)key.GetValue( "WindowHeight", -1 );
              int width  = (int)key.GetValue( "WindowWidth", -1 );


              // validate (to prevent drawing window off-screen)

              bool valid = true;

              Screen screen = null;
              foreach( Screen s in Screen.AllScreens )
                if( s.WorkingArea.Contains( winx, winy ) )
                  screen = s;
              if( screen == null || height > screen.WorkingArea.Height || width > screen.WorkingArea.Width )
                valid = false;
              if( height < this.MinimumSize.Height || width < this.MinimumSize.Width )
                valid = false;

              if( valid )
              {
                this.Location = new Point( winx, winy );
                this.Size = new Size( width, height );
              }
              else // failed validation
              {
                itmOptionsRememberWinPos.Checked = false;
              }
            }


            // set splitter positions

            int splitMain = (int)key.GetValue( "SplitMain", -1 );
            int splitRegex = (int)key.GetValue( "SplitRegex", -1 );

            if( splitMain > 0 && splitRegex > 0 )
            {
              scMain.SplitterDistance  = splitMain;  // will be bounded
              scRegex.SplitterDistance = splitRegex; // automatically
            }

            key.Close();
          }
        }

#if !DEBUG
      }
      catch {}
#endif

      EnableUpdates = true;
    }
    private void SaveSettings()
    {
#if !DEBUG
      try
      {
#endif
        // general

        using( RegistryKey key = Registry.CurrentUser.CreateSubKey( "Software\\RegexRenamer" ) )
        {
          if( key != null )
          {
            key.SetValue( "LastPath", activePath );
            key.SetValue( "MoveCopyPath", fbdMoveCopy.SelectedPath );
            key.SetValue( "RenameFolders", RenameFolders );
            key.Close();
          }
        }


        // options

        using( RegistryKey key = Registry.CurrentUser.CreateSubKey( "Software\\RegexRenamer\\Options" ) )
        {
          if( key != null )
          {
            key.SetValue( "ShowHiddenFiles", itmOptionsShowHidden.Checked );
            key.SetValue( "PreserveExtension", itmOptionsPreserveExt.Checked );
            key.SetValue( "RealtimePreview", itmOptionsRealtimePreview.Checked );
            key.SetValue( "AllowRenameIntoSubfolders", itmOptionsAllowRenSub.Checked );
            key.SetValue( "RememberWindowPosition", itmOptionsRememberWinPos.Checked );
            key.SetValue( "OnlyRenameSelectedRows", itmOptionsRenameSelectedRows.Checked );
            key.Close();
          }
        }


        // stats

        using( RegistryKey key = Registry.CurrentUser.CreateSubKey( "Software\\RegexRenamer\\Stats" ) )
        {
          if( key != null )
          {
            key.SetValue( "ProgramLaunches", countProgLaunches );
            key.SetValue( "FilesRenamed", countFilesRenamed );
            key.Close();
          }
        }


        // window position

        using( RegistryKey key = Registry.CurrentUser.CreateSubKey( "Software\\RegexRenamer\\WindowPosition" ) )
        {
          if( key != null )
          {
            key.SetValue( "WindowX", this.Location.X.ToString() ); // store as string to preserve negative values
            key.SetValue( "WindowY", this.Location.Y.ToString() );
            key.SetValue( "WindowHeight", this.Height );
            key.SetValue( "WindowWidth", this.Width );
            key.SetValue( "SplitMain", scMain.SplitterDistance );
            key.SetValue( "SplitRegex", scRegex.SplitterDistance );
            key.SetValue( "WindowState", this.WindowState );
            key.Close();
          }
        }

#if !DEBUG
      }
      catch {}
#endif
    }

    private void LoadRegexHistory()
    {
#if !DEBUG
      try
      {
#endif
        using( RegistryKey key = Registry.CurrentUser.OpenSubKey( "Software\\RegexRenamer\\History" ) )
        {
          if( key == null ) return;

          this.cmbMatch.Items.Clear();

          foreach( string name in key.GetValueNames() )
            this.cmbMatch.Items.Add( key.GetValue( name ) );

          key.Close();
        }
#if !DEBUG
      }
      catch {}
#endif
    }
    private void SaveRegexHistory()
    {
#if !DEBUG
      try
      {
#endif
        using( RegistryKey key = Registry.CurrentUser.CreateSubKey( "Software\\RegexRenamer\\History" ) )
        {
          if( key == null ) return;

          foreach( string name in key.GetValueNames() )
            key.DeleteValue( name );

          for( int i = 0; i < this.cmbMatch.Items.Count; i++ )
            key.SetValue( i.ToString( "00" ), this.cmbMatch.Items[i] );  // update padding if changing MAX_HISTORY

          key.Close();
        }
#if !DEBUG
      }
      catch {}
#endif
    }


    // update directory tree/filenames/previews/validation (each cascades into the one below)

    private void UpdateFolderTree()
    {
      // get selected path (regardless whether it exists)

      if( tvwFolders.SelectedNode != null )
        activePath = tvwFolders.ForceGetSelectedNodePath();


      // save prev path to preserve node expansion

      string prevPathExpand = null;
      if( tvwFolders.SelectedNode != null && tvwFolders.SelectedNode.IsExpanded )
        prevPathExpand = activePath.ToLower();


      // init tvwFolders with directory tree

      tvwFolders.InitFolderTreeView();


      // get active path

      while( !Directory.Exists( activePath ) )  // if doesn't exist, walk tree backwards
      {
        DirectoryInfo di = null;
        try { di = Directory.GetParent( activePath ); } catch {}
        if( di == null ) break;

        activePath = di.FullName;
      }

      if( !Directory.Exists( activePath ) )  // still not found, default to system drive
        activePath = Environment.GetEnvironmentVariable( "SystemDrive" ) + "\\";


      // drill to folder and expand

      EnableUpdates = false;
      if( activePath.StartsWith( "\\\\" ) )
      {
        // select My Network Places
        tvwFolders.SelectedNode = (TreeNode)tvwFolders.Tag;
      }
      else if( Environment.GetFolderPath( Environment.SpecialFolder.DesktopDirectory ).Equals( activePath, StringComparison.CurrentCultureIgnoreCase ) )
      {
        // select root node
        tvwFolders.SelectedNode = tvwFolders.Nodes[0];
      }
      else
      {
        // find folder in tree
        if( !tvwFolders.DrillToFolder( activePath ) )
          activePath = tvwFolders.GetSelectedNodePath();
      }
      EnableUpdates = true;


      // re-expand

      if( tvwFolders.SelectedNode != null && prevPathExpand == activePath.ToLower() )
        tvwFolders.SelectedNode.Expand();


      UpdateFileList();
    }
    private void UpdateFileList()
    {
      if( !EnableUpdates ) return;
      dgvFiles.Tag = 0;  // reset files ignored
      fileCount.Reset();


      // update txtPath

      txtPath.BackColor = SystemColors.Window;
      txtPath.Text = activePath;
      txtPath.Update();


      // if invalid selection, clear all

      if( activePath == "" )
      {
        activeFiles.Clear();
        inactiveFiles.Clear();
        icons.Clear();
        dgvFiles.Rows.Clear();
        lblNumMatched.Text = "0";
        lblNumConflict.Text = "0";
        UpdateFileStats();

        return;
      }
      
      this.Cursor = Cursors.AppStarting;


      // create filter regex, if necessary
      
      Regex filter = null;
      string filterText = activeFilter;

      if( filterText != "" )
      {
        if( rbFilterGlob.Checked && filterText == "*.*" )  // convert to "*" (include files with no extension)
          filterText = "*";

        if( rbFilterGlob.Checked )  // convert glob to regex
          filterText = "^" + Regex.Escape( filterText ).Replace( "\\*", ".*" ).Replace( "\\?", "." ) + "$";

        filter = new Regex( filterText, RegexOptions.IgnoreCase );
      }


      // loop through file list, build RRItem array

      activeFiles.Clear();
      inactiveFiles.Clear();
      icons.Clear();

      DirectoryInfo activeDir = new DirectoryInfo( activePath );

      if( RenameFolders )  // folders
      {
        DirectoryInfo[] dirs = new DirectoryInfo[0];
        try
        {
          dirs = activeDir.GetDirectories();
        }
        catch( Exception ex )
        {
          MessageBox.Show( ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error );
        }

        foreach( DirectoryInfo dir in dirs )
        {
          fileCount.total++;

          // ignore if filtered out

          if( filter != null && filter.IsMatch( dir.Name ) == cbFilterExclude.Checked )
          {
            if( !inactiveFiles.ContainsKey( dir.Name.ToLower() ) )
              inactiveFiles.Add( dir.Name.ToLower(), InactiveReason.Filtered );
            fileCount.filtered++;
            continue;
          }

          // ignore if hidden and not showing hidden files

          bool hidden = false;
          try
          {
            hidden = ( dir.Attributes & FileAttributes.Hidden ) != 0;
          }
          catch { }  // reported System.UnauthorizedAccessException here under some versions of Samba when item is a link to /dev/null

          if( hidden ) fileCount.hidden++;
          if( !itmOptionsShowHidden.Checked && hidden )
          {
            if( !inactiveFiles.ContainsKey( dir.Name.ToLower() ) )
              inactiveFiles.Add( dir.Name.ToLower(), InactiveReason.Hidden );
            continue;
          }

          activeFiles.Add( new RRItem( dir, hidden, itmOptionsPreserveExt.Checked ) );
        }
      }
      else // files
      {
        FileInfo[] files = new FileInfo[0];
        try
        {
          files = activeDir.GetFiles();
        }
        catch( Exception ex )
        {
          MessageBox.Show( ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error );
        }

        foreach( FileInfo file in files )
        {
          fileCount.total++;


          // ignore if filtered out

          if( filter != null && filter.IsMatch( file.Name ) == cbFilterExclude.Checked )
          {
            if( !inactiveFiles.ContainsKey( file.Name.ToLower() ) )
              inactiveFiles.Add( file.Name.ToLower(), InactiveReason.Filtered );
            fileCount.filtered++;
            continue;
          }


          // ignore if hidden and not showing hidden files

          bool hidden = false;
          try
          {
            hidden = ( file.Attributes & FileAttributes.Hidden ) != 0;
          }
          catch { }  // reported System.UnauthorizedAccessException here under some versions of Samba when item is a link to /dev/null

          if( hidden ) fileCount.hidden++;
          if( !itmOptionsShowHidden.Checked && hidden )
          {
            if( !inactiveFiles.ContainsKey( file.Name.ToLower() ) )
              inactiveFiles.Add( file.Name.ToLower(), InactiveReason.Hidden );
            continue;
          }

          activeFiles.Add( new RRItem( file, hidden, itmOptionsPreserveExt.Checked ) );
        }
      }


      // create datagridview items w/ filename

      dgvFiles.Rows.Clear();

      for( int i = 0; i < activeFiles.Count; i++ )
      {
        if( i >= MAX_FILES )  // reached limit
        {
          dgvFiles.Tag = activeFiles.Count - i;  // num files ignored, will display warning dialog after preview is updated
          activeFiles.RemoveRange( i, activeFiles.Count - MAX_FILES );
          break;
        }


        // add new item

        dgvFiles.Rows.Add( null, activeFiles[i].Name, null );
        dgvFiles.Rows[i].Tag = i;  // store activeFiles index so we can refer back when under different sorting


        // add image (keyed by extension)

#if !DEBUG
        try  
        {
#endif
          if( RenameFolders )
          {
            if( icons.Count == 0 )
              icons.Add( "folder", ExtractIcons.GetFolderIcon() );
            dgvFiles.Rows[i].Cells[0].Value = icons["folder"];
          }
          else
          {
            string ext = activeFiles[i].Extension.ToLower();
            if( ext == ".lnk" )  // shortcut, don't key by extension as each may have different icon
            {
              ext = ".lnk." + icons.Count;
              icons.Add( ext, ExtractIcons.GetIcon( activeFiles[i].Fullpath, false ) );
              dgvFiles.Rows[i].Cells[0].Value = icons[ext];
            }
            else  // non-shortcut
            {
              if( !icons.ContainsKey( ext ) )
                icons.Add( ext, ExtractIcons.GetIcon( activeFiles[i].Fullpath, false ) );

              dgvFiles.Rows[i].Cells[0].Value = icons[ext];
            }
          }
#if !DEBUG
        }
        catch  // default: no image
        {
          dgvFiles.Rows[i].Cells[0].Value = new Bitmap( 1, 1 );
        }
#endif
      }


      fileCount.shown = dgvFiles.Rows.Count;
      UpdateFileStats();
      UpdateSelection();
      UpdatePreview();
    }
    private void UpdatePreview()
    {
      if( !EnableUpdates || !validMatch ) return;

      this.Cursor = Cursors.AppStarting;

      const string rxDoller = @"(?<=(?:^|[^$])(?:\$\$)*)\$";  // regex for an actual (non-escaped) doller sign


      // generate preview

      if( cmbMatch.Text != "" )
      {
        // compile regex

        RegexOptions options = RegexOptions.None;
        int count = cbModifierG.Checked ? -1 : 1;
        if( cbModifierI.Checked ) { options |= RegexOptions.IgnoreCase;              }
        if( cbModifierX.Checked ) { options |= RegexOptions.IgnorePatternWhitespace; }

        Regex regex = new Regex( cmbMatch.Text, options );


        // auto numbering

        int numCurrent = 0, numInc = 0, numStart = 0, numReset = 0;
        string numFormatted = "";
        bool doingAutoNum = false;
        bool doingAutoNumLetter = false;  // number sequence is actually a-z letter sequence
        bool doingAutoNumLetterUpper = false;  // letter sequence is uppercase

        if( this.validNumber && Regex.IsMatch( this.txtReplace.Text, rxDoller + "#" ) )
          doingAutoNum = true;

        Match match = Regex.Match( this.txtNumberingStart.Text, @"^(([a-z]+)|([A-Z]+))$" );
        doingAutoNumLetter = match.Success;
        doingAutoNumLetterUpper = match.Success && match.Groups[3].Length > 0;

        if( doingAutoNum )
        {
          if( doingAutoNumLetter )
            numStart = SequenceLetterToNumber( txtNumberingStart.Text.ToLower() );
          else
            numStart = Int32.Parse( txtNumberingStart.Text );

          numInc   = Int32.Parse( txtNumberingInc.Text   );
          numReset = Int32.Parse( txtNumberingReset.Text );
        }
        numCurrent = numStart - numInc;  // back up one


        // regex each filename

        string userReplacePattern = txtReplace.Text;
        if( doingAutoNum )
          userReplacePattern = Regex.Replace( userReplacePattern, rxDoller + @"(\d+)" + rxDoller + "#", "$${$1}$$#" );

        for( int afi = 0; afi < activeFiles.Count; afi++ )
        {
          // check if matches

          activeFiles[afi].Matched = regex.IsMatch( activeFiles[afi].Name );


          // if not, bail early, don't incrememnt autonum

          if( !activeFiles[afi].Matched )
          {
            activeFiles[afi].Preview = activeFiles[afi].Name;
            continue;
          }


          // else, matched

          string replacePattern;

          if( doingAutoNum )
          {
            numCurrent += numInc;

            if( numReset != 0 && ( numCurrent - numStart ) % numReset == 0 )
              numCurrent = numStart;

            if( numFormatted != "$#" )  // basic int overflow & negative number detection
            {
              if( !doingAutoNumLetter )  // number sequence
              {
                if( numCurrent < 0 )
                  numFormatted = "$#";
                else
                  numFormatted = numCurrent.ToString( txtNumberingPad.Text );
              }
              else  // letter sequence
              {
                if( numCurrent < 1 )
                  numFormatted = "$#";
                else if( doingAutoNumLetterUpper )
                  numFormatted = SequenceNumberToLetter( numCurrent ).ToUpper();
                else
                  numFormatted = SequenceNumberToLetter( numCurrent );
              }
            }

            replacePattern = Regex.Replace( userReplacePattern, rxDoller + "#", numFormatted );
          }
          else
          {
            replacePattern = userReplacePattern;
          }

          if( !itmChangeCaseNoChange.Checked )
            replacePattern = "\n" + replacePattern + "\n";  // delimit change-case boundaries
          
          activeFiles[afi].Preview = regex.Replace( activeFiles[afi].Name, replacePattern, count );

          if( !itmChangeCaseNoChange.Checked )
            activeFiles[afi].Preview = Regex.Replace( activeFiles[afi].Preview, @"\n([^\n]*)\n", new MatchEvaluator( MatchEvalChangeCase ) );

          if( activeFiles[afi].Preview.Length == 0 )
            activeFiles[afi].Preview = activeFiles[afi].Name;
        }

      }
      else  // cmbMatch.Text == ""
      {
        foreach( RRItem file in activeFiles )
        {
          file.Preview = file.Name;
          file.Matched = false;
        }
      }
      

      // update file list

      for( int dfi = 0; dfi < dgvFiles.Rows.Count; dfi++ )
      {
        int afi = (int)dgvFiles.Rows[dfi].Tag;
        dgvFiles.Rows[dfi].Cells[2].Value = activeFiles[afi].Preview;
      }


      // do preview filename validation

      UpdateValidation();


      // redraw

      dgvFiles.Sort( this.dgvFiles.SortedColumn ?? this.colFilename,
                     dgvFiles.SortOrder == SortOrder.Descending ? ListSortDirection.Descending : ListSortDirection.Ascending );  // resort
      this.Cursor = Cursors.Default;

      
      // show warning if any ignored files

      if( (int)dgvFiles.Tag > 0 )
      {
        MessageBox.Show( "For performance reasons, RegexRenamer will only display " + MAX_FILES
                       + " " + strFile + "s at once (" + (int)dgvFiles.Tag + " " + strFile + "s ignored).\r\n"
                       + "Use a filter to display only the " + strFile + "s you need to rename.",
                         "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning );
        dgvFiles.Tag = 0;  // prevent re-display
      }


      // keep selection cleared

      if( !itmOptionsRenameSelectedRows.Checked )
        dgvFiles.ClearSelection();


      PreviewNeedsUpdate = false;
    }
    private void UpdateValidation()
    {
      bool[] hasError = new bool[dgvFiles.Rows.Count];
      Dictionary<string, List<int>> hashPreview = new Dictionary<string, List<int>>();


      // generate hash of preview filenames (value = list of dfi indexes)

      for( int dfi = 0; dfi < dgvFiles.Rows.Count; dfi++ )  // dfi = dgvFiles index
      {
        int afi = (int)dgvFiles.Rows[dfi].Tag;              // afi = activeFiles index

        string preview = activeFiles[afi].PreviewExt.ToLower();

        if( !hashPreview.ContainsKey( preview ) )
          hashPreview.Add( preview, new List<int>() );
        hashPreview[preview].Add( dfi );

        dgvFiles.Rows[dfi].Cells[2].Tag = null;  // clear all errors
      }


      // check for errors

      string outputPath = activePath;
      if( itmOutputMoveTo.Checked || itmOutputCopyTo.Checked )
        outputPath = fbdMoveCopy.SelectedPath;

      for( int dfi = 0; dfi < dgvFiles.Rows.Count; dfi++ )
      {
        int afi = (int)dgvFiles.Rows[dfi].Tag;
        string preview = activeFiles[afi].PreviewExt.ToLower();


        // skip if already has an error, or an ignored file

        if( hasError[dfi] ) continue;

        if( itmOutputRenameInPlace.Checked )
        {
          if( activeFiles[afi].Name == activeFiles[afi].Preview ) continue;
        }
        else
        {
          if( !activeFiles[afi].Matched ) continue;
        }


        // check for valid filename

        string validFilenameErrmsg = ValidateFilename( activeFiles[afi].PreviewExt, itmOptionsAllowRenSub.Checked );

        if( validFilenameErrmsg != null )
        {
          dgvFiles.Rows[dfi].Cells[2].Tag = validFilenameErrmsg;
          continue;
        }


        // check for dupe filename conflicts

        if( !activeFiles[afi].Preview.Contains( "\\" )
            && ( itmOutputRenameInPlace.Checked || itmOutputBackupTo.Checked ) )  // destination is same directory
        {
          // check against active files

          if( hashPreview[preview].Count > 1 )
          {
            foreach( int dfi2 in hashPreview[preview] )
            {
              if( hasError[dfi2] )
                continue;
              else
                hasError[dfi2] = true;

              int afi2 = (int)dgvFiles.Rows[dfi2].Tag;
              dgvFiles.Rows[dfi2].Cells[2].Tag = "The " + strFilename + " '" + activeFiles[afi2].PreviewExt
                                               + "' conflicts with another " + strFile + " in the preview column.";
            }
            continue;
          }
          

          // check against inactive files

          if( inactiveFiles.ContainsKey( preview ) )  
          {
            switch( inactiveFiles[preview] )
            {
              case InactiveReason.Filtered:
                dgvFiles.Rows[dfi].Cells[2].Tag = "The " + strFilename + " '" + activeFiles[afi].PreviewExt
                                                + "' already exists in this directory but is currently filtered out.";
                break;
              case InactiveReason.Hidden:
                dgvFiles.Rows[dfi].Cells[2].Tag = "The " + strFilename + " '" + activeFiles[afi].PreviewExt
                                                + "' already exists in this directory as a hidden " + strFile + ".";
                break;
            }
            continue;
          }


          // check for file-folder conflicts

          if( dgvFiles.Rows.Count < 2000 )  // this check is expensive, only run if < 2000 items
          {
            string previewFullpath = Path.Combine( outputPath, activeFiles[afi].PreviewExt );
            if( RenameFolders ? File.Exists( previewFullpath ) : Directory.Exists( previewFullpath ) )
            {
              dgvFiles.Rows[dfi].Cells[2].Tag = "The " + strFilename + " '" + activeFiles[afi].PreviewExt
                                              + "' conflicts with a " + ( RenameFolders ? "file" : "folder" )
                                              + " in the current path.";
              continue;
            }
          }
        }
        else  // destination is other directory, check against file system
        {
          string previewFullpath = Path.Combine( outputPath, activeFiles[afi].PreviewExt );

          if( RenameFolders ? Directory.Exists( previewFullpath ) : File.Exists( previewFullpath ) )
          {
            dgvFiles.Rows[dfi].Cells[2].Tag = "The " + strFilename + " '"
                                            + Path.GetFileName( activeFiles[afi].PreviewExt )
                                            + "' already exists in the destination folder.";
            continue;
          }

          if( RenameFolders ? File.Exists( previewFullpath ) : Directory.Exists( previewFullpath ) )
          {
            dgvFiles.Rows[dfi].Cells[2].Tag = "The " + strFilename + " '"
                                            + Path.GetFileName( activeFiles[afi].PreviewExt ) + "' conflicts with a "
                                            + ( RenameFolders ? "file" : "folder" ) + " in the destination path.";
            continue;
          }
        }


        // if doing 'Backup to' (files only), also check original names against backup path

        if( itmOutputBackupTo.Checked )
        {
          string previewFullpath = Path.Combine( fbdMoveCopy.SelectedPath, activeFiles[afi].Filename );

          if( File.Exists( previewFullpath ) )
          {
            dgvFiles.Rows[dfi].Cells[2].Tag = "The original filename '" + activeFiles[afi].Filename
                                            + "' already exists in the selected backup folder.";
            continue;
          }

          if( Directory.Exists( previewFullpath ) )
          {
            dgvFiles.Rows[dfi].Cells[2].Tag = "The original filename '" + activeFiles[afi].Filename
                                            + "' conflicts with a folder in the selected backup path.";
            continue;
          }
        }

      }  // end error loop


      // update filename/preview column colour

      for( int dfi = 0; dfi < dgvFiles.Rows.Count; dfi++ )
      {
        int afi = (int)dgvFiles.Rows[dfi].Tag;

        if( activeFiles[afi].Matched )
          dgvFiles.Rows[dfi].Cells[1].Style.ForeColor = Color.Blue;
        else if( activeFiles[afi].Hidden )
          dgvFiles.Rows[dfi].Cells[1].Style.ForeColor = SystemColors.GrayText;
        else
          dgvFiles.Rows[dfi].Cells[1].Style.ForeColor = SystemColors.WindowText;

        if( dgvFiles.Rows[dfi].Cells[2].Tag != null )
          dgvFiles.Rows[dfi].Cells[2].Style.ForeColor = Color.Red;
        else if( itmOutputRenameInPlace.Checked && activeFiles[afi].Name != activeFiles[afi].Preview )
          dgvFiles.Rows[dfi].Cells[2].Style.ForeColor = Color.Blue;
        else if( !itmOutputRenameInPlace.Checked && activeFiles[afi].Matched )
          dgvFiles.Rows[dfi].Cells[2].Style.ForeColor = Color.Blue;
        else if( activeFiles[afi].Hidden )
          dgvFiles.Rows[dfi].Cells[2].Style.ForeColor = SystemColors.GrayText;
        else
          dgvFiles.Rows[dfi].Cells[2].Style.ForeColor = SystemColors.WindowText;
      }


      // update matched/conflicts counters

      int matched = 0, conflict = 0;

      for( int i = 0; i < activeFiles.Count; i++ )
        if( activeFiles[i].Matched ) matched++;

      foreach( DataGridViewRow row in dgvFiles.Rows )
        if( row.Cells[2].Tag != null )
          conflict++;

      lblNumMatched.Text = matched.ToString();
      lblNumConflict.Text = conflict.ToString();
    }
    private void UpdateSelection()
    {
      if( dgvFiles.Rows.Count != this.activeFiles.Count )
        return;

      foreach( DataGridViewRow row in dgvFiles.Rows )
      {
        if( row.Tag == null ) continue;

        int afi = (int)row.Tag;
        this.activeFiles[afi].Selected = row.Selected;
      }
    }


    // letter sequences

    private static string SequenceNumberToLetter( int i )
    {
      int dividend = i;
      string columnName = String.Empty;

      while( dividend > 0 )
      {
        int modulo = ( dividend - 1 ) % 26;
        columnName = Convert.ToChar( 97 + modulo ) + columnName;  // note: A-Z = 65-90, a-z = 97-122
        dividend = ( dividend - modulo ) / 26;
      }

      return columnName;
    }
    private static int SequenceLetterToNumber( string letter )
    {
      int number = 0;
      int pow = 1;
      for( int i = letter.Length - 1; i >= 0; i-- )
      {
        number += ( letter[i] - 'a' + 1 ) * pow;
        pow *= 26;
      }

      return number;
    }


    // regex match evaluator for changing case

    private string MatchEvalChangeCase( Match match )
    {
      TextInfo ti = new CultureInfo( "en" ).TextInfo;

      if     ( itmChangeCaseUppercase.Checked ) return ti.ToUpper( match.Groups[1].Value );
      else if( itmChangeCaseLowercase.Checked ) return ti.ToLower( match.Groups[1].Value );
      else if( itmChangeCaseTitlecase.Checked ) return ti.ToTitleCase( match.Groups[1].Value.ToLower() );
      else                                      return match.Groups[1].Value;
    }


    // validate glob/regex/filename

    private string ValidateGlob( string testGlob )
    {
      Regex regex = new Regex( "([\\\\/:\"<>|])" );
      Match match = regex.Match( testGlob );
      if( match.Success )
        return "Invalid character: " + match.Groups[0].Value;
      else
        return null;
    }
    private string ValidateRegex( string testRegex )
    {
      try
      {
        Regex regex = new Regex( testRegex );
      }
      catch( Exception ex )
      {
        Regex regex = new Regex( "^parsing \".+\" - " );
        return regex.Replace( ex.Message, "" );
      }
      return null;
    }

    Regex regValidateInvalidChars          = new Regex( "([\\\\/:*?\"<>|])" );
    Regex regValidateInvalidCharsAllowPath = new Regex( "([/:*?\"<>|])"     );
    Regex regValidateOnlyDotSpace          = new Regex( "^[ .]+$"           );
    Regex regValidateEndsInDotSpace        = new Regex( "([ .]+)$"          );

    private string ValidateFilename( string testFilename, bool allowRenSub )
    {
      Match match;
      

      // invalid character

      string[] parts = allowRenSub ? testFilename.Split( '\\' ) : new string[] { testFilename };
      for( int i = 0; i < parts.Length; i++ )
      {
        if( allowRenSub )
          match = regValidateInvalidCharsAllowPath.Match( parts[i] );  // ([/:*?\"<>|])
        else
          match = regValidateInvalidChars.Match( parts[i] );           // ([\\\\/:*?\"<>|])

        if( match.Success )
          if( parts.Length > 1 && i != parts.Length - 1 )
            return "The subfolder '" + parts[i] + "' contains an invalid character: '" + match.Groups[0].Value + "'.";
          else
            return "The " + strFilename + " '" + parts[i] + "' contains an invalid character: '" + match.Groups[0].Value + "'.";
      }


      // starts with "\"

      if( testFilename.StartsWith( "\\" ) )
        if( parts.Length > 2 )
          return "The subfolder cannot begin with '\\'.";
        else
          return "The " + strFilename + " cannot begin with a backslash.";


      // element is empty

      for( int i = 0; i < parts.Length; i++ )
      {
        if( parts[i] == "" )
          if( i != parts.Length - 1 )
            return "Duplicate path seperator";
          else
            return "The " + strFilename + " cannot end with a backslash.";
      }


      // element is [ .]+ (eg, "/../", "/ /", ...)
      
      for( int i = 0; i < parts.Length; i++ )
      {
        match = regValidateOnlyDotSpace.Match( parts[i] );  // ^[ .]+$
        if( match.Success )
          if( i != parts.Length - 1 )
            return "Invalid subfolder: '" + parts[i] + "'.";
          else
            return "Invalid " + strFilename + ": '" + parts[i] + "'.";
      }


      // element ends with [ .]+
      
      for( int i = 0; i < parts.Length; i++ )
      {
        match = regValidateEndsInDotSpace.Match( parts[i] );  // ([ .]+)$
        if( match.Success )
          if( i != parts.Length - 1 )
            return "The subfolder '" + parts[i] + "' ends with invalid character(s): '" + match.Groups[0].Value + "'.";
          else
            return "The " + strFilename + " '" + parts[i] + "' ends with invalid character(s): '" + match.Groups[0].Value + "'.";
      }


      return null;
    }


    // misc helper methods

    private void ResetFields()
    {
      EnableUpdates       = false;
      cmbMatch.Text       = "";
      txtReplace.Text     = "";
      cbModifierI.Checked = false;
      cbModifierG.Checked = false;
      cbModifierX.Checked = false;
      EnableUpdates       = true;
    }
    private void UpdateFileStats()
    {
      lblStatsTotal.Text    = fileCount.total    + " total";
      lblStatsShown.Text    = fileCount.shown    + " shown";
      lblStatsFiltered.Text = fileCount.filtered + " filtered";
      lblStatsHidden.Text   = fileCount.hidden   + " hidden";
    }
    private void SetFormActive( bool active )
    {
      EnableUpdates = active;
      tvwFolders.Enabled = active;
    }
    private string WrapText( string input, int maxlen )
    {
      if( input.Length <= maxlen ) return input;

      int i = input.IndexOf( ' ', maxlen );
      while( i > 0 )
      {
        input = input.Insert( i + 1, "\n" );

        if( input.Length <= i + 2 + maxlen )
          i = -1;
        else
          i = input.IndexOf( ' ', i + 2 + maxlen );
      }

      return input;
    }
    private void UnFocusAll()
    {
      lblMatch.Focus();
    }

    #endregion

    #region Event Handlers

    // MAINFORM

    // load settings, update folder tree view
    private void MainForm_Load( object sender, EventArgs e )
    {
      // load settings & regex history from registry

      LoadSettings();
      LoadRegexHistory();

      
      // popluate folder tree and file list

      UpdateFolderTree();
      dgvFiles.ClearSelection();
      

      // focus folder list

      tvwFolders.Focus();
    }

    // save current settings
    private void MainForm_FormClosing( object sender, FormClosingEventArgs e )
    {
      SaveSettings();
    }

    // global shortcuts
    private void MainForm_KeyDown( object sender, KeyEventArgs e )
    {
      if( btnRename.Enabled && e.Control )
      {
        if( e.KeyCode == Keys.R )  // Ctrl+R = start rename
        {
          e.Handled = true;
          btnRename.PerformClick();
        }
        else if( e.KeyCode == Keys.M )  // Ctrl+M = focus match textbox
        {
          e.Handled = true;
          cmbMatch.Focus();
        }
      }
      else if( btnCancel.Enabled && e.KeyCode == Keys.Escape && e.Modifiers == Keys.None )  // Esc = cancel rename
      {
        e.Handled = true;
        btnCancel.PerformClick();
      }

      if( e.Handled )
        e.SuppressKeyPress = true;
    }


    // SPLIT CONTAINERS

    // workaround for SplitContainer bug
    private void scMain_Resize( object sender, EventArgs e )
    {
      if( scMain.Width == 0 ) return;  // if minimized

      // when scMain.FixedPanel = Panel1 and the parent resized, scMain.Panel2MinSize is ignored...
      if( scMain.Width < scMain.Panel1.Width + scMain.SplitterWidth + scMain.Panel2MinSize )
        scMain.SplitterDistance = scMain.Width - scMain.SplitterWidth - scMain.Panel2MinSize;
    }

    // draw thin 3d box around scRegex splitter
    private void scRegex_Paint( object sender, PaintEventArgs e )
    {
      e.Graphics.DrawLine( SystemPens.ControlLight, 0, 0, scRegex.Width, 0 );
      e.Graphics.DrawLine( SystemPens.ControlDark, 0, scRegex.Height - 1, scRegex.Width, scRegex.Height - 1 );
    }
    private void scRegex_Panel1_Paint( object sender, PaintEventArgs e )
    {
      e.Graphics.DrawLine( SystemPens.ControlLight, scRegex.Panel1.Width - 1, 0, scRegex.Panel1.Width - 1, scRegex.Panel1.Height );
    }
    private void scRegex_Panel2_Paint( object sender, PaintEventArgs e )
    {
      e.Graphics.DrawLine( SystemPens.ControlDark, 0, 0, 0, scRegex.Panel2.Height );
    }

    // prevent split containers obtaining focus
    private void scMain_MouseUp( object sender, MouseEventArgs e )
    {
      UnFocusAll();
    }
    private void scRegex_MouseUp( object sender, MouseEventArgs e )
    {
      UnFocusAll();
    }

    // restore default size on double-click
    private void scMain_DoubleClick( object sender, EventArgs e )
    {
      scMain.SplitterDistance = 300;
    }
    private void scRegex_DoubleClick( object sender, EventArgs e )
    {
      scRegex.SplitterDistance = 348;
    }


    // MATCH & REPLACE

    // handle match regex validation
    private void ValidateMatch()
    {
      string errorMessage = ValidateRegex( cmbMatch.Text );

      if( errorMessage == null )
      {
        cmbMatch.BackColor = SystemColors.Window;
        toolTip.Hide( cmbMatch );
        validMatch = true;
      }
      else
      {
        cmbMatch.BackColor = Color.MistyRose;
        toolTip.Show( errorMessage, cmbMatch, 0, cmbMatch.Height );
        validMatch = false;
      }
    }
    private void cmbMatch_TextChanged( object sender, EventArgs e )
    {
      if( !EnableUpdates || cmbMatch.Text.StartsWith( "/" ) ) return;  // skip when selection from combobox

      ValidateMatch();
      cmbMatch.Update();

      if( itmOptionsRealtimePreview.Checked )
        UpdatePreview();
    }
    private void cmbMatch_Enter( object sender, EventArgs e )
    {
      ValidateMatch();  // show tooltip if left during error
    }
    private void cmbMatch_Leave( object sender, EventArgs e )
    {
      toolTip.Hide( cmbMatch );

      if( txtReplace.Focused || cbModifierI.Focused || cbModifierG.Focused || cbModifierX.Focused ) return;

      if( PreviewNeedsUpdate )
        UpdatePreview();
    }
    private void txtReplace_TextChanged( object sender, EventArgs e )
    {
      if( !EnableUpdates || cmbMatch.Text.StartsWith( "/" ) ) return;  // skip when selection from combobox

      if( itmOptionsRealtimePreview.Checked )
        UpdatePreview();
    }
    private void txtReplace_Leave( object sender, EventArgs e )
    {
      if( cmbMatch.Focused || cbModifierI.Focused || cbModifierG.Focused || cbModifierX.Focused ) return;

      if( PreviewNeedsUpdate )
        UpdatePreview();
    }

    // parse combobox history string
    private void cmbMatch_SelectedIndexChanged( object sender, EventArgs e )
    {
      if( cmbMatch.SelectedIndex == -1 )
        return;  // can occur when alt-tabbing away and back while dropdown is open

      string regexString = (string)cmbMatch.Items[cmbMatch.SelectedIndex];

      Regex regex = new Regex( "^/(?<match>[^/]+)/(?<replace>[^/]*)/(?<modifiers>[igx]*)$" );
      Match match = regex.Match( regexString );

      if( match.Success )
      {
        EnableUpdates = false;

        cmbMatch.newText = match.Groups["match"].Value; cmbMatch.Invalidate();  // see MyComboBox
        txtReplace.Text = match.Groups["replace"].Value;
        cbModifierI.Checked = match.Groups["modifiers"].Value.Contains( "i" );
        cbModifierG.Checked = match.Groups["modifiers"].Value.Contains( "g" );
        cbModifierX.Checked = match.Groups["modifiers"].Value.Contains( "x" );

        EnableUpdates = true;
      }
      else  // invalid regexString, remove
      {
        cmbMatch.Items.RemoveAt( cmbMatch.SelectedIndex );
        SaveRegexHistory();
        ResetFields();
      }
    }

    // custom key behaviour
    private void cmbMatch_KeyDown( object sender, KeyEventArgs e )
    {
      if( cmbMatch.DroppedDown ) return;

      if( e.KeyCode == Keys.Enter && !itmOptionsRealtimePreview.Checked )  // enter (when no realtime preview) = gen preview
      {
        if( validMatch && PreviewNeedsUpdate )
        {
          UpdatePreview();
          e.SuppressKeyPress = true;
        }
        e.Handled = true;
      }
      else if( e.KeyCode == Keys.Delete && e.Control )  // ctrl+delete = delete history
      {
        ResetFields();
        cmbMatch.Items.Clear();
        SaveRegexHistory();
        UpdateFileList();
        e.Handled = true;
      }
      else if( e.KeyCode == Keys.Back && e.Control )  // ctrl+backspace = clear fields
      {
        ResetFields();
        UpdateFileList();
        e.Handled = true;
        e.SuppressKeyPress = true;
      }
      else if( e.KeyCode == Keys.Down && !e.Alt )  // down arrow = move to replace field (still allow alt+down to open menu)
      {
        txtReplace.Select( cmbMatch.SelectionStart, 0 );
        txtReplace.Focus();
        e.Handled = true;
      }
      else if( e.KeyCode == Keys.Up || e.KeyCode == Keys.PageUp || e.KeyCode == Keys.PageDown )  // ignore default behaviour
      {
        e.Handled = true;
      }
    }
    private void txtReplace_KeyDown( object sender, KeyEventArgs e )
    {
      if( e.KeyCode == Keys.Enter && !itmOptionsRealtimePreview.Checked )  // enter (when no realtime preview) = gen preview
      {
        if( validMatch && PreviewNeedsUpdate )
        {
          UpdatePreview();
          e.SuppressKeyPress = true;
        }
        e.Handled = true;
      }
      else if( e.KeyCode == Keys.Back && e.Control )  // ctrl+backspace = clear fields
      {
        ResetFields();
        UpdateFileList();
        e.Handled = true;
        e.SuppressKeyPress = true;
      }
      else if( e.KeyCode == Keys.Up )  // up arrow = move to match field
      {
        cmbMatch.Focus();
        cmbMatch.Select( txtReplace.SelectionStart, 0 );
      }
      else if( e.KeyCode == Keys.Down )  // ignore default behaviour
      {
        e.Handled = true;
      }
    }

    // context menus
    private Control lastControlRightClicked;
    private void InsertRegexFragment( object sender, EventArgs e )
    {
      InsertArgs ia = (InsertArgs)( (MenuItem)sender ).Tag;
      TextBox textBox = null;
      ComboBox comboBox = null;

      int selectionStart, selectionLength;
      string text;

      if( lastControlRightClicked.GetType().Name == "TextBox" )
      {
        textBox = (TextBox)lastControlRightClicked;
        selectionStart = textBox.SelectionStart;
        selectionLength = textBox.SelectionLength;
        text = textBox.Text;
      }
      else
      {
        comboBox = (ComboBox)lastControlRightClicked;
        selectionStart = comboBox.SelectionStart;
        selectionLength = comboBox.SelectionLength;
        text = comboBox.Text;
      }

      if( ia.InsertBefore == "" && selectionLength == 0 )
      {
        ia.InsertBefore = ia.InsertAfter;
        ia.InsertAfter = "";
      }

      if( ia.WrapIfSelection && selectionLength > 0 )
        if( ia.InsertAfter == "" )
          ia.InsertAfter = ia.InsertBefore;
        else
          ia.InsertBefore = ia.InsertAfter;

      int group = 0;
      if( ia.GroupSelection && selectionLength > 0 )
      {
        text = text.Insert( selectionStart, "(" );
        selectionStart += 1;
        text = text.Insert( selectionStart + selectionLength, ")" );
        group = 1;
      }

      if( selectionLength > 0 && ( ia.InsertBefore == "" || ia.InsertAfter == "" ) && !ia.GroupSelection )
      {
        text = text.Remove( selectionStart, selectionLength );
        selectionLength = 0;
      }

      if( ia.InsertBefore != "" )
      {
        text = text.Insert( selectionStart - group, ia.InsertBefore );
        selectionStart += ia.InsertBefore.Length;
      }
      if( ia.InsertAfter != "" )
      {
        text = text.Insert( selectionStart + selectionLength + group, ia.InsertAfter );
      }
      if( ia.SelectionStartOffset > 0 )
      {
        selectionStart = selectionStart - group - ia.InsertBefore.Length + ia.SelectionStartOffset;
      }
      if( ia.SelectionStartOffset < 0 )
      {
        selectionStart = selectionStart + selectionLength + group + ia.InsertAfter.Length + ia.SelectionStartOffset;
      }
      if( ia.SelectionLength != -1 )
        selectionLength = ia.SelectionLength;

      if( textBox != null )
      {
        textBox.SelectAll(); textBox.Paste( text );  // allow undo
        textBox.SelectionStart = selectionStart;
        textBox.SelectionLength = selectionLength;
      }
      else
      {
        comboBox.SelectAll(); comboBox.SelectedText = text;  // allow undo
        comboBox.SelectionStart = selectionStart;
        comboBox.SelectionLength = selectionLength;
      }
    }
    private void cmbMatch_MouseDown( object sender, MouseEventArgs e )
    {
      if( e.Button == MouseButtons.Right && ( Control.ModifierKeys & Keys.Shift ) == Keys.Shift )
      {
        lastControlRightClicked = cmbMatch;
        cmbMatch.ContextMenuStrip = cmsBlank;  // prevent default cms from being displayed
        if( !cmbMatch.Focused )  // prevent combobox from selecting all if already focused
          cmbMatch.Focus();
        cmRegexMatch.Show( cmbMatch, e.Location );
      }
    }
    private void cmbMatch_MouseUp( object sender, MouseEventArgs e )
    {
      if( cmbMatch.ContextMenuStrip != null )
        cmbMatch.ContextMenuStrip = null;  // restore default cms
    }
    private void txtReplace_MouseDown( object sender, MouseEventArgs e )
    {
      if( e.Button == MouseButtons.Right && ( Control.ModifierKeys & Keys.Shift ) == Keys.Shift )
      {
        lastControlRightClicked = txtReplace;
        txtReplace.ContextMenuStrip = cmsBlank;  // prevent default cms from being displayed
        txtReplace.Focus();
        cmRegexReplace.Show( txtReplace, e.Location );
      }
    }
    private void txtReplace_MouseUp( object sender, MouseEventArgs e )
    {
      if( txtReplace.ContextMenuStrip != null )
        txtReplace.ContextMenuStrip = null;  // restore default cms
    }


    // MODIFIERS

    // update preview on change
    private void cbModifierI_CheckedChanged( object sender, EventArgs e )
    {
      cbModifierI.Refresh();
      UpdatePreview();
    }
    private void cbModifierG_CheckedChanged( object sender, EventArgs e )
    {
      cbModifierG.Refresh();
      UpdatePreview();
    }
    private void cbModifierX_CheckedChanged( object sender, EventArgs e )
    {
      cbModifierX.Refresh();
      UpdatePreview();
    }


    // UPPER MENUS

    // change case
    private void ChangeCaseMenuItem( object sender )
    {
      if( !EnableUpdates ) return;

      ToolStripMenuItem checkedMenuItem = (ToolStripMenuItem)sender;
      if( checkedMenuItem.Checked ) return;  // already checked


      // update checked marks

      for( int i = 0; i < mnuChangeCase.DropDownItems.Count; i++ )
      {
        if( i == 1 ) continue;  // seperator

        if( mnuChangeCase.DropDownItems[i] == checkedMenuItem )
          ( (ToolStripMenuItem)mnuChangeCase.DropDownItems[i] ).Checked = true;
        else
          ( (ToolStripMenuItem)mnuChangeCase.DropDownItems[i] ).Checked = false;
      }


      // set default match/replace values (if empty)

      if( checkedMenuItem != itmChangeCaseNoChange )
      {
        if( cmbMatch.Text == "" )
        {
          EnableUpdates = false;
          cmbMatch.Text = ".*";
          EnableUpdates = true;
        }
        if( txtReplace.Text == "" )
        {
          EnableUpdates = false;
          txtReplace.Text = "$0";
          EnableUpdates = true;
        }
      }


      // set button text to bold if an option selected

      if( itmChangeCaseNoChange.Checked )
      {
        mnuChangeCase.Font = new Font( "Tahoma", 8.25F );
        mnuChangeCase.Padding = new Padding( 0, 0, 8, 0 );
      }
      else
      {
        mnuChangeCase.Font = new Font( "Tahoma", 8.25F, FontStyle.Bold );
        mnuChangeCase.Padding = new Padding( 0, 0, 0, 0 );
      }


      // update preview

      this.Update();
      UpdatePreview();
    }
    private void itmChangeCaseNoChange_Click( object sender, EventArgs e )
    {
      ChangeCaseMenuItem( sender );
    }
    private void itmChangeCaseUppercase_Click( object sender, EventArgs e )
    {
      ChangeCaseMenuItem( sender );
    }
    private void itmChangeCaseLowercase_Click( object sender, EventArgs e )
    {
      ChangeCaseMenuItem( sender );
    }
    private void itmChangeCaseTitlecase_Click( object sender, EventArgs e )
    {
      ChangeCaseMenuItem( sender );
    }
    private void mnuChangeCase_MouseDown( object sender, MouseEventArgs e )
    {
      if( e.Button == MouseButtons.Right && !itmChangeCaseNoChange.Checked )  // set default
        itmChangeCaseNoChange.PerformClick();
    }

    // numbering
    private void NumberingMenuItem( object sender )
    {
      ToolStripTextBox textBox = (ToolStripTextBox)sender;
      bool error = false;
      int num;

      if( textBox == txtNumberingPad && textBox.Text == "" )
      {
        EnableUpdates = false;
        textBox.Text = "0";  // default: no padding
        EnableUpdates = true;
      }


      // parse int, check valid range

      if( !Int32.TryParse( textBox.Text, out num ) )
        error = true;
      else if( textBox == txtNumberingStart && num < 0 )
        error = true;
      else if( textBox == txtNumberingPad && num != 0 )
        error = true;
      else if( textBox == txtNumberingInc && num == 0 )
        error = true;
      else if( textBox == txtNumberingReset && num < 0 )
        error = true;


      // or, check for letter(s)

      if( textBox == txtNumberingStart )
      {
        if( Regex.IsMatch( textBox.Text, @"^([a-z]+|[A-Z]+)$" ) )
        {
          error = false;
          this.txtNumberingPad.Enabled = false;
        }
        else if( !this.txtNumberingPad.Enabled )
        {
          this.txtNumberingPad.Enabled = true;
        }
      }


      // set bg colour

      if( error )
        textBox.BackColor = Color.MistyRose;
      else
        textBox.BackColor = SystemColors.Window;


      // if all valid, update preview

      textBox.Tag = !error;
      validNumber = (bool)mnuNumbering.DropDownItems[0].Tag
                  && (bool)mnuNumbering.DropDownItems[1].Tag
                  && (bool)mnuNumbering.DropDownItems[2].Tag
                  && (bool)mnuNumbering.DropDownItems[3].Tag;

      if( validNumber )
        UpdatePreview();
    }
    private void txtNumberingStart_TextChanged( object sender, EventArgs e )
    {
      NumberingMenuItem( sender );
    }
    private void txtNumberingPad_TextChanged( object sender, EventArgs e )
    {
      NumberingMenuItem( sender );
    }
    private void txtNumberingInc_TextChanged( object sender, EventArgs e )
    {
      NumberingMenuItem( sender );
    }
    private void txtNumberingReset_TextChanged( object sender, EventArgs e )
    {
      NumberingMenuItem( sender );
    }
    private void mnuNumbering_MouseDown( object sender, MouseEventArgs e )
    {
      if( e.Button == MouseButtons.Right )  // set defaults
      {
        EnableUpdates = false;
        if( txtNumberingStart.Text != "1"   ) txtNumberingStart.Text = "1";
        if( txtNumberingPad.Text   != "000" ) txtNumberingPad.Text   = "000";
        if( txtNumberingInc.Text   != "1"   ) txtNumberingInc.Text   = "1";
        if( txtNumberingReset.Text != "0"   ) txtNumberingReset.Text = "0";
        EnableUpdates = true;

        UpdatePreview();
      }
    }

    // move/copy
    private void OutputMenuItem( object sender )
    {
      if( !EnableUpdates ) return;
      ToolStripMenuItem clickedMenuItem = (ToolStripMenuItem)sender;


      if( clickedMenuItem != itmOutputRenameInPlace )
      {
        // update dialog text

        if( clickedMenuItem == itmOutputMoveTo )
          fbdMoveCopy.Description = "During the rename operation, " + strFile + "s that match the current regex will be "
                                  + "moved to the selected folder and renamed (if necessary).";
        else if( clickedMenuItem == itmOutputCopyTo )
          fbdMoveCopy.Description = "During the rename operation, files that match the current regex will be "
                                  + "copied to the selected folder and the copies renamed (if necessary).";
        else if( clickedMenuItem == itmOutputBackupTo )
          fbdMoveCopy.Description = "During the rename operation, files that match the current regex will be "
                                  + "copied to the selected folder and the originals renamed (if necessary).";


        // show dialog, ignore if cancelled

        if( fbdMoveCopy.ShowDialog() != DialogResult.OK ) return;


        // update folder tree in case user created new folder within fbdNetwork

        if( !activePath.StartsWith( fbdMoveCopy.SelectedPath ) )
        {
          DirectoryInfo parent = Directory.GetParent( fbdMoveCopy.SelectedPath );
          if( parent != null )
          {
            EnableUpdates = false;
            tvwFolders.RefreshNode( parent.FullName );  // may DrillToFolder()
            EnableUpdates = true;
          }
        }


        // show warning if same as activePath

        if( fbdMoveCopy.SelectedPath == activePath )
        {
          string errorMessage = "This '";
          if( clickedMenuItem == itmOutputMoveTo ) errorMessage += "Move to";
          else if( clickedMenuItem == itmOutputCopyTo ) errorMessage += "Copy to";
          else if( clickedMenuItem == itmOutputBackupTo ) errorMessage += "Backup to";
          errorMessage += "' folder is the same as the currently selected folder.\r\n";

          MessageBox.Show( errorMessage, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning );
        }
      }


      // update checked marks

      for( int i = 0; i < mnuMoveCopy.DropDownItems.Count; i++ )
      {
        if( i == 1 ) continue;  // seperator

        if( mnuMoveCopy.DropDownItems[i] == clickedMenuItem )
          ( (ToolStripMenuItem)mnuMoveCopy.DropDownItems[i] ).Checked = true;
        else
          ( (ToolStripMenuItem)mnuMoveCopy.DropDownItems[i] ).Checked = false;
      }


      // set button text to bold if an option selected

      if( itmOutputRenameInPlace.Checked )
      {
        mnuMoveCopy.Font = new Font( "Tahoma", 8.25F );
        mnuMoveCopy.Padding = new Padding( 0, 0, 17, 0 );
      }
      else
      {
        mnuMoveCopy.Font = new Font( "Tahoma", 8.25F, FontStyle.Bold );
        mnuMoveCopy.Padding = new Padding( 0, 0, 7, 0 );
      }


      // redo validation

      this.Update();
      UpdateValidation();
    }
    private void itmOutputRenameInPlace_Click( object sender, EventArgs e )
    {
      OutputMenuItem( sender );
    }
    private void itmOutputMoveTo_Click( object sender, EventArgs e )
    {
      OutputMenuItem( sender );
    }
    private void itmOutputCopyTo_Click( object sender, EventArgs e )
    {
      OutputMenuItem( sender );
    }
    private void itmOutputBackupTo_Click( object sender, EventArgs e )
    {
      OutputMenuItem( sender );
    }
    private void mnuMoveCopy_MouseDown( object sender, MouseEventArgs e )
    {
      if( e.Button == MouseButtons.Right && !itmOutputRenameInPlace.Checked )  // set default
        itmOutputRenameInPlace.PerformClick();
    }


    // FILTER

    // handle filter regex/glob validation
    private void ValidateFilter()
    {
      if( !EnableUpdates ) return;

      string errorMessage;

      if( rbFilterGlob.Checked )
        errorMessage = ValidateGlob( txtFilter.Text );
      else  // regex
        errorMessage = ValidateRegex( txtFilter.Text );

      if( errorMessage == null )
      {
        txtFilter.BackColor = SystemColors.Window;
        toolTip.Hide( txtFilter );
        validFilter = true;
      }
      else
      {
        txtFilter.BackColor = Color.MistyRose;
        toolTip.Show( errorMessage, txtFilter, 0, txtFilter.Height );
        validFilter = false;
      }
    }
    private void ApplyFilter()
    {
      if( !EnableUpdates || !validFilter ) return;

      activeFilter = txtFilter.Text;
      UpdateFileList();
    }
    private void rbFilterRegex_CheckedChanged( object sender, EventArgs e )
    {
      if( rbFilterGlob.Checked && ( txtFilter.Text == ".*" || txtFilter.Text == ".+" ) )
      {
        EnableUpdates = false;
        txtFilter.Text = "*.*";
        activeFilter = "*.*";
        EnableUpdates = true;
      }
      else if( rbFilterRegex.Checked && ( txtFilter.Text == "*.*" || txtFilter.Text == "*" ) )
      {
        EnableUpdates = false;
        txtFilter.Text = ".*";
        activeFilter = ".*";
        EnableUpdates = true;
      }
      else
      {
        activeFilter = "";
        ValidateFilter();
      }
    }
    private void rbFilterGlob_Click( object sender, EventArgs e )
    {
      txtFilter.Focus();
    }
    private void rbFilterRegex_Click( object sender, EventArgs e )
    {
      txtFilter.Focus();
    }
    private void txtFilter_TextChanged( object sender, EventArgs e )
    {
      ValidateFilter();
    }
    private void txtFilter_KeyDown( object sender, KeyEventArgs e )
    {
      if( e.KeyCode == Keys.Enter )  // apply filter
      {
        if( validFilter )
          e.SuppressKeyPress = true;  // prevent beep on enter if valid filter

        ApplyFilter();
      }
      else if( e.KeyCode == Keys.Escape )  // revert
      {
        EnableUpdates = false;
        txtFilter.Text = activeFilter;
        EnableUpdates = true;

        validFilter = true;
        txtFilter.BackColor = SystemColors.Window;
        toolTip.Hide( txtFilter );
        txtFilter.SelectionStart = activeFilter.Length;

        e.SuppressKeyPress = true;  // prevent beep
      }
    }
    private void txtFilter_Leave( object sender, EventArgs e )
    {
      if( cbFilterExclude.Focused || rbFilterGlob.Focused || rbFilterRegex.Focused ) return;

      EnableUpdates = false;
      txtFilter.Text = activeFilter;
      EnableUpdates = true;

      validFilter = true;
      txtFilter.BackColor = SystemColors.Window;
      toolTip.Hide( txtFilter );
    }
    private void cbFilterExclude_CheckedChanged( object sender, EventArgs e )
    {
      if( validFilter )
        ApplyFilter();
      else
        txtFilter.Focus();
    }
    
    // context menu
    private void txtFilter_MouseDown( object sender, MouseEventArgs e )
    {
      if( e.Button == MouseButtons.Right && ( Control.ModifierKeys & Keys.Shift ) == Keys.Shift )
      {
        lastControlRightClicked = txtFilter;
        txtFilter.ContextMenuStrip = cmsBlank;  // prevent default cms from being displayed
        txtFilter.Focus();
        if( rbFilterGlob.Checked )
          cmGlobMatch.Show( txtFilter, e.Location );
        else
          cmRegexMatch.Show( txtFilter, e.Location );
      }
    }
    private void txtFilter_MouseUp( object sender, MouseEventArgs e )
    {
      if( txtFilter.ContextMenuStrip != null )
        txtFilter.ContextMenuStrip = null;  // restore default cms
    }

    // stats mouseover
    private void lblStats_MouseEnter( object sender, EventArgs e )
    {
      if( this.ActiveControl == txtFilter || this.ActiveControl == cbFilterExclude )  // store state
      {
        txtFilter.Tag = new object[] { this.ActiveControl, txtFilter.Text, txtFilter.SelectionStart, txtFilter.SelectionLength };
        UnFocusAll();
      }
      else
        txtFilter.Tag = null;

      gbFilter.Enabled = false;
      lblStats.ForeColor = Color.FromArgb( 0, 70, 213 );  // default winxp groupbox header colour
      pnlStats.Visible = true;
    }
    private void lblStats_MouseLeave( object sender, EventArgs e )
    {
      gbFilter.Enabled = true;
      lblStats.ForeColor = SystemColors.ControlDark;
      pnlStats.Visible = false;

      if( txtFilter.Tag != null )  // restore state
      {
        object[] state = (object[])txtFilter.Tag;
        ( (Control)state[0] ).Focus();
        txtFilter.Text = (string)state[1];
        txtFilter.SelectionStart = (int)state[2];
        txtFilter.SelectionLength = (int)state[3];
      }
    }

    // FOLDER TREE

    // update file list on select different path
    private void tvwFolders_AfterSelect( object sender, TreeViewEventArgs e )
    {
      if( !EnableUpdates ) return;

      if( tvwFolders.Tag != null && tvwFolders.SelectedNode == (TreeNode)tvwFolders.Tag )  // My Network Places
        toolTip.Show( "Click to browse the network", btnNetwork, 0, btnNetwork.Height, 5000 );

      activePath = tvwFolders.GetSelectedNodePath();
      UpdateFileList();
    }

    // F5 = refresh
    private void tvwFolders_KeyUp( object sender, KeyEventArgs e )
    {
      if( e.KeyCode == Keys.F5 )  // refresh directory tree
      {
        UpdateFolderTree();
      }
    }

    // path field
    private string ValidatePath()
    {
      string errorMessage = null;
      this.Cursor = Cursors.AppStarting;
      try
      {
        string normPath = Path.GetFullPath( txtPath.Text );
        if( normPath.Length > 3 )
          normPath = normPath.TrimEnd( '\\' );

        if( !Directory.Exists( normPath ) )
        {
          errorMessage = "Path does not exist.";
        }
        else if( normPath != txtPath.Text )
        {
          int ss = txtPath.SelectionStart;
          txtPath.Text = normPath;
          txtPath.SelectionStart = ss;
        }
      }
      catch( Exception ex )
      {
        errorMessage = ex.Message;
      }
      this.Cursor = Cursors.Default;
      return errorMessage;
    }
    private void txtPath_KeyDown( object sender, KeyEventArgs e )
    {
      if( e.KeyCode == Keys.Enter )  // apply
      {
        if( !EnableUpdates ) return;
        string errorMessage = ValidatePath();

        if( errorMessage == null )
        {
          EnableUpdates = false;
          if( txtPath.Text.StartsWith( "\\\\" ) )
          {
            // select My Network Places
            tvwFolders.SelectedNode = (TreeNode)tvwFolders.Tag;
          }
          else if( Environment.GetFolderPath( Environment.SpecialFolder.DesktopDirectory ).Equals( txtPath.Text, StringComparison.CurrentCultureIgnoreCase ) )
          {
            // select root node
            tvwFolders.SelectedNode = tvwFolders.Nodes[0];
          }
          else
          {
            // find folder in tree
            if( !tvwFolders.DrillToFolder( txtPath.Text ) )
              tvwFolders.SelectedNode = null;
          }
          EnableUpdates = true;

          e.SuppressKeyPress = true;  // prevent beep

          activePath = txtPath.Text;
          toolTip.Hide( txtPath );
          this.Update();
          UpdateFileList();
        }
        else
        {
          tvwFolders.SelectedNode = null;
          txtPath.BackColor = Color.MistyRose;
          toolTip.Show( errorMessage, txtPath, 0, txtPath.Height );
        }
      }
      else if( e.KeyCode == Keys.Escape )  // revert
      {
        txtPath.BackColor = SystemColors.Window;
        txtPath.Text = activePath;
        txtPath.SelectionStart = activePath.Length;
        toolTip.Hide( txtPath );

        if( tvwFolders.SelectedNode == null )
        {
          if( txtPath.Text.StartsWith( "\\\\" ) )  // select My Network Places
          {
            EnableUpdates = false;
            tvwFolders.SelectedNode = (TreeNode)tvwFolders.Tag;
            EnableUpdates = true;
          }
          else  // find folder in tree
          {
            EnableUpdates = false;
            if( !tvwFolders.DrillToFolder( txtPath.Text ) )
              tvwFolders.SelectedNode = null;
            EnableUpdates = true;
          }
        }

        e.SuppressKeyPress = true;  // prevent beep
      }
      else  // other keypress
      {
        toolTip.Hide( txtPath );
      }
    }
    private void txtPath_Enter( object sender, EventArgs e )
    {
      ValidatePath();  // show tooltip if left during error
    }
    private void txtPath_Leave( object sender, EventArgs e )
    {
      toolTip.Hide( txtPath );
    }

    // browse network button
    private void btnNetwork_Click( object sender, EventArgs e )
    {
      if( !EnableUpdates ) return;


      // show dialog

      if( activePath.StartsWith( "\\\\" ) )
        fbdNetwork.SelectedPath = activePath;

      if( fbdNetwork.ShowDialog() == DialogResult.Cancel ) return;


      // get new path

      string newPath;
      try { newPath = fbdNetwork.SelectedPath; }
      catch { return; }  // invalid path


      // make sure exists

      if( !Directory.Exists( newPath ) ) return;


      // select My Network Places

      EnableUpdates = false;
      tvwFolders.SelectedNode = (TreeNode)tvwFolders.Tag;
      EnableUpdates = true;


      // update filelist

      activePath = newPath;
      UpdateFileList();
    }


    // FILE LIST

    // F5 = refresh
    private void dgvFiles_KeyUp( object sender, KeyEventArgs e )
    {
      if( !EnableUpdates ) return;

      if( e.KeyCode == Keys.F5 )
        UpdateFileList();
    }

    // rename single file
    bool editing = false;
    private void dgvFiles_CellBeginEdit( object sender, DataGridViewCellCancelEventArgs e )
    {
      if( !EnableUpdates ) e.Cancel = true;

      editing = true;
    }
    private void dgvFiles_CellValidating( object sender, DataGridViewCellValidatingEventArgs e )
    {
      if( !editing ) return;
      
      int afi = (int)dgvFiles.Rows[e.RowIndex].Tag;
      string newFilename = (string)e.FormattedValue;
      string prevFilename = activeFiles[afi].Name;


      // cancel if new value empty or unchanged

      if( string.IsNullOrEmpty( newFilename ) || newFilename == activeFiles[afi].Name )
      {
        dgvFiles.CancelEdit();
        return;
      }


      // get new name/path

      if( itmOptionsPreserveExt.Checked )
        newFilename += activeFiles[afi].Extension;
      string newFullpath = Path.Combine( activePath, newFilename );


      // validate

      string errorMessage = ValidateFilename( newFilename, false );
      if( errorMessage != null )
      {
        MessageBox.Show( errorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error );
        dgvFiles.CancelEdit();
        return;
      }

      Regex regex = new Regex( "^[ .]" );
      if( regex.IsMatch( newFilename ) && !regex.IsMatch( activeFiles[afi].Filename ) )  // now starts with [ .]
      {
        errorMessage = "This " + strFilename + " begins with a space or a dot. While this is technically possible, Windows\n"
                     + "normally won't let you do this as it may cause problems with other programs.\n"
                     + "\n"
                     + "Are you sure you want to continue?";

        if( MessageBox.Show( errorMessage, "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning )
            == DialogResult.Cancel )
        {
          dgvFiles.CancelEdit();
          return;
        }
      }


      // rename

      try
      {
        if( RenameFolders )
          Directory.Move( activeFiles[afi].Fullpath, newFullpath );
        else
          File.Move( activeFiles[afi].Fullpath, newFullpath );
      }
      catch( Exception exception )
      {
        MessageBox.Show( exception.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error );
        dgvFiles.CancelEdit();
        return;
      }


      // update icon (if file)

      FileInfo fi = new FileInfo( newFullpath );

      if( !RenameFolders && fi.Extension != activeFiles[afi].Extension )
      {
        try  // add image (keyed by extension)
        {
          string ext = fi.Extension.ToLower();
          if( !icons.ContainsKey( ext ) )
            icons.Add( ext, ExtractIcons.GetIcon( newFullpath, false ) );

          dgvFiles.Rows[e.RowIndex].Cells[0].Value = icons[ext];
        }
        catch  // default = no image
        {
          dgvFiles.Rows[e.RowIndex].Cells[0].Value = new Bitmap( 1, 1 );
        }
      }


      // update RRItem

      if( RenameFolders )
        activeFiles[afi] = new RRItem( new DirectoryInfo( fi.FullName ), activeFiles[afi].Hidden, activeFiles[afi].PreserveExt );
      else
        activeFiles[afi] = new RRItem( fi, activeFiles[afi].Hidden, activeFiles[afi].PreserveExt );


      // update folder tree (if folder)

      if( RenameFolders )
      {
        foreach( TreeNode node in tvwFolders.SelectedNode.Nodes )
        {
          if( node.Text == prevFilename )
          {
            node.Text = activeFiles[afi].Name;
            Shell32.Folder folder = new Shell32.ShellClass().NameSpace( activePath );
            node.Tag = folder.ParseName( activeFiles[afi].Filename );
            break;
          }
        }
      }


      // workaround for exception when ending edit by pressing ENTER in last cell

      dgvFiles.CommitEdit( DataGridViewDataErrorContexts.Commit );
      e.Cancel = true;  


      // update preview

      editing = false;  // prevent recursion: dgvFiles.Sort() in UpdatePreview() causes dgvFiles.CellValidating
      UpdatePreview();

    }
    private void dgvFiles_CellEndEdit( object sender, DataGridViewCellEventArgs e )
    {
      editing = false;
    }

    // double-click = open file
    private void dgvFiles_CellDoubleClick( object sender, DataGridViewCellEventArgs e )
    {
      if( !EnableUpdates ) return;

      try
      {
        Process.Start( activeFiles[(int)dgvFiles.Rows[e.RowIndex].Tag].Fullpath );
      }
      catch( Exception ex )
      {
        MessageBox.Show( ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error );
      }
    }

    // hide selection when leaving
    private void dgvFiles_Leave( object sender, EventArgs e )
    {
      if( !itmOptionsRenameSelectedRows.Checked )
        dgvFiles.ClearSelection();
    }

    // error tooltips for lvwFiles subitems
    private void dgvFiles_CellMouseEnter( object sender, DataGridViewCellEventArgs e )
    {
      if( e.RowIndex < 0 ) return;
      if( e.ColumnIndex != 2 ) return;  // not preview column
      if( dgvFiles.Rows[e.RowIndex].Cells[2].Tag == null ) return;  // no preview error

      ttPreviewError.SetToolTip( dgvFiles, WrapText( (string)dgvFiles.Rows[e.RowIndex].Cells[2].Tag, 50 ) );
    }
    private void dgvFiles_CellMouseLeave( object sender, DataGridViewCellEventArgs e )
    {
      ttPreviewError.SetToolTip( dgvFiles, null );
    }
    
    // selected rows
    private void dgvFiles_SelectionChanged( object sender, EventArgs e )
    {
      UpdateSelection();
    }


    // OPTIONS/HELP

    // options
    private void itmOptionsShowHidden_Click( object sender, EventArgs e )
    {
      this.Update();
      UpdateFileList();
    }
    private void itmOptionsPreserveExt_Click( object sender, EventArgs e )
    {
      this.Update();

      // update activeFiles
      for( int afi = 0; afi < activeFiles.Count; afi++ )
        activeFiles[afi].PreserveExt = itmOptionsPreserveExt.Checked;

      // update filename column
      for( int dfi = 0; dfi < dgvFiles.Rows.Count; dfi++ )
        dgvFiles.Rows[dfi].Cells[1].Value = activeFiles[(int)dgvFiles.Rows[dfi].Tag].Name;

      // update preview column
      UpdatePreview();
    }
    private void itmOptionsAllowRenSub_Click( object sender, EventArgs e )
    {
      this.Update();
      UpdateValidation();
    }
    private void itmOptionsAddContextMenu_Click( object sender, EventArgs e )
    {
      try
      {
        if( !itmOptionsAddContextMenu.Checked )  // add key
        {
          using( RegistryKey key = Registry.ClassesRoot.CreateSubKey( "Folder\\shell\\RegexRenamer" ) )
          {
            if( key != null )
            {
              key.SetValue( "", "Rename using RegexRenamer" );
              key.Close();
            }
          }
          using( RegistryKey key = Registry.ClassesRoot.CreateSubKey( "Folder\\shell\\RegexRenamer\\command" ) )
          {
            if( key != null )
            {
              key.SetValue( "", Application.ExecutablePath + " \"%L\"" );
              key.Close();
            }
          }
          itmOptionsAddContextMenu.Checked = true;
        }
        else  // delete key
        {
          using( RegistryKey key = Registry.ClassesRoot.OpenSubKey( "Folder\\shell\\RegexRenamer" ) )
          {
            if( key != null )  // make sure exists before trying to delete
            {
              key.Close();
              Registry.ClassesRoot.DeleteSubKeyTree( "Folder\\shell\\RegexRenamer" );
            }
          }
          itmOptionsAddContextMenu.Checked = false;
        }
      }
      catch( Exception ex )
      {
        MessageBox.Show( ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error );
      }
    }

    // help
    private void itmHelpContents_Click( object sender, EventArgs e )
    {
      Help.ShowHelp( this, Path.Combine( Application.StartupPath, "RegexRenamer.chm" ), "html/index.html" );
    }
    private void itmHelpRegexReference_Click( object sender, EventArgs e )
    {
      Help.ShowHelp( this, Path.Combine( Application.StartupPath, "Regex Quick Reference.chm" ), "html/regex_quickref2.html" );
    }
    private void itmHelpEmailAuthor_Click( object sender, EventArgs e )
    {
      try
      {
        Process.Start( "mailto:xiperware@gmail.com" );
      }
      catch( Exception ex )
      {
        MessageBox.Show( ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error );
      }
    }
    private void itmHelpReportBug_Click( object sender, EventArgs e )
    {
      try
      {
        Process.Start( "http://sourceforge.net/tracker/?func=add&group_id=177064&atid=879743" );
      }
      catch( Exception ex )
      {
        MessageBox.Show( ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error );
      }
    }
    private void itmHelpHomepage_Click( object sender, EventArgs e )
    {
      try
      {
        Process.Start( "http://regexrenamer.sourceforge.net/" );
      }
      catch( Exception ex )
      {
        MessageBox.Show( ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error );
      }
    }
    private void itmHelpAbout_Click( object sender, EventArgs e )
    {
      if( aboutForm == null )
        aboutForm = new About();

      aboutForm.SetStats( countProgLaunches, countFilesRenamed );
      aboutForm.ShowDialog( this );
    }


    // RENAME

    // drop-down menu
    private void cmsRename_Opening( object sender, CancelEventArgs e )
    {
      if( btnRename.State != System.Windows.Forms.VisualStyles.PushButtonState.Pressed )
        e.Cancel = true;  // prevent context menu on right-click
    }
    private void itmRenameFiles_Click( object sender, EventArgs e )
    {
      RenameFolders = false;
      UpdateFileList();
    }
    private void itmRenameFolders_Click( object sender, EventArgs e )
    {
      RenameFolders = true;
      UpdateFileList();
    }

    // click
    private void btnRename_Click( object sender, EventArgs e )
    {
      // check for errors

      string errorMessage = null;


      // invalid match regex

      if( !validMatch ) errorMessage = "The match regular expression in invalid.";


      // preview errors exist

      if( errorMessage == null )
      {
        foreach( DataGridViewRow row in dgvFiles.Rows )
        {
          int afi = (int)row.Tag;
          if( itmOptionsRenameSelectedRows.Checked && !activeFiles[afi].Selected )
            continue;  // ignore unselected rows

          if( row.Cells[2].Tag != null )
          {
            errorMessage = "Can't rename while errors exist (highlighted in red).";
            break;
          }
        }
      }


      // no files need renaming

      int filesToRename = 0;

      if( errorMessage == null )
      {
        foreach( RRItem file in activeFiles )
        {
          if( itmOptionsRenameSelectedRows.Checked && !file.Selected )
            continue;  // ignore unselected rows

          if( (itmOutputRenameInPlace.Checked && file.Name != file.Preview)
           || (!itmOutputRenameInPlace.Checked && file.Matched) )
            filesToRename++;
        }

        if( filesToRename == 0 ) errorMessage = "There are no " + strFile + "s to rename.";
      }


      // move/copy path doesn't exist

      if( errorMessage == null && !itmOutputRenameInPlace.Checked && !Directory.Exists( fbdMoveCopy.SelectedPath ) )
      {
        if( itmOutputMoveTo.Checked ) errorMessage = "'Move to' folder '" + fbdMoveCopy.SelectedPath + "' is not a valid path.";
        else if( itmOutputCopyTo.Checked ) errorMessage = "'Copy to' folder '" + fbdMoveCopy.SelectedPath + "' is not a valid path.";
        else if( itmOutputBackupTo.Checked ) errorMessage = "'Backup to' folder '" + fbdMoveCopy.SelectedPath + "' is not a valid path.";
      }


      // move/copy path same as activePath

      if( errorMessage == null && !itmOutputRenameInPlace.Checked && fbdMoveCopy.SelectedPath == activePath )
      {
        if( itmOutputMoveTo.Checked ) errorMessage = "'Move to' folder is the same as the currently selected folder.";
        else if( itmOutputCopyTo.Checked ) errorMessage = "'Copy to' folder is the same as the currently selected folder.";
        else if( itmOutputBackupTo.Checked ) errorMessage = "'Backup to' folder is the same as the currently selected folder.";
      }


      // if error found, display dialog & abort

      if( errorMessage != null )
      {
        MessageBox.Show( errorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error );
        return;
      }


      // warn if any files start with space or dot

      bool beginWithInvalidChars = false;
      Regex regexInvalidChars = new Regex( "(^|\\\\)[ .]" );

      foreach( RRItem file in activeFiles )
      {
        if( itmOutputRenameInPlace.Checked )
        {
          if( file.Name == file.Preview ) continue;
        }
        else
        {
          if( !file.Matched ) continue;
        }

        if( regexInvalidChars.IsMatch( file.Preview ) )
        {
          beginWithInvalidChars = true;
          break;
        }
      }

      if( beginWithInvalidChars )
      {
        errorMessage = "One or more " + strFilename + "s begin with a space or a dot. While this is technically possible, Windows\n"
                     + "normally won't let you do this as it may cause problems with other programs.\n"
                     + "\n"
                     + "Are you sure you want to continue?";

        if( MessageBox.Show( errorMessage, "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning )
            == DialogResult.Cancel )
          return;
      }


      // remember regex history string to store later (in case the user changes the fields during rename)

      string regexString = "/" + cmbMatch.Text + "/" + txtReplace.Text + "/";
      if( cbModifierI.Checked ) regexString += "i";
      if( cbModifierG.Checked ) regexString += "g";
      if( cbModifierX.Checked ) regexString += "x";

      cmbMatch.Tag = regexString;


      // change button to cancel
      
      btnRename.Visible = false;
      btnCancel.Visible = true;
      btnRename.Enabled = false;
      btnCancel.Enabled = true;


      // init progressbar

      progressBar.Value = 0;
      tsOptions.Visible = lblNumMatched.Visible = lblNumConflict.Visible = false;
      progressBar.Visible = true;
      progressBar.Focus();


      // semi-disable form during rename

      SetFormActive( false );  


      // perform rename operation in background thread

      bgwRename.RunWorkerAsync( filesToRename );

    }
    private void btnCancel_Click( object sender, EventArgs e )
    {
      btnCancel.Enabled = false;
      btnCancel.Text = "Cancelling...";
      bgwRename.CancelAsync();
    }

    // background worker
    private void bgwRename_DoWork( object sender, DoWorkEventArgs e )
    {
      BackgroundWorker bw = sender as BackgroundWorker;
      RenameResult result = new RenameResult();
      int filesToRename = (int)e.Argument;
      float filesRenamed = 0.5F;

      string outputPath = activePath;
      if( itmOutputMoveTo.Checked || itmOutputCopyTo.Checked )
        outputPath = fbdMoveCopy.SelectedPath;


      for( int afi = 0; afi < activeFiles.Count; afi++ )
      {
        // abort if user cancelled

        if( bw.CancellationPending )
        {
          // e.Cancel = true;       // don't use this as it prevents access to the result object
          result.Cancelled = true;  // use our own instead
          break;
        }


        // skip ignored/unselected files

        if( itmOutputRenameInPlace.Checked )
        {
          if( activeFiles[afi].Name == activeFiles[afi].Preview ) continue;
        }
        else
        {
          if( !activeFiles[afi].Matched ) continue;
        }

        if( itmOptionsRenameSelectedRows.Checked )
        {
          if( !activeFiles[afi].Selected ) continue;
        }

        
        // update progressbar
        
        bw.ReportProgress( (int)( ( filesRenamed / filesToRename ) * 100 ) );
        filesRenamed++;


        // get new fullpath

        string newFullpath = Path.Combine( outputPath, activeFiles[afi].Preview );
        if( itmOptionsPreserveExt.Checked )
          newFullpath += activeFiles[afi].Extension;


        // create subdirs (if any)

        if( activeFiles[afi].Preview.Contains( "\\" ) )
        {
          string newDirectory = Path.GetDirectoryName( newFullpath );
          if( !Directory.Exists( newDirectory ) )
          {
            try
            {
              Directory.CreateDirectory( newDirectory );
            }
            catch( Exception ex )
            {
              result.ReportError( activeFiles[afi].Name,
                                  activeFiles[afi].Preview,
                                  "Create folder '" + newDirectory + "' failed: " + ex.Message );
              continue;
            }
          }
          result.RenameToSubfolders = true;
        }


        // rename/move/copy, catch any errors

        try
        {
          if( RenameFolders )
          {
            Directory.Move( activeFiles[afi].Fullpath, newFullpath );
          }
          else
          {
            if( itmOutputRenameInPlace.Checked || itmOutputMoveTo.Checked )
              File.Move( activeFiles[afi].Fullpath, newFullpath );
            else if( itmOutputCopyTo.Checked )
              File.Copy( activeFiles[afi].Fullpath, newFullpath );
            else  // backup to
            {
              File.Copy( activeFiles[afi].Fullpath, Path.Combine( fbdMoveCopy.SelectedPath, Path.GetFileName( activeFiles[afi].Filename ) ) );
              File.Move( activeFiles[afi].Fullpath, newFullpath );
            }
          }

          result.ReportSuccess();
        }
        catch( Exception ex )
        {
          result.ReportError( activeFiles[afi].Name, activeFiles[afi].Preview, ex.Message );
          continue;
        }

      }  // end rename loop


      bw.ReportProgress( 100 );
      e.Result = result;
    }
    private void bgwRename_ProgressChanged( object sender, ProgressChangedEventArgs e )
    {
      progressBar.Value = e.ProgressPercentage;
    }
    private void bgwRename_RunWorkerCompleted( object sender, RunWorkerCompletedEventArgs e )
    {
      // re-throw exception if one occured during rename

      if( e.Error != null )
        throw e.Error;


      // get result

      RenameResult result = (RenameResult)e.Result;


      // if necessary, refresh nodes in folder tree

      if( RenameFolders )
      {
        tvwFolders.RefreshNode( tvwFolders.SelectedNode );
        if( itmOutputMoveTo.Checked && !fbdMoveCopy.SelectedPath.StartsWith( activePath ) )
          tvwFolders.RefreshNode( fbdMoveCopy.SelectedPath );
      }
      else if( result.RenameToSubfolders )
      {
        if( itmOutputRenameInPlace.Checked || itmOutputBackupTo.Checked )
          tvwFolders.RefreshNode( tvwFolders.SelectedNode );
        else  // Move to, Copy to
          tvwFolders.RefreshNode( fbdMoveCopy.SelectedPath );
      }


      // update stats

      countFilesRenamed += result.FilesRenamed;


      // swap rename/cancel buttons

      btnCancel.Visible = false;
      btnRename.Visible = true;
      btnCancel.Enabled = false;
      btnRename.Enabled = true;
      btnCancel.Text = "&Cancel";  // reset text


      // hide progress bar

      progressBar.Visible = false;
      tsOptions.Visible = lblNumMatched.Visible = lblNumConflict.Visible = true;
      UnFocusAll();


      // show error dialog if any errors occured

      if( result.AnyErrors )
        result.ShowErrorDialog( strFile );


      if( !result.AnyErrors && !result.Cancelled )
      {
        // save regex to history

        string regexString = (string)cmbMatch.Tag;

        if( cmbMatch.Items.Contains( regexString ) )
          cmbMatch.Items.Remove( regexString );

        cmbMatch.Items.Insert( 0, regexString );

        while( cmbMatch.Items.Count > MAX_HISTORY )
          cmbMatch.Items.RemoveAt( cmbMatch.Items.Count - 1 );

        SaveRegexHistory();


        // reset fields

        ResetFields();
      }


      // reactivate form & refresh filelist

      SetFormActive( true );
      UpdateFileList();
      cmbMatch.Focus();
    }

    #endregion
  }
}


/* ==============================================================================
 * Tag usage
 * ------------------------------------------------------------------------------
 * Variable                    Type        Usage                     Can be null?
 * ------------------------------------------------------------------------------
 * cmbMatch                    string      Regex string for history entry     N
 * tvwFolders                  TreeNode    The 'My Network Places' node       Y
 * tvwFolders.Nodes            FolderItem  Folder information for node        N
 * dgvFiles                    int         Number of files ignored            N
 * dgvFiles.Rows[i]            int         Index for entry in activeFiles     N
 * dgvFiles.Rows[i].Cells[2]   string      Preview validation error message   Y
 * mnuNumbering.DropDownItems  bool        True if validation error           N
 * cmRegexMatch.MenuItems      InsertArgs  The text to be inserted and how    N
 * txtFilter                   object[]    Store state during stat mouseover  Y
 * =============================================================================
 */
