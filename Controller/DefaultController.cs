using System;
using System.Collections.Generic;
using System.Linq;
using BundesligaVerwaltung.Model.Entities;
using BundesligaVerwaltung.Repository;
using BundesligaVerwaltung.View;
using BundesligaVerwaltung;
namespace BundesligaVerwaltung.Controller
{
    public class DefaultController
    {
        #region properties
        private bool _debug;

        //beinhaltet die Typen, welche vom Repository kontrolliert werden
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
        private List<League> Leagues
        {
            get
            {
                return Repository.Entities[EntityTypes["League"]].Cast<League>().ToList();
            }
        }
        private List<Team> Teams
        {
            get
            {
                return Repository.Entities[EntityTypes["Team"]].Cast<Team>().Where(x => x.League == League).ToList();
            }
        }
        private List<Match> Matches
        {
            get
            {
                return Repository.Entities[EntityTypes["Match"]].Cast<Match>().ToList();
            }
        }
        private List<Member> Members
        {
            get
            {
                return Repository.Entities[EntityTypes["Member"]].Cast<Member>().ToList();
            }
        }
        private List<Role> Roles
        {
            get
            {
                return Repository.Entities[EntityTypes["Role"]].Cast<Role>().ToList();
            }
        }
        //die aktive Liga
        private League League
        {
            get
            {
                return Leagues.Single(x => x.Name == "Bundesliga");
            }
        }
        #endregion

        #region constructors
        public DefaultController()
        {
          //  debug = false;
          debug = true;

            Terminal = new Terminal();

            //Definiere die Typen , welche vom Repository gesteuert werden sollen.
            EntityTypes = new Dictionary<string, Type>
            {
                { "League", Type.GetType("BundesligaVerwaltung.Model.Entities.League") },
                { "Role", Type.GetType("BundesligaVerwaltung.Model.Entities.Role") },
                { "Team", Type.GetType("BundesligaVerwaltung.Model.Entities.Team") },
                { "Match", Type.GetType("BundesligaVerwaltung.Model.Entities.Match") },
                { "Member", Type.GetType("BundesligaVerwaltung.Model.Entities.Member") }
            };
            Repository = new EntityRepository(EntityTypes, debug);
            //Repository.DefaultMigration();
            Repository.Pull();
        }
        #endregion

        #region workers
        private void Scoreboard()
        {
            if (0 == Terminal.Menu(
                new string[] { "Tabelle aktualisieren", "zurück zum Hauptmenü" },
                Terminal.Scoreboard(Matches, Teams) + "\nBitte wählen"))
            {
                Repository.Flush();
                Repository.Pull();
                Scoreboard();
            }
            else { }
        }
        public void Run()
        {
          //  this.Repository.CreateSchema(Type.GetType("BundesligaVerwaltung.Model.Entities.Match"));
          //  this.Repository.Flush();
          //  this.Repository.Pull();
            Terminal.SplashScreen();
            MainMenu();
        }

        private void MainMenu()
        {
            try
            {
                int choice = Terminal.Menu(new string[] { "Tabelle anzeigen", "Spielergebnis hinzufügen", "Team hinzufügen", "Team entfernen", "Mitglied hinzufügen", "Mitglied Teamwechsel", "Mitglied entfernen", "Programm beenden" }, "Bitte wählen");
                if (choice != 7)
                {

                    if (choice == 0)
                    {
                        //Tabelle anzeigen
                        Scoreboard();
                    }
                    else if (choice == 1)
                    {
                        //Spielergebnis hinzufügen
                        int? day = null; //spieltag
                        foreach (Team team in Teams)
                        {
                            int teamMatchesAmount = Matches.Where(x => x.Team == team || x.Opponent == team).Count();
                            if (day == null || day > teamMatchesAmount)
                            {
                                day = teamMatchesAmount;
                            }
                            else
                            {

                            }
                        }
                        int gameday;
                        if ((int?)day == null)
                        {
                            gameday = 1;
                        } else
                        {
                            gameday = (int)day;
                        }
                        List<Team> possibleTeams = Teams.Where(x => (Matches.Where(y => y.Team == x || y.Opponent == x).Count() <= gameday)).ToList();
                        Team HomeTeam = possibleTeams[Terminal.Menu(possibleTeams.Select(x => x.Name).ToArray(), "Bitte Team wählen")];

                        List<Team> possibleGuests = possibleTeams.Where(x => x != HomeTeam).ToList();
                        Team GuestTeam = possibleGuests[Terminal.Menu(possibleGuests.Select(x => x.Name).ToArray(), "Bitte Team wählen")];


                        Match match = new Match(
                            null,
                            HomeTeam,
                            GuestTeam,
                            Terminal.AskForInteger("Tore eingeben"),
                            Terminal.AskForInteger("Gegentore Tore eingeben")
                        );
                        Repository.Save(match);


                    }
                    else if (choice == 2)
                    {
                        //Team hinzufügen
                        if (Teams.Count() < League.MaximumTeams)
                        {
                            Repository.Save(new Team(null, Terminal.AskForString("Team Name"), League));
                        }
                        else
                        {
                            Terminal.Message("Maximal " + League.MaximumTeams + " Teams");
                        }
                    }
                    else if (choice == 3)
                    {
                        //Team entfernen
                        Team team = Teams[Terminal.Menu(Teams.Select(i => i.Name).ToArray(), "Team löschen")];
                        team.League = null;
                        Repository.Save(team);
                    }
                    else if (choice == 4)
                    {
                        //Spieler hinzufügen
                        Repository.Save(
                            new Member(
                                null,
                                Terminal.AskForString("Name"),
                                Teams[Terminal.Menu(Teams.Select(x => x.Name).ToArray(), "Bitte Team wählen")],
                                Roles[Terminal.Menu(Roles.Select(x => x.Name).ToArray(), "Bitte Rolle wählen")])
                            );
                    }
                    else if (choice == 5)
                    {
                        //Spieler Teamwechsel
                        Team team = Teams[Terminal.Menu(Teams.Select(x => x.Name).ToArray(), "Bitte wählen")];
                        List<Member> teamMembers = Members.Where(x => x.Team == team).ToList();
                        Member member = teamMembers[Terminal.Menu(teamMembers.Select(x => " [" + x.Role.Name + "] " + x.Name).ToArray(), "Bitte wählen")];
                        member.Team = Teams.Where(x => x != member.Team).ToArray()[Terminal.Menu(Teams.Where(x => x != member.Team).Select(x => x.Name).ToArray(), "Bitte wählen")];
                        Repository.Save(member);

                    }
                    else if (choice == 6)
                    {
                        //Spieler entfernen
                        Team team = Teams[Terminal.Menu(Teams.Select(x => x.Name).ToArray(), "Bitte wählen")];
                        List<Member> teamMembers = Members.Where(x => x.Team == team).ToList();
                        Member member = teamMembers[Terminal.Menu(teamMembers.Select(x => " [" + x.Role.Name + "] " + x.Name).ToArray(), "Bitte wählen")];
                        member.Team = null;
                        Repository.Save(member);

                    }
                    else
                    { }
                    MainMenu();
                }
                else
                {
                    Repository.Flush();
                }
            }
            catch (SelectMenu.NoElementsException)
            {
                Terminal.Message("Fehler\n Die Liste ist leer!");
                MainMenu();
            }
        }
        #endregion
    }
}
