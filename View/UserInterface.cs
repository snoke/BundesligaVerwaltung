using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BundesligaVerwaltung.Model.Entities;

namespace BundesligaVerwaltung.View
{
    public abstract class UserInterface
    {
        public abstract int Menu(string[] options, string header);
        public abstract void SplashScreen();
        public abstract int AskForInteger(string question);
        public abstract string AskForString(string message);
        public abstract void Message(string message);
        public abstract int Scoreboard(List<Match>matches,List<Team>teams);
    }
}
