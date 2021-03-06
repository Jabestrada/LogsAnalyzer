﻿namespace LogAnalyzer.UI.WinForms {
    partial class AnalysisResultsForm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            this.closeButton = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.logFileListContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.openContainingFolderCommand = new System.Windows.Forms.ToolStripMenuItem();
            this.clearFilterButton = new System.Windows.Forms.Button();
            this.filterTextBox = new System.Windows.Forms.TextBox();
            this.resultsTreeView = new System.Windows.Forms.TreeView();
            this.filterButton = new System.Windows.Forms.Button();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.formCaptionTextbox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.setFormCaptionButton = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.logFilesList = new System.Windows.Forms.TreeView();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.logFileListContextMenu.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // closeButton
            // 
            this.closeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.closeButton.Location = new System.Drawing.Point(619, 400);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(102, 30);
            this.closeButton.TabIndex = 1;
            this.closeButton.Text = "Close";
            this.closeButton.UseVisualStyleBackColor = true;
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.splitContainer1.Location = new System.Drawing.Point(12, 12);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.BackColor = System.Drawing.SystemColors.Control;
            this.splitContainer1.Panel1.Controls.Add(this.tabControl1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.BackColor = System.Drawing.SystemColors.Control;
            this.splitContainer1.Panel2.Controls.Add(this.clearFilterButton);
            this.splitContainer1.Panel2.Controls.Add(this.filterTextBox);
            this.splitContainer1.Panel2.Controls.Add(this.resultsTreeView);
            this.splitContainer1.Panel2.Controls.Add(this.filterButton);
            this.splitContainer1.Size = new System.Drawing.Size(716, 378);
            this.splitContainer1.SplitterDistance = 112;
            this.splitContainer1.TabIndex = 3;
            // 
            // logFileListContextMenu
            // 
            this.logFileListContextMenu.ImageScalingSize = new System.Drawing.Size(40, 40);
            this.logFileListContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openContainingFolderCommand});
            this.logFileListContextMenu.Name = "logFileListContextMenu";
            this.logFileListContextMenu.Size = new System.Drawing.Size(198, 26);
            this.logFileListContextMenu.Opened += new System.EventHandler(this.logFileListContextMenu_Opened);
            // 
            // openContainingFolderCommand
            // 
            this.openContainingFolderCommand.Name = "openContainingFolderCommand";
            this.openContainingFolderCommand.Size = new System.Drawing.Size(197, 22);
            this.openContainingFolderCommand.Text = "Open containing folder";
            this.openContainingFolderCommand.Click += new System.EventHandler(this.openContainingFolderCommand_Click);
            // 
            // clearFilterButton
            // 
            this.clearFilterButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.clearFilterButton.Location = new System.Drawing.Point(623, 11);
            this.clearFilterButton.Name = "clearFilterButton";
            this.clearFilterButton.Size = new System.Drawing.Size(86, 23);
            this.clearFilterButton.TabIndex = 8;
            this.clearFilterButton.Text = "Clear Filter";
            this.clearFilterButton.UseVisualStyleBackColor = true;
            this.clearFilterButton.Click += new System.EventHandler(this.clearFilterButton_Click);
            // 
            // filterTextBox
            // 
            this.filterTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.filterTextBox.Location = new System.Drawing.Point(0, 13);
            this.filterTextBox.Name = "filterTextBox";
            this.filterTextBox.Size = new System.Drawing.Size(530, 20);
            this.filterTextBox.TabIndex = 6;
            this.filterTextBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.filterTextBox_KeyUp);
            // 
            // resultsTreeView
            // 
            this.resultsTreeView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.resultsTreeView.HideSelection = false;
            this.resultsTreeView.Location = new System.Drawing.Point(0, 40);
            this.resultsTreeView.Name = "resultsTreeView";
            this.resultsTreeView.Size = new System.Drawing.Size(709, 222);
            this.resultsTreeView.TabIndex = 5;
            // 
            // filterButton
            // 
            this.filterButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.filterButton.Location = new System.Drawing.Point(536, 11);
            this.filterButton.Name = "filterButton";
            this.filterButton.Size = new System.Drawing.Size(84, 23);
            this.filterButton.TabIndex = 7;
            this.filterButton.Text = "Apply Filter";
            this.filterButton.UseVisualStyleBackColor = true;
            this.filterButton.Click += new System.EventHandler(this.filterButton_Click);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.setFormCaptionButton);
            this.tabPage3.Controls.Add(this.label1);
            this.tabPage3.Controls.Add(this.formCaptionTextbox);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(708, 86);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Options";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // formCaptionTextbox
            // 
            this.formCaptionTextbox.Location = new System.Drawing.Point(6, 29);
            this.formCaptionTextbox.Name = "formCaptionTextbox";
            this.formCaptionTextbox.Size = new System.Drawing.Size(264, 20);
            this.formCaptionTextbox.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Form caption";
            // 
            // setFormCaptionButton
            // 
            this.setFormCaptionButton.Location = new System.Drawing.Point(276, 27);
            this.setFormCaptionButton.Name = "setFormCaptionButton";
            this.setFormCaptionButton.Size = new System.Drawing.Size(75, 23);
            this.setFormCaptionButton.TabIndex = 2;
            this.setFormCaptionButton.Text = "Set";
            this.setFormCaptionButton.UseVisualStyleBackColor = true;
            this.setFormCaptionButton.Click += new System.EventHandler(this.setFormCaptionButton_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.logFilesList);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(708, 86);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Log files";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // logFilesList
            // 
            this.logFilesList.ContextMenuStrip = this.logFileListContextMenu;
            this.logFilesList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.logFilesList.Location = new System.Drawing.Point(3, 3);
            this.logFilesList.Name = "logFilesList";
            this.logFilesList.Size = new System.Drawing.Size(702, 80);
            this.logFilesList.TabIndex = 0;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(716, 112);
            this.tabControl1.TabIndex = 3;
            // 
            // AnalysisResultsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(740, 438);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.closeButton);
            this.Name = "AnalysisResultsForm";
            this.Text = "Analysis Results";
            this.Shown += new System.EventHandler(this.AnalysisResultsForm_Shown);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.logFileListContextMenu.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ContextMenuStrip logFileListContextMenu;
        private System.Windows.Forms.ToolStripMenuItem openContainingFolderCommand;
        private System.Windows.Forms.TreeView resultsTreeView;
        private System.Windows.Forms.TextBox filterTextBox;
        private System.Windows.Forms.Button filterButton;
        private System.Windows.Forms.Button clearFilterButton;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TreeView logFilesList;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Button setFormCaptionButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox formCaptionTextbox;
    }
}