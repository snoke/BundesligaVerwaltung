using System.Collections.Generic;
using System.Linq;
using System;
using BundesligaVerwaltung.Model;
using BundesligaVerwaltung.Repository;
using BundesligaVerwaltung.Repository.DataStorage;
using BundesligaVerwaltung.View;
using System.Reflection;
namespace BundesligaVerwaltung.Controller
{
	internal class DefaultController
	{
		#region properties
		private EntityRepository repository;
		private Terminal terminal;
		#endregion


		#region accessors
		private EntityRepository Repository
		{
			get { return repository; }
			set { repository = value; }
		}

		private Terminal Terminal
		{
			get { return terminal; }
			set { terminal = value; }
		}
		#endregion

		#region constructors
		public DefaultController()
		{
			//this.Repository = new EntityRepository(new XmlStrategy());
			Repository = new EntityRepository(new SQLiteStrategy("db.sqlite"));
			Terminal = new Terminal();
		}
        #endregion

        #region workers
		private void Scoreboard()
		{
            if (Terminal.Menu(new string[] { "Tabelle aktualisieren", "zurück zum Hauptmenü" }, Terminal.Scoreboard(Repository.Matches, Repository.Teams) + "\nBitte wählen") == 0)
            {
                Repository.Reload();
                Scoreboard();
            } else  {  }
        }
        public void Run()
		{
            int choice = Terminal.Menu(new string[] { "Tabelle anzeigen", "Team hinzufügen", "Team entfernen", "Programm beenden" }, "Bitte wählen");
          
            switch (choice)
            {
                case 0: this.Scoreboard(); break;
                case 1: Repository.Save(new Team((int?)null, Terminal.AskForString("Team Name"))); break;
                case 2: Repository.Remove(Repository.Teams[Terminal.Menu(Repository.Teams.Select(i => i.Name).ToArray(), "Team löschen")]); break;
            }
            if (choice==3)
            {

            } else
            {
                this.Run();
            }
		}
		#endregion
	}
}
