using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordStepApp
{
    /// <summary>
    /// The Word Step Main class.
    /// Parses command line arguments or accepts user input. 
    /// </summary>
    /// <remarks>
    /// This class executes once using command line parameters or n times based on user input.
    /// </remarks>
    class WordStep
    {
        /// <summary>
        /// Allow the user to select whether to run with command line parameters or enable user input. 
        /// </summary>
        static void Main(string[] args)
        {
            bool commandLineParameters = false;

            if (args.Length == 4)
            {
                string userOption = GetUserOption("Use command line parameters? Y/N");

                if (userOption == "Y")
                {
                    commandLineParameters = true;
                }
            }
            if (commandLineParameters)
            {
                RunAlgorithm(args);
            }
            else
            {
                RunAlgorithmUserInput();
            }
        }

        /// <summary>
        /// Run the algorithm multiple times via user input.
        /// </summary>
        private static void RunAlgorithmUserInput()
        {
            bool running = true;
            string dictionaryFilePath = GetUserInputPath("\nPlease enter a path to a vocabulary file: ", false);
            string resultsFilePath = GetUserInputPath("\nPlease enter a path to a results file: ", true);
            string startWord = GetUserInputWord("\nPlease enter a start word: ");
            string endWord = GetUserInputWord("\nPlease enter an end word: ");

            WordStepAlgorithm wordDictionary = new WordStepAlgorithm(dictionaryFilePath, startWord, endWord, resultsFilePath);
            wordDictionary.RunAlgorithm();

            do
            {
                string option = GetUserOption("\nFind a word chain for more words? Y/N");
                if (option == "N")
                {
                    running = false;
                    break;
                }
                wordDictionary.StartWord = GetUserInputWord("\nPlease enter a start word: ");
                wordDictionary.EndWord = GetUserInputWord("\nPlease enter an end word: ");
                wordDictionary.RunAlgorithm();

            } while (running);
            
        }

        /// <summary>
        /// Run the algorithm once using command line parameters.
        /// </summary>
        private static void RunAlgorithm(string[] parameters)
        {
            WordStepAlgorithm wordDictionary = new WordStepAlgorithm(parameters[0], parameters[1], parameters[2], parameters[3]);
            wordDictionary.RunAlgorithm();
            Console.WriteLine("Press enter to terminate program.");
            Console.ReadLine();
        }

        /// <summary>
        /// Retrieve a file path from user input, if a file doesn't exist then it can be created (to store results).
        /// </summary>
        /// <returns>
        /// A string containing a path to a file.
        /// </returns>
        private static string GetUserInputPath(string question, bool createFile)
        {
            string input;
            do
            {
                Console.WriteLine(question);
                input = Console.ReadLine();
                if (!File.Exists(input))
                {
                    if(createFile)
                    {
                        File.Create(input).Dispose();
                    }
                    else
                    {
                        Console.WriteLine("\nFile does not exist, please enter a valid path.");
                    }
                }
            }
            while (!File.Exists(input));

            return input;
        }

        /// <summary>
        /// Retrieve an input word from user input.
        /// </summary>
        /// <returns>
        /// A string containing a 4 letter word.
        /// </returns>
        private static string GetUserInputWord(string question)
        {
            string input;
            do
            {
                Console.WriteLine(question);
                input = Console.ReadLine().Trim();
                if (input.Length != 4)
                {
                    Console.WriteLine("\nThe entered word is not 4 letters in length.");
                }
            }
            while (input.Length != 4);

            return input;
        }

        /// <summary>
        /// Retrieve a selection from the user.
        /// </summary>
        /// <returns>
        /// A string containing a "Y" or "N" value.
        /// </returns>
        private static string GetUserOption(string question)
        {
            string input;
            do
            {
                Console.WriteLine(question);
                input = Console.ReadLine();

                if (input.ToUpper() != "Y" && input.ToUpper() != "N")
                {
                    Console.WriteLine("\nIncorrect selection. Please enter Y or N.");
                }
            }
            while (input.ToUpper() != "Y" && input.ToUpper() != "N");

            return input.ToUpper();
        }

    }
}
