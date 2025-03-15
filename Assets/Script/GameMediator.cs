using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class GameMediator : MonoBehaviour
{
    //Reference Scripts
    [SerializeField] GameLogic gameLogic;
    [SerializeField] GameView gameView;
    public TMP_InputField userInput;

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

    //Get user input
    public string GetUserInput()
    {
        return userInput.text.Trim().ToLower();
    }
    public void PlayAgain()
    {
        gameLogic.NextRound();
        gameView.ShowGameCanvas();
    }

    //Method to restart the game
    public void RestartGame()
    {
        gameView.ResetBoard();
        gameView.ShowStartCanvas();
        gameLogic.score = 0;
        gameView.UpdateScore(0);
        gameView.ClearUserInput();
    }

    //Submit user guess
    public void SubmitGuess()
    {
        if(gameLogic != null && gameView != null)
        {
            string userGuess = GetUserInput();
            if (!string.IsNullOrEmpty(userGuess))
            {
                string[] guessResult = gameLogic.CheckGuess(userGuess);
                if(gameLogic.currentAttempt < gameLogic.maxAttempts)
                {
                    gameView.GuessLetterDisplay(gameLogic.currentAttempt, userGuess.ToCharArray().Select(c => c.ToString()).ToArray());
                    gameView.ChangeSquareColor(gameLogic.currentAttempt, guessResult);
                    gameView.ClearUserInput();
                }
            }
        }
    }
}
