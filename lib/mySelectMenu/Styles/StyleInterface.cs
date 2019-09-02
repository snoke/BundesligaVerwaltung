/*
 * Author: Stefan Sander
 * Since: 09.02.2019
 */
using System;

namespace SelectMenu
{
	public interface StyleInterface
	{
		 void ElementSelected(string caption);
		 void ElementNotSelected(string caption);
	}
}
