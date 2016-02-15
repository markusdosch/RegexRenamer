/*

  Windows Forms Folder Tree View control for .Net
  Version 1.1, posted 20-Oct-2002
  (c)Copyright 2002 Furty (furty74@yahoo.com). All rights reserved.
  Free for any use, so long as copyright is acknowledged.
  
  This is an all-new version of the FolderTreeView control I posted here at CP some weeks ago.
  The control now starts in the Desktop namespace, and a new DrillToFolder method has been added
  so the startup folder can be specified. Please note that this control is not intended to have 
  all of the functionality of the actual Windows Explorer TreeView - it is a light-weight control 
  designed for use in projects where you want to supply a treeview for folder navigation, without supporting
  windows shell extensions. If you are looking for a control that supports shell extensions
  you should be looking at the excellent ËxplorerTreeControl submitted by Carlos H Perez at the CP website.
  
  The 3 classes that make up the control have been merged into the one file here for ease of
  integration into your own projects. The reason for separate classes is that this code has been
  extracted from a much larger project I'm working on, and the code that is not required for this
  control has been removed.  
  
  Acknowledgments:
  Substantial portions of the ShellOperations and ExtractIcons classes were borrowed from the 
  FTPCom article written by Jerome Lacaille, available on the www.codeproject.com website.
  
  If you improve this control, please email me the updated source, and if you have any 
  comments or suggestions, please post your thoughts in the feedback section on the 
  codeproject.com page for this control.
  
  Version 1.11 Changes:
  Updated the GetDesktopIcon method so that the small (16x16) desktop icon is returned instead of the large version
  Added code to give the Desktop root node a FolderItem object tag equal to the DesktopDirectory SpecialFolder,
  this ensures that the desktop node returns a file path.
 
 */
using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Drawing;
using System.IO;

using System.Diagnostics;

namespace Furty.Windows.Forms
{
  #region FolderTreeView Class

  public class FolderTreeView : System.Windows.Forms.TreeView
  {
    private System.Windows.Forms.ImageList folderTreeViewImageList;
    private System.Globalization.CultureInfo cultureInfo = System.Globalization.CultureInfo.CurrentCulture;

    #region Constructors

    public FolderTreeView()
    {
      this.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler( this.TreeViewBeforeExpand );
    }

    public void InitFolderTreeView()
    {
      InitImageList();
      ShellOperations.PopulateTree( this, base.ImageList );
      if( this.Nodes.Count > 0 )
      {
        this.Nodes[0].Expand();
      }
    }

    private void InitImageList()
    {
      // setup the image list to hold the folder icons
      folderTreeViewImageList = new System.Windows.Forms.ImageList();
      folderTreeViewImageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
      folderTreeViewImageList.ImageSize = new System.Drawing.Size( 16, 16 );
      folderTreeViewImageList.TransparentColor = System.Drawing.Color.Transparent;

      // add the Desktop icon to the image list
      try
      {
        folderTreeViewImageList.Images.Add( ExtractIcons.GetDesktopIcon() );
      }
      catch
      {
        // Create a blank icon if the desktop icon fails for some reason
        Bitmap bmp = new Bitmap( 16, 16 );
        Image img = (Image)bmp;
        folderTreeViewImageList.Images.Add( (Image)img.Clone() );
        bmp.Dispose();
      }
      this.ImageList = folderTreeViewImageList;
    }

    #endregion

    #region Event Handlers

    private void TreeViewBeforeExpand( object sender, System.Windows.Forms.TreeViewCancelEventArgs e )
    {
      this.BeginUpdate();
      ShellOperations.ExpandBranch( e.Node, this.ImageList );
      this.EndUpdate();
    }

    #endregion

    #region Furty.Windows.Forms.FolderTreeView Properties & Methods

    public string GetSelectedNodePath()
    {
      return ShellOperations.GetFilePath( SelectedNode );
    }

    // [xiperware]
    public string ForceGetSelectedNodePath()
    {
      return ShellOperations.ForceGetFilePath( SelectedNode );
    }

