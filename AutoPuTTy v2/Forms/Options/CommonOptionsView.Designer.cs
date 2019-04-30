using System;

namespace AutoPuTTY.Forms.Options
{
    partial class CommonOptionsView
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tbAppPath = new System.Windows.Forms.TextBox();
            this.lAppPath = new System.Windows.Forms.Label();
            this.bChangePath = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tbAppPath
            // 
            this.tbAppPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbAppPath.Location = new System.Drawing.Point(10, 35);
            this.tbAppPath.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tbAppPath.Name = "tbAppPath";
            this.tbAppPath.Size = new System.Drawing.Size(346, 26);
            this.tbAppPath.TabIndex = 5;
            // 
            // lAppPath
            // 
            this.lAppPath.AutoSize = true;
            this.lAppPath.Location = new System.Drawing.Point(11, 9);
            this.lAppPath.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lAppPath.Name = "lAppPath";
            this.lAppPath.Size = new System.Drawing.Size(42, 20);
            this.lAppPath.TabIndex = 4;
            this.lAppPath.Text = "Path";
            // 
            // bChangePath
            // 
            this.bChangePath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bChangePath.Location = new System.Drawing.Point(359, 33);
            this.bChangePath.Margin = new System.Windows.Forms.Padding(0);
            this.bChangePath.Name = "bChangePath";
            this.bChangePath.Size = new System.Drawing.Size(75, 34);
            this.bChangePath.TabIndex = 6;
            this.bChangePath.Text = "Select";
            this.bChangePath.UseCompatibleTextRendering = true;
            this.bChangePath.UseVisualStyleBackColor = true;
            // 
            // CommonOptionsView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Controls.Add(this.tbAppPath);
            this.Controls.Add(this.lAppPath);
            this.Controls.Add(this.bChangePath);
            this.Name = "CommonOptionsView";
            this.Size = new System.Drawing.Size(502, 76);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbAppPath;
        private System.Windows.Forms.Label lAppPath;
        private System.Windows.Forms.Button bChangePath;
    }
}
