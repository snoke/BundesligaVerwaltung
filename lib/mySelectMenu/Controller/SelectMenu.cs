/*
 * Author: Stefan Sander
 * Since: 09.02.2019
 */
using System;

namespace SelectMenu
{
	public class SelectMenu
	{
		
		#region properties
		private StyleInterface _style;
		private LanguageInterface _language;
		
		private int _page = 1;
		private int _pos = 0;
		private int _maxElementsPerPage = 24;
		private SelectElement[] _selectElements;
		private string[] _title;
		#endregion
		
		#region accessors
		
		public SelectMenu setLanguage(LanguageInterface language) {
			this._language = language;
			return this;
		}
		
		public SelectMenu setLanguage(string language) {
			switch (language.ToLower()) {
		 		case "german":
					this._language = new German();
					break;
				case "english":
					this._language = new English();
					break;
			 }
			return this;
		}
		public SelectMenu setStyle(string style) {
			switch (style.ToLower()) {
				case "color":
					this._style = new ColorMenu();
					break;
				case "prefix":
					this._style = new PrefixMenu();
					break;
			}
			return this;
		}
		public SelectMenu setStyle(StyleInterface style) {
			this._style=style;
			return this;
		}
		public SelectMenu setTitle(params string[] title) {
			this._title = title;
			return this;
		}
		
		public SelectMenu setMaxElementsPerPage(int maxElementsPerPage) {
			this._maxElementsPerPage = maxElementsPerPage;
			return this;
		}
		#endregion
		
		#region constructors
		public SelectMenu(SelectElement[] selectElements)
		{
		 	this.Initialize(selectElements);
		}
		public SelectMenu(params string[] stringArray)
		{
		 	this.Initialize(this.generateElementsByStrings(stringArray));
		}
		#endregion
		
		#region workers
		private void Initialize(SelectElement[] selectElements) {
			this.setLanguage(new German());
			this.setStyle(new ColorMenu());
		 	this._selectElements = selectElements;
		}
		
		public int call() {
			SelectElement element = this.handle();
			if (element._callable!=null) {
				element._callable();
			} else {}
			return element._id;
		}
		public int select() {
			return this.handle()._id;
		}
		public SelectElement handle() {
			View View = new View(this._style,this._language);
			while (true) {
				ConsoleKeyInfo UserInput = View.SelectMenu(this._selectElements,this._maxElementsPerPage,this._pos,this._page,this._title); 
				
				if ((UserInput.Key.ToString() == "DownArrow") || (UserInput.Key.ToString() == "RightArrow")) {
			    	this._pos++;
				} else if ((UserInput.Key.ToString() == "UpArrow") || (UserInput.Key.ToString() == "LeftArrow")) {
				   	this._pos--;
				} else if (UserInput.Key.ToString() == "Enter") {
					Console.Clear();
		       		return this._selectElements[this._pos];
				}
				
		        if (this._pos > this._selectElements.Length-1) {
		        	this._pos=0;
		        } else if (this._pos < 0) {
		        	this._pos = this._selectElements.Length-1;
				}
				
		       	while (this._pos+1 >this._maxElementsPerPage*this._page) {
		    		this._page++;
				}
				while (this._pos+1 < this._maxElementsPerPage*this._page-this._maxElementsPerPage+1) {
			    	this._page--;
				}
		 	}
		}
		private SelectElement[]  generateElementsByStrings(string[] stringArray) {
			SelectElement[] selectElements = new SelectElement[stringArray.Length];
			int i = 0;
			foreach(string e in stringArray) {
				selectElements[i] = new SelectElement(e,i);
				i++;
			}
			return selectElements;
		}
		#endregion
	}
}
