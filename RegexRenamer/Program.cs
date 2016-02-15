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
using System.Threading;
using System.Windows.Forms;


namespace RegexRenamer
{
  static class Program
  {
    [STAThread]
    static void Main( string[] args )
    {
      try
      {
#if !DEBUG
        
        Application.ThreadException += Application_ThreadException;
#endif
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault( false );


        // get initPath cmdline arg

        string initPath = null;
        if( args.Length > 0 )
        {
          if( args[0].EndsWith( "\"" ) )
            args[0] = args[0].Replace( '"', '\\' );  // drive letters arrive as:  c:"  rather than  c:\  ???
          if( System.IO.Directory.Exists( args[0] ) )
            initPath = args[0];
        }


        Application.Run( new MainForm( initPath ) );

      }
      catch( System.Security.SecurityException )
      {
        MessageBox.Show( "You are trying to run RegexRenamer from the Intranet Zone (ie, a network share).\n"
                       + "Due to .NET code access security, you need to either copy RegexRenamer locally\n"
                       + "and run it from your computer, or grant it permission to run from the Intranet Zone.\n"
                       + "\n"
                       + "To grant permission, run the batch file 'GrantIntranetPermission.bat' found in the\n"
                       + "installation directory, or read it's contents for manual instructions.",
                         "Permission Denied", MessageBoxButtons.OK, MessageBoxIcon.Error );
      }
#if !DEBUG
      catch( Exception ex )
      {
        // handle unhandled exception (main thread)
        UnhandledException( ex );
      }
#endif
    }

    static void Application_ThreadException( object sender, ThreadExceptionEventArgs e )
    {
      // handle unhandled exception (event thread)
      UnhandledException( e.Exception );
      Application.Exit();
    }

    static void UnhandledException( Exception ex )
    {
      MessageBox.Show( "Congratulations, you've made RegexRenamer crash! :)\n\nCould you please press Ctrl+C "
                     + "to copy this information and paste it in an email to xiperware@gmail.com\nalong with "
                     + "what you were doing at the time. This will help the developer to identify and fix "
                     + "the problem.\n\n\n" + ex, "Unhandled exception (RegexRenamer v" + Application.ProductVersion + ")",
                       MessageBoxButtons.OK, MessageBoxIcon.Error );
    }

  }
}