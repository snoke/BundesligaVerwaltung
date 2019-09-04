using System.Collections.Generic;
using System.Linq;
using BundesligaVerwaltung.Model;
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
		public void Tabelle()
		{
			List<Team> teams = Repository.teams.OrderByDescending(x => x.GetWins().Count()).ToList();
			string output = "Spieltag:" + teams.Min(x => x.GetMatches().Count() + 1) + "\n";
			string[] cells = { "Mannschaft".PadLeft(24), "Spiele", "Siege", "Unentschieden", "Niederlagen", "Tore" };
			for (int i = 0; i < cells.Length; i++)
			{
				string message = cells[i];
				output = output + (message + " | ");
			}
			output = output + ("\n");
			for (int i = 0; i < cells.Length; i++)
			{
				string message = "".PadRight(cells[i].Length, (char)'-');
				output = output + (message + "---");
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
			elements.Add("Tabelle aktualisieren");
			elements.Add("Team hinzufügen");
			elements.Add("Programm beenden");
			int choice = new SelectMenu.SelectMenu((string[])elements.ToArray()).setTitle(output).select();
		}
		public void MainMenu()
		{
			Tabelle();
		}
		public void Run()
		{
			MainMenu();
		}
		#endregion
	}
}
