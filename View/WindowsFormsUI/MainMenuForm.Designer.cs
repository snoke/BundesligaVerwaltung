using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace BundesligaVerwaltung.View
{
    partial class MainMenuForm
    {
        private object selectedElement;
        public object SelectedElement { get { return selectedElement; } set { selectedElement = value; } }
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

            TableLayoutPanel tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
           // this.AutoSize(tableLayoutPanel1);
            // no smaller than design time size
            tableLayoutPanel1.MinimumSize = new System.Drawing.Size(this.Width, this.Height);

            // no larger than screen size
            tableLayoutPanel1.MaximumSize = new System.Drawing.Size(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);

            tableLayoutPanel1.AutoSize = true;
            tableLayoutPanel1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.OutsetDouble;
            tableLayoutPanel1.ColumnCount = 7;
            int i = 0;
            for (i = 0; i < tableLayoutPanel1.ColumnCount; i++)
            {
                tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());

            }
            tableLayoutPanel1.Location = new System.Drawing.Point(12, 48);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = Rows.Count;
            for (i = 0; i < Rows.Count; i++)
            {
                tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());

            }
            tableLayoutPanel1.TabIndex = 0;
            tableLayoutPanel1.Paint += new System.Windows.Forms.PaintEventHandler(TableLayoutPanel1_Paint);

            i = 0;
            int c = 0;
            foreach (string[] row in Rows)
            {
                foreach (string cell in row)
                {
                    tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
                    tableLayoutPanel1.Controls.Add(new Label() { Text = cell }, i, 0);
                    i++;
                }
                i++;
            }
            this.Controls.Add(tableLayoutPanel1);
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
            i = 0;
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

        private void TableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {
        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem firstToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem secondToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem thirdToolStripMenuItem;
    }
}