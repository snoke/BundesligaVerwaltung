using System.Collections.Generic;
using System.Linq;
using BundesligaVerwaltung.Model;
using BundesligaVerwaltung.Repository;
using BundesligaVerwaltung.Repository.DataStorage;
using BundesligaVerwaltung.View;
namespace BundesligaVerwaltung.Controller
{
	internal class TestController
	{
		private readonly EntityRepository Repository;
		private readonly Terminal Terminal;

		private List<Team> _teams;
		private List<Member> _members;
		private List<Match> _matches;

		private List<Team> Teams
		{
			get
			{
				//lazy loading
				if (_teams == null)
				{
					_teams = Repository.LoadTeams();
				}
				else { }
				return _teams;
			}
			set
			{
				_teams = value;
			}
		}
		private List<Match> Matches
		{
			get
			{
				//lazy loading
				if (_matches == null)
				{
					_matches = Repository.LoadMatches();
				}
				else { }
				return _matches;
			}
			set
			{
				_matches = value;
			}
		}
		private List<Member> Members
		{
			get
			{
				//lazy loading
				if (_members == null)
				{
					_members = Repository.LoadMembers();
				}
				else { }
				return _members;
			}
			set
			{
				_members = value;
			}
		}


		public TestController()
		{
			//this.Repository = new EntityRepository(new XmlStrategy());
			Repository = new EntityRepository(new SQLiteStrategy("db.sqlite"));
			Terminal = new Terminal();
		}

		private void __MainMenu()
		{
			string[] menuElements = new string[Teams.Count + 2];
			for (int i = 0; i < Teams.Count; i++)
			{
				menuElements[i] = Teams[i].Name;
			}
			menuElements[Teams.Count] = "Verein hinzufügen";
			menuElements[Teams.Count + 1] = "Tabelle anzeigen";
			int choice = new SelectMenu.SelectMenu(menuElements).setTitle("Bitte wählen").select();
			if (choice == Teams.Count)
			{
				//Add
				Teams.Add(
					new Team(
						Teams.Count,
						Terminal.AskForString("Bitte geben Sie einen Vereinsnamen ein"))
				);
				Repository.SaveTeams(Teams);
				MainMenu();
			}
			else if (choice == Teams.Count + 1)
			{
				//Tabelle anzeigen
			}
			else
			{
				TeamMenu(Teams[choice]);
			}
		}

		private void TeamMenu(Team team)
		{
			List<Member> members = new List<Member>();
			foreach (Member member in Members)
			{
				if (member.Teamid == team.id)
				{
					members.Add(member);
				}
			}
			string[] menuElements = new string[members.Count + 3];

			for (int i = 0; i < members.Count; i++)
			{
				menuElements[i] = members[i].Name;
			}
			menuElements[members.Count] = "Neues Mitglied hinzufügen";
			menuElements[members.Count + 1] = "Spielergebnis hinzufügen";
			menuElements[members.Count + 2] = "Zurück";
			int choice = new SelectMenu.SelectMenu(menuElements).setTitle(team.Name + "\n" + "".PadLeft(20, (char)'-') + "\nBitte wählen").select();

			if (choice == members.Count)
			{
				switch (new SelectMenu.SelectMenu("Spieler", "Trainer", "Physio", "Zurück").setTitle("Bitte eine Rolle wählen").select())
				{
					case 0:
						Members.Add(new Member(
							Members.Count,
							Terminal.AskForString("Bitte geben Sie einen Namen ein"),
							team.id,
                            "Player"
						));
						Repository.SaveMembers(Members);
						MainMenu();
						break;
					case 1:
						Members.Add(new Member(
							Members.Count,
							Terminal.AskForString("Bitte geben Sie einen Namen ein"),
							team.id,
                            "Trainer"
                        ));
						Repository.SaveMembers(Members);
						MainMenu();
						break;
					case 2:
						Members.Add(new Member(
							Members.Count,
							Terminal.AskForString("Bitte geben Sie einen Namen ein"),
							team.id,
                            "Physio"
                        ));
						Repository.SaveMembers(Members);
						MainMenu();
						break;
					case 3:
						TeamMenu(team);
						break;
				}
			}
			else if (choice == members.Count + 1)
			{
				if (team.GetMatches().Count() < Teams.Min(x => x.GetMatches().Count() + 1))
				{
					List<Team> opponentTeams = Teams.Where(x => x.id != team.id).ToList();
					string[] opponentElements = new string[opponentTeams.Count + 1];
					for (int i = 0; i < opponentTeams.Count; i++)
					{
						opponentElements[i] = opponentTeams[i].Name;
					}

					opponentElements[opponentTeams.Count] = "Zurück";
					int selection = new SelectMenu.SelectMenu(opponentElements).setTitle(team.Name + "\n" + "".PadLeft(20, (char)'-') + "\nBitte Gastmannschaft wählen").select();
					if (selection == opponentTeams.Count)
					{
						TeamMenu(team);
					}
					else
					{
						Matches.Add(new Match(
							Matches.Count(),
							team.id,
							opponentTeams[selection].id,
							Terminal.AskForInteger("Bitte (Heim)Tore eingeben"),
							Terminal.AskForInteger("Bitte Gegentore eingeben")
						));
						Repository.SaveMatches(Matches);
						Teams = Repository.LoadTeams();
						MainMenu();
					}
				}
				else
				{
					Terminal.Message("Es darf für jeden Spieltag nur 1 Spiel eingetragen werden!");
					TeamMenu(team);
				}

			}
			else if (choice == members.Count + 2)
			{
				MainMenu();
			}
			else
			{
				MemberMenu(members[choice]);
			}
		}

		public void MemberMenu(Member member)
		{
			switch (new SelectMenu.SelectMenu("Verein wechseln", "Zurück").setTitle("Bitte wählen").select())
			{
				case 1:
					TeamMenu(Teams[member.Teamid]);
					break;
				case 0:
					string[] menuElements = new string[Teams.Count + 1];
					for (int i = 0; i < Teams.Count; i++)
					{
						menuElements[i] = Teams[i].Name;
					}
					menuElements[Teams.Count] = "Zurück";
					int teamChoice = new SelectMenu.SelectMenu(menuElements).setTitle("Bitte wählen").select();
					if (teamChoice == Teams.Count)
					{
						MemberMenu(member);
					}
					else
					{
						Members[member.id].Teamid = teamChoice;
						Repository.SaveMembers(Members);
						MainMenu();
					}
					break;
			}
		}
		public void Tabelle()
		{
			List<Team> teams = Teams.OrderByDescending(x => GetTeamPoints(x)).ToList();
			//	string output="Spieltag:" + teams.Min(x => x.GetMatches().Count()+1) + "\n";
			string output = "";
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
					//output = output + (values[i].PadLeft(length)+ " | ");
				}
				elements.Add(line);
				//output = output + ("\n");
			}
			elements.Add("Tabelle aktualisieren");
			elements.Add("Team hinzufügen");
			elements.Add("Programm beenden");
			int choice = new SelectMenu.SelectMenu((string[])elements.ToArray()).setTitle(output).select();
			if (choice == elements.Count() - 3)
			{
				Teams = Repository.LoadTeams();
				Tabelle();
			}
			else if (choice == elements.Count() - 1)
			{
			}
			else if (choice == elements.Count() - 2)
			{
				//do nothing, but close program
			}
			else
			{
				TeamMenu(teams[choice]);
			}
		}
		public void MainMenu()
		{
			Tabelle();
		}
		public void Run()
		{
			MainMenu();
		}
	}
}
