/*
 * Created by SharpDevelop.
 * User: Stefan
 * Date: 31.08.2019
 * Time: 13:12
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Linq;
using BundesligaVerwaltung.Model;

namespace BundesligaVerwaltung.View
{
	public class Terminal
	{
		#region properties
		#endregion

		#region accessors
		#endregion

		#region constructors
		public Terminal()
		{
		}
        #endregion

        #region workers
        public string Scoreboard(List<Match> matches,List<Team> teams)
        {

            List<string[]> rows = new List<string[]>();
            rows.Add(new string[] {
                    "Mannschaft",
                    "Siege",
                    "Unentschieden",
                    "Niederlagen",
                   "Punkte",
                });
            foreach (Team team in teams)
            {
                int wins = matches.Where(x => (x.OpponentId == team.id && x.OpponentScore > x.Score) || (x.TeamId == team.id && x.Score > x.OpponentScore)).Count();
                int draws = matches.Where(x => (x.OpponentId == team.id || x.TeamId == team.id) && x.Score == x.OpponentScore).Count();
                int losses = matches.Where(x => (x.OpponentId == team.id && x.OpponentScore < x.Score) || (x.TeamId == team.id && x.Score < x.OpponentScore)).Count();
                int points = wins * 3 + draws;

                rows.Add(new string[] {
                    team.Name,
                    wins.ToString(),
                    draws.ToString(),
                    losses.ToString(),
                   points.ToString(),
                });
            }
            return this.Table(rows);
        }

        public string Table(List<string[]> rows)
        {
            string output = "";

            List<int> longest = new List<int>();
            foreach (string[] row in rows) {
                while (longest.Count() < row.Count())
                {
                    longest.Add(0);
                }
                for(int i = 0;i<row.Count(); i++)
                if(longest[i]<row[i].Length)
                {
                    longest[i] = row[i].Length;
                }
            }

                foreach (string[] row in rows)
            {
                for (int i = 0; i < row.Length; i++)
                {
                    output += row[i].PadLeft(longest[i]) + " | ";
                }
                output += "\n";
            }
            return output;
        }
        public int Menu(string[] options,string header)
        {
            return new SelectMenu.SelectMenu(options).setTitle(header).select();
        }
        public int AskForInteger(string question)
        {
            return AskForInteger(question, 0);
        }
        public int AskForInteger(string question, int intVal)
        {
            Console.Clear();
            if (intVal != 0)
            {
                Console.Write(question + " :" + intVal);
            }
            else
            {
                Console.Write(question + " :");
            }
            ConsoleKeyInfo cki = Console.ReadKey(false);
            if (cki.Key.ToString() == "Backspace")
            {
                return AskForInteger(question, intVal / 10);
            }
            else if (cki.Key.ToString() == "Enter")
            {
                return intVal;
            }
            else
            {
                int input;
                if (false == Int32.TryParse(cki.KeyChar.ToString(), out input))
                {
                    return AskForInteger(question, intVal);
                }
                else
                {
                    try
                    {
                        intVal = intVal * 10 + input;
                    }
                    catch (System.OverflowException) { }
                    return AskForInteger(question, intVal);
                }
            }
        }

        public void Message(string message)
        {
            new SelectMenu.SelectMenu("OK").setTitle(message).select();
        }
        public string AskForString(string question)
        {
            Console.WriteLine(question);
            string input = Console.ReadLine();
            return input;
        }
        #endregion
    }
}
