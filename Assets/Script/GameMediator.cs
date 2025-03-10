using System;
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
        gameLogic.StartGame();
        gameView.ShowGameCanvas();
    }

    //Method to handle user input
    public void SubmitGuess(string userGuess)
    {
        bool isValid = gameLogic.CheckGuess(userGuess);

        if (isValid)
        {
            //If user guesses the word pudate score and display win canvas
            gameView.UpdateScore(gameLogic.Score);
            gameView.ShowWinCanvas();
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
