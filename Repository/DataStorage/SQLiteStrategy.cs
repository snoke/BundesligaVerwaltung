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
				if (_dbConnection == null)
				{
					_dbConnection = new SQLiteConnection("Data Source =" + filename + "; Version = 3;");
					_dbConnection.Open();
				}
				else { }
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
		private List<List<object>> query(string sql)
		{
			SQLiteCommand Command = new SQLiteCommand(sql, dbConnection);
			SQLiteDataReader reader = Command.ExecuteReader();
			List<List<object>> rows = new List<List<object>>();

			while (reader.Read())
			{
				List<object> row = new List<object>();
				for (int i = 0; i < reader.FieldCount; i++)
				{
					row.Add(reader[i]);
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
			string sql = "REPLACE INTO " + e.GetType().Name + "(";
			List<string[]> keys = e.GetKeys();
			foreach (string[] k in keys)
			{
				sql += k[0] + ",";
			}
			sql = sql.Substring(0, sql.Length - 1) + ") VALUES(";
			int i = 0;
			foreach (object v in e.GetValues())
			{
				if (keys[i][1] == "STRING")
				{
					sql += "'" + v.ToString() + "',";
				}
				else
				{
					sql += v.ToString() + ",";
				}
				i++;
			}
			sql = sql.Substring(0, sql.Length - 1) + ");";
			query(sql);
		}

		public void SaveEntities(Entity[] entities)
		{
			foreach (Entity e in entities)
			{
				SaveEntity(e);
			}
		}

		public override List<Entity> LoadEntities(Type entityType)
		{
            List<List<object>> entities = query("SELECT * FROM " + entityType.Name + ";");
            List<Entity> list = new List<Entity>();
			foreach (List<object> row in entities)
			{
				list.Add((Entity)Activator.CreateInstance(entityType, row));
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
