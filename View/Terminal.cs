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
        public int Scoreboard(List<Team> teams)
        {
            string output = "Spieltag:" + teams.Min(x => x.GetMatches().Count() + 1) + "\n";
            string[] cells = { "Mannschaft".PadLeft(24), "Spiele", "Siege", "Unentschieden", "Niederlagen", "Tore", "Punkte" };
            for (int i = 0; i < cells.Length; i++)
            {
                output += cells[i] + " | ";
            }
            output += "\n";
            for (int i = 0; i < cells.Length; i++)
            {
                output += ("".PadRight(cells[i].Length, (char)'-') + "---");
            }
            List<string> elements = new List<string>();
            foreach (Team team in teams)
            {
                string line = "";
                string name = team.GetName();
                string[] values = {
                        name,
                        team.GetMatches().Count().ToString(),
                        team.GetWins().Count().ToString(),
                        team.GetDraws().Count().ToString(),
                        team.GetLosses().Count().ToString(),
                        team.GetGoals().ToString(),
                        team.GetPoints().ToString(),
                    };
                for (int i = 0; i < values.Length; i++)
                {
                    int length = cells[i].Length;
                    if (values[i].Length > length)
                    {
                        values[i] = values[i].Substring(0, length);
                    }
                    line = line + (values[i].PadLeft(length) + " | ");
                }
                elements.Add(line);
            }
            return Menu((string[])elements.ToArray(), output);
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
                bool success = Int32.TryParse(cki.KeyChar.ToString(), out input);
                if (false == success)
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
