/*
 * Created by SharpDevelop.
 * User: Stefan
 * Date: 31.08.2019
 * Time: 13:12
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using BundesligaVerwaltung.Model.Entities;
using BundesligaVerwaltung.View;
namespace BundesligaVerwaltung.View
{
    public class WindowsFormsUI:UserInterface
    {
        #region properties
        #endregion

        #region accessors
        #endregion

        #region constructors
        public WindowsFormsUI()
        {
        }
        #endregion

        #region workers
        public override  int Scoreboard(List<Match> matches, List<Team> teams)
        {
            if (teams.Count<1)
            {
                throw new Exception.NoElementsException();
            } else{ }
            List<string[]> rows = new List<string[]>();
            foreach (Team team in teams)
            {
                rows.Add(new string[] {
                    team.Name,
                    matches.Where(x => (x.Opponent == team || x.Team == team)).Count().ToString(),
                    matches.Where(x => (x.Opponent == team && x.OpponentScore > x.Score) || (x.Team == team && x.Score > x.OpponentScore)).Count().ToString(),
                    matches.Where(x => (x.Opponent == team || x.Team == team) && x.Score == x.OpponentScore).Count().ToString(),
                    matches.Where(x => (x.Opponent == team && x.OpponentScore < x.Score) || (x.Team == team && x.Score < x.OpponentScore)).Count().ToString(),
                    (matches.Where(x => x.Opponent == team).Select(x => x.OpponentScore).Sum() + matches.Where(x => x.Team == team).Select(x => x.Score).Sum()).ToString(),
                    (matches.Where(x => (x.Opponent == team && x.OpponentScore > x.Score) || (x.Team == team && x.Score > x.OpponentScore)).Count()*3+matches.Where(x => (x.Opponent == team || x.Team == team) && x.Score == x.OpponentScore).Count()).ToString(),
                });
            }
            List<string[]> header = new List<string[]> { new string[] { "Mannschaft",
                    "Spiele",
                    "Siege",
                    "Unentschieden",
                    "Niederlagen",
                   "Tore",
                   "Punkte"
            }
        };
            int gameDay = 0;
            Int32.TryParse(rows.Select(x => x[1]).Min(), out gameDay);
            header.AddRange(rows.OrderByDescending(x => x[6]).ThenByDescending(x => x[5]).ToList());
         //   return "Spieltag: " + (gameDay + 1) + "\n" + Table(header);
            return Table(header);
        }

        public int Table(List<string[]> rows)
        {
            TableForm form = new TableForm(rows);
            form.ShowDialog();
            return form.SelectedElement;
        }
        public override int Menu(string[] options, string header)
        {
            if (options.Length < 1)
            {
                throw new SelectMenu.NoElementsException();
            }
            else { }
            MenuForm f1 = new MenuForm(options);
            f1.ShowDialog();
            return options.ToList<string>().IndexOf(((System.Windows.Forms.ToolStripMenuItem)f1.SelectedElement).Name);
           // return new SelectMenu.SelectMenu(options).setTitle(header).select();
        }
        public override int AskForInteger(string question)
        {
            string input = Microsoft.VisualBasic.Interaction.InputBox(question, question, "Default", 0, 0);
            int result = 0;
            bool success = Int32.TryParse(input, out result);
            if (success)
            {
                return result;
            } else
            {
                return this.AskForInteger(question);
            }
        }

        public override void Message(string message)
        {
            MessageBoxButtons buttons = MessageBoxButtons.OK;
            DialogResult result;
            result = MessageBox.Show(message, "BundesligaVerwalter", buttons);
        }
        public override string AskForString(string question)
        {//
            return Microsoft.VisualBasic.Interaction.InputBox(question, question, "Default", 0, 0);
        }
        public override void SplashScreen()
        {
            Message(@"
        ____                  __          __
       / __ )__  ______  ____/ /__  _____/ (_)___ _____ _
      / __  / / / / __ \/ __  / _ \/ ___/ / / __ `/ __ `/
     / /_/ / /_/ / / / / /_/ /  __(__  ) / / /_/ / /_/ / 
    /_____/\__,_/_/ /_/\__,_/\___/____/_/_/\__, /\__,_/  
    | |  / /__  ______      ______ _/ / /_/____/____     
    | | / / _ \/ ___/ | /| / / __ `/ / __/ _ \/ ___/     
    | |/ /  __/ /   | |/ |/ / /_/ / / /_/  __/ /         
    |___/\___/_/    |__/|__/\__,_/_/\__/\___/_/ v0.1 

     (C)Copyright 2019 Stefan Sander<stowwel@gmail.com>
");
        }
        #endregion
    }
}
