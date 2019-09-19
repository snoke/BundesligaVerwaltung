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
    public partial class MainMenuForm : Form
    {
        private string[] options;
        public MainMenuForm(string[] options)
        {
            Options = options;
            InitializeComponent();
        }

        public string[] Options { get { return options; } set { options = value; } }
    }
}
