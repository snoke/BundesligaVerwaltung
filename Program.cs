/*
 * Created by SharpDevelop.
 * User: Stefan
 * Date: 02.09.2019
 * Time: 18:29
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using BundesligaVerwaltung.Controller;
namespace BundesligaVerwaltung
{
	class Program
	{
		public static void Main(string[] args)
		{
        	DefaultController defaultController = new DefaultController();
          	defaultController.Run();
		}
	}
}