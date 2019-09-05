using System.Collections.Generic;
using System.Linq;
using System;
using BundesligaVerwaltung.Model;
using BundesligaVerwaltung.Model.Members;
using BundesligaVerwaltung.Repository;
using BundesligaVerwaltung.Repository.DataStorage;
using BundesligaVerwaltung.View;
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
		public void Scoreboard()
		{
			List<Team> teams = Repository.Teams.OrderByDescending(x => x.GetPoints()).ThenByDescending(x=>x.GetGoals()).ToList();
            int choice = Terminal.Scoreboard(teams);
            if (choice!=teams.Count()) {
                Playerlist(teams[choice]);
            } else {

            }
        }
        public void Playerlist(Team team)
        {
            List<string> options = new List<string>();

            foreach (Member member in Repository.Members.OrderBy(x => x.Name).ToList())
            {
                    options.Add(member.Name);
            }
            int choice = Terminal.Menu(options.ToArray(),"Spieler wählen");
            if (choice != options.Count())
            {
            }
            else
            {

            }
        }
        public void Run()
		{
            Scoreboard();
		}
		#endregion
	}
}
