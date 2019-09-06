/*
 * Created by SharpDevelop.
 * User: Stefan
 * Date: 03.09.2019
 * Time: 19:15
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using BundesligaVerwaltung.Model;
using System.Reflection;

namespace BundesligaVerwaltung.Repository.DataStorage
{
	public class SQLiteStrategy : DataStorage
	{
		#region properties
		private string _filename;
		private string filename
		{
			get
			{
				return _filename;
			}
			set
			{
				_filename = value;
			}

		}
		private SQLiteConnection _dbConnection;
		private SQLiteConnection dbConnection
		{
			get  
			{
				//lazy loading
				if (_dbConnection == null) {
					_dbConnection = new SQLiteConnection("Data Source =" + filename + "; Version = 3;");
					_dbConnection.Open();
				} else { }
				return _dbConnection;
			}
		}
		#endregion

		#region accessors
		#endregion

		#region constructors
		public SQLiteStrategy(string filename)
		{
			this.filename = filename;
        }
        #endregion

        #region workers
        private List<List<string>> query(string sql)
		{
			SQLiteCommand Command = new SQLiteCommand(sql, dbConnection);
			SQLiteDataReader reader = Command.ExecuteReader();
			List<List<string>> rows = new List<List<string>>();

			while (reader.Read())
			{
				List<string> row = new List<string>();
				for (int i = 0; i < reader.FieldCount; i++)
				{
					row.Add(reader[i].ToString());
				}
				rows.Add(row);
			}

			reader.Close();
			reader.Dispose();
			Command.Dispose();
			return rows;
		}

		public override void SaveEntity(Entity e)
		{
            Type eType = e.GetType();
			string sql = "REPLACE INTO " + eType.Name + "(";
            List<PropertyInfo> properties = eType.GetProperties().Reverse().ToList();
			foreach (PropertyInfo property in properties)
			{
				sql += property.Name.ToLower() + ",";
			}
			sql = sql.Substring(0, sql.Length - 1) + ") VALUES(";
            foreach (PropertyInfo property in properties)
            {
                object v = property.GetValue(e,null);

                if (null == v)
                {
                    sql += "null" + ",";

                }
                else
                {
                    sql += "'" + v.ToString() + "',";

                }
            }

			sql = sql.Substring(0, sql.Length - 1) + ");";
        //    Console.WriteLine(sql);
			query(sql);
		}

		public void SaveEntities(Entity[] entities)
		{
			foreach (Entity entity in entities)
			{
				SaveEntity(entity);
			}
		}

		public override List<Entity> LoadEntities(Type entityType)
		{
            List<List<string>> entities = query("SELECT * FROM " + entityType.Name + ";");
            List<Entity> list = new List<Entity>();
			foreach (List<string> row in entities)
			{
				list.Add((Entity)Activator.CreateInstance(entityType, row.ToArray()));
			}
			return list.ToList();
		}

		public override void RemoveEntity(Entity entity)
		{
			query("DELETE FROM " + entity.GetType().Name + " WHERE id=" + entity.id + ";");
		}
		#endregion
	}
}
