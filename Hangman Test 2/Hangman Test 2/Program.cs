using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;

namespace ConsoleApplication6
{
    class Hangman
    {

        //Words for the game

        static string[] wordBank = { "study", "cat", "dress", "shoes", "lipstick" };
        // Create new ArrayList and initialize with words from array wordBank
        static ArrayList wordList = new ArrayList(wordBank);


        static void Main(string[] args)
        {

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Title = "C# Hangman";
            Console.WriteLine("Hang man!");


            //Gamemenu
            string response = "";
            do
            {
                Console.Write("Enter Command (1. Add Words, 2. List Words , 3. Play , 4. Exit) Pick 1-4: ");
                response = Console.ReadLine();

                switch (response)
                {
                    case "1":
                        AddWord();
                        break;
                    case "2":
                        ListWords();
                        break;
                    case "3":
                        Play();
                        break;
                    case "4":
                        break;
                }
            } while (response != "4");



        }

        //add words to list
        static void AddWord()
        {
            Console.Write("Enter the word to add: ");
            String temp = Console.ReadLine();
            wordList.Add(temp);
            Console.WriteLine("{0} was added to the dictionary!", temp);

        }


        //Display words
        static void ListWords()
        {
            foreach (Object obj in wordList)
                Console.WriteLine("{0}", obj);
            Console.WriteLine();


        }


        //Gameplay
        static void Play()
        {
            Random random = new Random((int)DateTime.Now.Ticks);


            string wordToGuess = wordList[random.Next(0, wordList.Count)].ToString();
            string wordToGuessUppercase = wordToGuess.ToUpper();

            StringBuilder displayToPlayer = new StringBuilder(wordToGuess.Length);
            for (int i = 0; i < wordToGuess.Length; i++)
                displayToPlayer.Append('-');



            List<char> correctGuesses = new List<char>();
            List<char> incorrectGuesses = new List<char>();

            int lives = 5;
            bool won = false;
            int lettersRevealed = 0;

            string input;
            char guess;

            while (!won && lives > 0)
            {
                Console.WriteLine("Current word: " + displayToPlayer);
                Console.Write("Guess a letter: ");


                input = Console.ReadLine().ToUpper();
                guess = input[0];

                if (correctGuesses.Contains(guess))
                {
                    Console.WriteLine("You've already tried '{0}', and it was correct!", guess);
                    continue;
                }
                else if (incorrectGuesses.Contains(guess))
                {
                    Console.WriteLine("You've already tried '{0}', and it was wrong!", guess);
                    continue;
                }

                if (wordToGuessUppercase.Contains(guess))
                {
                    correctGuesses.Add(guess);

                    for (int i = 0; i < wordToGuess.Length; i++)
                    {
                        if (wordToGuessUppercase[i] == guess)
                        {
                            displayToPlayer[i] = wordToGuess[i];
                            lettersRevealed++;
                        }
                    }

                    if (lettersRevealed == wordToGuess.Length)
                        won = true;
                }
                else
                {
                    incorrectGuesses.Add(guess);

                    Console.WriteLine("Nope, there's no '{0}' in it!", guess);
                    lives--;
                }

                Console.WriteLine(displayToPlayer.ToString());
            }

            if (won)
                Console.WriteLine("You won!");
            else
                Console.WriteLine("You lost! It was '{0}'", wordToGuess);

            Console.Write("Press ENTER to exit...");
            Console.ReadLine();
        }
    }

}