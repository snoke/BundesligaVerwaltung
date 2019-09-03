/*
 * Created by SharpDevelop.
 * User: Stefan
 * Date: 03.09.2019
 * Time: 19:43
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Xml.Linq;
using System.Linq;
using System.Collections.Generic;
using BundesligaVerwaltung.Model;

namespace BundesligaVerwaltung.Repository.DataStorage
{
	public class XmlStrategy:DataStorage
	{
		#region properties
		#endregion
		
		#region accessors
		#endregion
		
		#region constructors
			public XmlStrategy()
			{
			}
		#endregion
		
		#region workers
		private List<object> XElementToList(XElement element) {
			List<object> list = new List<object>();
			foreach(XElement parameter in element.Descendants()) {
				list.Add(parameter.Value);
			}
			return list;
		}
		
		
		public override List<Entity> LoadEntities(Type entityType) {
			XDocument document = XDocument.Load(entityType.Name + ".xml");
			List<Entity> entities = new List<Entity>();
			foreach(XElement element in document.Root.Elements(entityType.Name)) {
				entities.Add((Entity) Activator.CreateInstance(entityType,XElementToList(element)));
			}
			return entities.OrderBy(x => x.id).ToList();
		}
		
		public override void RemoveEntity(Entity entity) {
			XElement root = new XElement("root");
			string name = entity.GetType().Name;
			List<Entity> entities = this.LoadEntities(entity.GetType()).Where(x=> x.id != entity.id).ToList();
			this.SaveList(entities);
		}
		
		private void SaveList(List<Entity> entities) {
			XElement root = new XElement("root");
			string name = "Entity";
			foreach(Entity entity in entities) {
				name = entity.GetType().Name;
				XElement e = new XElement(name);
				List<object> values = entity.GetValues();
				List<string[]> keys = entity.GetKeys();
				for(int i=0;i<keys.Count();i++) {
					e.Add(new XElement(keys[i][0],values[i].ToString()));
				}
				root.Add(e);
			}
			XDocument xdoc = new XDocument();
			xdoc.Add(root);
			xdoc.Save(name+".xml");
		}
		public override void SaveEntity(Entity entity) {
			XElement root = new XElement("root");
			string name = entity.GetType().Name;
			List<Entity> entities = this.LoadEntities(entity.GetType()).Where(x=> x.id != entity.id).ToList();
			entities.Add(entity);
			this.SaveList(entities);
		}
		#endregion
	}
}
