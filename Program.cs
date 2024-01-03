using System;
using System.Collections.Generic;
using System.Linq;
using WordUnscrambler.Data;
using WordUnscrambler.Workers;

namespace WordUnscrambler
{
    public class Program
    {
        private static readonly FileReader _fileReader = new FileReader();
        private static readonly WordMatcher _wordMatcher = new WordMatcher();
        static void Main(string[] args)
        {
            try {
                bool continueWordunscramble = true;
                do
                {
                    Console.WriteLine(Constants.OptionsOnHowToEnterScrambledWords);
                    var option = Console.ReadLine() ?? string.Empty;

                    switch (option.ToUpper())
                    {
                        case Constants.File:
                            Console.Write(Constants.EnterScrambledWordsViaFile);
                            ExecuteScrambledWordsInFileScenario();
                            break;
                        case Constants.Manual:
                            Console.Write(Constants.EnterScrambledWordsManually);
                            ExecuteScrambledWordsManualEntryScenario();
                            break;
                        default:
                            Console.WriteLine(Constants.EnterScrambledWordsOptionNotRecognized);
                            break;
                    }

                    var continueDecision = string.Empty;
                    do
                    {

                        Console.WriteLine(Constants.OptionOnContinuingTheProgram);
                        continueDecision = (Console.ReadLine() ?? string.Empty);

                    } while (!continueDecision.Equals(Constants.Yes, StringComparison.OrdinalIgnoreCase) &&
                           !continueDecision.Equals(Constants.No, StringComparison.OrdinalIgnoreCase));

                    continueWordunscramble = continueDecision.Equals(Constants.Yes, StringComparison.OrdinalIgnoreCase);

                } while (continueWordunscramble);
            }
            catch(Exception ex)
            {
                Console.WriteLine(Constants.ErrorProgramWillBeTerminated + ex.Message);
            }
          
        }

        private static void ExecuteScrambledWordsInFileScenario()
        {
            try
            {
                var filename = Console.ReadLine() ?? string.Empty;
                string[] scrambledWords = _fileReader.Read(filename);
                DisplayMatchedUnscrambledWords(scrambledWords);
            }
            catch(Exception ex)
            {
                Console.WriteLine(Constants.ErrorScrambledWordsCannotBeLoaded + ex.Message);
            }
           
        }

        private static void ExecuteScrambledWordsManualEntryScenario()
        {
            var manualInput = Console.ReadLine() ?? string.Empty;
            string[] scrambleWords = manualInput.Split(',');
            DisplayMatchedUnscrambledWords(scrambleWords);
        }

        private static void DisplayMatchedUnscrambledWords(string[] scrambledWords)
        {
            string[] wordList = _fileReader.Read(Constants.WordListFileName);
            List<MatchedWord> matchedwords = _wordMatcher.Match(scrambledWords, wordList);

            if (matchedwords.Any())
            {
                foreach(var matchedWord in matchedwords)
                {
                    Console.WriteLine(Constants.MatchFound , matchedWord.ScrambledWord, matchedWord.Word);
                }
            }
            else
            {
                Console.WriteLine(Constants.MatchNotFound);
            }
        }
    }

   
}
