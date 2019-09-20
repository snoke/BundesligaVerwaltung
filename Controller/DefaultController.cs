using System;
using System.Collections.Generic;
using System.Linq;
using BundesligaVerwaltung.Model.Entities;
using BundesligaVerwaltung.View;
using myEntityRepository;
using System.Runtime.InteropServices;
using myEntityRepository.DataAccessObject;

namespace BundesligaVerwaltung.Controller
{
    public class DefaultController
    {
        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);


        #region properties
        private bool _debug;

        //beinhaltet die Typen, welche vom Repository kontrolliert werden
        private List<Type> _entityTypes;

        private EntityRepository repository;
        private UserInterface terminal;
        #endregion
        
        #region accessors
        private bool debug
        {
            get { return _debug; }
            set { _debug = value; }
        }
        private List<Type> EntityTypes
        {
            get { return _entityTypes; }
            set { _entityTypes = value; }
        }

        private EntityRepository Repository
        {
            get { return repository; }
            set { repository = value; }
        }

        private UserInterface Terminal
        {
            get { return terminal; }
            set { terminal = value; }
        }
        private List<League> Leagues
        {
            get
            {
                return Repository.Entities[Type.GetType("BundesligaVerwaltung.Model.Entities.League")].Cast<League>().ToList();
            }
        }
        private List<Team> Teams
        {
            get
            {
                return Repository.Entities[Type.GetType("BundesligaVerwaltung.Model.Entities.Team")].Cast<Team>().Where(x => x.League == League).ToList();
             }
        }
        private List<Match> Matches
        {
            get
            {
                return Repository.Entities[Type.GetType("BundesligaVerwaltung.Model.Entities.Match")].Cast<Match>().ToList();
            }
        }
        private List<Member> Members
        {
            get
            {
                return Repository.Entities[Type.GetType("BundesligaVerwaltung.Model.Entities.Member")].Cast<Member>().ToList();
            }
        }
        private List<Role> Roles
        {
            get
            {
                return Repository.Entities[Type.GetType("BundesligaVerwaltung.Model.Entities.Role")].Cast<Role>().ToList();
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
            if (false==debug)
            {
                ShowWindow(GetConsoleWindow(), 0);
            }
            Terminal = new WindowsFormsUI();

            //Definiere die Typen , welche vom Repository gesteuert werden sollen.
            EntityTypes = new List<Type>()
            {
                 Type.GetType("BundesligaVerwaltung.Model.Entities.League") ,
                 Type.GetType("BundesligaVerwaltung.Model.Entities.Role") ,
                 Type.GetType("BundesligaVerwaltung.Model.Entities.Team"),
                 Type.GetType("BundesligaVerwaltung.Model.Entities.Match") ,
                 Type.GetType("BundesligaVerwaltung.Model.Entities.Member") 
            };
            DataAccessObject dataAccessObject = new SQLiteStrategy("db.sqlite", EntityTypes, debug);
            //DataStorage dataStorage = new SQLiteStrategy("db.sqlite", EntityTypes, debug);
            //DataStorage dataStorage = new MysqlStrategy("localhost","test", "root", "", EntityTypes, debug);
            //DataStorage dataStorage = new XmlStrategyPrototype();
            Repository = new EntityRepository(EntityTypes, dataAccessObject, debug);
            this.DefaultMigration();
        }
        #endregion

        #region workers
        private void DefaultMigration()
            {
            if (false==Leagues.Any(x => x.Name == "Bundesliga"))
            {
                Repository.Save(new League((int?)null,"Bundesliga",18));
            } else { }

            if (false == Roles.Any(x => x.Name == "Physio"))
            {
                Repository.Save(new Role((int?)null, "Physio"));
            }
            else { }

            if (false == Roles.Any(x => x.Name == "Trainer"))
            {
                Repository.Save(new Role((int?)null, "Trainer"));
            }
            else { }

            if (false == Roles.Any(x => x.Name == "Spieler"))
            {
                Repository.Save(new Role((int?)null, "Spieler"));
            } else { }
            Repository.Flush();
        }
        private void Scoreboard()
        {
            int choice = Terminal.MainMenu(Matches, Teams, new string[] { "Tabelle anzeigen", "Spielergebnis hinzufügen", "Team hinzufügen", "Team entfernen", "Mitglied hinzufügen", "Mitglied Teamwechsel", "Mitglied entfernen", "Programm beenden" });

           // int choice = Terminal.MainMenu(Matches, Teams, new string[] { "Spielergebnis hinzufügen", "Team hinzufügen", "Team entfernen", "Mitglied hinzufügen", "Mitglied Teamwechsel", "Mitglied entfernen", "Programm beenden" }, "Bitte wählen");
            if (choice==1)
            {
                Repository.Pull();
                Scoreboard();
            } else {}
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
                int choice = terminal.MainMenu(Matches, Teams, new string[] { "Tabelle anzeigen", "Spielergebnis hinzufügen", "Team hinzufügen", "Team entfernen", "Mitglied hinzufügen", "Mitglied Teamwechsel", "Mitglied entfernen", "Programm beenden" });
                if (choice != 7)
                {
                    if (choice == 0)
                    {
                        //Tabelle aktualisieren
                        if (Repository.IsDirty()) //TODO: insert dirty joke here!
                        {
                            switch (Terminal.Menu(new string[] { "Speichern", "Verwerfen" }, "Sie haben ungespeicherte Änderungen\nMöchten Sie diese Speichern oder Verwerfen?"))
                            {
                                case 0:
                                    Repository.Flush();
                                    break;
                                default:
                                    break;
                            }
                        }
                        MainMenu();
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
                        if (possibleTeams.Count()<2)
                        {
                            throw new Exception.NoElementsException();
                        } else
                        {}

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
                        MainMenu();
                    }
                    else if (choice == 2)
                    {
                        //Team hinzufügen
                        if (Teams.Count() < League.MaximumTeams)
                        {
                            Repository.Save(new Team(null, Terminal.AskForString("Team Name"), League));
                        } else {
                            Terminal.Message("Maximal " + League.MaximumTeams + " Teams");
                        }
                        MainMenu();
                    }
                    else if (choice == 3)
                    {
                        //Team entfernen
                        if (Teams.Count() < 1)
                        {
                            throw new Exception.NoElementsException();
                        } else { }
                        Team team = Teams[Terminal.Menu(Teams.Select(i => i.Name).ToArray(), "Team löschen")];
                        team.League = null;
                        Repository.Save(team);
                        MainMenu();
                    }
                    else if (choice == 4)
                    {
                        //Spieler entfernen
                        if (Teams.Count() < 1 )
                        {
                            throw new Exception.NoElementsException();
                        } else { }
                        //Spieler hinzufügen
                        Repository.Save(
                            new Member(
                                null,
                                Terminal.AskForString("Name"),
                                Teams[Terminal.Menu(Teams.Select(x => x.Name).ToArray(), "Bitte Team wählen")],
                                Roles[Terminal.Menu(Roles.Select(x => x.Name).ToArray(), "Bitte Rolle wählen")])
                            );
                        MainMenu();
                    }
                    else if (choice == 5)
                    {
                        //Spieler entfernen
                        if (Teams.Count() < 2 || Members.Count() < 1)
                        {
                            throw new Exception.NoElementsException();
                        } else { }
                        //Spieler Teamwechsel
                        Team team = Teams[Terminal.Menu(Teams.Select(x => x.Name).ToArray(), "Bitte wählen")];
                        List<Member> teamMembers = Members.Where(x => x.Team == team).ToList();
                        Member member = teamMembers[Terminal.Menu(teamMembers.Select(x => " [" + x.Role.Name + "] " + x.Name).ToArray(), "Bitte wählen")];
                        member.Team = Teams.Where(x => x != member.Team).ToArray()[Terminal.Menu(Teams.Where(x => x != member.Team).Select(x => x.Name).ToArray(), "Bitte wählen")];
                        Repository.Save(member);
                        MainMenu();
                    }
                    else if (choice == 6)
                    {
                        //Spieler entfernen
                        if (Teams.Count()<1 || Members.Count()<1)
                        {
                            throw new Exception.NoElementsException();
                        }
                        else { }

                        Team team = Teams[Terminal.Menu(Teams.Select(x => x.Name).ToArray(), "Bitte wählen")];
                        List<Member> teamMembers = Members.Where(x => x.Team == team).ToList();
                        Member member = teamMembers[Terminal.Menu(teamMembers.Select(x => " [" + x.Role.Name + "] " + x.Name).ToArray(), "Bitte wählen")];
                        member.Team = null;
                        Repository.Save(member);
                        MainMenu();
                    }
                    else
                    { }
                }
                else
                {
                    Repository.Flush();
                    if (debug) {
                        //wait a moment to view debug logs
                        System.Threading.Thread.Sleep(1000);
                    } else { }
                }
            } catch (Exception.NoElementsException)
            {
                Terminal.Message("Fehler\n Die Liste ist leer!");
                MainMenu();
            }
        }
        #endregion
    }
}
