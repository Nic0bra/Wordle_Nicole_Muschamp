using System.Collections.Generic;
using UnityEngine;

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
    public int Score { get; private set; }
    int maxAttempts = 5;
    int currentAttempt = 0;

    // Start game by loading text files into lsits and choosing a random one
    public void StartGame()
    {
        LoadWords();
        ChoseRandomWord();

        gameMediator.StartGame();

        Score = 0;
        currentAttempt = 0;
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

    //Check the player's guess against either list
    public bool CheckGuess(string userGuess)
    {
        currentAttempt++;

        //Check if guess is valid
        if (!allowedWordsList.Contains(userGuess.ToLower()) && !possibleAnswersList.Contains(userGuess.ToLower()))
        {
            return false;
        }

        //If guess is correct
        if(userGuess.ToLower() == chosenWord)
        {
            Score++;
            return true;
        }

        return false;
    }

    //Reset board for next round
    public void NextRound()
    {
        currentAttempt = 0;
        ChoseRandomWord();
        gameView.ResetBoard();
        gameView.ClearUserInput();

    }

    //Reset game for new game
    public void ResetGame()
    {
        StartGame();
    }
}
