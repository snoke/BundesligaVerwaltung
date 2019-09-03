/*
 * Created by SharpDevelop.
 * User: Stefan
 * Date: 03.09.2019
 * Time: 19:15
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Data.SQLite;
using System.Linq;
using System.Collections.Generic;
using BundesligaVerwaltung.Model;

namespace BundesligaVerwaltung.Repository.DataStorage
{
	public class SQLiteStrategy:DataStorage
	{
		#region properties
		private SQLiteConnection _dbConnection;
    	private SQLiteConnection dbConnection
	    {
	        get {
    			//lazy loading
    			if (this._dbConnection==null) {
    				this._dbConnection = new SQLiteConnection("Data Source = db.sqlite; Version = 3;");
					this._dbConnection.Open();
    			} else {}
    			return this._dbConnection;
	        } 
	    }
		#endregion
		
		#region accessors
		#endregion
		
		#region constructors
			public SQLiteStrategy()
			{
			}
		#endregion
		
		#region workers
		private List<List<object>> query(string sql) {
			SQLiteCommand Command = new SQLiteCommand(sql, this.dbConnection);
			SQLiteDataReader reader = Command.ExecuteReader();
			
			List<List<object>> rows = new List<List<object>>();
			while (reader.Read())
			{
				List<object> row = new List<object>();
				for(int i = 0;i<reader.FieldCount;i++) {
					row.Add(reader[i]);
				}
				rows.Add(row);
			}
			 
			reader.Close();
			reader.Dispose();
			Command.Dispose();
			return rows;
		}
		
		
		public override void SaveEntities(Entity[] entities) {
			foreach(Entity e in entities) {
				string sql = "REPLACE INTO " +e.GetType().Name+"(";
				List<string[]> keys = e.GetKeys();
				foreach(string[] k in keys) {
					sql +=  k[0] + ",";
				}
				sql = sql.Substring(0,sql.Length-1) + ") VALUES(";
				int i = 0;
				foreach(object v in e.GetValues()) {
					if (keys[i][1]=="STRING") {
						sql += "'" + v.ToString() + "',";
					} else {
						sql += v.ToString() + ",";
					}
					i++;
				}
				sql = sql.Substring(0,sql.Length-1) + ");";
				this.query(sql);
			}
		}
		
		public override List<Entity> LoadEntities(Type entityType) {
			List<Entity> list = new List<Entity>();
			foreach(List<object> row in  this.query("SELECT * FROM "+entityType.Name+";")) {
				var obj = Activator.CreateInstance(entityType,row);
				list.Add((Entity) obj);
			}
			return list;
		}
		#endregion
	}
}