    public bool DrillToFolder( string folderPath )
    {
      bool folderFound = false;
      if( Directory.Exists( folderPath ) ) // don't bother drilling unless the directory exists
      {
        this.BeginUpdate();
        // if there's a trailing \ on the folderPath, remove it unless it's a drive letter
        if( folderPath.Length > 3 && folderPath.LastIndexOf( "\\" ) == folderPath.Length - 1 )
          folderPath = folderPath.Substring( 0, folderPath.Length - 1 );
        //Start drilling the tree
        DrillTree( this.Nodes[0].Nodes, folderPath.ToUpper( cultureInfo ), ref folderFound );
        this.EndUpdate();
      }
      if( !folderFound )
        this.SelectedNode = this.Nodes[0];
      return folderFound;
    }

    private void DrillTree( TreeNodeCollection tnc, string path, ref bool folderFound )
    {
      foreach( TreeNode tn in tnc )
      {
        if( !folderFound )
        {
          this.SelectedNode = tn;
          string tnPath = ShellOperations.GetFilePath( tn ).ToUpper( cultureInfo );
          if( path == tnPath && !folderFound )
          {
            this.SelectedNode = tn;
            tn.EnsureVisible();
            folderFound = true;
            break;
          }
          else if( path.IndexOf( tnPath ) > -1 && !folderFound )
          {
            tn.Expand();
            DrillTree( tn.Nodes, path, ref folderFound );
          }
        }
      }
    }

    public void RefreshNode( string path )  // [xiperware]
    {
      if( path.StartsWith( "\\\\" ) ) return;
      path = path.ToLower();

      TreeNode currentNode = this.Nodes[0].Nodes[0];  // assume My Computer is first node under Desktop
      bool foundParent = false, foundChild = false;
      
      while( true )
      {
        foundParent = false;
        foreach( TreeNode node in currentNode.Nodes )
        {
          if( node.Tag.ToString() == "DUMMYNODE" )
            return;

          string nodePath = ( (Shell32.FolderItem)node.Tag ).Path.ToLower();
          if( path == nodePath )
          {
            currentNode = node;
            foundChild  = true;
            break;
          }
          else if( path.StartsWith( nodePath ) )
          {
            currentNode = node;
            foundParent = true;
            break;
          }
        }
        if( foundChild || !foundParent ) break;
      }

      RefreshNode( currentNode );
    }

    public void RefreshNode( TreeNode tn )  // [xiperware]
    {
      if( this.Tag != null && tn == (TreeNode)this.Tag )  // My Network Places
        return;

      if( tn.Nodes.Count == 1 && tn.Nodes[0].Tag.ToString() == "DUMMYNODE" )
        return;

      this.BeginUpdate();

      TreeNode selectedNode = this.SelectedNode;

      bool nodeWasExpanded = tn.IsExpanded;
      if( nodeWasExpanded )
        tn.Collapse();

      tn.Nodes.Clear();

      TreeNode dummyNode = new TreeNode();
      dummyNode.Tag = "DUMMYNODE";
      tn.Nodes.Add( dummyNode );

      tn.Expand();  // populates sub-nodes
      if( !nodeWasExpanded )
        tn.Collapse();

      if( this.SelectedNode != selectedNode )
        DrillToFolder( ( (Shell32.FolderItem)selectedNode.Tag ).Path );

      this.EndUpdate();
    }

    #endregion

    #region System.Windows.Forms.TreeView Properties

    public override System.Drawing.Color BackColor
    {
      get
      { return base.BackColor; }
      set
      { base.BackColor = value; }
    }

    public override System.Drawing.Image BackgroundImage
    {
      get
      { return base.BackgroundImage; }
      set
      { base.BackgroundImage = value; }
    }

    public override System.Drawing.Color ForeColor
    {
      get
      { return base.ForeColor; }
      set
      { base.ForeColor = value; }
    }

    public override string Text
    {
      get
      { return base.Text; }
      set
      { base.Text = value; }
    }

    public override bool AllowDrop
    {
      get
      { return base.AllowDrop; }
      set
      { base.AllowDrop = value; }
    }

    public override System.Windows.Forms.AnchorStyles Anchor
    {
      get
      { return base.Anchor; }
      set
      { base.Anchor = value; }
    }

    public override System.Windows.Forms.BindingContext BindingContext
    {
      get
      { return base.BindingContext; }
      set
      { base.BindingContext = value; }
    }

    public override System.Windows.Forms.ContextMenu ContextMenu
    {
      get
      { return base.ContextMenu; }
      set
      { base.ContextMenu = value; }
    }

