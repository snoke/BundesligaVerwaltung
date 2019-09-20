using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BundesligaVerwaltung.Model.Entities;

namespace BundesligaVerwaltung.View
{
    public partial class MainMenuForm : Form
    {
        private string[] options;
        List<string[]> rows;
        public MainMenuForm(List<string[]> rows,string[] options)
        {
            Rows = rows;
            Options = options;
            InitializeComponent();
        }

        public string[] Options { get { return options; } set { options = value; } }

        public List<string[]> Rows { get => rows; set => rows = value; }
    }
}
