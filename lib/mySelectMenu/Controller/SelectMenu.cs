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

        public SelectMenu setLanguage(LanguageInterface language)
        {
            _language = language;
            return this;
        }

        public SelectMenu setLanguage(string language)
        {
            switch (language.ToLower())
            {
                case "german":
                    _language = new German();
                    break;
                case "english":
                    _language = new English();
                    break;
            }
            return this;
        }
        public SelectMenu setStyle(string style)
        {
            switch (style.ToLower())
            {
                case "color":
                    _style = new ColorMenu();
                    break;
                case "prefix":
                    _style = new PrefixMenu();
                    break;
            }
            return this;
        }
        public SelectMenu setStyle(StyleInterface style)
        {
            _style = style;
            return this;
        }
        public SelectMenu setTitle(params string[] title)
        {
            _title = title;
            return this;
        }

        public SelectMenu setMaxElementsPerPage(int maxElementsPerPage)
        {
            _maxElementsPerPage = maxElementsPerPage;
            return this;
        }
        #endregion

        #region constructors
        public SelectMenu(SelectElement[] selectElements)
        {
            Initialize(selectElements);
        }
        public SelectMenu(params string[] stringArray)
        {
            Initialize(generateElementsByStrings(stringArray));
        }
        #endregion

        #region workers
        private void Initialize(SelectElement[] selectElements)
        {
            setLanguage(new German());
            setStyle(new ColorMenu());
            _selectElements = selectElements;
        }

        public int call()
        {
            SelectElement element = handle();
            if (element._callable != null)
            {
                element._callable();
            }
            else { }
            return element._id;
        }
        public int select()
        {
            if (_selectElements.Length < 1)
            {
                throw new NoElementsException();
            }
            else
            {

            }
            return handle()._id;
        }
        public SelectElement handle()
        {
            View View = new View(_style, _language);
            while (true)
            {
                ConsoleKeyInfo UserInput = View.SelectMenu(_selectElements, _maxElementsPerPage, _pos, _page, _title);

                if ((UserInput.Key.ToString() == "DownArrow") || (UserInput.Key.ToString() == "RightArrow"))
                {
                    _pos++;
                }
                else if ((UserInput.Key.ToString() == "UpArrow") || (UserInput.Key.ToString() == "LeftArrow"))
                {
                    _pos--;
                }
                else if (UserInput.Key.ToString() == "Enter")
                {
                    Console.Clear();
                    return _selectElements[_pos];
                }

                if (_pos > _selectElements.Length - 1)
                {
                    _pos = 0;
                }
                else if (_pos < 0)
                {
                    _pos = _selectElements.Length - 1;
                }

                while (_pos + 1 > _maxElementsPerPage * _page)
                {
                    _page++;
                }
                while (_pos + 1 < _maxElementsPerPage * _page - _maxElementsPerPage + 1)
                {
                    _page--;
                }
            }
        }
        private SelectElement[] generateElementsByStrings(string[] stringArray)
        {
            SelectElement[] selectElements = new SelectElement[stringArray.Length];
            int i = 0;
            foreach (string e in stringArray)
            {
                selectElements[i] = new SelectElement(e, i);
                i++;
            }
            return selectElements;
        }
        #endregion
    }
}