    public override System.Windows.Forms.Cursor Cursor
    {
      get
      { return base.Cursor; }
      set
      { base.Cursor = value; }
    }

    public override System.Drawing.Rectangle DisplayRectangle
    {
      get
      { return base.DisplayRectangle; }
    }

    public override System.Windows.Forms.DockStyle Dock
    {
      get
      { return base.Dock; }
      set
      { base.Dock = value; }
    }

    public override bool Focused
    {
      get
      { return base.Focused; }
    }

    public override System.Drawing.Font Font
    {
      get
      { return base.Font; }
      set
      { base.Font = value; }
    }

    public override System.Windows.Forms.RightToLeft RightToLeft
    {
      get
      { return base.RightToLeft; }
      set
      { base.RightToLeft = value; }
    }

    public override System.ComponentModel.ISite Site
    {
      get
      { return base.Site; }
      set
      { base.Site = value; }
    }

    #endregion

    #region System.Windows.Forms.TreeView Overrides

    public override void ResetText()
    {
      base.ResetText();
    }

    public override void Refresh()
    {
      base.Refresh();
    }

    public override void ResetRightToLeft()
    {
      base.ResetRightToLeft();
    }

    public override void ResetForeColor()
    {
      base.ResetForeColor();
    }

    public override void ResetFont()
    {
      base.ResetFont();
    }

    public override void ResetCursor()
    {
      base.ResetCursor();
    }

    public override void ResetBackColor()
    {
      base.ResetBackColor();
    }

    public override bool PreProcessMessage( ref System.Windows.Forms.Message msg )
    {
      return base.PreProcessMessage( ref msg );
    }

    public override System.Runtime.Remoting.ObjRef CreateObjRef( System.Type requestedType )
    {
      return base.CreateObjRef( requestedType );
    }

    public override object InitializeLifetimeService()
    {
      return base.InitializeLifetimeService();
    }

    public override int GetHashCode()
    {
      return base.GetHashCode();
    }

    public override bool Equals( object obj )
    {
      return base.Equals( obj );
    }

    public override string ToString()
    {
      return base.ToString();
    }

    #endregion

  }

  #endregion

  #region ShellOperations Class

  public class ShellOperations
  {

    #region ShellFolder Enums
    // Enums for standard Windows shell folders
    public enum ShellFolder
    {
      Desktop = Shell32.ShellSpecialFolderConstants.ssfDESKTOP,
      DesktopDirectory = Shell32.ShellSpecialFolderConstants.ssfDESKTOPDIRECTORY,
      MyComputer = Shell32.ShellSpecialFolderConstants.ssfDRIVES,
      MyDocuments = Shell32.ShellSpecialFolderConstants.ssfPERSONAL,
      MyPictures = Shell32.ShellSpecialFolderConstants.ssfMYPICTURES,
      History = Shell32.ShellSpecialFolderConstants.ssfHISTORY,
      Favorites = Shell32.ShellSpecialFolderConstants.ssfFAVORITES,
      Fonts = Shell32.ShellSpecialFolderConstants.ssfFONTS,
      ControlPanel = Shell32.ShellSpecialFolderConstants.ssfCONTROLS,
      TemporaryInternetFiles = Shell32.ShellSpecialFolderConstants.ssfINTERNETCACHE,
      MyNetworkPlaces = Shell32.ShellSpecialFolderConstants.ssfNETHOOD,
      NetworkNeighborhood = Shell32.ShellSpecialFolderConstants.ssfNETWORK,
      ProgramFiles = Shell32.ShellSpecialFolderConstants.ssfPROGRAMFILES,
      RecentFiles = Shell32.ShellSpecialFolderConstants.ssfRECENT,
      StartMenu = Shell32.ShellSpecialFolderConstants.ssfSTARTMENU,
      Windows = Shell32.ShellSpecialFolderConstants.ssfWINDOWS,
      Printers = Shell32.ShellSpecialFolderConstants.ssfPRINTERS,
      RecycleBin = Shell32.ShellSpecialFolderConstants.ssfBITBUCKET,
      Cookies = Shell32.ShellSpecialFolderConstants.ssfCOOKIES,
      ApplicationData = Shell32.ShellSpecialFolderConstants.ssfAPPDATA,
      SendTo = Shell32.ShellSpecialFolderConstants.ssfSENDTO,
      StartUp = Shell32.ShellSpecialFolderConstants.ssfSTARTUP
    }
    #endregion

