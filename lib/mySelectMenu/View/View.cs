/*
 * Author: Stefan Sander
 * Since: 09.02.2019
 */
using System;

namespace SelectMenu
{
    public class View
    {
        #region properties
        private StyleInterface _style;
        private LanguageInterface _language;
        #endregion

        #region accessors
        #endregion

        #region constructors
        public View setLanguage(LanguageInterface language)
        {
            _language = language;
            return this;
        }
        public View setStyle(StyleInterface style)
        {
            _style = style;
            return this;
        }
        public View(StyleInterface style, LanguageInterface language)
        {
            setLanguage(language);
            setStyle(style);
        }

        private void header(string[] title)
        {
            Console.Clear();
            if (title != null)
            {
                foreach (string e in title)
                {
                    Console.WriteLine(e);
                }
            }
        }

        public ConsoleKeyInfo SelectMenu(SelectElement[] selectElements, int maxElementsPerPage, int pos, int page, string[] title)
        {
            SelectElement[] elements = selectElements;
            int maxPages = (int)Math.Ceiling(elements.Length / (double)maxElementsPerPage);
            while (true)
            {
                header(title);
                int c = 0;
                for (int i = 0; i < elements.Length; i++)
                {
                    if (maxElementsPerPage * page > i && i + 1 > maxElementsPerPage * (page - 1))
                    {
                        c++;
                        if (i == pos)
                        {
                            _style.ElementSelected(elements[i]._caption);
                        }
                        else
                        {
                            _style.ElementNotSelected(elements[i]._caption);
                        }

                    }
                }
                if (maxPages != 1)
                {
                    if (c < maxElementsPerPage && 1 < page)
                    {
                        for (int i = 0; i < maxElementsPerPage - c; i++)
                        {
                            Console.WriteLine();
                        }
                    }
                    Console.WriteLine();
                    Console.WriteLine(_language.PageOfMaxPages(page, maxPages));
                }
                return Console.ReadKey();
            }
        }
        #endregion

        #region workers
        #endregion
    }
}
