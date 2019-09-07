/*
 * Author: Stefan Sander
 * Since: 10.02.2019
 */

namespace SelectMenu
{
    public class English : LanguageInterface
    {
        #region properties
        #endregion

        #region accessors
        #endregion

        #region constructors
        public English()
        {
        }
        #endregion

        #region workers
        public string PageOfMaxPages(int page, int maxPages)
        {
            return "page " + page + " of " + maxPages;
        }
        #endregion
    }
}
