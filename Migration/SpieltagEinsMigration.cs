using System.Collections.Generic;
using System.Linq;
using BundesligaVerwaltung.Model.Entities;

using myEntityRepository.DataStorage;
using myEntityRepository;
namespace BundesligaVerwaltung.Migration
{
    internal class SpieltagEinsMigration
    {
        private EntityRepository _repository;
        private EntityRepository Repository
        {
            get { return _repository; }
            set { _repository = value; }
        }

        private List<Team> _teams;
        private List<Team> Teams
        {
            get { return _teams; }
            set { _teams = value; }
        }

        public SpieltagEinsMigration(EntityRepository repository, List<Team> teams)
        {
            Repository = repository;
            Teams = teams;
        }

        public void up()
        {
            // Die Informationen entstammen offiziellen Webseite des DFB und sind frei zugänglich!
            // https://www.dfb.de/bundesliga
            Repository.Save(new Match(
                null,
                Teams.Single(x => x.Name == "FC Bayern München"),
                Teams.Single(x => x.Name == "Hertha BSC"),
                2,
                2
            ));
            Repository.Save(new Match(
                null,
                Teams.Single(x => x.Name == "Borussia Dortmund"),
                Teams.Single(x => x.Name == "FC Augsburg"),
                5,
                1
            ));
            Repository.Save(new Match(
                null,
                Teams.Single(x => x.Name == "Bayer 04 Leverkusen"),
                Teams.Single(x => x.Name == "SC Paderborn 07"),
                3,
                2
            ));
            Repository.Save(new Match(
                null,
                Teams.Single(x => x.Name == "VfL Wolfsburg"),
                Teams.Single(x => x.Name == "1.FC Köln"),
                2,
                1

            ));
            Repository.Save(new Match(
                null,
                Teams.Single(x => x.Name == "SV Werder Bremen"),
                Teams.Single(x => x.Name == "Fortuna Düsseldorf"),
                1,
                3
            ));
            Repository.Save(new Match(
                null,
                Teams.Single(x => x.Name == "SC Freiburg"),
                Teams.Single(x => x.Name == "1.FSV Mainz 05"),
                3, 0


            ));
            Repository.Save(new Match(
                null,
                Teams.Single(x => x.Name == "Borussia Mönchengladbach"),
                Teams.Single(x => x.Name == "FC Schalke 04"),
                0, 0


            ));
            Repository.Save(new Match(
                null,
                Teams.Single(x => x.Name == "Eintracht Frankfurt"),
                Teams.Single(x => x.Name == "TSG 1899 Hoffenheim"),
                1, 0


            ));
            Repository.Save(new Match(
                null,
                Teams.Single(x => x.Name == "1.FC Union Berlin"),
                Teams.Single(x => x.Name == "RB Leipzig"),
                0, 4
            ));

        }

    }
}
