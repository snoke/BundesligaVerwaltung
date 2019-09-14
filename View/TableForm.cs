using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BundesligaVerwaltung.View
{
    public partial class TableForm : Form
    {
        public TableForm(List<string[]> rows)
        {

            InitializeComponent(rows);
            int i = 0;
            int c = 0;
            foreach (string[] row in rows)
            {
                foreach (string cell in row)
                {
                     tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
                    tableLayoutPanel1.Controls.Add(new Label() { Text = cell }, i, 0);
                    i++;
                }
                i++;
            }
        }

        private void TableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
