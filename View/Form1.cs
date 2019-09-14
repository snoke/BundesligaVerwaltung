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
    public partial class Form1 : Form
    {
        private string[] options;
        public Form1(string[] options)
        {
            this.Options = options;
            InitializeComponent();
        }

        public string[] Options { get => options; set => options = value; }
    }
}
