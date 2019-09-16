using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace BundesligaVerwaltung.View
{
    partial class MenuForm
    {
        private object selectedElement;
        public object SelectedElement { get => selectedElement; set => selectedElement = value; }

        private System.Windows.Forms.MenuStrip menuStrip1;
        private List<System.Windows.Forms.ToolStripMenuItem> menuItems= new List<System.Windows.Forms.ToolStripMenuItem>();
        public List<ToolStripMenuItem> MenuItems { get => menuItems; set => menuItems = value; }

        public delegate String myMethodDelegate(int myInt);
        public void clicked(object sender, System.EventArgs e)
        {
            this.SelectedElement = sender;
            this.DialogResult = new DialogResult();
            this.Dispose();
        }
        private void tab1Button1_Click(object sender, System.EventArgs e)
        {
            // Inserts the code that should run when the button is clicked.
        }
        public void AddItem(string caption)
        {
            System.Windows.Forms.ToolStripMenuItem item = new ToolStripMenuItem();
            item.Click += new System.EventHandler(clicked);
            item.Text = caption;
            item.Name = caption;
            MenuItems.Add(item);
        }
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();

            this.ControlBox = false;
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 

            foreach (string option in this.Options)
            {
                this.AddItem(option);

            }
            this.menuStrip1.Items.AddRange(MenuItems.ToArray());
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 50);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "BundesligaVerwalter";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

    }
}