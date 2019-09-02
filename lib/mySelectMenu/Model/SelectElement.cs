/*
 * Author: Stefan Sander
 * Since: 09.02.2019
 */
using System;

namespace SelectMenu
{
	
	public class SelectElement
	{
		#region properties
		public string _caption;
		public int _id;
		public MenuChoiceCallable _callable;
		#endregion
		
		#region accessors
		#endregion
		
		#region constructors
			public SelectElement(string caption)
			{
				this._caption = caption;
			}
			public SelectElement(string caption,int id)
			{
				this._caption = caption;
				this._id = id;
			}
			public SelectElement(string caption,MenuChoiceCallable callable)
			{
				this._caption = caption;
				this._callable = callable;
			}
		#endregion
		
		#region workers
		#endregion
	}
}
