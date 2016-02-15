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
using System.Drawing;
using System.Windows.Forms;

namespace RegexRenamer
{
  public partial class RenameErrorDialog : Form
  {
    private Font wingdingsFont;

    public RenameErrorDialog()
    {
      wingdingsFont = new Font( "Wingdings", 10f, FontStyle.Regular, GraphicsUnit.Point );
      InitializeComponent();
    }

    public void AutoSizeColumns()
    {
      colOldName.AutoResize( ColumnHeaderAutoResizeStyle.ColumnContent );
      colNewName.AutoResize( ColumnHeaderAutoResizeStyle.ColumnContent );
      colError.AutoResize( ColumnHeaderAutoResizeStyle.ColumnContent );
    }
    public void AddEntry( string oldName, string newName, string errorMessage )
    {
      lvwErrors.Items.Add( oldName ).UseItemStyleForSubItems = false;
      lvwErrors.Items[lvwErrors.Items.Count - 1].SubItems.Add( (char)240 + "" ).Font = wingdingsFont;
      lvwErrors.Items[lvwErrors.Items.Count - 1].SubItems.Add( newName );
      lvwErrors.Items[lvwErrors.Items.Count - 1].SubItems.Add( "" );
      lvwErrors.Items[lvwErrors.Items.Count - 1].SubItems.Add( errorMessage.Replace( "\r\n", " " ) ).ForeColor = Color.Red;
    }
    public string Message
    {
      set { lblMessage.Text = value; }
    }


    private void lvwErrors_Enter( object sender, EventArgs e )
    {
      btnOK.Focus();
    }


    
  }
}