/*
 * Author: Stefan Sander
 * Since: 09.02.2019
 */
using System;

namespace SelectMenu
{
    public class ColorMenu : StyleInterface
    {
        #region properties
        private ConsoleColor _foregroundSelected = ConsoleColor.Green;
        private ConsoleColor _foregroundNotSelected = Console.ForegroundColor;
        private ConsoleColor _backgroundSelected = Console.BackgroundColor;
        private ConsoleColor _backgroundNotSelected = Console.BackgroundColor;
        #endregion

        #region accessors
        public ColorMenu setSelectedForgroundColor(ConsoleColor color)
        {
            _foregroundSelected = color;
            return this;
        }
        public ColorMenu setNotSelectedForgroundColor(ConsoleColor color)
        {
            _foregroundNotSelected = color;
            return this;
        }
        public ColorMenu setSelectedBackgroundColor(ConsoleColor color)
        {
            _backgroundSelected = color;
            return this;
        }
        public ColorMenu setNotSelectedBackgroundColor(ConsoleColor color)
        {
            _backgroundNotSelected = color;
            return this;
        }
        #endregion

        #region constructors
        public ColorMenu()
        {
        }
        #endregion

        #region workers
        private void Write(string line)
        {
            Console.WriteLine(line);
            Console.ResetColor();
        }
        public void ElementSelected(string caption)
        {
            Console.ForegroundColor = _foregroundSelected;
            Console.BackgroundColor = _backgroundSelected;
            Write("  " + caption);
        }
        public void ElementNotSelected(string caption)
        {
            Console.ForegroundColor = _foregroundNotSelected;
            Console.BackgroundColor = _backgroundNotSelected;
            Write(" " + caption);
        }
        #endregion
    }
}
