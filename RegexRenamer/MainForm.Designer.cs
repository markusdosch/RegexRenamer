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


namespace RegexRenamer
{
  partial class MainForm
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose( bool disposing )
    {
      if( disposing && ( components != null ) )
      {
        components.Dispose();
      }
      base.Dispose( disposing );
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.components = new System.ComponentModel.Container();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager( typeof( MainForm ) );
      this.miRegexMatchMatch = new System.Windows.Forms.MenuItem();
      this.miRegexMatchMatchSingleChar = new System.Windows.Forms.MenuItem();
      this.miRegexMatchMatchDigit = new System.Windows.Forms.MenuItem();
      this.miRegexMatchMatchAlpha = new System.Windows.Forms.MenuItem();
      this.miRegexMatchMatchSpace = new System.Windows.Forms.MenuItem();
      this.miRegexMatchMatchMultiChar = new System.Windows.Forms.MenuItem();
      this.miRegexMatchMatchNonDigit = new System.Windows.Forms.MenuItem();
      this.miRegexMatchMatchNonAlpha = new System.Windows.Forms.MenuItem();
      this.miRegexMatchMatchNonSpace = new System.Windows.Forms.MenuItem();
      this.gbFilter = new System.Windows.Forms.GroupBox();
      this.cbFilterExclude = new System.Windows.Forms.CheckBox();
      this.txtFilter = new System.Windows.Forms.TextBox();
      this.rbFilterGlob = new System.Windows.Forms.RadioButton();
      this.rbFilterRegex = new System.Windows.Forms.RadioButton();
      this.pnlStats = new System.Windows.Forms.Panel();
      this.lblStatsHidden = new System.Windows.Forms.Label();
      this.lblStatsShown = new System.Windows.Forms.Label();
      this.lblStatsFiltered = new System.Windows.Forms.Label();
      this.lblStatsTotal = new System.Windows.Forms.Label();
      this.lblStats = new System.Windows.Forms.Label();
      this.lblMatch = new System.Windows.Forms.Label();
      this.lblReplace = new System.Windows.Forms.Label();
      this.cbModifierI = new System.Windows.Forms.CheckBox();
      this.cbModifierG = new System.Windows.Forms.CheckBox();
      this.cbModifierX = new System.Windows.Forms.CheckBox();
      this.txtReplace = new System.Windows.Forms.TextBox();
      this.toolTip = new System.Windows.Forms.ToolTip( this.components );
      this.btnNetwork = new System.Windows.Forms.Button();
      this.txtPath = new System.Windows.Forms.TextBox();
      this.lblNumMatched = new System.Windows.Forms.Label();
      this.lblNumConflict = new System.Windows.Forms.Label();
      this.cmbMatch = new RegexRenamer.MyComboBox();
      this.tsMenu = new System.Windows.Forms.ToolStrip();
      this.mnuChangeCase = new System.Windows.Forms.ToolStripDropDownButton();
      this.itmChangeCaseNoChange = new System.Windows.Forms.ToolStripMenuItem();
      this.itmChangeCaseSep = new System.Windows.Forms.ToolStripSeparator();
      this.itmChangeCaseUppercase = new System.Windows.Forms.ToolStripMenuItem();
      this.itmChangeCaseLowercase = new System.Windows.Forms.ToolStripMenuItem();
      this.itmChangeCaseTitlecase = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuNumbering = new System.Windows.Forms.ToolStripDropDownButton();
      this.txtNumberingStart = new System.Windows.Forms.ToolStripTextBox();
      this.txtNumberingPad = new System.Windows.Forms.ToolStripTextBox();
      this.txtNumberingInc = new System.Windows.Forms.ToolStripTextBox();
      this.txtNumberingReset = new System.Windows.Forms.ToolStripTextBox();
      this.mnuMoveCopy = new System.Windows.Forms.ToolStripDropDownButton();
      this.itmOutputRenameInPlace = new System.Windows.Forms.ToolStripMenuItem();
      this.itmOutputSep = new System.Windows.Forms.ToolStripSeparator();
      this.itmOutputMoveTo = new System.Windows.Forms.ToolStripMenuItem();
      this.itmOutputCopyTo = new System.Windows.Forms.ToolStripMenuItem();
      this.itmOutputBackupTo = new System.Windows.Forms.ToolStripMenuItem();
      this.ttPreviewError = new System.Windows.Forms.ToolTip( this.components );
      this.fbdNetwork = new System.Windows.Forms.FolderBrowserDialog();
      this.fbdMoveCopy = new System.Windows.Forms.FolderBrowserDialog();
      this.cmRegexMatch = new System.Windows.Forms.ContextMenu();
      this.miRegexMatchAnchor = new System.Windows.Forms.MenuItem();
      this.miRegexMatchAnchorStart = new System.Windows.Forms.MenuItem();
      this.miRegexMatchAnchorEnd = new System.Windows.Forms.MenuItem();
      this.miRegexMatchAnchorStartEnd = new System.Windows.Forms.MenuItem();
      this.miRegexMatchAnchorBound = new System.Windows.Forms.MenuItem();
      this.miRegexMatchAnchorNonBound = new System.Windows.Forms.MenuItem();
      this.miRegexMatchGroup = new System.Windows.Forms.MenuItem();
      this.miRegexMatchGroupCapt = new System.Windows.Forms.MenuItem();
      this.miRegexMatchGroupNonCapt = new System.Windows.Forms.MenuItem();
      this.miRegexMatchGroupAlt = new System.Windows.Forms.MenuItem();
      this.miRegexMatchQuant = new System.Windows.Forms.MenuItem();
      this.miRegexMatchQuantGreedy = new System.Windows.Forms.MenuItem();
      this.miRegexMatchQuantZeroOneG = new System.Windows.Forms.MenuItem();
      this.miRegexMatchQuantOneMoreG = new System.Windows.Forms.MenuItem();
      this.miRegexMatchQuantZeroMoreG = new System.Windows.Forms.MenuItem();
      this.miRegexMatchQuantExactG = new System.Windows.Forms.MenuItem();
      this.miRegexMatchQuantAtLeastG = new System.Windows.Forms.MenuItem();
      this.miRegexMatchQuantBetweenG = new System.Windows.Forms.MenuItem();
      this.miRegexMatchQuantLazy = new System.Windows.Forms.MenuItem();
      this.miRegexMatchQuantZeroOneL = new System.Windows.Forms.MenuItem();
      this.miRegexMatchQuantOneMoreL = new System.Windows.Forms.MenuItem();
      this.miRegexMatchQuantZeroMoreL = new System.Windows.Forms.MenuItem();
      this.miRegexMatchQuantExactL = new System.Windows.Forms.MenuItem();
      this.miRegexMatchQuantAtLeastL = new System.Windows.Forms.MenuItem();
      this.miRegexMatchQuantBetweenL = new System.Windows.Forms.MenuItem();
      this.miRegexMatchClass = new System.Windows.Forms.MenuItem();
      this.miRegexMatchClassPos = new System.Windows.Forms.MenuItem();
      this.miRegexMatchClassNeg = new System.Windows.Forms.MenuItem();
      this.miRegexMatchClassLower = new System.Windows.Forms.MenuItem();
      this.miRegexMatchClassUpper = new System.Windows.Forms.MenuItem();
      this.miRegexMatchCapt = new System.Windows.Forms.MenuItem();
      this.miRegexMatchCaptCreateUnnamed = new System.Windows.Forms.MenuItem();
      this.miRegexMatchCaptMatchUnnamed = new System.Windows.Forms.MenuItem();
      this.miRegexMatchCaptCreateNamed = new System.Windows.Forms.MenuItem();
      this.miRegexMatchCaptMatchNamed = new System.Windows.Forms.MenuItem();
      this.miRegexMatchLook = new System.Windows.Forms.MenuItem();
      this.miRegexMatchLookPosAhead = new System.Windows.Forms.MenuItem();
      this.miRegexMatchLookNegAhead = new System.Windows.Forms.MenuItem();
      this.miRegexMatchLookPosBehind = new System.Windows.Forms.MenuItem();
      this.miRegexMatchLookNegBehind = new System.Windows.Forms.MenuItem();
      this.miRegexMatchSep1 = new System.Windows.Forms.MenuItem();
      this.miRegexMatchLiteral = new System.Windows.Forms.MenuItem();
      this.miRegexMatchLiteralDot = new System.Windows.Forms.MenuItem();
      this.miRegexMatchLiteralQuestion = new System.Windows.Forms.MenuItem();
      this.miRegexMatchLiteralPlus = new System.Windows.Forms.MenuItem();
      this.miRegexMatchLiteralStar = new System.Windows.Forms.MenuItem();
      this.miRegexMatchLiteralCaret = new System.Windows.Forms.MenuItem();
      this.miRegexMatchLiteralDollar = new System.Windows.Forms.MenuItem();
      this.miRegexMatchLiteralBackslash = new System.Windows.Forms.MenuItem();
      this.miRegexMatchLiteralOpenRound = new System.Windows.Forms.MenuItem();
      this.miRegexMatchLiteralCloseRound = new System.Windows.Forms.MenuItem();
      this.miRegexMatchLiteralOpenSquare = new System.Windows.Forms.MenuItem();
      this.miRegexMatchLiteralCloseSquare = new System.Windows.Forms.MenuItem();
      this.miRegexMatchLiteralOpenCurly = new System.Windows.Forms.MenuItem();
      this.miRegexMatchLiteralCloseCurly = new System.Windows.Forms.MenuItem();
      this.miRegexMatchLiteralPipe = new System.Windows.Forms.MenuItem();
      this.cmRegexReplace = new System.Windows.Forms.ContextMenu();
      this.miRegexReplaceCapture = new System.Windows.Forms.MenuItem();
      this.miRegexReplaceCaptureUnnamed = new System.Windows.Forms.MenuItem();
      this.miRegexReplaceCaptureNamed = new System.Windows.Forms.MenuItem();
      this.miRegexReplaceOrig = new System.Windows.Forms.MenuItem();
      this.miRegexReplaceOrigMatched = new System.Windows.Forms.MenuItem();
      this.miRegexReplaceOrigBefore = new System.Windows.Forms.MenuItem();
      this.miRegexReplaceOrigAfter = new System.Windows.Forms.MenuItem();
      this.miRegexReplaceOrigAll = new System.Windows.Forms.MenuItem();
      this.miRegexReplaceSpecial = new System.Windows.Forms.MenuItem();
      this.miRegexReplaceSpecialNumSeq = new System.Windows.Forms.MenuItem();
      this.miRegexReplaceSep1 = new System.Windows.Forms.MenuItem();
      this.miRegexReplaceLiteral = new System.Windows.Forms.MenuItem();
      this.miRegexReplaceLiteralDollar = new System.Windows.Forms.MenuItem();
      this.cmGlobMatch = new System.Windows.Forms.ContextMenu();
      this.miGlobMatchSingle = new System.Windows.Forms.MenuItem();
      this.miGlobMatchMultiple = new System.Windows.Forms.MenuItem();
      this.cmsBlank = new System.Windows.Forms.ContextMenuStrip( this.components );
      this.scRegex = new System.Windows.Forms.SplitContainer();
      this.scMain = new System.Windows.Forms.SplitContainer();
      this.lblPath = new System.Windows.Forms.Label();
      this.tvwFolders = new Furty.Windows.Forms.FolderTreeView();
      this.dgvFiles = new System.Windows.Forms.DataGridView();
      this.colIcon = new System.Windows.Forms.DataGridViewImageColumn();
      this.colFilename = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.colPreview = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.btnRename = new Microsoft.Samples.SplitButton();
      this.cmsRename = new System.Windows.Forms.ContextMenuStrip( this.components );
      this.itmRenameFiles = new System.Windows.Forms.ToolStripMenuItem();
      this.itmRenameFolders = new System.Windows.Forms.ToolStripMenuItem();
      this.tsOptions = new System.Windows.Forms.ToolStrip();
      this.mnuOptions = new System.Windows.Forms.ToolStripDropDownButton();
      this.itmOptionsShowHidden = new System.Windows.Forms.ToolStripMenuItem();
      this.itmOptionsPreserveExt = new System.Windows.Forms.ToolStripMenuItem();
      this.itmOptionsRealtimePreview = new System.Windows.Forms.ToolStripMenuItem();
      this.itmOptionsAllowRenSub = new System.Windows.Forms.ToolStripMenuItem();
      this.itmOptionsRenameSelectedRows = new System.Windows.Forms.ToolStripMenuItem();
      this.itmOptionsRememberWinPos = new System.Windows.Forms.ToolStripMenuItem();
      this.itmOptionsAddContextMenu = new System.Windows.Forms.ToolStripMenuItem();
      this.mnuHelp = new System.Windows.Forms.ToolStripDropDownButton();
      this.itmHelpContents = new System.Windows.Forms.ToolStripMenuItem();
      this.itmHelpRegexReference = new System.Windows.Forms.ToolStripMenuItem();
      this.itmHelpSep1 = new System.Windows.Forms.ToolStripSeparator();
      this.itmHelpEmailAuthor = new System.Windows.Forms.ToolStripMenuItem();
      this.itmHelpReportBug = new System.Windows.Forms.ToolStripMenuItem();
      this.itmHelpHomepage = new System.Windows.Forms.ToolStripMenuItem();
      this.itmHelpSep2 = new System.Windows.Forms.ToolStripSeparator();
      this.itmHelpAbout = new System.Windows.Forms.ToolStripMenuItem();
      this.progressBar = new System.Windows.Forms.ProgressBar();
      this.btnCancel = new System.Windows.Forms.Button();
      this.bgwRename = new System.ComponentModel.BackgroundWorker();
      this.gbFilter.SuspendLayout();
      this.pnlStats.SuspendLayout();
      this.tsMenu.SuspendLayout();
      this.scRegex.Panel1.SuspendLayout();
      this.scRegex.SuspendLayout();
      this.scMain.Panel1.SuspendLayout();
      this.scMain.Panel2.SuspendLayout();
      this.scMain.SuspendLayout();
      ( (System.ComponentModel.ISupportInitialize)( this.dgvFiles ) ).BeginInit();
      this.cmsRename.SuspendLayout();
      this.tsOptions.SuspendLayout();
      this.SuspendLayout();
      // 
      // miRegexMatchMatch
      // 
      this.miRegexMatchMatch.Index = 0;
      this.miRegexMatchMatch.MenuItems.AddRange( new System.Windows.Forms.MenuItem[] {
            this.miRegexMatchMatchSingleChar,
            this.miRegexMatchMatchDigit,
            this.miRegexMatchMatchAlpha,
            this.miRegexMatchMatchSpace,
            this.miRegexMatchMatchMultiChar,
            this.miRegexMatchMatchNonDigit,
            this.miRegexMatchMatchNonAlpha,
            this.miRegexMatchMatchNonSpace} );
      this.miRegexMatchMatch.Text = "Match";
      // 
      // miRegexMatchMatchSingleChar
      // 
      this.miRegexMatchMatchSingleChar.Index = 0;
      this.miRegexMatchMatchSingleChar.Text = "Single character\t.";
      this.miRegexMatchMatchSingleChar.Click += new System.EventHandler( this.InsertRegexFragment );
      // 
      // miRegexMatchMatchDigit
      // 
      this.miRegexMatchMatchDigit.Index = 1;
      this.miRegexMatchMatchDigit.Text = "Digit\t\\d";
      this.miRegexMatchMatchDigit.Click += new System.EventHandler( this.InsertRegexFragment );
      // 
      // miRegexMatchMatchAlpha
      // 
      this.miRegexMatchMatchAlpha.Index = 2;
      this.miRegexMatchMatchAlpha.Text = "Alphanumeric\t\\w";
      this.miRegexMatchMatchAlpha.Click += new System.EventHandler( this.InsertRegexFragment );
      // 
      // miRegexMatchMatchSpace
      // 
      this.miRegexMatchMatchSpace.Index = 3;
      this.miRegexMatchMatchSpace.Text = "Space\t\\s";
      this.miRegexMatchMatchSpace.Click += new System.EventHandler( this.InsertRegexFragment );
      // 
      // miRegexMatchMatchMultiChar
      // 
      this.miRegexMatchMatchMultiChar.Index = 4;
      this.miRegexMatchMatchMultiChar.Text = "Multiple characters\t.*";
      this.miRegexMatchMatchMultiChar.Click += new System.EventHandler( this.InsertRegexFragment );
      // 
      // miRegexMatchMatchNonDigit
      // 
      this.miRegexMatchMatchNonDigit.Index = 5;
      this.miRegexMatchMatchNonDigit.Text = "Non-digit\t\\D";
      this.miRegexMatchMatchNonDigit.Click += new System.EventHandler( this.InsertRegexFragment );
      // 
      // miRegexMatchMatchNonAlpha
      // 
      this.miRegexMatchMatchNonAlpha.Index = 6;
      this.miRegexMatchMatchNonAlpha.Text = "Non-alphanumeric\t\\W";
      this.miRegexMatchMatchNonAlpha.Click += new System.EventHandler( this.InsertRegexFragment );
      // 
      // miRegexMatchMatchNonSpace
      // 
      this.miRegexMatchMatchNonSpace.Index = 7;
      this.miRegexMatchMatchNonSpace.Text = "Non-space\t\\S";
      this.miRegexMatchMatchNonSpace.Click += new System.EventHandler( this.InsertRegexFragment );
      // 
      // gbFilter
      // 
      this.gbFilter.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right ) ) );
      this.gbFilter.Controls.Add( this.cbFilterExclude );
      this.gbFilter.Controls.Add( this.txtFilter );
      this.gbFilter.Controls.Add( this.rbFilterGlob );
      this.gbFilter.Controls.Add( this.rbFilterRegex );
      this.gbFilter.Location = new System.Drawing.Point( 519, 7 );
      this.gbFilter.Name = "gbFilter";
      this.gbFilter.Size = new System.Drawing.Size( 141, 52 );
      this.gbFilter.TabIndex = 2;
      this.gbFilter.TabStop = false;
      this.gbFilter.Text = "Filter";
      // 
      // cbFilterExclude
      // 
      this.cbFilterExclude.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right ) ) );
      this.cbFilterExclude.Appearance = System.Windows.Forms.Appearance.Button;
      this.cbFilterExclude.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
      this.cbFilterExclude.Image = global::RegexRenamer.Properties.Resources.x;
      this.cbFilterExclude.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
      this.cbFilterExclude.Location = new System.Drawing.Point( 65, 19 );
      this.cbFilterExclude.Margin = new System.Windows.Forms.Padding( 0 );
      this.cbFilterExclude.Name = "cbFilterExclude";
      this.cbFilterExclude.Size = new System.Drawing.Size( 12, 20 );
      this.cbFilterExclude.TabIndex = 2;
      this.toolTip.SetToolTip( this.cbFilterExclude, "Exclude (invert filter)" );
      this.cbFilterExclude.UseVisualStyleBackColor = true;
      this.cbFilterExclude.CheckedChanged += new System.EventHandler( this.cbFilterExclude_CheckedChanged );
      // 
      // txtFilter
      // 
      this.txtFilter.Anchor = System.Windows.Forms.AnchorStyles.Right;
      this.txtFilter.Font = new System.Drawing.Font( "Courier New", 8.25F );
      this.txtFilter.Location = new System.Drawing.Point( 6, 19 );
      this.txtFilter.Name = "txtFilter";
      this.txtFilter.Size = new System.Drawing.Size( 60, 20 );
      this.txtFilter.TabIndex = 1;
      this.txtFilter.Text = "*.*";
      this.toolTip.SetToolTip( this.txtFilter, "Press ENTER to apply filter" );
      this.txtFilter.TextChanged += new System.EventHandler( this.txtFilter_TextChanged );
      this.txtFilter.KeyDown += new System.Windows.Forms.KeyEventHandler( this.txtFilter_KeyDown );
      this.txtFilter.Leave += new System.EventHandler( this.txtFilter_Leave );
      this.txtFilter.MouseDown += new System.Windows.Forms.MouseEventHandler( this.txtFilter_MouseDown );
      this.txtFilter.MouseUp += new System.Windows.Forms.MouseEventHandler( this.txtFilter_MouseUp );
      // 
      // rbFilterGlob
      // 
      this.rbFilterGlob.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom )
                  | System.Windows.Forms.AnchorStyles.Right ) ) );
      this.rbFilterGlob.AutoSize = true;
      this.rbFilterGlob.Checked = true;
      this.rbFilterGlob.Location = new System.Drawing.Point( 83, 12 );
      this.rbFilterGlob.Name = "rbFilterGlob";
      this.rbFilterGlob.Size = new System.Drawing.Size( 47, 17 );
      this.rbFilterGlob.TabIndex = 3;
      this.rbFilterGlob.TabStop = true;
      this.rbFilterGlob.Text = "Glob";
      this.rbFilterGlob.UseVisualStyleBackColor = true;
      this.rbFilterGlob.Click += new System.EventHandler( this.rbFilterGlob_Click );
      // 
      // rbFilterRegex
      // 
      this.rbFilterRegex.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom )
                  | System.Windows.Forms.AnchorStyles.Right ) ) );
      this.rbFilterRegex.AutoSize = true;
      this.rbFilterRegex.Location = new System.Drawing.Point( 83, 30 );
      this.rbFilterRegex.Name = "rbFilterRegex";
      this.rbFilterRegex.Size = new System.Drawing.Size( 56, 17 );
      this.rbFilterRegex.TabIndex = 3;
      this.rbFilterRegex.Text = "Regex";
      this.rbFilterRegex.UseVisualStyleBackColor = true;
      this.rbFilterRegex.CheckedChanged += new System.EventHandler( this.rbFilterRegex_CheckedChanged );
      this.rbFilterRegex.Click += new System.EventHandler( this.rbFilterRegex_Click );
      // 
      // pnlStats
      // 
      this.pnlStats.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right ) ) );
      this.pnlStats.Controls.Add( this.lblStatsHidden );
      this.pnlStats.Controls.Add( this.lblStatsShown );
      this.pnlStats.Controls.Add( this.lblStatsFiltered );
      this.pnlStats.Controls.Add( this.lblStatsTotal );
      this.pnlStats.Location = new System.Drawing.Point( 525, 19 );
      this.pnlStats.Name = "pnlStats";
      this.pnlStats.Size = new System.Drawing.Size( 129, 36 );
      this.pnlStats.TabIndex = 6;
      this.pnlStats.Visible = false;
      // 
      // lblStatsHidden
      // 
      this.lblStatsHidden.AutoEllipsis = true;
      this.lblStatsHidden.Location = new System.Drawing.Point( 61, 20 );
      this.lblStatsHidden.Name = "lblStatsHidden";
      this.lblStatsHidden.Size = new System.Drawing.Size( 85, 12 );
      this.lblStatsHidden.TabIndex = 4;
      this.lblStatsHidden.Text = "0 hidden";
      // 
      // lblStatsShown
      // 
      this.lblStatsShown.AutoEllipsis = true;
      this.lblStatsShown.Location = new System.Drawing.Point( -2, 20 );
      this.lblStatsShown.Name = "lblStatsShown";
      this.lblStatsShown.Size = new System.Drawing.Size( 85, 12 );
      this.lblStatsShown.TabIndex = 3;
      this.lblStatsShown.Text = "0 shown";
      // 
      // lblStatsFiltered
      // 
      this.lblStatsFiltered.AutoEllipsis = true;
      this.lblStatsFiltered.Location = new System.Drawing.Point( 61, 4 );
      this.lblStatsFiltered.Name = "lblStatsFiltered";
      this.lblStatsFiltered.Size = new System.Drawing.Size( 85, 12 );
      this.lblStatsFiltered.TabIndex = 2;
      this.lblStatsFiltered.Text = "0 filtered";
      // 
      // lblStatsTotal
      // 
      this.lblStatsTotal.AutoEllipsis = true;
      this.lblStatsTotal.Location = new System.Drawing.Point( -2, 4 );
      this.lblStatsTotal.Name = "lblStatsTotal";
      this.lblStatsTotal.Size = new System.Drawing.Size( 85, 12 );
      this.lblStatsTotal.TabIndex = 1;
      this.lblStatsTotal.Text = "0 total";
      // 
      // lblStats
      // 
      this.lblStats.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right ) ) );
      this.lblStats.ForeColor = System.Drawing.SystemColors.ControlDark;
      this.lblStats.Location = new System.Drawing.Point( 618, 7 );
      this.lblStats.Margin = new System.Windows.Forms.Padding( 0 );
      this.lblStats.Name = "lblStats";
      this.lblStats.Size = new System.Drawing.Size( 31, 13 );
      this.lblStats.TabIndex = 4;
      this.lblStats.Text = "Stats";
      this.lblStats.MouseEnter += new System.EventHandler( this.lblStats_MouseEnter );
      this.lblStats.MouseLeave += new System.EventHandler( this.lblStats_MouseLeave );
      // 
      // lblMatch
      // 
      this.lblMatch.AutoSize = true;
      this.lblMatch.Location = new System.Drawing.Point( -3, 3 );
      this.lblMatch.Name = "lblMatch";
      this.lblMatch.Size = new System.Drawing.Size( 40, 13 );
      this.lblMatch.TabIndex = 0;
      this.lblMatch.Text = "Match:";
      // 
      // lblReplace
      // 
      this.lblReplace.AutoSize = true;
      this.lblReplace.Location = new System.Drawing.Point( -3, 30 );
      this.lblReplace.Name = "lblReplace";
      this.lblReplace.Size = new System.Drawing.Size( 50, 13 );
      this.lblReplace.TabIndex = 0;
      this.lblReplace.Text = "Replace:";
      // 
      // cbModifierI
      // 
      this.cbModifierI.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right ) ) );
      this.cbModifierI.AutoSize = true;
      this.cbModifierI.Location = new System.Drawing.Point( 305, -1 );
      this.cbModifierI.Name = "cbModifierI";
      this.cbModifierI.Size = new System.Drawing.Size( 33, 17 );
      this.cbModifierI.TabIndex = 3;
      this.cbModifierI.Tag = false;
      this.cbModifierI.Text = "/i";
      this.toolTip.SetToolTip( this.cbModifierI, "Ignore case" );
      this.cbModifierI.UseVisualStyleBackColor = true;
      this.cbModifierI.CheckedChanged += new System.EventHandler( this.cbModifierI_CheckedChanged );
      // 
      // cbModifierG
      // 
      this.cbModifierG.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right ) ) );
      this.cbModifierG.AutoSize = true;
      this.cbModifierG.Location = new System.Drawing.Point( 305, 16 );
      this.cbModifierG.Name = "cbModifierG";
      this.cbModifierG.Size = new System.Drawing.Size( 37, 17 );
      this.cbModifierG.TabIndex = 4;
      this.cbModifierG.Tag = false;
      this.cbModifierG.Text = "/g";
      this.toolTip.SetToolTip( this.cbModifierG, "Global (match as many times as possible)" );
      this.cbModifierG.UseVisualStyleBackColor = true;
      this.cbModifierG.CheckedChanged += new System.EventHandler( this.cbModifierG_CheckedChanged );
      // 
      // cbModifierX
      // 
      this.cbModifierX.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right ) ) );
      this.cbModifierX.AutoSize = true;
      this.cbModifierX.Location = new System.Drawing.Point( 305, 33 );
      this.cbModifierX.Name = "cbModifierX";
      this.cbModifierX.Size = new System.Drawing.Size( 36, 17 );
      this.cbModifierX.TabIndex = 5;
      this.cbModifierX.Tag = false;
      this.cbModifierX.Text = "/x";
      this.toolTip.SetToolTip( this.cbModifierX, "Extended regex (ignore unescaped spaces)" );
      this.cbModifierX.UseVisualStyleBackColor = true;
      this.cbModifierX.CheckedChanged += new System.EventHandler( this.cbModifierX_CheckedChanged );
      // 
      // txtReplace
      // 
      this.txtReplace.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right ) ) );
      this.txtReplace.Font = new System.Drawing.Font( "Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ( (byte)( 0 ) ) );
      this.txtReplace.Location = new System.Drawing.Point( 53, 27 );
      this.txtReplace.Name = "txtReplace";
      this.txtReplace.Size = new System.Drawing.Size( 247, 20 );
      this.txtReplace.TabIndex = 2;
      this.toolTip.SetToolTip( this.txtReplace, "Use $1, $2, ... to insert captured text" );
      this.txtReplace.TextChanged += new System.EventHandler( this.txtReplace_TextChanged );
      this.txtReplace.KeyDown += new System.Windows.Forms.KeyEventHandler( this.txtReplace_KeyDown );
      this.txtReplace.Leave += new System.EventHandler( this.txtReplace_Leave );
      this.txtReplace.MouseDown += new System.Windows.Forms.MouseEventHandler( this.txtReplace_MouseDown );
      this.txtReplace.MouseUp += new System.Windows.Forms.MouseEventHandler( this.txtReplace_MouseUp );
      // 
      // btnNetwork
      // 
      this.btnNetwork.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right ) ) );
      this.btnNetwork.Image = ( (System.Drawing.Image)( resources.GetObject( "btnNetwork.Image" ) ) );
      this.btnNetwork.Location = new System.Drawing.Point( 265, 326 );
      this.btnNetwork.Name = "btnNetwork";
      this.btnNetwork.Size = new System.Drawing.Size( 36, 24 );
      this.btnNetwork.TabIndex = 3;
      this.toolTip.SetToolTip( this.btnNetwork, "Browse network" );
      this.btnNetwork.UseVisualStyleBackColor = true;
      this.btnNetwork.Click += new System.EventHandler( this.btnNetwork_Click );
      // 
      // txtPath
      // 
      this.txtPath.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( ( System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left )
                  | System.Windows.Forms.AnchorStyles.Right ) ) );
      this.txtPath.Location = new System.Drawing.Point( 34, 328 );
      this.txtPath.Name = "txtPath";
      this.txtPath.Size = new System.Drawing.Size( 225, 20 );
      this.txtPath.TabIndex = 2;
      this.toolTip.SetToolTip( this.txtPath, "Press ENTER to apply path" );
      this.txtPath.Enter += new System.EventHandler( this.txtPath_Enter );
      this.txtPath.KeyDown += new System.Windows.Forms.KeyEventHandler( this.txtPath_KeyDown );
      this.txtPath.Leave += new System.EventHandler( this.txtPath_Leave );
      // 
      // lblNumMatched
      // 
      this.lblNumMatched.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right ) ) );
      this.lblNumMatched.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.lblNumMatched.ForeColor = System.Drawing.Color.Blue;
      this.lblNumMatched.Location = new System.Drawing.Point( 177, 330 );
      this.lblNumMatched.Name = "lblNumMatched";
      this.lblNumMatched.Size = new System.Drawing.Size( 35, 16 );
      this.lblNumMatched.TabIndex = 4;
      this.lblNumMatched.Text = "0";
      this.lblNumMatched.TextAlign = System.Drawing.ContentAlignment.TopCenter;
      this.toolTip.SetToolTip( this.lblNumMatched, "Number of matches" );
      // 
      // lblNumConflict
      // 
      this.lblNumConflict.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right ) ) );
      this.lblNumConflict.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.lblNumConflict.ForeColor = System.Drawing.Color.Red;
      this.lblNumConflict.Location = new System.Drawing.Point( 218, 330 );
      this.lblNumConflict.Name = "lblNumConflict";
      this.lblNumConflict.Size = new System.Drawing.Size( 35, 16 );
      this.lblNumConflict.TabIndex = 5;
      this.lblNumConflict.Text = "0";
      this.lblNumConflict.TextAlign = System.Drawing.ContentAlignment.TopCenter;
      this.toolTip.SetToolTip( this.lblNumConflict, "Number of conflicts" );
      // 
      // cmbMatch
      // 
      this.cmbMatch.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right ) ) );
      this.cmbMatch.Font = new System.Drawing.Font( "Courier New", 8.25F );
      this.cmbMatch.Location = new System.Drawing.Point( 53, 0 );
      this.cmbMatch.Name = "cmbMatch";
      this.cmbMatch.Size = new System.Drawing.Size( 247, 22 );
      this.cmbMatch.TabIndex = 1;
      this.toolTip.SetToolTip( this.cmbMatch, "Shift+rightclick for a menu of regex elements" );
      this.cmbMatch.SelectedIndexChanged += new System.EventHandler( this.cmbMatch_SelectedIndexChanged );
      this.cmbMatch.TextChanged += new System.EventHandler( this.cmbMatch_TextChanged );
      this.cmbMatch.Enter += new System.EventHandler( this.cmbMatch_Enter );
      this.cmbMatch.KeyDown += new System.Windows.Forms.KeyEventHandler( this.cmbMatch_KeyDown );
      this.cmbMatch.Leave += new System.EventHandler( this.cmbMatch_Leave );
      this.cmbMatch.MouseDown += new System.Windows.Forms.MouseEventHandler( this.cmbMatch_MouseDown );
      this.cmbMatch.MouseUp += new System.Windows.Forms.MouseEventHandler( this.cmbMatch_MouseUp );
      // 
      // tsMenu
      // 
      this.tsMenu.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right ) ) );
      this.tsMenu.AutoSize = false;
      this.tsMenu.BackColor = System.Drawing.SystemColors.ButtonFace;
      this.tsMenu.CanOverflow = false;
      this.tsMenu.Dock = System.Windows.Forms.DockStyle.None;
      this.tsMenu.Font = new System.Drawing.Font( "Tahoma", 8.25F );
      this.tsMenu.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
      this.tsMenu.Items.AddRange( new System.Windows.Forms.ToolStripItem[] {
            this.mnuChangeCase,
            this.mnuNumbering,
            this.mnuMoveCopy} );
      this.tsMenu.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
      this.tsMenu.Location = new System.Drawing.Point( 416, 6 );
      this.tsMenu.Name = "tsMenu";
      this.tsMenu.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
      this.tsMenu.Size = new System.Drawing.Size( 92, 54 );
      this.tsMenu.TabIndex = 1;
      this.tsMenu.TabStop = true;
      // 
      // mnuChangeCase
      // 
      this.mnuChangeCase.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
      this.mnuChangeCase.DropDownItems.AddRange( new System.Windows.Forms.ToolStripItem[] {
            this.itmChangeCaseNoChange,
            this.itmChangeCaseSep,
            this.itmChangeCaseUppercase,
            this.itmChangeCaseLowercase,
            this.itmChangeCaseTitlecase} );
      this.mnuChangeCase.Font = new System.Drawing.Font( "Tahoma", 8.25F );
      this.mnuChangeCase.Margin = new System.Windows.Forms.Padding( 0, 1, 0, 0 );
      this.mnuChangeCase.Name = "mnuChangeCase";
      this.mnuChangeCase.Padding = new System.Windows.Forms.Padding( 0, 0, 8, 0 );
      this.mnuChangeCase.Size = new System.Drawing.Size( 92, 17 );
      this.mnuChangeCase.Text = "Change Case";
      this.mnuChangeCase.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.mnuChangeCase.ToolTipText = "Only the matched portion of the filename will have its case changed";
      this.mnuChangeCase.MouseDown += new System.Windows.Forms.MouseEventHandler( this.mnuChangeCase_MouseDown );
      // 
      // itmChangeCaseNoChange
      // 
      this.itmChangeCaseNoChange.Checked = true;
      this.itmChangeCaseNoChange.CheckState = System.Windows.Forms.CheckState.Checked;
      this.itmChangeCaseNoChange.Font = new System.Drawing.Font( "Tahoma", 8.25F );
      this.itmChangeCaseNoChange.Name = "itmChangeCaseNoChange";
      this.itmChangeCaseNoChange.Size = new System.Drawing.Size( 125, 22 );
      this.itmChangeCaseNoChange.Text = "No change";
      this.itmChangeCaseNoChange.Click += new System.EventHandler( this.itmChangeCaseNoChange_Click );
      // 
      // itmChangeCaseSep
      // 
      this.itmChangeCaseSep.Name = "itmChangeCaseSep";
      this.itmChangeCaseSep.Size = new System.Drawing.Size( 122, 6 );
      // 
      // itmChangeCaseUppercase
      // 
      this.itmChangeCaseUppercase.Font = new System.Drawing.Font( "Tahoma", 8.25F );
      this.itmChangeCaseUppercase.Name = "itmChangeCaseUppercase";
      this.itmChangeCaseUppercase.Size = new System.Drawing.Size( 125, 22 );
      this.itmChangeCaseUppercase.Text = "Uppercase";
      this.itmChangeCaseUppercase.Click += new System.EventHandler( this.itmChangeCaseUppercase_Click );
      // 
      // itmChangeCaseLowercase
      // 
      this.itmChangeCaseLowercase.Font = new System.Drawing.Font( "Tahoma", 8.25F );
      this.itmChangeCaseLowercase.Name = "itmChangeCaseLowercase";
      this.itmChangeCaseLowercase.Size = new System.Drawing.Size( 125, 22 );
      this.itmChangeCaseLowercase.Text = "Lowercase";
      this.itmChangeCaseLowercase.Click += new System.EventHandler( this.itmChangeCaseLowercase_Click );
      // 
      // itmChangeCaseTitlecase
      // 
      this.itmChangeCaseTitlecase.Font = new System.Drawing.Font( "Tahoma", 8.25F );
      this.itmChangeCaseTitlecase.Name = "itmChangeCaseTitlecase";
      this.itmChangeCaseTitlecase.Size = new System.Drawing.Size( 125, 22 );
      this.itmChangeCaseTitlecase.Text = "Title case";
      this.itmChangeCaseTitlecase.Click += new System.EventHandler( this.itmChangeCaseTitlecase_Click );
      // 
      // mnuNumbering
      // 
      this.mnuNumbering.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
      this.mnuNumbering.DropDownItems.AddRange( new System.Windows.Forms.ToolStripItem[] {
            this.txtNumberingStart,
            this.txtNumberingPad,
            this.txtNumberingInc,
            this.txtNumberingReset} );
      this.mnuNumbering.Margin = new System.Windows.Forms.Padding( 0, 1, 0, 0 );
      this.mnuNumbering.Name = "mnuNumbering";
      this.mnuNumbering.Padding = new System.Windows.Forms.Padding( 0, 0, 21, 0 );
      this.mnuNumbering.Size = new System.Drawing.Size( 92, 17 );
      this.mnuNumbering.Tag = "mnuNumbering";
      this.mnuNumbering.Text = "Numbering";
      this.mnuNumbering.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.mnuNumbering.ToolTipText = "Enter \"$#\" in the replace field to insert a number sequence";
      this.mnuNumbering.MouseDown += new System.Windows.Forms.MouseEventHandler( this.mnuNumbering_MouseDown );
      // 
      // txtNumberingStart
      // 
      this.txtNumberingStart.MaxLength = 10;
      this.txtNumberingStart.Name = "txtNumberingStart";
      this.txtNumberingStart.Size = new System.Drawing.Size( 75, 23 );
      this.txtNumberingStart.Tag = true;
      this.txtNumberingStart.Text = "1";
      this.txtNumberingStart.TextBoxTextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      this.txtNumberingStart.ToolTipText = "Starting number (or letter)";
      this.txtNumberingStart.TextChanged += new System.EventHandler( this.txtNumberingStart_TextChanged );
      // 
      // txtNumberingPad
      // 
      this.txtNumberingPad.MaxLength = 10;
      this.txtNumberingPad.Name = "txtNumberingPad";
      this.txtNumberingPad.Size = new System.Drawing.Size( 75, 23 );
      this.txtNumberingPad.Tag = true;
      this.txtNumberingPad.Text = "000";
      this.txtNumberingPad.TextBoxTextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      this.txtNumberingPad.ToolTipText = "Eg: \"0000\" means 14 => 0014";
      this.txtNumberingPad.TextChanged += new System.EventHandler( this.txtNumberingPad_TextChanged );
      // 
      // txtNumberingInc
      // 
      this.txtNumberingInc.MaxLength = 10;
      this.txtNumberingInc.Name = "txtNumberingInc";
      this.txtNumberingInc.Size = new System.Drawing.Size( 75, 23 );
      this.txtNumberingInc.Tag = true;
      this.txtNumberingInc.Text = "1";
      this.txtNumberingInc.TextBoxTextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      this.txtNumberingInc.ToolTipText = "Increment by x each file (may be negative)";
      this.txtNumberingInc.TextChanged += new System.EventHandler( this.txtNumberingInc_TextChanged );
      // 
      // txtNumberingReset
      // 
      this.txtNumberingReset.MaxLength = 10;
      this.txtNumberingReset.Name = "txtNumberingReset";
      this.txtNumberingReset.Size = new System.Drawing.Size( 75, 23 );
      this.txtNumberingReset.Tag = true;
      this.txtNumberingReset.Text = "0";
      this.txtNumberingReset.TextBoxTextAlign = System.Windows.Forms.HorizontalAlignment.Right;
      this.txtNumberingReset.ToolTipText = "Reset to starting number every x files";
      this.txtNumberingReset.TextChanged += new System.EventHandler( this.txtNumberingReset_TextChanged );
      // 
      // mnuMoveCopy
      // 
      this.mnuMoveCopy.AutoToolTip = false;
      this.mnuMoveCopy.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
      this.mnuMoveCopy.DropDownItems.AddRange( new System.Windows.Forms.ToolStripItem[] {
            this.itmOutputRenameInPlace,
            this.itmOutputSep,
            this.itmOutputMoveTo,
            this.itmOutputCopyTo,
            this.itmOutputBackupTo} );
      this.mnuMoveCopy.Margin = new System.Windows.Forms.Padding( 0, 1, 0, 0 );
      this.mnuMoveCopy.Name = "mnuMoveCopy";
      this.mnuMoveCopy.Padding = new System.Windows.Forms.Padding( 0, 0, 17, 0 );
      this.mnuMoveCopy.Size = new System.Drawing.Size( 92, 17 );
      this.mnuMoveCopy.Text = "Move/Copy";
      this.mnuMoveCopy.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      this.mnuMoveCopy.MouseDown += new System.Windows.Forms.MouseEventHandler( this.mnuMoveCopy_MouseDown );
      // 
      // itmOutputRenameInPlace
      // 
      this.itmOutputRenameInPlace.Checked = true;
      this.itmOutputRenameInPlace.CheckState = System.Windows.Forms.CheckState.Checked;
      this.itmOutputRenameInPlace.Font = new System.Drawing.Font( "Tahoma", 8.25F );
      this.itmOutputRenameInPlace.Name = "itmOutputRenameInPlace";
      this.itmOutputRenameInPlace.Size = new System.Drawing.Size( 152, 22 );
      this.itmOutputRenameInPlace.Text = "Rename in place";
      this.itmOutputRenameInPlace.Click += new System.EventHandler( this.itmOutputRenameInPlace_Click );
      // 
      // itmOutputSep
      // 
      this.itmOutputSep.Name = "itmOutputSep";
      this.itmOutputSep.Size = new System.Drawing.Size( 149, 6 );
      // 
      // itmOutputMoveTo
      // 
      this.itmOutputMoveTo.Font = new System.Drawing.Font( "Tahoma", 8.25F );
      this.itmOutputMoveTo.Name = "itmOutputMoveTo";
      this.itmOutputMoveTo.Size = new System.Drawing.Size( 152, 22 );
      this.itmOutputMoveTo.Text = "Move to...";
      this.itmOutputMoveTo.ToolTipText = "Files that match are moved and renamed";
      this.itmOutputMoveTo.Click += new System.EventHandler( this.itmOutputMoveTo_Click );
      // 
      // itmOutputCopyTo
      // 
      this.itmOutputCopyTo.Font = new System.Drawing.Font( "Tahoma", 8.25F );
      this.itmOutputCopyTo.Name = "itmOutputCopyTo";
      this.itmOutputCopyTo.Size = new System.Drawing.Size( 152, 22 );
      this.itmOutputCopyTo.Text = "Copy to...";
      this.itmOutputCopyTo.ToolTipText = "Files that match are copied and the copies are renamed";
      this.itmOutputCopyTo.Click += new System.EventHandler( this.itmOutputCopyTo_Click );
      // 
      // itmOutputBackupTo
      // 
      this.itmOutputBackupTo.Font = new System.Drawing.Font( "Tahoma", 8.25F );
      this.itmOutputBackupTo.Name = "itmOutputBackupTo";
      this.itmOutputBackupTo.Size = new System.Drawing.Size( 152, 22 );
      this.itmOutputBackupTo.Text = "Backup to...";
      this.itmOutputBackupTo.ToolTipText = "Files that match are copied and the originals are renamed";
      this.itmOutputBackupTo.Click += new System.EventHandler( this.itmOutputBackupTo_Click );
      // 
      // ttPreviewError
      // 
      this.ttPreviewError.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Error;
      this.ttPreviewError.ToolTipTitle = "Preview validation error";
      // 
      // fbdNetwork
      // 
      this.fbdNetwork.Description = "\r\n Select a network share or subfolder:";
      this.fbdNetwork.ShowNewFolderButton = false;
      // 
      // cmRegexMatch
      // 
      this.cmRegexMatch.MenuItems.AddRange( new System.Windows.Forms.MenuItem[] {
            this.miRegexMatchMatch,
            this.miRegexMatchAnchor,
            this.miRegexMatchGroup,
            this.miRegexMatchQuant,
            this.miRegexMatchClass,
            this.miRegexMatchCapt,
            this.miRegexMatchLook,
            this.miRegexMatchSep1,
            this.miRegexMatchLiteral} );
      // 
      // miRegexMatchAnchor
      // 
      this.miRegexMatchAnchor.Index = 1;
      this.miRegexMatchAnchor.MenuItems.AddRange( new System.Windows.Forms.MenuItem[] {
            this.miRegexMatchAnchorStart,
            this.miRegexMatchAnchorEnd,
            this.miRegexMatchAnchorStartEnd,
            this.miRegexMatchAnchorBound,
            this.miRegexMatchAnchorNonBound} );
      this.miRegexMatchAnchor.Text = "Anchor";
      // 
      // miRegexMatchAnchorStart
      // 
      this.miRegexMatchAnchorStart.Index = 0;
      this.miRegexMatchAnchorStart.Text = "Start\t^";
      this.miRegexMatchAnchorStart.Click += new System.EventHandler( this.InsertRegexFragment );
      // 
      // miRegexMatchAnchorEnd
      // 
      this.miRegexMatchAnchorEnd.Index = 1;
      this.miRegexMatchAnchorEnd.Text = "End\t$";
      this.miRegexMatchAnchorEnd.Click += new System.EventHandler( this.InsertRegexFragment );
      // 
      // miRegexMatchAnchorStartEnd
      // 
      this.miRegexMatchAnchorStartEnd.Index = 2;
      this.miRegexMatchAnchorStartEnd.Text = "Start and End\t^(...)$";
      this.miRegexMatchAnchorStartEnd.Click += new System.EventHandler( this.InsertRegexFragment );
      // 
      // miRegexMatchAnchorBound
      // 
      this.miRegexMatchAnchorBound.Index = 3;
      this.miRegexMatchAnchorBound.Text = "Word boundary\t\\b";
      this.miRegexMatchAnchorBound.Click += new System.EventHandler( this.InsertRegexFragment );
      // 
      // miRegexMatchAnchorNonBound
      // 
      this.miRegexMatchAnchorNonBound.Index = 4;
      this.miRegexMatchAnchorNonBound.Text = "Non-word boundary\t\\B";
      this.miRegexMatchAnchorNonBound.Click += new System.EventHandler( this.InsertRegexFragment );
      // 
      // miRegexMatchGroup
      // 
      this.miRegexMatchGroup.Index = 2;
      this.miRegexMatchGroup.MenuItems.AddRange( new System.Windows.Forms.MenuItem[] {
            this.miRegexMatchGroupCapt,
            this.miRegexMatchGroupNonCapt,
            this.miRegexMatchGroupAlt} );
      this.miRegexMatchGroup.Text = "Group";
      // 
      // miRegexMatchGroupCapt
      // 
      this.miRegexMatchGroupCapt.Index = 0;
      this.miRegexMatchGroupCapt.Text = "With capture\t(...)";
      this.miRegexMatchGroupCapt.Click += new System.EventHandler( this.InsertRegexFragment );
      // 
      // miRegexMatchGroupNonCapt
      // 
      this.miRegexMatchGroupNonCapt.Index = 1;
      this.miRegexMatchGroupNonCapt.Text = "Without capture\t(?:...)";
      this.miRegexMatchGroupNonCapt.Click += new System.EventHandler( this.InsertRegexFragment );
      // 
      // miRegexMatchGroupAlt
      // 
      this.miRegexMatchGroupAlt.Index = 2;
      this.miRegexMatchGroupAlt.Text = "Alternative\t(...|...)";
      this.miRegexMatchGroupAlt.Click += new System.EventHandler( this.InsertRegexFragment );
      // 
      // miRegexMatchQuant
      // 
      this.miRegexMatchQuant.Index = 3;
      this.miRegexMatchQuant.MenuItems.AddRange( new System.Windows.Forms.MenuItem[] {
            this.miRegexMatchQuantGreedy,
            this.miRegexMatchQuantZeroOneG,
            this.miRegexMatchQuantOneMoreG,
            this.miRegexMatchQuantZeroMoreG,
            this.miRegexMatchQuantExactG,
            this.miRegexMatchQuantAtLeastG,
            this.miRegexMatchQuantBetweenG,
            this.miRegexMatchQuantLazy,
            this.miRegexMatchQuantZeroOneL,
            this.miRegexMatchQuantOneMoreL,
            this.miRegexMatchQuantZeroMoreL,
            this.miRegexMatchQuantExactL,
            this.miRegexMatchQuantAtLeastL,
            this.miRegexMatchQuantBetweenL} );
      this.miRegexMatchQuant.Text = "Quantifiers";
      // 
      // miRegexMatchQuantGreedy
      // 
      this.miRegexMatchQuantGreedy.Enabled = false;
      this.miRegexMatchQuantGreedy.Index = 0;
      this.miRegexMatchQuantGreedy.Text = "Match as much as possible";
      // 
      // miRegexMatchQuantZeroOneG
      // 
      this.miRegexMatchQuantZeroOneG.Index = 1;
      this.miRegexMatchQuantZeroOneG.Text = "Zero or one times\t?";
      this.miRegexMatchQuantZeroOneG.Click += new System.EventHandler( this.InsertRegexFragment );
      // 
      // miRegexMatchQuantOneMoreG
      // 
      this.miRegexMatchQuantOneMoreG.Index = 2;
      this.miRegexMatchQuantOneMoreG.Text = "One or more times\t+";
      this.miRegexMatchQuantOneMoreG.Click += new System.EventHandler( this.InsertRegexFragment );
      // 
      // miRegexMatchQuantZeroMoreG
      // 
      this.miRegexMatchQuantZeroMoreG.Index = 3;
      this.miRegexMatchQuantZeroMoreG.Text = "Zero or more times\t*";
      this.miRegexMatchQuantZeroMoreG.Click += new System.EventHandler( this.InsertRegexFragment );
      // 
      // miRegexMatchQuantExactG
      // 
      this.miRegexMatchQuantExactG.Index = 4;
      this.miRegexMatchQuantExactG.Text = "Exactly n times\t{n}";
      this.miRegexMatchQuantExactG.Click += new System.EventHandler( this.InsertRegexFragment );
      // 
      // miRegexMatchQuantAtLeastG
      // 
      this.miRegexMatchQuantAtLeastG.Index = 5;
      this.miRegexMatchQuantAtLeastG.Text = "At least n times\t{n,}";
      this.miRegexMatchQuantAtLeastG.Click += new System.EventHandler( this.InsertRegexFragment );
      // 
      // miRegexMatchQuantBetweenG
      // 
      this.miRegexMatchQuantBetweenG.Index = 6;
      this.miRegexMatchQuantBetweenG.Text = "Between n to m times\t{n,m}";
      this.miRegexMatchQuantBetweenG.Click += new System.EventHandler( this.InsertRegexFragment );
      // 
      // miRegexMatchQuantLazy
      // 
      this.miRegexMatchQuantLazy.Enabled = false;
      this.miRegexMatchQuantLazy.Index = 7;
      this.miRegexMatchQuantLazy.Text = "Match as little as possible";
      // 
      // miRegexMatchQuantZeroOneL
      // 
      this.miRegexMatchQuantZeroOneL.Index = 8;
      this.miRegexMatchQuantZeroOneL.Text = "Zero or one times\t??";
      this.miRegexMatchQuantZeroOneL.Click += new System.EventHandler( this.InsertRegexFragment );
      // 
      // miRegexMatchQuantOneMoreL
      // 
      this.miRegexMatchQuantOneMoreL.Index = 9;
      this.miRegexMatchQuantOneMoreL.Text = "One or more times\t+?";
      this.miRegexMatchQuantOneMoreL.Click += new System.EventHandler( this.InsertRegexFragment );
      // 
      // miRegexMatchQuantZeroMoreL
      // 
      this.miRegexMatchQuantZeroMoreL.Index = 10;
      this.miRegexMatchQuantZeroMoreL.Text = "Zero or more times\t*?";
      this.miRegexMatchQuantZeroMoreL.Click += new System.EventHandler( this.InsertRegexFragment );
      // 
      // miRegexMatchQuantExactL
      // 
      this.miRegexMatchQuantExactL.Index = 11;
      this.miRegexMatchQuantExactL.Text = "Exactly n times\t{n}?";
      this.miRegexMatchQuantExactL.Click += new System.EventHandler( this.InsertRegexFragment );
      // 
      // miRegexMatchQuantAtLeastL
      // 
      this.miRegexMatchQuantAtLeastL.Index = 12;
      this.miRegexMatchQuantAtLeastL.Text = "At least n times\t{n,}?";
      this.miRegexMatchQuantAtLeastL.Click += new System.EventHandler( this.InsertRegexFragment );
      // 
      // miRegexMatchQuantBetweenL
      // 
      this.miRegexMatchQuantBetweenL.Index = 13;
      this.miRegexMatchQuantBetweenL.Text = "Between n to m times\t{n,m}?";
      this.miRegexMatchQuantBetweenL.Click += new System.EventHandler( this.InsertRegexFragment );
      // 
      // miRegexMatchClass
      // 
      this.miRegexMatchClass.Index = 4;
      this.miRegexMatchClass.MenuItems.AddRange( new System.Windows.Forms.MenuItem[] {
            this.miRegexMatchClassPos,
            this.miRegexMatchClassNeg,
            this.miRegexMatchClassLower,
            this.miRegexMatchClassUpper} );
      this.miRegexMatchClass.Text = "Character class";
      // 
      // miRegexMatchClassPos
      // 
      this.miRegexMatchClassPos.Index = 0;
      this.miRegexMatchClassPos.Text = "Positive class\t[...]";
      this.miRegexMatchClassPos.Click += new System.EventHandler( this.InsertRegexFragment );
      // 
      // miRegexMatchClassNeg
      // 
      this.miRegexMatchClassNeg.Index = 1;
      this.miRegexMatchClassNeg.Text = "Negative class\t[^...]";
      this.miRegexMatchClassNeg.Click += new System.EventHandler( this.InsertRegexFragment );
      // 
      // miRegexMatchClassLower
      // 
      this.miRegexMatchClassLower.Index = 2;
      this.miRegexMatchClassLower.Text = "Lowercase\t[a-z]";
      this.miRegexMatchClassLower.Click += new System.EventHandler( this.InsertRegexFragment );
      // 
      // miRegexMatchClassUpper
      // 
      this.miRegexMatchClassUpper.Index = 3;
      this.miRegexMatchClassUpper.Text = "Uppercase\t[A-Z]";
      this.miRegexMatchClassUpper.Click += new System.EventHandler( this.InsertRegexFragment );
      // 
      // miRegexMatchCapt
      // 
      this.miRegexMatchCapt.Index = 5;
      this.miRegexMatchCapt.MenuItems.AddRange( new System.Windows.Forms.MenuItem[] {
            this.miRegexMatchCaptCreateUnnamed,
            this.miRegexMatchCaptMatchUnnamed,
            this.miRegexMatchCaptCreateNamed,
            this.miRegexMatchCaptMatchNamed} );
      this.miRegexMatchCapt.Text = "Captures";
      // 
      // miRegexMatchCaptCreateUnnamed
      // 
      this.miRegexMatchCaptCreateUnnamed.Index = 0;
      this.miRegexMatchCaptCreateUnnamed.Text = "Create unnamed capture\t(...)";
      this.miRegexMatchCaptCreateUnnamed.Click += new System.EventHandler( this.InsertRegexFragment );
      // 
      // miRegexMatchCaptMatchUnnamed
      // 
      this.miRegexMatchCaptMatchUnnamed.Index = 1;
      this.miRegexMatchCaptMatchUnnamed.Text = "Match unnamed capture\t\\n";
      this.miRegexMatchCaptMatchUnnamed.Click += new System.EventHandler( this.InsertRegexFragment );
      // 
      // miRegexMatchCaptCreateNamed
      // 
      this.miRegexMatchCaptCreateNamed.Index = 2;
      this.miRegexMatchCaptCreateNamed.Text = "Create named capture\t(?<name>...)";
      this.miRegexMatchCaptCreateNamed.Click += new System.EventHandler( this.InsertRegexFragment );
      // 
      // miRegexMatchCaptMatchNamed
      // 
      this.miRegexMatchCaptMatchNamed.Index = 3;
      this.miRegexMatchCaptMatchNamed.Text = "Match named capture\t\\<name>";
      this.miRegexMatchCaptMatchNamed.Click += new System.EventHandler( this.InsertRegexFragment );
      // 
      // miRegexMatchLook
      // 
      this.miRegexMatchLook.Index = 6;
      this.miRegexMatchLook.MenuItems.AddRange( new System.Windows.Forms.MenuItem[] {
            this.miRegexMatchLookPosAhead,
            this.miRegexMatchLookNegAhead,
            this.miRegexMatchLookPosBehind,
            this.miRegexMatchLookNegBehind} );
      this.miRegexMatchLook.Text = "Lookaround";
      // 
      // miRegexMatchLookPosAhead
      // 
      this.miRegexMatchLookPosAhead.Index = 0;
      this.miRegexMatchLookPosAhead.Text = "Positive lookahead\t(?=...)";
      this.miRegexMatchLookPosAhead.Click += new System.EventHandler( this.InsertRegexFragment );
      // 
      // miRegexMatchLookNegAhead
      // 
      this.miRegexMatchLookNegAhead.Index = 1;
      this.miRegexMatchLookNegAhead.Text = "Negative lookahead\t(?!...)";
      this.miRegexMatchLookNegAhead.Click += new System.EventHandler( this.InsertRegexFragment );
      // 
      // miRegexMatchLookPosBehind
      // 
      this.miRegexMatchLookPosBehind.Index = 2;
      this.miRegexMatchLookPosBehind.Text = "Positive lookbehind\t(?<=...)";
      this.miRegexMatchLookPosBehind.Click += new System.EventHandler( this.InsertRegexFragment );
      // 
      // miRegexMatchLookNegBehind
      // 
      this.miRegexMatchLookNegBehind.Index = 3;
      this.miRegexMatchLookNegBehind.Text = "Negative lookbehind\t(?<!...)";
      this.miRegexMatchLookNegBehind.Click += new System.EventHandler( this.InsertRegexFragment );
      // 
      // miRegexMatchSep1
      // 
      this.miRegexMatchSep1.Index = 7;
      this.miRegexMatchSep1.Text = "-";
      // 
      // miRegexMatchLiteral
      // 
      this.miRegexMatchLiteral.Index = 8;
      this.miRegexMatchLiteral.MenuItems.AddRange( new System.Windows.Forms.MenuItem[] {
            this.miRegexMatchLiteralDot,
            this.miRegexMatchLiteralQuestion,
            this.miRegexMatchLiteralPlus,
            this.miRegexMatchLiteralStar,
            this.miRegexMatchLiteralCaret,
            this.miRegexMatchLiteralDollar,
            this.miRegexMatchLiteralBackslash,
            this.miRegexMatchLiteralOpenRound,
            this.miRegexMatchLiteralCloseRound,
            this.miRegexMatchLiteralOpenSquare,
            this.miRegexMatchLiteralCloseSquare,
            this.miRegexMatchLiteralOpenCurly,
            this.miRegexMatchLiteralCloseCurly,
            this.miRegexMatchLiteralPipe} );
      this.miRegexMatchLiteral.Text = "Literals";
      // 
      // miRegexMatchLiteralDot
      // 
      this.miRegexMatchLiteralDot.Index = 0;
      this.miRegexMatchLiteralDot.Text = "Dot\t\\.";
      this.miRegexMatchLiteralDot.Click += new System.EventHandler( this.InsertRegexFragment );
      // 
      // miRegexMatchLiteralQuestion
      // 
      this.miRegexMatchLiteralQuestion.Index = 1;
      this.miRegexMatchLiteralQuestion.Text = "Question mark\t\\?";
      this.miRegexMatchLiteralQuestion.Click += new System.EventHandler( this.InsertRegexFragment );
      // 
      // miRegexMatchLiteralPlus
      // 
      this.miRegexMatchLiteralPlus.Index = 2;
      this.miRegexMatchLiteralPlus.Text = "Plus sign\t\\+";
      this.miRegexMatchLiteralPlus.Click += new System.EventHandler( this.InsertRegexFragment );
      // 
      // miRegexMatchLiteralStar
      // 
      this.miRegexMatchLiteralStar.Index = 3;
      this.miRegexMatchLiteralStar.Text = "Star\t\\*";
      this.miRegexMatchLiteralStar.Click += new System.EventHandler( this.InsertRegexFragment );
      // 
      // miRegexMatchLiteralCaret
      // 
      this.miRegexMatchLiteralCaret.Index = 4;
      this.miRegexMatchLiteralCaret.Text = "Caret\t\\^";
      this.miRegexMatchLiteralCaret.Click += new System.EventHandler( this.InsertRegexFragment );
      // 
      // miRegexMatchLiteralDollar
      // 
      this.miRegexMatchLiteralDollar.Index = 5;
      this.miRegexMatchLiteralDollar.Text = "Dollar sign\t\\$";
      this.miRegexMatchLiteralDollar.Click += new System.EventHandler( this.InsertRegexFragment );
      // 
      // miRegexMatchLiteralBackslash
      // 
      this.miRegexMatchLiteralBackslash.Index = 6;
      this.miRegexMatchLiteralBackslash.Text = "Backslash\t\\\\";
      this.miRegexMatchLiteralBackslash.Click += new System.EventHandler( this.InsertRegexFragment );
      // 
      // miRegexMatchLiteralOpenRound
      // 
      this.miRegexMatchLiteralOpenRound.Index = 7;
      this.miRegexMatchLiteralOpenRound.Text = "Open round bracket\t\\(";
      this.miRegexMatchLiteralOpenRound.Click += new System.EventHandler( this.InsertRegexFragment );
      // 
      // miRegexMatchLiteralCloseRound
      // 
      this.miRegexMatchLiteralCloseRound.Index = 8;
      this.miRegexMatchLiteralCloseRound.Text = "Close round bracket\t\\)";
      this.miRegexMatchLiteralCloseRound.Click += new System.EventHandler( this.InsertRegexFragment );
      // 
      // miRegexMatchLiteralOpenSquare
      // 
      this.miRegexMatchLiteralOpenSquare.Index = 9;
      this.miRegexMatchLiteralOpenSquare.Text = "Open square bracket\t\\[";
      this.miRegexMatchLiteralOpenSquare.Click += new System.EventHandler( this.InsertRegexFragment );
      // 
      // miRegexMatchLiteralCloseSquare
      // 
      this.miRegexMatchLiteralCloseSquare.Index = 10;
      this.miRegexMatchLiteralCloseSquare.Text = "Close square bracket\t\\]";
      this.miRegexMatchLiteralCloseSquare.Click += new System.EventHandler( this.InsertRegexFragment );
      // 
      // miRegexMatchLiteralOpenCurly
      // 
      this.miRegexMatchLiteralOpenCurly.Index = 11;
      this.miRegexMatchLiteralOpenCurly.Text = "Open curly bracket\t\\{";
      this.miRegexMatchLiteralOpenCurly.Click += new System.EventHandler( this.InsertRegexFragment );
      // 
      // miRegexMatchLiteralCloseCurly
      // 
      this.miRegexMatchLiteralCloseCurly.Index = 12;
      this.miRegexMatchLiteralCloseCurly.Text = "Close curly bracket\t\\}";
      this.miRegexMatchLiteralCloseCurly.Click += new System.EventHandler( this.InsertRegexFragment );
      // 
      // miRegexMatchLiteralPipe
      // 
      this.miRegexMatchLiteralPipe.Index = 13;
      this.miRegexMatchLiteralPipe.Text = "Pipe\t\\|";
      this.miRegexMatchLiteralPipe.Click += new System.EventHandler( this.InsertRegexFragment );
      // 
      // cmRegexReplace
      // 
      this.cmRegexReplace.MenuItems.AddRange( new System.Windows.Forms.MenuItem[] {
            this.miRegexReplaceCapture,
            this.miRegexReplaceOrig,
            this.miRegexReplaceSpecial,
            this.miRegexReplaceSep1,
            this.miRegexReplaceLiteral} );
      // 
      // miRegexReplaceCapture
      // 
      this.miRegexReplaceCapture.Index = 0;
      this.miRegexReplaceCapture.MenuItems.AddRange( new System.Windows.Forms.MenuItem[] {
            this.miRegexReplaceCaptureUnnamed,
            this.miRegexReplaceCaptureNamed} );
      this.miRegexReplaceCapture.Text = "Capture";
      // 
      // miRegexReplaceCaptureUnnamed
      // 
      this.miRegexReplaceCaptureUnnamed.Index = 0;
      this.miRegexReplaceCaptureUnnamed.Text = "Unnamed\t$n";
      this.miRegexReplaceCaptureUnnamed.Click += new System.EventHandler( this.InsertRegexFragment );
      // 
      // miRegexReplaceCaptureNamed
      // 
      this.miRegexReplaceCaptureNamed.Index = 1;
      this.miRegexReplaceCaptureNamed.Text = "Named\t${name}";
      this.miRegexReplaceCaptureNamed.Click += new System.EventHandler( this.InsertRegexFragment );
      // 
      // miRegexReplaceOrig
      // 
      this.miRegexReplaceOrig.Index = 1;
      this.miRegexReplaceOrig.MenuItems.AddRange( new System.Windows.Forms.MenuItem[] {
            this.miRegexReplaceOrigMatched,
            this.miRegexReplaceOrigBefore,
            this.miRegexReplaceOrigAfter,
            this.miRegexReplaceOrigAll} );
      this.miRegexReplaceOrig.Text = "Original text";
      // 
      // miRegexReplaceOrigMatched
      // 
      this.miRegexReplaceOrigMatched.Index = 0;
      this.miRegexReplaceOrigMatched.Text = "Matched text\t$0";
      this.miRegexReplaceOrigMatched.Click += new System.EventHandler( this.InsertRegexFragment );
      // 
      // miRegexReplaceOrigBefore
      // 
      this.miRegexReplaceOrigBefore.Index = 1;
      this.miRegexReplaceOrigBefore.Text = "Text before match\t$`";
      this.miRegexReplaceOrigBefore.Click += new System.EventHandler( this.InsertRegexFragment );
      // 
      // miRegexReplaceOrigAfter
      // 
      this.miRegexReplaceOrigAfter.Index = 2;
      this.miRegexReplaceOrigAfter.Text = "Text after match\t$\'";
      this.miRegexReplaceOrigAfter.Click += new System.EventHandler( this.InsertRegexFragment );
      // 
      // miRegexReplaceOrigAll
      // 
      this.miRegexReplaceOrigAll.Index = 3;
      this.miRegexReplaceOrigAll.Text = "Original filename\t$_";
      this.miRegexReplaceOrigAll.Click += new System.EventHandler( this.InsertRegexFragment );
      // 
      // miRegexReplaceSpecial
      // 
      this.miRegexReplaceSpecial.Index = 2;
      this.miRegexReplaceSpecial.MenuItems.AddRange( new System.Windows.Forms.MenuItem[] {
            this.miRegexReplaceSpecialNumSeq} );
      this.miRegexReplaceSpecial.Text = "Special";
      // 
      // miRegexReplaceSpecialNumSeq
      // 
      this.miRegexReplaceSpecialNumSeq.Index = 0;
      this.miRegexReplaceSpecialNumSeq.Text = "Number sequence\t$#";
      this.miRegexReplaceSpecialNumSeq.Click += new System.EventHandler( this.InsertRegexFragment );
      // 
      // miRegexReplaceSep1
      // 
      this.miRegexReplaceSep1.Index = 3;
      this.miRegexReplaceSep1.Text = "-";
      // 
      // miRegexReplaceLiteral
      // 
      this.miRegexReplaceLiteral.Index = 4;
      this.miRegexReplaceLiteral.MenuItems.AddRange( new System.Windows.Forms.MenuItem[] {
            this.miRegexReplaceLiteralDollar} );
      this.miRegexReplaceLiteral.Text = "Literals";
      // 
      // miRegexReplaceLiteralDollar
      // 
      this.miRegexReplaceLiteralDollar.Index = 0;
      this.miRegexReplaceLiteralDollar.Text = "Dollar sign\t$$";
      this.miRegexReplaceLiteralDollar.Click += new System.EventHandler( this.InsertRegexFragment );
      // 
      // cmGlobMatch
      // 
      this.cmGlobMatch.MenuItems.AddRange( new System.Windows.Forms.MenuItem[] {
            this.miGlobMatchSingle,
            this.miGlobMatchMultiple} );
      // 
      // miGlobMatchSingle
      // 
      this.miGlobMatchSingle.Index = 0;
      this.miGlobMatchSingle.Text = "Single character\t?";
      this.miGlobMatchSingle.Click += new System.EventHandler( this.InsertRegexFragment );
      // 
      // miGlobMatchMultiple
      // 
      this.miGlobMatchMultiple.Index = 1;
      this.miGlobMatchMultiple.Text = "Multiple characters\t*";
      this.miGlobMatchMultiple.Click += new System.EventHandler( this.InsertRegexFragment );
      // 
      // cmsBlank
      // 
      this.cmsBlank.Name = "cmsBlank";
      this.cmsBlank.Size = new System.Drawing.Size( 61, 4 );
      // 
      // scRegex
      // 
      this.scRegex.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left )
                  | System.Windows.Forms.AnchorStyles.Right ) ) );
      this.scRegex.Location = new System.Drawing.Point( 12, 12 );
      this.scRegex.Name = "scRegex";
      // 
      // scRegex.Panel1
      // 
      this.scRegex.Panel1.Controls.Add( this.lblMatch );
      this.scRegex.Panel1.Controls.Add( this.cmbMatch );
      this.scRegex.Panel1.Controls.Add( this.txtReplace );
      this.scRegex.Panel1.Controls.Add( this.lblReplace );
      this.scRegex.Panel1.Controls.Add( this.cbModifierX );
      this.scRegex.Panel1.Controls.Add( this.cbModifierI );
      this.scRegex.Panel1.Controls.Add( this.cbModifierG );
      this.scRegex.Panel1.Paint += new System.Windows.Forms.PaintEventHandler( this.scRegex_Panel1_Paint );
      this.scRegex.Panel1MinSize = 248;
      // 
      // scRegex.Panel2
      // 
      this.scRegex.Panel2.Paint += new System.Windows.Forms.PaintEventHandler( this.scRegex_Panel2_Paint );
      this.scRegex.Panel2MinSize = 8;
      this.scRegex.Size = new System.Drawing.Size( 401, 47 );
      this.scRegex.SplitterDistance = 348;
      this.scRegex.SplitterWidth = 3;
      this.scRegex.TabIndex = 0;
      this.scRegex.TabStop = false;
      this.scRegex.Paint += new System.Windows.Forms.PaintEventHandler( this.scRegex_Paint );
      this.scRegex.DoubleClick += new System.EventHandler( this.scRegex_DoubleClick );
      this.scRegex.MouseUp += new System.Windows.Forms.MouseEventHandler( this.scRegex_MouseUp );
      // 
      // scMain
      // 
      this.scMain.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom )
                  | System.Windows.Forms.AnchorStyles.Left )
                  | System.Windows.Forms.AnchorStyles.Right ) ) );
      this.scMain.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
      this.scMain.Location = new System.Drawing.Point( 12, 65 );
      this.scMain.Name = "scMain";
      // 
      // scMain.Panel1
      // 
      this.scMain.Panel1.Controls.Add( this.btnNetwork );
      this.scMain.Panel1.Controls.Add( this.lblPath );
      this.scMain.Panel1.Controls.Add( this.txtPath );
      this.scMain.Panel1.Controls.Add( this.tvwFolders );
      this.scMain.Panel1MinSize = 200;
      // 
      // scMain.Panel2
      // 
      this.scMain.Panel2.Controls.Add( this.dgvFiles );
      this.scMain.Panel2.Controls.Add( this.lblNumConflict );
      this.scMain.Panel2.Controls.Add( this.lblNumMatched );
      this.scMain.Panel2.Controls.Add( this.btnRename );
      this.scMain.Panel2.Controls.Add( this.tsOptions );
      this.scMain.Panel2.Controls.Add( this.progressBar );
      this.scMain.Panel2.Controls.Add( this.btnCancel );
      this.scMain.Size = new System.Drawing.Size( 648, 349 );
      this.scMain.SplitterDistance = 300;
      this.scMain.SplitterWidth = 5;
      this.scMain.TabIndex = 3;
      this.scMain.TabStop = false;
      this.scMain.DoubleClick += new System.EventHandler( this.scMain_DoubleClick );
      this.scMain.MouseUp += new System.Windows.Forms.MouseEventHandler( this.scMain_MouseUp );
      this.scMain.Resize += new System.EventHandler( this.scMain_Resize );
      // 
      // lblPath
      // 
      this.lblPath.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left ) ) );
      this.lblPath.AutoSize = true;
      this.lblPath.Location = new System.Drawing.Point( -4, 331 );
      this.lblPath.Name = "lblPath";
      this.lblPath.Size = new System.Drawing.Size( 32, 13 );
      this.lblPath.TabIndex = 0;
      this.lblPath.Text = "Path:";
      // 
      // tvwFolders
      // 
      this.tvwFolders.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom )
                  | System.Windows.Forms.AnchorStyles.Left )
                  | System.Windows.Forms.AnchorStyles.Right ) ) );
      this.tvwFolders.HideSelection = false;
      this.tvwFolders.Location = new System.Drawing.Point( 0, 0 );
      this.tvwFolders.Name = "tvwFolders";
      this.tvwFolders.Size = new System.Drawing.Size( 300, 320 );
      this.tvwFolders.TabIndex = 1;
      this.tvwFolders.AfterSelect += new System.Windows.Forms.TreeViewEventHandler( this.tvwFolders_AfterSelect );
      this.tvwFolders.KeyUp += new System.Windows.Forms.KeyEventHandler( this.tvwFolders_KeyUp );
      // 
      // dgvFiles
      // 
      this.dgvFiles.AllowUserToAddRows = false;
      this.dgvFiles.AllowUserToDeleteRows = false;
      this.dgvFiles.AllowUserToResizeColumns = false;
      this.dgvFiles.AllowUserToResizeRows = false;
      this.dgvFiles.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom )
                  | System.Windows.Forms.AnchorStyles.Left )
                  | System.Windows.Forms.AnchorStyles.Right ) ) );
      this.dgvFiles.BackgroundColor = System.Drawing.SystemColors.Window;
      this.dgvFiles.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
      this.dgvFiles.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
      this.dgvFiles.Columns.AddRange( new System.Windows.Forms.DataGridViewColumn[] {
            this.colIcon,
            this.colFilename,
            this.colPreview} );
      this.dgvFiles.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnF2;
      this.dgvFiles.GridColor = System.Drawing.SystemColors.Control;
      this.dgvFiles.Location = new System.Drawing.Point( 0, 0 );
      this.dgvFiles.Name = "dgvFiles";
      this.dgvFiles.RowHeadersVisible = false;
      this.dgvFiles.RowTemplate.Height = 17;
      this.dgvFiles.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
      this.dgvFiles.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
      this.dgvFiles.ShowCellToolTips = false;
      this.dgvFiles.Size = new System.Drawing.Size( 343, 320 );
      this.dgvFiles.StandardTab = true;
      this.dgvFiles.TabIndex = 6;
      this.dgvFiles.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler( this.dgvFiles_CellBeginEdit );
      this.dgvFiles.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler( this.dgvFiles_CellDoubleClick );
      this.dgvFiles.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler( this.dgvFiles_CellEndEdit );
      this.dgvFiles.CellMouseEnter += new System.Windows.Forms.DataGridViewCellEventHandler( this.dgvFiles_CellMouseEnter );
      this.dgvFiles.CellMouseLeave += new System.Windows.Forms.DataGridViewCellEventHandler( this.dgvFiles_CellMouseLeave );
      this.dgvFiles.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler( this.dgvFiles_CellValidating );
      this.dgvFiles.SelectionChanged += new System.EventHandler( this.dgvFiles_SelectionChanged );
      this.dgvFiles.KeyUp += new System.Windows.Forms.KeyEventHandler( this.dgvFiles_KeyUp );
      this.dgvFiles.Leave += new System.EventHandler( this.dgvFiles_Leave );
      // 
      // colIcon
      // 
      this.colIcon.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
      this.colIcon.HeaderText = "";
      this.colIcon.Name = "colIcon";
      this.colIcon.ReadOnly = true;
      this.colIcon.Resizable = System.Windows.Forms.DataGridViewTriState.False;
      this.colIcon.Width = 20;
      // 
      // colFilename
      // 
      this.colFilename.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
      this.colFilename.HeaderText = "Filename";
      this.colFilename.Name = "colFilename";
      this.colFilename.Resizable = System.Windows.Forms.DataGridViewTriState.False;
      // 
      // colPreview
      // 
      this.colPreview.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
      this.colPreview.HeaderText = "Preview";
      this.colPreview.Name = "colPreview";
      this.colPreview.ReadOnly = true;
      this.colPreview.Resizable = System.Windows.Forms.DataGridViewTriState.False;
      // 
      // btnRename
      // 
      this.btnRename.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right ) ) );
      this.btnRename.AutoSize = true;
      this.btnRename.ContextMenuStrip = this.cmsRename;
      this.btnRename.Location = new System.Drawing.Point( 259, 326 );
      this.btnRename.Name = "btnRename";
      this.btnRename.Size = new System.Drawing.Size( 85, 24 );
      this.btnRename.State = System.Windows.Forms.VisualStyles.PushButtonState.Normal;
      this.btnRename.TabIndex = 3;
      this.btnRename.Text = "&Rename";
      this.btnRename.UseVisualStyleBackColor = true;
      this.btnRename.Click += new System.EventHandler( this.btnRename_Click );
      // 
      // cmsRename
      // 
      this.cmsRename.AutoSize = false;
      this.cmsRename.Items.AddRange( new System.Windows.Forms.ToolStripItem[] {
            this.itmRenameFiles,
            this.itmRenameFolders} );
      this.cmsRename.Name = "cmsRename";
      this.cmsRename.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
      this.cmsRename.Size = new System.Drawing.Size( 130, 48 );
      this.cmsRename.Opening += new System.ComponentModel.CancelEventHandler( this.cmsRename_Opening );
      // 
      // itmRenameFiles
      // 
      this.itmRenameFiles.AutoSize = false;
      this.itmRenameFiles.Checked = true;
      this.itmRenameFiles.CheckState = System.Windows.Forms.CheckState.Checked;
      this.itmRenameFiles.Name = "itmRenameFiles";
      this.itmRenameFiles.Size = new System.Drawing.Size( 129, 22 );
      this.itmRenameFiles.Text = "Rename files";
      this.itmRenameFiles.Click += new System.EventHandler( this.itmRenameFiles_Click );
      // 
      // itmRenameFolders
      // 
      this.itmRenameFolders.AutoSize = false;
      this.itmRenameFolders.Name = "itmRenameFolders";
      this.itmRenameFolders.Size = new System.Drawing.Size( 129, 22 );
      this.itmRenameFolders.Text = "Rename folders";
      this.itmRenameFolders.Click += new System.EventHandler( this.itmRenameFolders_Click );
      // 
      // tsOptions
      // 
      this.tsOptions.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left ) ) );
      this.tsOptions.BackColor = System.Drawing.SystemColors.ButtonFace;
      this.tsOptions.CanOverflow = false;
      this.tsOptions.Dock = System.Windows.Forms.DockStyle.None;
      this.tsOptions.Font = new System.Drawing.Font( "Tahoma", 8.25F );
      this.tsOptions.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
      this.tsOptions.Items.AddRange( new System.Windows.Forms.ToolStripItem[] {
            this.mnuOptions,
            this.mnuHelp} );
      this.tsOptions.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
      this.tsOptions.Location = new System.Drawing.Point( 11, 329 );
      this.tsOptions.Name = "tsOptions";
      this.tsOptions.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
      this.tsOptions.Size = new System.Drawing.Size( 109, 18 );
      this.tsOptions.TabIndex = 2;
      this.tsOptions.TabStop = true;
      // 
      // mnuOptions
      // 
      this.mnuOptions.AutoToolTip = false;
      this.mnuOptions.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
      this.mnuOptions.DropDownItems.AddRange( new System.Windows.Forms.ToolStripItem[] {
            this.itmOptionsShowHidden,
            this.itmOptionsPreserveExt,
            this.itmOptionsRealtimePreview,
            this.itmOptionsAllowRenSub,
            this.itmOptionsRenameSelectedRows,
            this.itmOptionsRememberWinPos,
            this.itmOptionsAddContextMenu} );
      this.mnuOptions.Margin = new System.Windows.Forms.Padding( 0, 1, 10, 0 );
      this.mnuOptions.Name = "mnuOptions";
      this.mnuOptions.Size = new System.Drawing.Size( 57, 17 );
      this.mnuOptions.Text = "Options";
      this.mnuOptions.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // itmOptionsShowHidden
      // 
      this.itmOptionsShowHidden.CheckOnClick = true;
      this.itmOptionsShowHidden.Name = "itmOptionsShowHidden";
      this.itmOptionsShowHidden.Size = new System.Drawing.Size( 205, 22 );
      this.itmOptionsShowHidden.Text = "Show hidden files";
      this.itmOptionsShowHidden.Click += new System.EventHandler( this.itmOptionsShowHidden_Click );
      // 
      // itmOptionsPreserveExt
      // 
      this.itmOptionsPreserveExt.CheckOnClick = true;
      this.itmOptionsPreserveExt.Name = "itmOptionsPreserveExt";
      this.itmOptionsPreserveExt.Size = new System.Drawing.Size( 205, 22 );
      this.itmOptionsPreserveExt.Text = "Preserve file extension";
      this.itmOptionsPreserveExt.Click += new System.EventHandler( this.itmOptionsPreserveExt_Click );
      // 
      // itmOptionsRealtimePreview
      // 
      this.itmOptionsRealtimePreview.Checked = true;
      this.itmOptionsRealtimePreview.CheckOnClick = true;
      this.itmOptionsRealtimePreview.CheckState = System.Windows.Forms.CheckState.Checked;
      this.itmOptionsRealtimePreview.Name = "itmOptionsRealtimePreview";
      this.itmOptionsRealtimePreview.Size = new System.Drawing.Size( 205, 22 );
      this.itmOptionsRealtimePreview.Text = "Enable realtime preview";
      this.itmOptionsRealtimePreview.ToolTipText = "When unchecked, press ENTER in the regex fields to update the preview";
      // 
      // itmOptionsAllowRenSub
      // 
      this.itmOptionsAllowRenSub.CheckOnClick = true;
      this.itmOptionsAllowRenSub.Name = "itmOptionsAllowRenSub";
      this.itmOptionsAllowRenSub.Size = new System.Drawing.Size( 205, 22 );
      this.itmOptionsAllowRenSub.Text = "Allow rename to subfolders";
      this.itmOptionsAllowRenSub.Click += new System.EventHandler( this.itmOptionsAllowRenSub_Click );
      // 
      // itmOptionsRenameSelectedRows
      // 
      this.itmOptionsRenameSelectedRows.CheckOnClick = true;
      this.itmOptionsRenameSelectedRows.Name = "itmOptionsRenameSelectedRows";
      this.itmOptionsRenameSelectedRows.Size = new System.Drawing.Size( 205, 22 );
      this.itmOptionsRenameSelectedRows.Text = "Only rename selected rows";
      // 
      // itmOptionsRememberWinPos
      // 
      this.itmOptionsRememberWinPos.Checked = true;
      this.itmOptionsRememberWinPos.CheckOnClick = true;
      this.itmOptionsRememberWinPos.CheckState = System.Windows.Forms.CheckState.Checked;
      this.itmOptionsRememberWinPos.Name = "itmOptionsRememberWinPos";
      this.itmOptionsRememberWinPos.Size = new System.Drawing.Size( 205, 22 );
      this.itmOptionsRememberWinPos.Text = "Remember window position";
      // 
      // itmOptionsAddContextMenu
      // 
      this.itmOptionsAddContextMenu.Name = "itmOptionsAddContextMenu";
      this.itmOptionsAddContextMenu.Size = new System.Drawing.Size( 205, 22 );
      this.itmOptionsAddContextMenu.Text = "Add explorer context menu";
      this.itmOptionsAddContextMenu.Click += new System.EventHandler( this.itmOptionsAddContextMenu_Click );
      // 
      // mnuHelp
      // 
      this.mnuHelp.AutoToolTip = false;
      this.mnuHelp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
      this.mnuHelp.DropDownItems.AddRange( new System.Windows.Forms.ToolStripItem[] {
            this.itmHelpContents,
            this.itmHelpRegexReference,
            this.itmHelpSep1,
            this.itmHelpEmailAuthor,
            this.itmHelpReportBug,
            this.itmHelpHomepage,
            this.itmHelpSep2,
            this.itmHelpAbout} );
      this.mnuHelp.Margin = new System.Windows.Forms.Padding( 0, 1, 0, 0 );
      this.mnuHelp.Name = "mnuHelp";
      this.mnuHelp.Size = new System.Drawing.Size( 41, 17 );
      this.mnuHelp.Text = "Help";
      this.mnuHelp.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // itmHelpContents
      // 
      this.itmHelpContents.Name = "itmHelpContents";
      this.itmHelpContents.ShortcutKeys = System.Windows.Forms.Keys.F1;
      this.itmHelpContents.Size = new System.Drawing.Size( 207, 22 );
      this.itmHelpContents.Text = "Contents";
      this.itmHelpContents.Click += new System.EventHandler( this.itmHelpContents_Click );
      // 
      // itmHelpRegexReference
      // 
      this.itmHelpRegexReference.Name = "itmHelpRegexReference";
      this.itmHelpRegexReference.ShortcutKeys = ( (System.Windows.Forms.Keys)( ( System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.F1 ) ) );
      this.itmHelpRegexReference.Size = new System.Drawing.Size( 207, 22 );
      this.itmHelpRegexReference.Text = "Regex Reference";
      this.itmHelpRegexReference.Click += new System.EventHandler( this.itmHelpRegexReference_Click );
      // 
      // itmHelpSep1
      // 
      this.itmHelpSep1.Name = "itmHelpSep1";
      this.itmHelpSep1.Size = new System.Drawing.Size( 204, 6 );
      // 
      // itmHelpEmailAuthor
      // 
      this.itmHelpEmailAuthor.Name = "itmHelpEmailAuthor";
      this.itmHelpEmailAuthor.Size = new System.Drawing.Size( 207, 22 );
      this.itmHelpEmailAuthor.Text = "Email the author";
      this.itmHelpEmailAuthor.Click += new System.EventHandler( this.itmHelpEmailAuthor_Click );
      // 
      // itmHelpReportBug
      // 
      this.itmHelpReportBug.Name = "itmHelpReportBug";
      this.itmHelpReportBug.Size = new System.Drawing.Size( 207, 22 );
      this.itmHelpReportBug.Text = "Report a bug";
      this.itmHelpReportBug.Click += new System.EventHandler( this.itmHelpReportBug_Click );
      // 
      // itmHelpHomepage
      // 
      this.itmHelpHomepage.Name = "itmHelpHomepage";
      this.itmHelpHomepage.Size = new System.Drawing.Size( 207, 22 );
      this.itmHelpHomepage.Text = "Homepage";
      this.itmHelpHomepage.Click += new System.EventHandler( this.itmHelpHomepage_Click );
      // 
      // itmHelpSep2
      // 
      this.itmHelpSep2.Name = "itmHelpSep2";
      this.itmHelpSep2.Size = new System.Drawing.Size( 204, 6 );
      // 
      // itmHelpAbout
      // 
      this.itmHelpAbout.Name = "itmHelpAbout";
      this.itmHelpAbout.Size = new System.Drawing.Size( 207, 22 );
      this.itmHelpAbout.Text = "About RegexRenamer";
      this.itmHelpAbout.Click += new System.EventHandler( this.itmHelpAbout_Click );
      // 
      // progressBar
      // 
      this.progressBar.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( ( System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left )
                  | System.Windows.Forms.AnchorStyles.Right ) ) );
      this.progressBar.Location = new System.Drawing.Point( 2, 327 );
      this.progressBar.Name = "progressBar";
      this.progressBar.Size = new System.Drawing.Size( 251, 22 );
      this.progressBar.TabIndex = 0;
      this.progressBar.Visible = false;
      // 
      // btnCancel
      // 
      this.btnCancel.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right ) ) );
      this.btnCancel.Enabled = false;
      this.btnCancel.Location = new System.Drawing.Point( 259, 326 );
      this.btnCancel.Name = "btnCancel";
      this.btnCancel.Size = new System.Drawing.Size( 85, 24 );
      this.btnCancel.TabIndex = 3;
      this.btnCancel.Text = "&Cancel";
      this.btnCancel.UseVisualStyleBackColor = true;
      this.btnCancel.Visible = false;
      this.btnCancel.Click += new System.EventHandler( this.btnCancel_Click );
      // 
      // bgwRename
      // 
      this.bgwRename.WorkerReportsProgress = true;
      this.bgwRename.WorkerSupportsCancellation = true;
      this.bgwRename.DoWork += new System.ComponentModel.DoWorkEventHandler( this.bgwRename_DoWork );
      this.bgwRename.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler( this.bgwRename_ProgressChanged );
      this.bgwRename.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler( this.bgwRename_RunWorkerCompleted );
      // 
      // MainForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size( 672, 426 );
      this.Controls.Add( this.pnlStats );
      this.Controls.Add( this.scRegex );
      this.Controls.Add( this.lblStats );
      this.Controls.Add( this.tsMenu );
      this.Controls.Add( this.scMain );
      this.Controls.Add( this.gbFilter );
      this.Icon = global::RegexRenamer.Properties.Resources.icon;
      this.KeyPreview = true;
      this.MinimumSize = new System.Drawing.Size( 538, 300 );
      this.Name = "MainForm";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "RegexRenamer";
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler( this.MainForm_FormClosing );
      this.Load += new System.EventHandler( this.MainForm_Load );
      this.KeyDown += new System.Windows.Forms.KeyEventHandler( this.MainForm_KeyDown );
      this.gbFilter.ResumeLayout( false );
      this.gbFilter.PerformLayout();
      this.pnlStats.ResumeLayout( false );
      this.tsMenu.ResumeLayout( false );
      this.tsMenu.PerformLayout();
      this.scRegex.Panel1.ResumeLayout( false );
      this.scRegex.Panel1.PerformLayout();
      this.scRegex.ResumeLayout( false );
      this.scMain.Panel1.ResumeLayout( false );
      this.scMain.Panel1.PerformLayout();
      this.scMain.Panel2.ResumeLayout( false );
      this.scMain.Panel2.PerformLayout();
      this.scMain.ResumeLayout( false );
      ( (System.ComponentModel.ISupportInitialize)( this.dgvFiles ) ).EndInit();
      this.cmsRename.ResumeLayout( false );
      this.tsOptions.ResumeLayout( false );
      this.tsOptions.PerformLayout();
      this.ResumeLayout( false );

    }

    #endregion

    private System.Windows.Forms.SplitContainer scMain;
    private System.Windows.Forms.GroupBox gbFilter;
    private System.Windows.Forms.RadioButton rbFilterRegex;
    private System.Windows.Forms.RadioButton rbFilterGlob;
    private System.Windows.Forms.TextBox txtFilter;
    private MyComboBox cmbMatch;
    private System.Windows.Forms.Label lblMatch;
    private System.Windows.Forms.Label lblReplace;
    private System.Windows.Forms.CheckBox cbModifierI;
    private System.Windows.Forms.CheckBox cbModifierG;
    private System.Windows.Forms.CheckBox cbModifierX;
    private System.Windows.Forms.TextBox txtReplace;
    private System.Windows.Forms.ToolTip toolTip;
    private Furty.Windows.Forms.FolderTreeView tvwFolders;
    private System.Windows.Forms.ToolStrip tsMenu;
    private System.Windows.Forms.ToolStripDropDownButton mnuNumbering;
    private System.Windows.Forms.ToolStripTextBox txtNumberingStart;
    private System.Windows.Forms.ToolStripTextBox txtNumberingPad;
    private System.Windows.Forms.ToolStripTextBox txtNumberingInc;
    private System.Windows.Forms.ToolStripDropDownButton mnuChangeCase;
    private System.Windows.Forms.ToolStripMenuItem itmChangeCaseNoChange;
    private System.Windows.Forms.ToolStripMenuItem itmChangeCaseUppercase;
    private System.Windows.Forms.ToolStripMenuItem itmChangeCaseLowercase;
    private System.Windows.Forms.ToolStripMenuItem itmChangeCaseTitlecase;
    private System.Windows.Forms.ToolStripDropDownButton mnuHelp;
    private System.Windows.Forms.ToolStripMenuItem itmHelpContents;
    private System.Windows.Forms.ToolStripMenuItem itmHelpRegexReference;
    private System.Windows.Forms.ToolStripMenuItem itmHelpEmailAuthor;
    private System.Windows.Forms.ToolStripMenuItem itmHelpReportBug;
    private System.Windows.Forms.ToolStripMenuItem itmHelpHomepage;
    private System.Windows.Forms.ToolStripMenuItem itmHelpAbout;
    private System.Windows.Forms.ToolStripSeparator itmHelpSep1;
    private System.Windows.Forms.ToolStripSeparator itmHelpSep2;
    private System.Windows.Forms.ToolTip ttPreviewError;
    private System.Windows.Forms.ToolStripSeparator itmChangeCaseSep;
    private System.Windows.Forms.FolderBrowserDialog fbdNetwork;
    private System.Windows.Forms.TextBox txtPath;
    private System.Windows.Forms.Button btnNetwork;
    private System.Windows.Forms.Label lblPath;
    private System.Windows.Forms.CheckBox cbFilterExclude;
    private System.Windows.Forms.ToolStrip tsOptions;
    private System.Windows.Forms.ToolStripDropDownButton mnuOptions;
    private System.Windows.Forms.ToolStripMenuItem itmOptionsShowHidden;
    private System.Windows.Forms.ToolStripMenuItem itmOptionsPreserveExt;
    private System.Windows.Forms.ToolStripMenuItem itmOptionsAllowRenSub;
    private System.Windows.Forms.ProgressBar progressBar;
    private System.Windows.Forms.ToolStripTextBox txtNumberingReset;
    private System.Windows.Forms.ToolStripDropDownButton mnuMoveCopy;
    private System.Windows.Forms.ToolStripMenuItem itmOutputRenameInPlace;
    private System.Windows.Forms.ToolStripSeparator itmOutputSep;
    private System.Windows.Forms.ToolStripMenuItem itmOutputMoveTo;
    private System.Windows.Forms.ToolStripMenuItem itmOutputCopyTo;
    private System.Windows.Forms.ToolStripMenuItem itmOutputBackupTo;
    private System.Windows.Forms.FolderBrowserDialog fbdMoveCopy;
    private System.Windows.Forms.ContextMenu cmRegexMatch;
    private System.Windows.Forms.MenuItem miRegexMatchMatch;
    private System.Windows.Forms.MenuItem miRegexMatchMatchSingleChar;
    private System.Windows.Forms.MenuItem miRegexMatchMatchDigit;
    private System.Windows.Forms.MenuItem miRegexMatchMatchAlpha;
    private System.Windows.Forms.MenuItem miRegexMatchMatchSpace;
    private System.Windows.Forms.MenuItem miRegexMatchMatchMultiChar;
    private System.Windows.Forms.MenuItem miRegexMatchMatchNonDigit;
    private System.Windows.Forms.MenuItem miRegexMatchMatchNonAlpha;
    private System.Windows.Forms.MenuItem miRegexMatchMatchNonSpace;
    private System.Windows.Forms.MenuItem miRegexMatchAnchor;
    private System.Windows.Forms.MenuItem miRegexMatchAnchorStart;
    private System.Windows.Forms.MenuItem miRegexMatchAnchorBound;
    private System.Windows.Forms.MenuItem miRegexMatchAnchorEnd;
    private System.Windows.Forms.MenuItem miRegexMatchAnchorNonBound;
    private System.Windows.Forms.MenuItem miRegexMatchGroup;
    private System.Windows.Forms.MenuItem miRegexMatchQuant;
    private System.Windows.Forms.MenuItem miRegexMatchClass;
    private System.Windows.Forms.MenuItem miRegexMatchCapt;
    private System.Windows.Forms.MenuItem miRegexMatchLook;
    private System.Windows.Forms.MenuItem miRegexMatchGroupCapt;
    private System.Windows.Forms.MenuItem miRegexMatchGroupNonCapt;
    private System.Windows.Forms.MenuItem miRegexMatchGroupAlt;
    private System.Windows.Forms.MenuItem miRegexMatchQuantGreedy;
    private System.Windows.Forms.MenuItem miRegexMatchQuantZeroOneG;
    private System.Windows.Forms.MenuItem miRegexMatchQuantOneMoreG;
    private System.Windows.Forms.MenuItem miRegexMatchQuantZeroMoreG;
    private System.Windows.Forms.MenuItem miRegexMatchQuantExactG;
    private System.Windows.Forms.MenuItem miRegexMatchQuantAtLeastG;
    private System.Windows.Forms.MenuItem miRegexMatchQuantBetweenG;
    private System.Windows.Forms.MenuItem miRegexMatchQuantLazy;
    private System.Windows.Forms.MenuItem miRegexMatchQuantZeroOneL;
    private System.Windows.Forms.MenuItem miRegexMatchQuantOneMoreL;
    private System.Windows.Forms.MenuItem miRegexMatchQuantZeroMoreL;
    private System.Windows.Forms.MenuItem miRegexMatchQuantExactL;
    private System.Windows.Forms.MenuItem miRegexMatchQuantAtLeastL;
    private System.Windows.Forms.MenuItem miRegexMatchQuantBetweenL;
    private System.Windows.Forms.MenuItem miRegexMatchClassPos;
    private System.Windows.Forms.MenuItem miRegexMatchClassNeg;
    private System.Windows.Forms.MenuItem miRegexMatchClassLower;
    private System.Windows.Forms.MenuItem miRegexMatchClassUpper;
    private System.Windows.Forms.MenuItem miRegexMatchCaptCreateUnnamed;
    private System.Windows.Forms.MenuItem miRegexMatchCaptMatchUnnamed;
    private System.Windows.Forms.MenuItem miRegexMatchCaptCreateNamed;
    private System.Windows.Forms.MenuItem miRegexMatchCaptMatchNamed;
    private System.Windows.Forms.MenuItem miRegexMatchLookPosAhead;
    private System.Windows.Forms.MenuItem miRegexMatchLookNegAhead;
    private System.Windows.Forms.MenuItem miRegexMatchLookPosBehind;
    private System.Windows.Forms.MenuItem miRegexMatchLookNegBehind;
    private System.Windows.Forms.MenuItem miRegexMatchAnchorStartEnd;
    private System.Windows.Forms.ContextMenu cmRegexReplace;
    private System.Windows.Forms.MenuItem miRegexReplaceCapture;
    private System.Windows.Forms.MenuItem miRegexReplaceCaptureUnnamed;
    private System.Windows.Forms.MenuItem miRegexReplaceCaptureNamed;
    private System.Windows.Forms.MenuItem miRegexReplaceOrig;
    private System.Windows.Forms.MenuItem miRegexReplaceOrigMatched;
    private System.Windows.Forms.MenuItem miRegexReplaceOrigBefore;
    private System.Windows.Forms.MenuItem miRegexReplaceOrigAfter;
    private System.Windows.Forms.MenuItem miRegexReplaceOrigAll;
    private System.Windows.Forms.MenuItem miRegexReplaceSpecialNumSeq;
    private System.Windows.Forms.MenuItem miRegexReplaceSpecial;
    private System.Windows.Forms.MenuItem miRegexReplaceLiteral;
    private System.Windows.Forms.MenuItem miRegexReplaceLiteralDollar;
    private System.Windows.Forms.MenuItem miRegexReplaceSep1;
    private System.Windows.Forms.MenuItem miRegexMatchSep1;
    private System.Windows.Forms.MenuItem miRegexMatchLiteral;
    private System.Windows.Forms.MenuItem miRegexMatchLiteralDot;
    private System.Windows.Forms.MenuItem miRegexMatchLiteralQuestion;
    private System.Windows.Forms.MenuItem miRegexMatchLiteralPlus;
    private System.Windows.Forms.MenuItem miRegexMatchLiteralStar;
    private System.Windows.Forms.MenuItem miRegexMatchLiteralCaret;
    private System.Windows.Forms.MenuItem miRegexMatchLiteralDollar;
    private System.Windows.Forms.MenuItem miRegexMatchLiteralBackslash;
    private System.Windows.Forms.MenuItem miRegexMatchLiteralOpenRound;
    private System.Windows.Forms.MenuItem miRegexMatchLiteralCloseRound;
    private System.Windows.Forms.MenuItem miRegexMatchLiteralOpenSquare;
    private System.Windows.Forms.MenuItem miRegexMatchLiteralCloseSquare;
    private System.Windows.Forms.MenuItem miRegexMatchLiteralOpenCurly;
    private System.Windows.Forms.MenuItem miRegexMatchLiteralCloseCurly;
    private System.Windows.Forms.MenuItem miRegexMatchLiteralPipe;
    private System.Windows.Forms.ContextMenu cmGlobMatch;
    private System.Windows.Forms.MenuItem miGlobMatchSingle;
    private System.Windows.Forms.MenuItem miGlobMatchMultiple;
    private System.Windows.Forms.ContextMenuStrip cmsBlank;
    private System.Windows.Forms.SplitContainer scRegex;
    private System.Windows.Forms.Button btnCancel;
    private System.ComponentModel.BackgroundWorker bgwRename;
    private System.Windows.Forms.ToolStripMenuItem itmOptionsRememberWinPos;
    private Microsoft.Samples.SplitButton btnRename;
    private System.Windows.Forms.ContextMenuStrip cmsRename;
    private System.Windows.Forms.ToolStripMenuItem itmRenameFiles;
    private System.Windows.Forms.ToolStripMenuItem itmRenameFolders;
    private System.Windows.Forms.Label lblNumMatched;
    private System.Windows.Forms.Label lblNumConflict;
    private System.Windows.Forms.Label lblStats;
    private System.Windows.Forms.Panel pnlStats;
    private System.Windows.Forms.Label lblStatsHidden;
    private System.Windows.Forms.Label lblStatsShown;
    private System.Windows.Forms.Label lblStatsFiltered;
    private System.Windows.Forms.Label lblStatsTotal;
    private System.Windows.Forms.ToolStripMenuItem itmOptionsRealtimePreview;
    private System.Windows.Forms.DataGridView dgvFiles;
    private System.Windows.Forms.DataGridViewImageColumn colIcon;
    private System.Windows.Forms.DataGridViewTextBoxColumn colFilename;
    private System.Windows.Forms.DataGridViewTextBoxColumn colPreview;
    private System.Windows.Forms.ToolStripMenuItem itmOptionsAddContextMenu;
    private System.Windows.Forms.ToolStripMenuItem itmOptionsRenameSelectedRows;
  }
}

