/*
 * Author: Stefan Sander
 * Since: 09.02.2019
 */
using System;

namespace SelectMenu
{
	public class ColorMenu: StyleInterface
	{
		#region properties
		private ConsoleColor _foregroundSelected = ConsoleColor.Green;
		private ConsoleColor _foregroundNotSelected = Console.ForegroundColor;
		private ConsoleColor _backgroundSelected = Console.BackgroundColor;
		private ConsoleColor _backgroundNotSelected = Console.BackgroundColor;
		#endregion
		
		#region accessors
		public ColorMenu setSelectedForgroundColor(ConsoleColor color) {
			this._foregroundSelected=color;
			return this;
		}
		public ColorMenu setNotSelectedForgroundColor(ConsoleColor color) {
			this._foregroundNotSelected=color;
			return this;
		}
		public ColorMenu setSelectedBackgroundColor(ConsoleColor color) {
			this._backgroundSelected=color;
			return this;
		}
		public ColorMenu setNotSelectedBackgroundColor(ConsoleColor color) {
			this._backgroundNotSelected=color;
			return this;
		}
		#endregion
		
		#region constructors
		public ColorMenu()
		{
		}
		#endregion
		
		#region workers
		private void Write(string line) {
			Console.WriteLine(line);
			Console.ResetColor();
		}
		public void ElementSelected(string caption) {
			Console.ForegroundColor  = this._foregroundSelected;
			Console.BackgroundColor = this._backgroundSelected;
			this.Write("  " + caption);
		}
		public void ElementNotSelected(string caption) {
			Console.ForegroundColor  = this._foregroundNotSelected;
			Console.BackgroundColor = this._backgroundNotSelected;
			this.Write(" " + caption);
		}
		#endregion
	}
}
