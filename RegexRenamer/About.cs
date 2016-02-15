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
using System.Diagnostics;
using System.Reflection;
using System.Windows.Forms;

namespace RegexRenamer
{
  public partial class About : Form
  {
    public About()
    {
      InitializeComponent();
      string copyright = ( (AssemblyCopyrightAttribute)Assembly.GetEntryAssembly().GetCustomAttributes( typeof( AssemblyCopyrightAttribute ), false )[0] ).Copyright;

      lblHeader.Text = "RegexRenamer v" + Application.ProductVersion + "\r\n" + copyright + "\r\nGNU General Public License\r\n";
    }

    public void SetStats( int countProgLaunches, int countFilesRenamed )
    {
      lblStats.Text = String.Format( "RegexRenamer has been run {0:N0} {1} and renamed a total of {2:N0} {3}.",
                                     countProgLaunches, countProgLaunches == 1 ? "time" : "times",
                                     countFilesRenamed, countFilesRenamed == 1 ? "file" : "files" );
    }

    private void btnOK_Click( object sender, EventArgs e )
    {
      this.Close();
    }

    private void linkHomepage_LinkClicked( object sender, LinkLabelLinkClickedEventArgs ea )
    {
      try
      {
        Process.Start( "http://regexrenamer.sourceforge.net/" );
      }
      catch( Exception e )
      {
        MessageBox.Show( e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error );
      }
    }
    private void linkEmail_LinkClicked( object sender, LinkLabelLinkClickedEventArgs ea )
    {
      try
      {
        Process.Start( "mailto:xiperware@gmail.com" );
      }
      catch( Exception e )
      {
        MessageBox.Show( e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error );
      }
    }
  }
}