    #region FolderTreeView Methods

    #region GetFilePath
    public static string GetFilePath( TreeNode tn )
    {
      try
      {
        Shell32.FolderItem folderItem = (Shell32.FolderItem)tn.Tag;
        string folderPath = folderItem.Path;
        if( Directory.Exists( folderPath ) )
          return folderPath;
        else
          return "";
      }
      catch
      {
        return "";
      }
    }
    // [xiperware]
    public static string ForceGetFilePath( TreeNode tn )
    {
      try
      {
        Shell32.FolderItem folderItem = (Shell32.FolderItem)tn.Tag;
        return folderItem.Path;
      }
      catch
      {
        return "";
      }
    }
    #endregion

    #region Populate Tree
    public static void PopulateTree( TreeView tree, ImageList imageList )
    {
      int imageCount = imageList.Images.Count - 1;
      tree.Nodes.Clear();
      AddRootNode( tree, ref imageCount, imageList, ShellFolder.Desktop, true );
      if( tree.Nodes.Count > 1 )
      {
        tree.SelectedNode = tree.Nodes[1];
        ExpandBranch( tree.Nodes[1], imageList );
      }
    }
    #endregion

    #region Add Root Node
    private static void AddRootNode( TreeView tree, ref int imageCount, ImageList imageList, ShellFolder shellFolder, bool getIcons )
    {
      Shell32.Shell shell32 = new Shell32.ShellClass();
      Shell32.Folder shell32Folder = shell32.NameSpace( shellFolder );
      Shell32.FolderItems items = shell32Folder.Items();

      tree.Nodes.Clear();
      TreeNode desktop = new TreeNode( "Desktop", 0, 0 );

      // Added in version 1.11
      // add a FolderItem object to the root (Desktop) node tag that corresponds to the DesktopDirectory namespace
      // This ensures that the GetSelectedNodePath will return the actual Desktop folder path when queried.
      // There's possibly a better way to create a Shell32.FolderItem instance for this purpose, 
      // but I surely don't know it

      Shell32.Folder dfolder = shell32.NameSpace( ShellFolder.DesktopDirectory );
      foreach( Shell32.FolderItem fi in dfolder.ParentFolder.Items() )
      {
        if( fi.Name == dfolder.Title )
        {
          desktop.Tag = fi;
          break;
        }
      }

      // Add the Desktop root node to the tree
      tree.Nodes.Add( desktop );

      // [xiperware] Get FolderItem that represents Recycle Bin
      Shell32.Folder recFolder = shell32.NameSpace( ShellFolder.RecycleBin );
      Shell32.FolderItem recycle = null;
      foreach( Shell32.FolderItem fi in recFolder.ParentFolder.Items() )
      {
        if( fi.Name == recFolder.Title )
        {
          recycle = fi;
          break;
        }
      }

      // [xiperware] Get FolderItem that represents My Network Places
      Shell32.Folder netFolder = shell32.NameSpace( ShellFolder.NetworkNeighborhood );
      Shell32.FolderItem mynetwork = null;
      foreach( Shell32.FolderItem fi in netFolder.ParentFolder.Items() )
      {
        if( fi.Name == netFolder.Title )
        {
          mynetwork = fi;
          break;
        }
      }

      // iterate through the Desktop namespace and populate the first level nodes
      foreach( Shell32.FolderItem item in items )
      {
        if( !item.IsFolder   ) continue;  // this ensures that desktop shortcuts etc are not displayed
        if( item.IsBrowsable ) continue;  // [xiperware] exclude zip files
        if( recycle != null && item.Path == recycle.Path ) continue;  // [xiperware] skip recycle bin

        TreeNode tn = AddTreeNode( item, ref imageCount, imageList, getIcons );
        desktop.Nodes.Add( tn );

        if( mynetwork != null && item.Path == mynetwork.Path )  // [xiperware] skip my network places subdirs
        {
          tree.Tag = tn;  // store node in tag
          continue;
        }

        CheckForSubDirs( tn, imageList );
      }

    }
    #endregion

