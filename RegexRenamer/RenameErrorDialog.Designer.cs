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
  partial class RenameErrorDialog
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager( typeof( RenameErrorDialog ) );
      this.lvwErrors = new System.Windows.Forms.ListView();
      this.colOldName = new System.Windows.Forms.ColumnHeader();
      this.colArrow = new System.Windows.Forms.ColumnHeader();
      this.colNewName = new System.Windows.Forms.ColumnHeader();
      this.colSpacer = new System.Windows.Forms.ColumnHeader();
      this.colError = new System.Windows.Forms.ColumnHeader();
      this.btnOK = new System.Windows.Forms.Button();
      this.lblMessage = new System.Windows.Forms.Label();
      this.picErrorIcon = new System.Windows.Forms.PictureBox();
      ( (System.ComponentModel.ISupportInitialize)( this.picErrorIcon ) ).BeginInit();
      this.SuspendLayout();
      // 
      // lvwErrors
      // 
      this.lvwErrors.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom )
                  | System.Windows.Forms.AnchorStyles.Left )
                  | System.Windows.Forms.AnchorStyles.Right ) ) );
      this.lvwErrors.Columns.AddRange( new System.Windows.Forms.ColumnHeader[] {
            this.colOldName,
            this.colArrow,
            this.colNewName,
            this.colSpacer,
            this.colError} );
      this.lvwErrors.FullRowSelect = true;
      this.lvwErrors.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
      this.lvwErrors.Location = new System.Drawing.Point( 12, 53 );
      this.lvwErrors.Name = "lvwErrors";
      this.lvwErrors.Size = new System.Drawing.Size( 448, 72 );
      this.lvwErrors.TabIndex = 0;
      this.lvwErrors.UseCompatibleStateImageBehavior = false;
      this.lvwErrors.View = System.Windows.Forms.View.Details;
      this.lvwErrors.Enter += new System.EventHandler( this.lvwErrors_Enter );
      // 
      // colOldName
      // 
      this.colOldName.Text = "Old Filename";
      this.colOldName.Width = 100;
      // 
      // colArrow
      // 
      this.colArrow.Text = "";
      this.colArrow.Width = 18;
      // 
      // colNewName
      // 
      this.colNewName.Text = "New Filename";
      this.colNewName.Width = 100;
      // 
      // colSpacer
      // 
      this.colSpacer.Text = "";
      this.colSpacer.Width = 10;
      // 
      // colError
      // 
      this.colError.Text = "Error";
      this.colError.Width = 100;
      // 
      // btnOK
      // 
      this.btnOK.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
      this.btnOK.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.btnOK.Location = new System.Drawing.Point( 199, 131 );
      this.btnOK.Name = "btnOK";
      this.btnOK.Size = new System.Drawing.Size( 75, 23 );
      this.btnOK.TabIndex = 1;
      this.btnOK.Text = "OK";
      this.btnOK.UseVisualStyleBackColor = true;
      // 
      // lblMessage
      // 
      this.lblMessage.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left )
                  | System.Windows.Forms.AnchorStyles.Right ) ) );
      this.lblMessage.Location = new System.Drawing.Point( 44, 12 );
      this.lblMessage.Name = "lblMessage";
      this.lblMessage.Size = new System.Drawing.Size( 416, 32 );
      this.lblMessage.TabIndex = 2;
      this.lblMessage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // picErrorIcon
      // 
      this.picErrorIcon.Image = ( (System.Drawing.Image)( resources.GetObject( "picErrorIcon.Image" ) ) );
      this.picErrorIcon.Location = new System.Drawing.Point( 12, 12 );
      this.picErrorIcon.Name = "picErrorIcon";
      this.picErrorIcon.Size = new System.Drawing.Size( 32, 32 );
      this.picErrorIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
      this.picErrorIcon.TabIndex = 3;
      this.picErrorIcon.TabStop = false;
      // 
      // RenameErrorDialog
      // 
      this.AcceptButton = this.btnOK;
      this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.CancelButton = this.btnOK;
      this.ClientSize = new System.Drawing.Size( 472, 166 );
      this.Controls.Add( this.picErrorIcon );
      this.Controls.Add( this.lblMessage );
      this.Controls.Add( this.btnOK );
      this.Controls.Add( this.lvwErrors );
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.MinimumSize = new System.Drawing.Size( 480, 200 );
      this.Name = "RenameErrorDialog";
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "Error";
      ( (System.ComponentModel.ISupportInitialize)( this.picErrorIcon ) ).EndInit();
      this.ResumeLayout( false );
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.ListView lvwErrors;
    private System.Windows.Forms.Button btnOK;
    private System.Windows.Forms.ColumnHeader colOldName;
    private System.Windows.Forms.ColumnHeader colNewName;
    private System.Windows.Forms.ColumnHeader colError;
    private System.Windows.Forms.Label lblMessage;
    private System.Windows.Forms.PictureBox picErrorIcon;
    private System.Windows.Forms.ColumnHeader colArrow;
    private System.Windows.Forms.ColumnHeader colSpacer;
  }
}