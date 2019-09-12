using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using BundesligaVerwaltung.Controller;
using System.Windows.Forms;

namespace BundesligaVerwaltung
{
    public partial class AddTeamForm : Form
    {
        private DefaultController controller;

        internal DefaultController Controller { get => controller; set => controller = value; }

        public AddTeamForm(DefaultController controller)
        {
            this.controller = controller;
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
        }
    }
}
