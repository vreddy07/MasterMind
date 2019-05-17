using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleMastermindGame
{
	class Program
	{
		const int MAX_NUMBER = 6666;
		const int MIN_NUMBER = 1111;
		const int MAX_INDIV_NUMBER = 6;
		const int MIN_INDIV_NUMBER = 1;


		static void Main(string[] args)
		{
			//Intro Sequence & Game Setup
			Console.WriteLine("Welcome to my world of Mastermind Gaming!");
			Console.WriteLine("You have 10 chances to guess the answer correctly\n");
			System.Threading.Thread.Sleep(2000);

			int intGuesses = 10;
			int intSecretCode = GenerateFourDigitCode();

			bool winState = false;
			bool showValidationMessage = false;


			Console.Clear();


			while (intGuesses > 0)
			{
				if (intGuesses != 10)
				{
					Console.WriteLine("Guesses Remaining: " + intGuesses.ToString());
				}
				if (showValidationMessage)
				{
					Console.WriteLine("\nMake sure your input is between 1111 and 6666, with each digit being no larger that 6.");
					System.Threading.Thread.Sleep(2000);
					showValidationMessage = false;
				}
				Console.WriteLine("\nMake your guess:\n");
				int intUserGuess = 0;
				string strUserGuess = Console.ReadLine();


				if (IsGuessCorrectFormat(ref strUserGuess, intSecretCode))
				{
					intUserGuess = Int32.Parse(strUserGuess);

					if (intUserGuess == intSecretCode) //Game has been won.
					{
						winState = true;
						break;
					}

					int inPlaceCount = GetInPlaceDigitCount(intUserGuess.ToString(), intSecretCode.ToString());
					int OutPlaceDigitCount = GetOutPlaceDigitCount(intUserGuess.ToString(), intSecretCode.ToString());

					string strFeedback = "\nScore: ";

					//Switch statement builds feedback string.
					#region Switch statement w/cases
					switch (inPlaceCount)
					{
						case 0:
							break;
						case 1:
							strFeedback += "+";
							break;
						case 2:
							strFeedback += "++";
							break;
						case 3:
							strFeedback += "+++";
							break;
					}
					switch (4 - inPlaceCount - OutPlaceDigitCount)  // out place digits are digits which are not existing and inplace are digits which are in correct place
					{
						case 0:
							break;
						case 1:
							strFeedback += "-";
							break;
						case 2:
							strFeedback += "--";
							break;
						case 3:
							strFeedback += "---";
							break;
						case 4:
							strFeedback += "----";
							break;
					}
					#endregion

					Console.WriteLine(strFeedback + "\n");
					Console.WriteLine("--------------------\n");
					intGuesses--;
				}
				else
				{
					showValidationMessage = true;
					intGuesses--;
				}
			}
			if (winState)
			{
				Console.WriteLine("--------------------\n");
				Console.WriteLine("\nYou solved it!");
			}
			else
			{
				Console.WriteLine("\nYou lose. :(\n");
				Console.WriteLine("The code was " + intSecretCode);
			}
			Console.ReadLine();
		}
		#region Functions

		/// Calculates the number of digits that are correct and in place.

		private static int GetInPlaceDigitCount(string intUserGuess, string intSecretCode)
		{

			int inPlaceCount = 0;
			for (int i = 0; i < 4; i++)
			{
				if (intUserGuess[i] == intSecretCode[i])
				{
					inPlaceCount++;
				}
			}
			return inPlaceCount;
		}

		/// Calculates the number of digits that are not existing.
		private static int GetOutPlaceDigitCount(string intUserGuess, string intSecretCode)
		{

			int outPlaceCount = 0;
			for (int i = 0; i < 4; i++)
			{
				if (intUserGuess[i] != intSecretCode[i] && !intSecretCode.ToString().Contains(intUserGuess[i]))
				{
					outPlaceCount++;
				}
			}
			return outPlaceCount;
		}


		/// Checks to see if the user guess is correct.

		private static bool IsGuessCorrectFormat(ref string strUserGuess, int intSecretCode)
		{
			int intUserGuess = 0;
			try
			{
				intUserGuess = Int32.Parse(strUserGuess);
				int guessDigit = 0;
				int tempGuess = intUserGuess;
				if (tempGuess < 1000 || tempGuess > 6666 || strUserGuess.Contains('0'))
				{
					return false;
				}
				for (int i = 0; i < 4; i++)
				{
					guessDigit = tempGuess % 10;
					if (guessDigit > MAX_INDIV_NUMBER || guessDigit < MIN_INDIV_NUMBER || intUserGuess < MIN_NUMBER || intUserGuess > MAX_NUMBER)
					{
						throw (new Exception());
					}
				}
			}
			catch
			{
				return false;
			}
			return true;
		}

		/// Generates a code from 1111 to 6666

		private static int GenerateFourDigitCode()
		{
			string strCraftedNum = "";
			Random srand = new Random();
			int length = 4;
			for (int i = 0; i < length; i++)
			{
				strCraftedNum += srand.Next(MIN_INDIV_NUMBER, MAX_INDIV_NUMBER + 1);
			}
			return Int32.Parse(strCraftedNum);
		}

		#endregion
	}
}