using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class GameLogic : MonoBehaviour
{
    //Initialize Variables
    [SerializeField] GameView gameView;
    [SerializeField] GameMediator gameMediator;
    [SerializeField] TextAsset possibleAnswers;
    [SerializeField] TextAsset allowedWords;
    List<string> possibleAnswersList;
    List<string> allowedWordsList;
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
        possibleAnswersList = new List<string>(possibleAnswers.text.Split('\n'));
        allowedWordsList = new List<string>(allowedWords.text.Split('\n'));

        possibleAnswersList = possibleAnswersList.ConvertAll(word => word.Trim().ToLower());
        allowedWordsList = allowedWordsList.ConvertAll(word => word.Trim().ToLower());
    }

    //Choose word at random from possible answers list
    public void ChoseRandomWord()
    {
        int randomIndex = Random.Range(0, possibleAnswersList.Count);
        chosenWord = possibleAnswersList[randomIndex].Trim();
    }

    //Check the user guess against chosen random word
    public string[] CheckGuess(string userGuess)
    {
        //Check if they still have a turn
        if (currentAttempt >= maxAttempts)
        {
            gameView.ShowLoseCanvas();
            return new string[0];
        }

        string formatUserGuess = userGuess.Trim().ToLower();

        //Check if the word is in allowed words or possible answers
        bool isValidWord = allowedWordsList.Contains(formatUserGuess) || possibleAnswersList.Contains(formatUserGuess);

        //Display ivalid canvas if not a real word or empty
        if (!isValidWord || string.IsNullOrEmpty(formatUserGuess))
        {
            gameView.ShowInvalidCanvas();
            return new string[0];
        }

        string[] result = new string[5];

        //If the word is the chosen word
        if (formatUserGuess == chosenWord.ToLower())
        {
            score++;
            gameView.UpdateScore(score);
            gameView.ShowWinCanvas();
            return new string[] { "green", "green", "green", "green", "green" };
        }

        //Convert words to char arrays
        char[] guessArray = formatUserGuess.ToCharArray();
        char[] wordArray = chosenWord.ToLower().ToCharArray();

        //Copy chosen word to track which letters have been matched
        char[] wordArrayCopy = (char[])wordArray.Clone();

        //Store color for each letter in user's guess
        string[] tempResult = new string[5];

        //First pass- Mark correct letters in the correct positions
        for (int i = 0; i < 5; i++)
        {
            //If the word is the word put green in the result array
            if (guessArray[i] == wordArrayCopy[i])
            {
                tempResult[i] = "green";

                //Mark it as used
                wordArrayCopy[i] = ' ';
            }
        }

        //Second pass- Mark misplaced letters
        for (int i = 0; i < 5; i++)
        {
            //If the letter placement is not already green
            if (tempResult[i] != "green")
            {
                //Check if it exists in the word at all
                bool found = false;
                for (int j = 0; j < 5; j++)
                {
                    //Check if the letter exists
                    if (wordArrayCopy[j] == guessArray[i])
                    {
                        tempResult[i] = "yellow";
                        //Mark as used
                        wordArrayCopy[j] = ' ';
                        found = true;
                        //Stop searching after first occurance
                        break;
                    }
                }
                if (!found)
                {
                    tempResult[i] = "gray";
                }
            }
        }

        result = tempResult;
        currentAttempt++;

        if (currentAttempt >= maxAttempts)
        {
            gameView.ShowLoseCanvas();
        }
        return result;
    }
       

    //Reset board for next round
    public void NextRound()
    {
        currentAttempt = 0;
        ChoseRandomWord();
        score = 0;
        gameView.UpdateScore(score);
        gameView.ResetBoard();
        gameView.ClearUserInput();

    }
}
