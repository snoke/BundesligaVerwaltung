/*
 * Created by SharpDevelop.
 * User: Stefan
 * Date: 02.09.2019
 * Time: 18:29
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using BundesligaVerwaltung.Controller;
namespace BundesligaVerwaltung
{
	internal class Program
	{
		public static void Main(string[] args)
		{
			// new TestController().Run();
			new DefaultController().Run();
			//      	DefaultController defaultController = new DefaultController();
			//      	defaultController.Run();
		}
	}
}