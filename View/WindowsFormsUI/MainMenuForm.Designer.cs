using System.Collections.Generic;
using System.Windows.Forms;

namespace BundesligaVerwaltung.View
{
    partial class MainMenuForm
    {
        private object selectedElement;
        public object SelectedElement { get => selectedElement; set => selectedElement = value; }
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

        public void clicked(object sender, System.EventArgs e)
        {
            this.SelectedElement = sender;
            this.DialogResult = new DialogResult();
            this.Dispose();
        }
        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // firstToolStripMenuItem
            // 

            List<System.Windows.Forms.ToolStripMenuItem> opt = new List<System.Windows.Forms.ToolStripMenuItem>();
            List<System.Windows.Forms.ToolStripItem> ToolStrip = new List<System.Windows.Forms.ToolStripItem>();
            int i = 0;
            foreach (string action in this.Options)
            {
                System.Windows.Forms.ToolStripMenuItem element = new System.Windows.Forms.ToolStripMenuItem();
                element.Text = action;
                element.Name = action;
                element.Tag = i;
                element.Size = new System.Drawing.Size(180, 22);
                element.Click += new System.EventHandler(clicked);
                opt.Add(element);
                ToolStrip.Add(element);
            }
            // 
            // menuToolStripMenuItem
            // 
            this.menuToolStripMenuItem.DropDownItems.AddRange(ToolStrip.ToArray());
            this.menuToolStripMenuItem.Name = "menuToolStripMenuItem";
            this.menuToolStripMenuItem.Size = new System.Drawing.Size(50, 20);
            this.menuToolStripMenuItem.Text = "BundesligaVerwalter";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem firstToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem secondToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem thirdToolStripMenuItem;
    }
}