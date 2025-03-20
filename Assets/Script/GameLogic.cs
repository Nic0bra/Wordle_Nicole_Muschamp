using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class GameLogic : MonoBehaviour
{
    //Reference other scripts and assets
    [SerializeField] GameView gameView;
    [SerializeField] GameMediator gameMediator;
    [SerializeField] TextAsset possibleAnswers;
    [SerializeField] TextAsset allowedWords;
    //Variables to hold lists from text asset
    List<string> possibleAnswersList;
    List<string> allowedWordsList;
    //Game variables
    string chosenWord;
    public int score;
    public int maxAttempts = 5;
    public int currentAttempt = 0;

    // Start game by loading text files into lsits and choosing a random one
    public void StartGame()
    {

        LoadWords();
        ChoseRandomWord();
        score = 0;
        currentAttempt = 0;

        gameView.UpdateScore(score);
        gameView.ResetBoard();
        gameView.ClearUserInput();

    }

    //Load the words into a list
    private void LoadWords()
    {
        //Load text files into lists and split them on new line \n
        possibleAnswersList = new List<string>(possibleAnswers.text.Split('\n'));
        allowedWordsList = new List<string>(allowedWords.text.Split('\n'));

        //trim lists to remove whitespace and force them all to lowercase
        possibleAnswersList = possibleAnswersList.ConvertAll(word => word.Trim().ToLower());
        allowedWordsList = allowedWordsList.ConvertAll(word => word.Trim().ToLower());
    }

    //Choose word at random from possible answers list
    public void ChoseRandomWord()
    {
        //Create random index within bounds of list
        int randomIndex = Random.Range(0, possibleAnswersList.Count);

        //Get word at radom word via its index and trim
        chosenWord = possibleAnswersList[randomIndex].Trim();
    }

    //Check if the word is valid
    public bool IsValidWord(string userGuess)
    {
        //format users guess by trimming whitespace and forcing lowercase
        string formatUserGuess = userGuess.Trim().ToLower();

        //check if input box is empty or if the users word is not on either list
        if (string.IsNullOrEmpty(formatUserGuess) || !(allowedWordsList.Contains(formatUserGuess) ||
               possibleAnswersList.Contains(formatUserGuess)))
        {
            return false;
        }
        //If input box not empty and the word is located on either list word is valid
        else
            return true;
    }

    //Check the user guess against chosen random word and return string of results
    public string[] CheckGuess(string userGuess)
    {
        //Check if user still has turns left
        if (currentAttempt >= maxAttempts)
        {
            //display lose canvas if not and return null string
            gameView.ShowLoseCanvas(chosenWord);
            return new string[0];
        }

        //format the users guess for comparison
        string formatUserGuess = userGuess.Trim().ToLower();

        //If the word is the chosen word
        if (formatUserGuess == chosenWord.ToLower())
        {
            //if the guess is right add to the score & update, show win canvas and return all green results
            score++;
            gameView.UpdateScore(score);
            gameView.ShowWinCanvas(chosenWord);
            return new string[] { "green", "green", "green", "green", "green" };
        }

        //Convert words to char arrays for letter comparison
        char[] guessArray = formatUserGuess.ToCharArray();
        char[] wordArray = chosenWord.ToLower().ToCharArray();

        //Copy chosen word to track which letters have been matched
        char[] wordArrayCopy = (char[])wordArray.Clone();

        //Create an array to store the color results for return
        string[] tempResult = new string[5];

        //First pass- Mark correct letters in the correct positions with green
        for (int i = 0; i < 5; i++)
        {
            //If the word is the word put green in the result array
            if (guessArray[i] == wordArrayCopy[i])
            {
                tempResult[i] = "green";

                //Mark the letter in the copy as used by replacing it with blank
                wordArrayCopy[i] = ' ';
            }
        }

        //Second pass- Mark misplaced letters with yellow and incorrect letters with gray
        for (int i = 0; i < 5; i++)
        {
            //If the letter placement is not already green check for other matches
            if (tempResult[i] != "green")
            {
                //Check if it exists in the word at all
                bool found = false;
                for (int j = 0; j < 5; j++)
                {
                    //If match is found mark it as yellow in result array
                    if (wordArrayCopy[j] == guessArray[i])
                    {
                        tempResult[i] = "yellow";

                        //Mark the letter in the copy as used by replacing it with blank
                        wordArrayCopy[j] = ' ';
                        found = true;

                        //Stop searching after first occurance
                        break;
                    }
                }
                //If no match was found mark the letter as gray
                if (!found)
                {
                    tempResult[i] = "gray";
                }
            }
        }
        //Give the results to the result array for return
        string[] result = tempResult;
        //Mark turn as used
        currentAttempt++;

        //Check if player has remaining turns
        if (currentAttempt >= maxAttempts)
        {
            //Display lose canvas if no more turns remain
            gameView.ShowLoseCanvas(chosenWord);
        }
        //return the color results
        return result;
    }
       

    //Reset board for next round
    public void NextRound()
    {
        //Reset turns for next round
        currentAttempt = 0;
        //choose new word
        ChoseRandomWord();
        //Update score
        gameView.UpdateScore(score);
        //Clear the board for new round
        gameView.ResetBoard();
        //Clear inupt
        gameView.ClearUserInput();

    }
}
