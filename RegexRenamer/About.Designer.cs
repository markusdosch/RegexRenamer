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
  partial class About
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
      this.picLogo = new System.Windows.Forms.PictureBox();
      this.btnOK = new System.Windows.Forms.Button();
      this.lblHeader = new System.Windows.Forms.Label();
      this.linkHomepage = new System.Windows.Forms.LinkLabel();
      this.linkEmail = new System.Windows.Forms.LinkLabel();
      this.lblStats = new System.Windows.Forms.Label();
      ( (System.ComponentModel.ISupportInitialize)( this.picLogo ) ).BeginInit();
      this.SuspendLayout();
      // 
      // picLogo
      // 
      this.picLogo.Dock = System.Windows.Forms.DockStyle.Top;
      this.picLogo.Image = global::RegexRenamer.Properties.Resources.xiperware_small;
      this.picLogo.Location = new System.Drawing.Point( 0, 0 );
      this.picLogo.Name = "picLogo";
      this.picLogo.Size = new System.Drawing.Size( 242, 87 );
      this.picLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
      this.picLogo.TabIndex = 0;
      this.picLogo.TabStop = false;
      // 
      // btnOK
      // 
      this.btnOK.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right ) ) );
      this.btnOK.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.btnOK.Location = new System.Drawing.Point( 155, 231 );
      this.btnOK.Name = "btnOK";
      this.btnOK.Size = new System.Drawing.Size( 75, 23 );
      this.btnOK.TabIndex = 1;
      this.btnOK.Text = "OK";
      this.btnOK.UseVisualStyleBackColor = true;
      this.btnOK.Click += new System.EventHandler( this.btnOK_Click );
      // 
      // lblHeader
      // 
      this.lblHeader.Location = new System.Drawing.Point( 12, 93 );
      this.lblHeader.Name = "lblHeader";
      this.lblHeader.Size = new System.Drawing.Size( 218, 45 );
      this.lblHeader.TabIndex = 2;
      this.lblHeader.Text = "RegexRenamer v0.0\r\nCopyright © 2011 Xiperware\r\nGNU General Public License\r\n";
      this.lblHeader.TextAlign = System.Drawing.ContentAlignment.TopCenter;
      // 
      // linkHomepage
      // 
      this.linkHomepage.AutoSize = true;
      this.linkHomepage.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
      this.linkHomepage.Location = new System.Drawing.Point( 29, 146 );
      this.linkHomepage.Name = "linkHomepage";
      this.linkHomepage.Size = new System.Drawing.Size( 184, 13 );
      this.linkHomepage.TabIndex = 3;
      this.linkHomepage.TabStop = true;
      this.linkHomepage.Text = "http://regexrenamer.sourceforge.net/";
      this.linkHomepage.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler( this.linkHomepage_LinkClicked );
      // 
      // linkEmail
      // 
      this.linkEmail.AutoSize = true;
      this.linkEmail.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
      this.linkEmail.Location = new System.Drawing.Point( 66, 159 );
      this.linkEmail.Name = "linkEmail";
      this.linkEmail.Size = new System.Drawing.Size( 110, 13 );
      this.linkEmail.TabIndex = 4;
      this.linkEmail.TabStop = true;
      this.linkEmail.Text = "xiperware@gmail.com";
      this.linkEmail.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler( this.linkEmail_LinkClicked );
      // 
      // lblStats
      // 
      this.lblStats.Location = new System.Drawing.Point( 12, 188 );
      this.lblStats.Name = "lblStats";
      this.lblStats.Size = new System.Drawing.Size( 218, 26 );
      this.lblStats.TabIndex = 5;
      this.lblStats.Text = "RegexRenamer has been run ___ times and renamed a total of ___ files.";
      // 
      // About
      // 
      this.AcceptButton = this.btnOK;
      this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BackColor = System.Drawing.SystemColors.Window;
      this.CancelButton = this.btnOK;
      this.ClientSize = new System.Drawing.Size( 242, 266 );
      this.Controls.Add( this.lblStats );
      this.Controls.Add( this.linkEmail );
      this.Controls.Add( this.linkHomepage );
      this.Controls.Add( this.lblHeader );
      this.Controls.Add( this.btnOK );
      this.Controls.Add( this.picLogo );
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "About";
      this.ShowIcon = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "About RegexRenamer";
      ( (System.ComponentModel.ISupportInitialize)( this.picLogo ) ).EndInit();
      this.ResumeLayout( false );
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.PictureBox picLogo;
    private System.Windows.Forms.Button btnOK;
    private System.Windows.Forms.Label lblHeader;
    private System.Windows.Forms.LinkLabel linkHomepage;
    private System.Windows.Forms.LinkLabel linkEmail;
    private System.Windows.Forms.Label lblStats;
  }
}