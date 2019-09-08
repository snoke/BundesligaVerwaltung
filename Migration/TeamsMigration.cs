using System.Collections.Generic;
using System.Linq;
using BundesligaVerwaltung.Model.Entities;
using BundesligaVerwaltung.Repository.DataStorage;

namespace BundesligaVerwaltung.Migration
{
    internal class TeamsMigration
    {
        private DataStorage _dataStorage;
        public DataStorage DataStorage { get => _dataStorage; set => _dataStorage = value; }

        private List<Team> _teams;
        private List<League> _leagues;
        public List<Team> Teams { get => _teams; set => _teams = value; }
        public List<League> Leagues { get => _leagues; set => _leagues = value; }

        public TeamsMigration(DataStorage dataStorage, List<Team> teams, List<League> leagues)
        {
            DataStorage = dataStorage;
            Teams = teams;
            Leagues = leagues;
        }


        public void up()
        {
            // Die Informationen entstammen der offiziellen Webseite des DFB und sind frei zugänglich
            // https://www.dfb.de/bundesliga
            League league = Leagues.Single(x => x.id == 1);
            DataStorage.SaveEntity(new Team(null, "FC Bayern München", league));
            DataStorage.SaveEntity(new Team(null, "Borussia Dortmund", league));
            DataStorage.SaveEntity(new Team(null, "RB Leipzig", league));
            DataStorage.SaveEntity(new Team(null, "Bayer 04 Leverkusen", league));
            DataStorage.SaveEntity(new Team(null, "Borussia Mönchengladbach", league));
            DataStorage.SaveEntity(new Team(null, "TSG 1899 Hoffenheim", league));
            DataStorage.SaveEntity(new Team(null, "VfL Wolfsburg", league));
            DataStorage.SaveEntity(new Team(null, "Hertha BSC", league));
            DataStorage.SaveEntity(new Team(null, "FC Schalke 04", league));
            DataStorage.SaveEntity(new Team(null, "Eintracht Frankfurt", league));
            DataStorage.SaveEntity(new Team(null, "SV Werder Bremen", league));
            DataStorage.SaveEntity(new Team(null, "1.FSV Mainz 05", league));
            DataStorage.SaveEntity(new Team(null, "FC Augsburg", league));
            DataStorage.SaveEntity(new Team(null, "SC Freiburg", league));
            DataStorage.SaveEntity(new Team(null, "1.FC Köln", league));
            DataStorage.SaveEntity(new Team(null, "Fortuna Düsseldorf", league));
            DataStorage.SaveEntity(new Team(null, "1.FC Union Berlin", league));
            DataStorage.SaveEntity(new Team(null, "SC Paderborn 07", league));
        }

    }
}
