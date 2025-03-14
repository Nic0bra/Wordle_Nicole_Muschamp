using System.Collections.Generic;
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
    int maxAttempts = 5;
    int currentAttempt = 0;

    // Start game by loading text files into lsits and choosing a random one
    public void StartGame()
    {
        LoadWords();
        ChoseRandomWord();
        score = 0;
        currentAttempt = 0;

        while (currentAttempt <= maxAttempts)
        {
            string userGuess = gameMediator.GetUserInput();
            CheckGuess(userGuess); 
        }

    }

    //Load the words into a list
    private void LoadWords()
    {
        possibleAnswersList = new List<string>(possibleAnswers.text.Split('\n'));
        allowedWordsList = new List<string>(allowedWords.text.Split('\n'));
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
        if (string.IsNullOrEmpty(userGuess))
        {
            gameView.ShowInvalidCanvas();
            return new string[0];
        }
        string[] result = new string[userGuess.Length];

        if (userGuess == chosenWord)
        {
            score++;
            gameView.UpdateScore(score);
            gameView.ShowWinCanvas();
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = "correct";
            }
            return result;
        }
        else
        {
            //Convert both words to char arrays
            char[] guessArray = userGuess.ToCharArray();
            char[] wordArray = chosenWord.ToCharArray();

            for (int i = 0; i < guessArray.Length; i++)
            {
                if (i < wordArray.Length && guessArray[i] == wordArray[i])
                {
                    //Letter is correct and in the right spot
                    result[i] = "correct";
                }
                else if (chosenWord.Contains(guessArray[i]))
                {
                    //Letter is correct but in the wrong spot
                    result[i] = "contains";
                }
                else
                {
                    //Letter is not in the word
                    result[i] = "wrong";
                }
            }
            currentAttempt--;
            return result;
        }
    }

    //Reset board for next round
    public void NextRound()
    {
        currentAttempt = 0;
        ChoseRandomWord();
        gameView.ResetBoard();
        gameView.ClearUserInput();

    }
}
