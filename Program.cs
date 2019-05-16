using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterMindGame
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
			Console.WriteLine("You have 10 changes to guess the answer correctly\n");
			System.Threading.Thread.Sleep(2000);

			int intGuesses = 10;
			int intSecretCode = GenerateFourDigitCode();

			bool winState = false;

			bool[] guessAry = { false, false, false, false };
			bool[] answerAry = { false, false, false, false };
			Console.Clear();

			//Guesses Loop
			while (intGuesses > 0)
			{
				if (intGuesses != 10)
				{
					Console.WriteLine("Guesses Remaining: " + intGuesses.ToString());
				}

				Console.WriteLine("\nMake your guess:\n");
				int intUserGuess = 0;
				string strUserGuess = Console.ReadLine();


				if (isGuessCorrectFormat(ref strUserGuess, intSecretCode))
				{
					intUserGuess = Int32.Parse(strUserGuess);

					if (intUserGuess == intSecretCode) //Game has been won.
					{
						winState = true;
						break;
					}

					int inPlaceCount = getInPlaceDigitCount(intUserGuess, guessAry, answerAry, intSecretCode);
					int outOfPlaceCount = getOutOfPlaceDigitCount(intUserGuess, guessAry, answerAry, intSecretCode);

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
					switch (outOfPlaceCount)
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
					Console.WriteLine("Make sure your input is between 1111 and 6666, with each digit being no larger that 6.");
			}
			if (winState)
			{
				Console.WriteLine("--------------------\n");
				Console.WriteLine("\nYou nailed it!");
			}
			else
			{
				Console.WriteLine("\nBetter luck next time, You lose. :(\n");
				Console.WriteLine("The code was " + intSecretCode);
			}
			Console.ReadLine();
		}


		#region Functions

		/// Calculates the number of digits that are correct and in place.
		private static int getInPlaceDigitCount(int intUserGuess, bool[] guessAry, bool[] answerAry, int intSecretCode)
		{
			for (int i = 0; i < 4; i++)
			{
				guessAry[i] = false;
				answerAry[i] = false;
			}

			int inPlaceCount = 0;
			int guessDigit = 0;
			int randDigit = 0;
			int tempGuess = intUserGuess;
			int tempRand = intSecretCode;

			for (int i = 0; i < 4; i++)
			{
				guessDigit = tempGuess % 10;
				tempGuess = tempGuess / 10;
				randDigit = tempRand % 10;
				tempRand = tempRand / 10;

				if (guessDigit == randDigit)
				{
					guessAry[i] = true;
					answerAry[i] = true;
					inPlaceCount++;
				}
			}
			return inPlaceCount;
		}


		/// Calulates the number of digits that are correct, but out of place
		public static int getOutOfPlaceDigitCount(int userGuess, bool[] guessAry, bool[] answerAry, int intSecretCode)
		{
			int outOfPlaceCount = 0;
			int guessDigit;
			int randDigit;
			int tempRand = intSecretCode;

			for (int i = 0; i < 4; i++)
			{
				guessDigit = userGuess % 10;
				userGuess = userGuess / 10;
				randDigit = tempRand % 10;
				tempRand = intSecretCode;
				if (guessAry[i] == false)
				{
					for (int j = 0; j < 4; j++)
					{
						randDigit = tempRand % 10;
						tempRand = tempRand / 10;
						if (answerAry[j] == false)
						{
							if (guessDigit == randDigit)
							{
								outOfPlaceCount++;
								guessAry[i] = true;
								answerAry[j] = true;
								break;
							}
						}
					}
				}
			}
			return outOfPlaceCount;
		}


		/// Checks to see if the user guess is correct.
		/// <param name="strUserGuess">The string representation of the user entered guess, passed by reference so that the string is updated in Main.</param>
		private static bool isGuessCorrectFormat(ref string strUserGuess, int intSecretCode)
		{
			int intUserGuess = 0;
			try
			{
				intUserGuess = Int32.Parse(strUserGuess);
				int guessDigit = 0;
				int tempGuess = intUserGuess;
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
				Console.WriteLine("\nMake sure your input is between 1111 and 6666, with each digit being no larger that 6.");
				System.Threading.Thread.Sleep(2000);
				Console.WriteLine("\nMake your guess:\n");
				strUserGuess = Console.ReadLine();
				if (isGuessCorrectFormat(ref strUserGuess, intSecretCode))
					return true;
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
				strCraftedNum += srand.Next(MIN_INDIV_NUMBER, MAX_INDIV_NUMBER);
			}
			return Int32.Parse(strCraftedNum);
		}

		#endregion
	}
}