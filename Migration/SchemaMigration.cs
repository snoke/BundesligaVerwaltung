using System;
using System.Collections.Generic;
using BundesligaVerwaltung.Repository.DataStorage;

namespace BundesligaVerwaltung.Migration
{
    internal class SchemaMigration
    {
        private EntityRepository _dataStorage;
        private Dictionary<string, Type> _types;

        public EntityRepository DataStorage
        {
            get { return _dataStorage; }
            set { _dataStorage = value; }
        }
        public Dictionary<string, Type> Types
        {
            get { return _types; }
            set { _types = value; }
        }

        public SchemaMigration(EntityRepository dataStorage, Dictionary<string, Type> types)
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

        }

    }
}
