using System;
using System.Collections.Generic;
using BundesligaVerwaltung.Model.Entities;
using BundesligaVerwaltung.Repository.DataStorage;

namespace BundesligaVerwaltung.Migration
{
    internal class SchemaMigration
    {
        private DataStorage _dataStorage;
        private Dictionary<string, Type> _types;
        public DataStorage DataStorage { get => _dataStorage; set => _dataStorage = value; }
        public Dictionary<string, Type> Types { get => _types; set => _types = value; }

        public SchemaMigration(DataStorage dataStorage, Dictionary<string, Type> types)
        {
            DataStorage = dataStorage;
            Types = types;
        }


        public void up()
        {
            foreach (KeyValuePair<string, Type> type in Types)
            {
                DataStorage.CreateSchema(type.Value);
            }

            DataStorage.SaveEntity(new Role(null, "Spieler"));
            DataStorage.SaveEntity(new Role(null, "Trainer"));
            DataStorage.SaveEntity(new Role(null, "Physio"));
            DataStorage.SaveEntity(new League(null, "Bundesliga", 18));

        }

    }
}