    #region Fill Sub Dirs
    private static void FillSubDirectories( TreeNode tn, ref int imageCount, ImageList imageList, bool getIcons )
    {
      Shell32.FolderItem folderItem = (Shell32.FolderItem)tn.Tag;
      Shell32.Folder folder = (Shell32.Folder)folderItem.GetFolder;

      foreach( Shell32.FolderItem item in folder.Items() )
      {
        if( item.IsFileSystem && item.IsFolder && !item.IsBrowsable )
        {
          TreeNode ntn = AddTreeNode( item, ref imageCount, imageList, getIcons );
          tn.Nodes.Add( ntn );
          CheckForSubDirs( ntn, imageList );
        }
      }
    }
    #endregion

    #region Create Dummy Node
    private static void CheckForSubDirs( TreeNode tn, ImageList imageList )
    {
      if( tn.Nodes.Count == 0 )
      {
        try
        {
          // create dummy nodes for any subfolders that have further subfolders
          Shell32.FolderItem folderItem = (Shell32.FolderItem)tn.Tag;
          Shell32.Folder folder = (Shell32.Folder)folderItem.GetFolder;

          bool hasFolders = false;
          foreach( Shell32.FolderItem item in folder.Items() )
          {
            if( item.IsFileSystem && item.IsFolder && !item.IsBrowsable )
            {
              hasFolders = true;
              break;
            }
          }
          if( hasFolders )
          {
            TreeNode ntn = new TreeNode();
            ntn.Tag = "DUMMYNODE";
            tn.Nodes.Add( ntn );
          }
        }
        catch { }
      }
    }
    #endregion

    #region Expand Branch
    public static void ExpandBranch( TreeNode tn, ImageList imageList )
    {
      // if there's a dummy node present, clear it and replace with actual contents
      if( tn.Nodes.Count == 1 && tn.Nodes[0].Tag.ToString() == "DUMMYNODE" )
      {
        tn.Nodes.Clear();
        Shell32.FolderItem folderItem = (Shell32.FolderItem)tn.Tag;
        Shell32.Folder folder = (Shell32.Folder)folderItem.GetFolder;
        int imageCount = imageList.Images.Count - 1;
        foreach( Shell32.FolderItem item in folder.Items() )
        {
          if( item.IsFileSystem && item.IsFolder && !item.IsBrowsable )
          {
            TreeNode ntn = AddTreeNode( item, ref imageCount, imageList, true );
            tn.Nodes.Add( ntn );
            CheckForSubDirs( ntn, imageList );
          }
        }
      }
    }
    #endregion

    #region Add Tree Node
    public static TreeNode AddTreeNode( Shell32.FolderItem item, ref int imageCount, ImageList imageList, bool getIcons )
    {
      TreeNode tn = new TreeNode();
      tn.Text = item.Name;
      tn.Tag = item;

      if( getIcons )
      {
        try
        {
          imageCount++;
          tn.ImageIndex = imageCount;
          imageCount++;
          tn.SelectedImageIndex = imageCount;
          imageList.Images.Add( ExtractIcons.GetIcon( item.Path, false ) ); // normal icon
          imageList.Images.Add( ExtractIcons.GetIcon( item.Path, true ) ); // selected icon
        }
        catch // use default 
        {
          tn.ImageIndex = 1;
          tn.SelectedImageIndex = 2;
        }
      }
      else // use default
      {
        tn.ImageIndex = 1;
        tn.SelectedImageIndex = 2;
      }
      return tn;
    }

    #endregion

    #endregion
  }

  #endregion

  #region ExtractIcons Class

  public class ExtractIcons
  {
    #region Structs & Enum

    [StructLayout( LayoutKind.Sequential )]
    private struct SHFILEINFO
    {
      public SHFILEINFO( bool b )
      {
        hIcon = IntPtr.Zero; iIcon = 0; dwAttributes = 0; szDisplayName = ""; szTypeName = "";
      }
      public IntPtr hIcon;
      public int iIcon;
      public uint dwAttributes;
      [MarshalAs( UnmanagedType.LPStr, SizeConst = 260 )]
      public string szDisplayName;
      [MarshalAs( UnmanagedType.LPStr, SizeConst = 80 )]
      public string szTypeName;
    };

