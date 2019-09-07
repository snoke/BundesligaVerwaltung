using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BundesligaVerwaltung.Repository;
using BundesligaVerwaltung.Model;
namespace BundesligaVerwaltung.Migration
{
    class TeamsMigration
    {
        private EntityRepository _repository;
        public EntityRepository Repository { get => _repository; set => _repository = value; }

        public TeamsMigration(EntityRepository repository)
        {
            this.Repository = repository;
        }


        public void up()
        {
            // Die Informationen entstammen der offiziellen Webseite des DFB und sind frei zugänglich
            // https://www.dfb.de/bundesliga
            Repository.Save(new Team((int?)null, "FC Bayern München"));
            Repository.Save(new Team((int?)null, "Borussia Dortmund"));
            Repository.Save(new Team((int?)null, "RB Leipzig"));
            Repository.Save(new Team((int?)null, "Bayer 04 Leverkusen"));
            Repository.Save(new Team((int?)null, "Borussia Mönchengladbach"));
            Repository.Save(new Team((int?)null, "TSG 1899 Hoffenheim"));
            Repository.Save(new Team((int?)null, "VfL Wolfsburg"));
            Repository.Save(new Team((int?)null, "Hertha BSC"));
            Repository.Save(new Team((int?)null, "FC Schalke 04"));
            Repository.Save(new Team((int?)null, "Eintracht Frankfurt"));
            Repository.Save(new Team((int?)null, "SV Werder Bremen"));
            Repository.Save(new Team((int?)null, "1.FSV Mainz 05"));
            Repository.Save(new Team((int?)null, "FC Augsburg"));
            Repository.Save(new Team((int?)null, "SC Freiburg"));
            Repository.Save(new Team((int?)null, "1.FC Köln"));
            Repository.Save(new Team((int?)null, "Fortuna Düsseldorf"));
            Repository.Save(new Team((int?)null, "1.FC Union Berlin"));
            Repository.Save(new Team((int?)null, "SC Paderborn 07"));
        }

    }
}
