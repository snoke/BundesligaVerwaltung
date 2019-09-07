/*
 * Author: Stefan Sander
 * Since: 10.02.2019
 */

namespace SelectMenu
{
    public class German : LanguageInterface
    {
        #region properties
        #endregion

        #region accessors
        #endregion

        #region constructors
        public German()
        {
        }
        #endregion

        #region workers
        public string PageOfMaxPages(int page, int maxPages)
        {
            return "Seite " + page + " von " + maxPages;
        }
        #endregion
    }
}