    private enum SHGFI
    {
      SHGFI_ICON = 0x000000100,     // get icon
      SHGFI_DISPLAYNAME = 0x000000200,     // get display name
      SHGFI_TYPENAME = 0x000000400,     // get type name
      SHGFI_ATTRIBUTES = 0x000000800,     // get attributes
      SHGFI_ICONLOCATION = 0x000001000,     // get icon location
      SHGFI_EXETYPE = 0x000002000,     // return exe type
      SHGFI_SYSICONINDEX = 0x000004000,     // get system icon index
      SHGFI_LINKOVERLAY = 0x000008000,     // put a link overlay on icon
      SHGFI_SELECTED = 0x000010000,     // show icon in selected state
      SHGFI_ATTR_SPECIFIED = 0x000020000,     // get only specified attributes
      SHGFI_LARGEICON = 0x000000000,     // get large icon
      SHGFI_SMALLICON = 0x000000001,     // get small icon
      SHGFI_OPENICON = 0x000000002,     // get open icon
      SHGFI_SHELLICONSIZE = 0x000000004,     // get shell size icon
      SHGFI_PIDL = 0x000000008,     // pszPath is a pidl
      SHGFI_USEFILEATTRIBUTES = 0x000000010     // use passed dwFileAttribute
    }

    #endregion

    #region Get Folder Icons

    [DllImport( "Shell32.dll" )]
    private static extern IntPtr SHGetFileInfo( string pszPath, uint dwFileAttributes,
      out SHFILEINFO psfi, uint cbfileInfo, SHGFI uFlags );

    public static Icon GetIcon( string strPath, bool selected )  // [xiperware]  , ImageList imageList)
    {
      SHFILEINFO info = new SHFILEINFO( true );
      int cbFileInfo = Marshal.SizeOf( info );
      SHGFI flags;
      if( !selected )
        flags = SHGFI.SHGFI_ICON | SHGFI.SHGFI_SMALLICON;
      else
        flags = SHGFI.SHGFI_ICON | SHGFI.SHGFI_SMALLICON | SHGFI.SHGFI_OPENICON;

      SHGetFileInfo( strPath, 256, out info, (uint)cbFileInfo, flags );
      return Icon.FromHandle( info.hIcon );
    }

    public static Icon GetFolderIcon()  // [xiperware]
    {
      SHFILEINFO info = new SHFILEINFO();
      SHGetFileInfo( null,
                     0x00000010,  // Shell32.FILE_ATTRIBUTE_DIRECTORY,
                     out info,
                     (uint)Marshal.SizeOf( info ),
                     SHGFI.SHGFI_ICON | SHGFI.SHGFI_USEFILEATTRIBUTES | SHGFI.SHGFI_SMALLICON );
      return Icon.FromHandle( info.hIcon );
    }

    #endregion

    #region Get Desktop Icon

    // Retreive the desktop icon from Shell32.dll - it always appears at index 34 in all shell32 versions.
    // This is probably NOT the best way to retreive this icon, but it works - if you have a better way
    // by all means let me know..

    //    [DllImport("Shell32.dll", CharSet=CharSet.Auto)]
    //    public static extern IntPtr ExtractIcon(int hInst, string lpszExeFileName, int nIconIndex);
    //
    //    public static Icon GetDesktopIcon()
    //    {
    //      IntPtr i = ExtractIcon(0, Environment.SystemDirectory + "\\shell32.dll", 34);
    //      return Icon.FromHandle(i);
    //    }

    // Updated this method in v1.11 so that the icon returned is a small icon, not a large icon as
    // returned by the old method above

    [DllImport( "Shell32.dll", CharSet = CharSet.Auto )]
    public static extern uint ExtractIconEx(
      string lpszFile, int nIconIndex, IntPtr[] phiconLarge, IntPtr[] phiconSmall, uint nIcons );

    public static Icon GetDesktopIcon()
    {
      IntPtr[] handlesIconLarge = new IntPtr[1];
      IntPtr[] handlesIconSmall = new IntPtr[1];
      uint i = ExtractIconEx( Environment.SystemDirectory + "\\shell32.dll", 34,
        handlesIconLarge, handlesIconSmall, 1 );

      return Icon.FromHandle( handlesIconSmall[0] );
    }

    #endregion

  }

  #endregion


}

