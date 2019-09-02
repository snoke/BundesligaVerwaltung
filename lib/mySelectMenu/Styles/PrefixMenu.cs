/*
 * Author: Stefan Sander
 * Since: 09.02.2019
 */
using System;

namespace SelectMenu
{
	public class PrefixMenu: StyleInterface
	{
		#region properties
		private string _prefixElementNotSelected="[ ] ";
		private string _prefixElementSelected="[X] ";
		#endregion
		
		#region accessors
		public PrefixMenu setSelectedPrefix(string prefix) {
			this._prefixElementSelected=prefix;
			return this;
		}
		public PrefixMenu setNotSelectedPrefix(string prefix) {
			this._prefixElementNotSelected=prefix;
			return this;
		}
		#endregion
		
		#region constructors
		public PrefixMenu()
		{
		}
		#endregion
		
		#region workers
		public void ElementSelected(string line) {
			Console.WriteLine(this._prefixElementSelected + line);
		}
		public void ElementNotSelected(string line) {
			Console.WriteLine(this._prefixElementNotSelected + line);
		}
		#endregion
	}
}
