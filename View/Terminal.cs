/*
 * Created by SharpDevelop.
 * User: Stefan
 * Date: 31.08.2019
 * Time: 13:12
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace BundesligaVerwaltung.View
{
	public class Terminal
	{
		#region properties
		#endregion

		#region accessors
		#endregion

		#region constructors
		public Terminal()
		{
		}
		public int Menu(string[] options)
		{
			return new SelectMenu.SelectMenu(options).setTitle("Bitte wählen").select();
		}
		public int AskForInteger(string question)
		{
			return AskForInteger(question, 0);
		}
		public int AskForInteger(string question, int intVal)
		{
			Console.Clear();
			if (intVal != 0)
			{
				Console.Write(question + " :" + intVal);
			}
			else
			{
				Console.Write(question + " :");
			}
			ConsoleKeyInfo cki = Console.ReadKey(false);
			if (cki.Key.ToString() == "Backspace")
			{
				return AskForInteger(question, intVal / 10);
			}
			else if (cki.Key.ToString() == "Enter")
			{
				return intVal;
			}
			else
			{
				int input;
				bool success = Int32.TryParse(cki.KeyChar.ToString(), out input);
				if (false == success)
				{
					return AskForInteger(question, intVal);
				}
				else
				{
					try
					{
						intVal = intVal * 10 + input;
					}
					catch (System.OverflowException) { }
					return AskForInteger(question, intVal);
				}
			}
		}

		public void Message(string message)
		{
			new SelectMenu.SelectMenu("OK").setTitle(message).select();
		}
		public string AskForString(string question)
		{
			Console.WriteLine(question);
			string input = Console.ReadLine();
			return input;
		}
		#endregion

		#region workers
		#endregion
	}
}
