using System.Collections.Generic;
using System.Linq;
using BundesligaVerwaltung.Model.Entities;
using BundesligaVerwaltung.Repository;

namespace BundesligaVerwaltung.Migration
{
    internal class TeamsMigration
    {
        private EntityRepository _repository;
        public EntityRepository Repository { get => _repository; set => _repository = value; }

        private List<Team> _teams;
        private List<League> _leagues;
        public List<Team> Teams { get => _teams; set => _teams = value; }
        public List<League> Leagues { get => _leagues; set => _leagues = value; }

        public TeamsMigration(EntityRepository repository, List<Team> teams, List<League> leagues)
        {
            Repository = repository;
            Teams = teams;
            Leagues = leagues;
        }


        public void up()
        {
            // Die Informationen entstammen der offiziellen Webseite des DFB und sind frei zugänglich
            // https://www.dfb.de/bundesliga
            League league = Leagues.Single(x => x.Name == "Bundesliga");
            Repository.Save(new Team(null, "FC Bayern München", league));
            Repository.Save(new Team(null, "Borussia Dortmund", league));
            Repository.Save(new Team(null, "RB Leipzig", league));
            Repository.Save(new Team(null, "Bayer 04 Leverkusen", league));
            Repository.Save(new Team(null, "Borussia Mönchengladbach", league));
            Repository.Save(new Team(null, "TSG 1899 Hoffenheim", league));
            Repository.Save(new Team(null, "VfL Wolfsburg", league));
            Repository.Save(new Team(null, "Hertha BSC", league));
            Repository.Save(new Team(null, "FC Schalke 04", league));
            Repository.Save(new Team(null, "Eintracht Frankfurt", league));
            Repository.Save(new Team(null, "SV Werder Bremen", league));
            Repository.Save(new Team(null, "1.FSV Mainz 05", league));
            Repository.Save(new Team(null, "FC Augsburg", league));
            Repository.Save(new Team(null, "SC Freiburg", league));
            Repository.Save(new Team(null, "1.FC Köln", league));
            Repository.Save(new Team(null, "Fortuna Düsseldorf", league));
            Repository.Save(new Team(null, "1.FC Union Berlin", league));
            Repository.Save(new Team(null, "SC Paderborn 07", league));
        }

    }
}
