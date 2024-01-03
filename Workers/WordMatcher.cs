using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordUnscrambler.Data;

namespace WordUnscrambler.Workers
{
    public class WordMatcher
    {
        public List<MatchedWord> Match(string[] scrambledWords, string[] wordList)
        {
            var matchedWords = new List<MatchedWord>();

            foreach (var scrambledword in scrambledWords)
            {
                foreach(var word in wordList)
                {
                    if (scrambledword.Equals(word, StringComparison.OrdinalIgnoreCase))
                    {
                        matchedWords.Add(BuildMatchedWord(scrambledword, word));
                    }
                    else
                    {
                        var scrambledWordArray = scrambledword.ToCharArray();
                        var wordArray = word.ToCharArray();

                        Array.Sort(scrambledWordArray);
                        Array.Sort(wordArray);
                        var sortedScrambleWord = new string(scrambledWordArray);
                        var sortedWord = new string(wordArray);

                        if (sortedScrambleWord.Equals(sortedWord, StringComparison.OrdinalIgnoreCase))
                        {
                            matchedWords.Add(BuildMatchedWord(scrambledword, word));
                        }
                    }
                }

            }

            return matchedWords;
        }

        private MatchedWord BuildMatchedWord(string scrambledWord,string word)
        {
            MatchedWord matchedWord = new MatchedWord
            {
                ScrambledWord = scrambledWord,
                Word = word

            };
            return matchedWord;
        }
    }
}
