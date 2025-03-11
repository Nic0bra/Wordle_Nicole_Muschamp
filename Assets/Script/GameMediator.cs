using System;
using TMPro;
using UnityEngine;

public class GameMediator : MonoBehaviour
{
    //Reference Scripts
    [SerializeField] GameLogic gameLogic;
    [SerializeField] GameView gameView;

    void Start()
    {
        //Initialize the game view
        gameView.ShowStartCanvas();
    }

    //Start Game once clicked start
    public void StartGame()
    {
        gameView.ShowGameCanvas();
        gameLogic.StartGame();
    }

    //Method to check for Valid usser Input
    public void SubmitGuess(string userGuess)
    {
        if (string.IsNullOrEmpty(userGuess))
        {
            gameView.ShowInvalidCanvas();
            return;
        }
        bool isValid = gameLogic.CheckGuess(userGuess);

        if (isValid)
        {
            gameLogic.CheckGuess(userGuess);
        }
        
        else
        {
            //If the guess is invalid
            gameView.ShowInvalidCanvas();
        }

        gameView.ClearUserInput();
    }

    public void PlayAgain()
    {
        gameLogic.NextRound();

        gameView.ShowGameCanvas();
    }

    //Method to restart the game
    public void RestartGame()
    {
        gameLogic.ResetGame();
        gameView.ResetBoard();
        gameView.ShowStartCanvas();
        gameView.UpdateScore(0);
    }
}
