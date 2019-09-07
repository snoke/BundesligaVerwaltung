using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BundesligaVerwaltung.Repository;
using BundesligaVerwaltung.Model;
namespace BundesligaVerwaltung.Migration
{
    class SpieltagEinsMigration
    {
        private EntityRepository _repository;
        public EntityRepository Repository { get => _repository; set => _repository = value; }

        public SpieltagEinsMigration(EntityRepository repository)
        {
            this.Repository = repository;
        }

        public void up()
        {
            // Die Informationen entstammen offiziellen Webseite des DFB und sind frei zugänglich!
            // https://www.dfb.de/bundesliga
            List<Team> teams = Repository.Load(Type.GetType("BundesligaVerwaltung.Model.Team")).Cast<Team>().ToList();
            Repository.Save(new Match(
                (int?)null,
                teams.SingleOrDefault(x => x.Name == "FC Bayern München"),
                teams.SingleOrDefault(x => x.Name == "Hertha BSC"),
                2,
                2
            ));
            Repository.Save(new Match(
                (int?)null,
                teams.SingleOrDefault(x => x.Name == "Borussia Dortmund"),
                teams.SingleOrDefault(x => x.Name == "FC Augsburg"),
                5,
                1
            ));
            Repository.Save(new Match(
                (int?)null,
                teams.SingleOrDefault(x => x.Name == "Bayer 04 Leverkusen"),
                teams.SingleOrDefault(x => x.Name == "SC Paderborn 07"),
                3,
                2
            ));
            Repository.Save(new Match(
                (int?)null,
                teams.SingleOrDefault(x => x.Name == "VfL Wolfsburg"),
                teams.SingleOrDefault(x => x.Name == "1.FC Köln"),
                2,
                1

            ));
            Repository.Save(new Match(
                (int?)null,
                teams.SingleOrDefault(x => x.Name == "SV Werder Bremen"),
                teams.SingleOrDefault(x => x.Name == "Fortuna Düsseldorf"),
                1,
                3
            ));
            Repository.Save(new Match(
                (int?)null,
                teams.SingleOrDefault(x => x.Name == "SC Freiburg"),
                teams.SingleOrDefault(x => x.Name == "1.FSV Mainz 05"),
                3, 0


            ));
            Repository.Save(new Match(
                (int?)null,
                teams.SingleOrDefault(x => x.Name == "Borussia Mönchengladbach"),
                teams.SingleOrDefault(x => x.Name == "FC Schalke 04"),
                0, 0


            ));
            Repository.Save(new Match(
                (int?)null,
                teams.SingleOrDefault(x => x.Name == "Eintracht Frankfurt"),
                teams.SingleOrDefault(x => x.Name == "TSG 1899 Hoffenheim"),
                1, 0


            ));
            Repository.Save(new Match(
                (int?)null,
                teams.SingleOrDefault(x => x.Name == "1.FC Union Berlin"),
                teams.SingleOrDefault(x => x.Name == "RB Leipzig"),
                0, 4
            ));

        }

    }
}
