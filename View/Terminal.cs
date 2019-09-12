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
using BundesligaVerwaltung.Model.Entities;
using BundesligaVerwaltung.View;

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
        public string Scoreboard(List<Match> matches, List<Team> teams)
        {
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
            return "Spieltag: " + (gameDay + 1) + "\n" + Table(header);
        }

        public string Table(List<string[]> rows)
        {
            string output = "";

            List<int> longest = new List<int>();
            foreach (string[] row in rows)
            {
                while (longest.Count() < row.Count())
                {
                    longest.Add(0);
                }
                for (int i = 0; i < row.Count(); i++)
                {
                    if (longest[i] < row[i].Length)
                    {
                        longest[i] = row[i].Length;
                    }
                }
            }

            foreach (string[] row in rows)
            {
                for (int i = 0; i < row.Length; i++)
                {
                    output += row[i].PadLeft(longest[i]) + " |";
                }
                output += "\n";
            }
            return output;
        }
        public int Menu(string[] options, string header)
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
            Console.Write(question + " :" + intVal);
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
            return Console.ReadLine();
        }
        public void SplashScreen()
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
