using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class GameMediator : MonoBehaviour
{
    //Reference Scripts and input box
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
        //Show the game board
        gameView.ShowGameCanvas();
        //Start the game logic
        gameLogic.StartGame();
    }

    //Get user input
    public string GetUserInput()
    {
        //return user input removing whitespace and forcing lowercase
        return userInput.text.Trim().ToLower();
    }
    
    //New Round on Play Again Click
    public void PlayAgain()
    {
        //Start new round
        gameLogic.NextRound();
        //Show game canvas
        gameView.ShowGameCanvas();
    }

    //Method to restart the game
    public void RestartGame()
    {
        //Resets board
        //Resets score and displays that change
        //Shows the start canvas
        //Clear any user input
        gameView.ResetBoard();
        gameView.ShowStartCanvas();
        gameLogic.score = 0;
        gameView.UpdateScore(0);
        gameView.ClearUserInput();
    }

    //Submit user guess
    public void SubmitGuess()
    {
        //ensure game logic and view are initialized
        if(gameLogic != null && gameView != null)
        {
            //Get user input
            string userGuess = GetUserInput();

            //Call is valid for validity check via game logic
            if (!gameLogic.IsValidWord(userGuess))
            {
                //If not valid show invalid canvas and exit the method
                gameView.ShowInvalidCanvas();
                return;
            }
            //Process the users guess and get the results form check guess via game logic
            string[] guessResult = gameLogic.CheckGuess(userGuess);
            
            //Make sure they player still has a turn
            if(gameLogic.currentAttempt < gameLogic.maxAttempts)
            {
                //Display users guess on game board converting characters to a string via lambda expression in the right row of the gameboard
                gameView.GuessLetterDisplay(gameLogic.currentAttempt, userGuess.ToCharArray().Select(c => c.ToString()).ToArray());
                //Update background color of 'button' for each letters square based on check guess results
                gameView.ChangeSquareColor(gameLogic.currentAttempt, guessResult);
                //Clear the users input
                gameView.ClearUserInput();
            }
        }
    }
}
