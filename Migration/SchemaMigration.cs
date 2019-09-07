using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BundesligaVerwaltung.Repository;
using BundesligaVerwaltung.Model;
namespace BundesligaVerwaltung.Migration
{
    class SchemaMigration
    {
        private EntityRepository _repository;
        public EntityRepository Repository { get => _repository; set => _repository = value; }

        public SchemaMigration(EntityRepository repository)
        {
            this.Repository = repository;
        }


        public void up()
        {
            Repository.CreateSchema(Type.GetType("BundesligaVerwaltung.Model.Member"));
            Repository.CreateSchema(Type.GetType("BundesligaVerwaltung.Model.Match"));
            Repository.CreateSchema(Type.GetType("BundesligaVerwaltung.Model.Team"));
            Repository.CreateSchema(Type.GetType("BundesligaVerwaltung.Model.Role"));

            Repository.Save(new Role((int?)null, "Spieler"));
            Repository.Save(new Role((int?)null, "Trainer"));
            Repository.Save(new Role((int?)null, "Physio"));

        }
        
    }
}
