using System;
using TMPro;
using UnityEngine;

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
        string userGuess = userInput.ToString().ToLower();
        return userGuess;
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
        gameView.UpdateScore(0);
        gameView.ClearUserInput();
    }
}
