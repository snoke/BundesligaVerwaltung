using System.Collections.Generic;
using System.Linq;
using System;
using BundesligaVerwaltung.Model;
using BundesligaVerwaltung.Repository;
using BundesligaVerwaltung.View;
using System.Reflection;
namespace BundesligaVerwaltung.Controller
{
    internal class DefaultController
    {
        #region properties
        private bool _debug;
        private Dictionary<string, Type> _entityTypes;
        private EntityRepository repository;
        private Terminal terminal;
        #endregion


        #region accessors
        private bool debug
        {
            get { return _debug; }
            set { _debug = value; }
        }
        private Dictionary<string, Type> EntityTypes
        {
            get { return _entityTypes; }
            set { _entityTypes = value; }
        }

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
        private List<Team> Teams
        {
            get {
                return Repository.Entities[this.EntityTypes["Team"]].Cast<Team>().ToList();
           }
        }
        private List<Match> Matches
        {
            get
            {
                return Repository.Entities[this.EntityTypes["Match"]].Cast<Match>().ToList();
            }
        }
        private List<Member> Members
        {
            get
            {
                return Repository.Entities[this.EntityTypes["Member"]].Cast<Member>().ToList();
            }
        }
        private List<Role> Roles
        {
            get
            {
                return Repository.Entities[this.EntityTypes["Role"]].Cast<Role>().ToList();
            }
        }
        #endregion

        #region constructors
        public DefaultController()
        {
            this.debug = true;
            this.EntityTypes = new Dictionary<string, Type>();
            this.EntityTypes.Add("Team", Type.GetType("BundesligaVerwaltung.Model.Team"));
            this.EntityTypes.Add("Role", Type.GetType("BundesligaVerwaltung.Model.Role"));
            this.EntityTypes.Add("Match", Type.GetType("BundesligaVerwaltung.Model.Match"));
            this.EntityTypes.Add("Member", Type.GetType("BundesligaVerwaltung.Model.Member"));
            Repository = new EntityRepository(this.EntityTypes,this.debug);
            Terminal = new Terminal();
        }
        #endregion

        #region workers
        private void Scoreboard()
        {
            if (0 == Terminal.Menu(
                new string[] { "Tabelle aktualisieren", "zurück zum Hauptmenü" },
                Terminal.Scoreboard(Matches, Teams) + "\nBitte wählen"))
            {
                Scoreboard();
            }
            else { }
        }
        public void Run()
        {
            MainMenu();
        }
        private void MainMenu()
        {
            try
            {

                int choice = Terminal.Menu(new string[] { "Tabelle anzeigen", "Spielergebnis hinzufügen", "Team hinzufügen", "Team entfernen", "Mitglied hinzufügen", "Mitglied Teamwechsel", "Mitglied entfernen", "Programm beenden" }, "Bitte wählen");

                if (choice == 0)
                {
                    //Tabelle anzeigen
                    Repository.Refresh();
                    Scoreboard();
                    Run();
                }
                else if (choice == 1)
                {
                    //Spielergebnis hinzufügen
                    Team homeTeam = Teams[Terminal.Menu(Teams.Select(x => x.Name).ToArray(), "Bitte Team wählen")];
                    List<Team> possibleGuests = Teams.Where(x => x != homeTeam).ToList();
                    Match match = new Match(
                        (int?)null,
                        (Team)homeTeam,
                        (Team)possibleGuests[Terminal.Menu(possibleGuests.Select(x => x.Name).ToArray(), "Bitte Team wählen")],
                        (int)Terminal.AskForInteger("Tore eingeben"),
                        (int)Terminal.AskForInteger("Gegentore Tore eingeben")
                    );

                    Repository.Save(match);
                    Repository.Refresh();
                    Run();
                }
                else if (choice == 2)
                {
                    //Team hinzufügen
                    if (Teams.Count()<18)
                    {
                        Repository.Save(new Team((int?)null, Terminal.AskForString("Team Name")));
                        Repository.Refresh();

                    } else
                    {
                        Terminal.Message("Maximal 18 Teams");
                    }
                    Run();
                }
                else if (choice == 3)
                {
                    //Team entfernen
                    Repository.Remove(Teams[Terminal.Menu(Teams.Select(i => i.Name).ToArray(), "Team löschen")]);
                    Repository.Refresh();
                    Run();
                }
                else if (choice == 4)
                {
                    //Spieler hinzufügen
                    Repository.Save(
                        new Member(
                            (int?)null,
                            Terminal.AskForString("Name"),
                            (Team)Teams[Terminal.Menu(Teams.Select(x => x.Name).ToArray(), "Bitte Team wählen")],
                            Roles[Terminal.Menu(Roles.Select(x => x.Name).ToArray(), "Bitte Rolle wählen")])
                        );
                    Repository.Refresh();
                    Run();
                }
                else if (choice == 5)
                {
                    //Spieler Teamwechsel
                    Team team = Teams[Terminal.Menu(Teams.Select(x => x.Name).ToArray(), "Bitte wählen")];
                    List<Member> teamMembers = Members.Where(x => x.Team == team).ToList();
                    Member member = teamMembers[Terminal.Menu(teamMembers.Select(x => x.Name).ToArray(), "Bitte wählen")];
                    member.Team = Teams.Where(x => x != member.Team).ToArray()[Terminal.Menu(Teams.Where(x => x != member.Team).Select(x => x.Name).ToArray(), "Bitte wählen")];
                    Repository.Save(member);
                    Repository.Refresh();
                    Run();
                }
                else if (choice == 6)
                {
                    //Spieler entfernen
                    Team team = Teams[Terminal.Menu(Teams.Select(x => x.Name).ToArray(), "Bitte wählen")];
                    List<Member> teamMembers = Members.Where(x => x.Team == team).ToList();
                    Member member = teamMembers[Terminal.Menu(teamMembers.Select(x => x.Name).ToArray(), "Bitte wählen")];
                    Repository.Remove(member);
                    Run();
                }
                else
                {
                    //Programm beenden
                }
            } catch(SelectMenu.NoElementsException)
            {
                Terminal.Message("Fehler: Die Liste ist leer!");
                Run();
            }
        }
        #endregion
    }
}
