/*
 * Author: Stefan Sander
 * Since: 09.02.2019
 */

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
            _caption = caption;
        }
        public SelectElement(string caption, int id)
        {
            _caption = caption;
            _id = id;
        }
        public SelectElement(string caption, MenuChoiceCallable callable)
        {
            _caption = caption;
            _callable = callable;
        }
        #endregion

        #region workers
        #endregion
    }
}
