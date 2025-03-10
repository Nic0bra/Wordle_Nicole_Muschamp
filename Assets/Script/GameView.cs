using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameView : MonoBehaviour
{
    [SerializeField] GameMediator gameMediator;
    //Game Canvas to be dispalyed
    [SerializeField] GameObject startCanvas;
    [SerializeField] GameObject gameCanvas;
    [SerializeField] GameObject winCanvas;
    [SerializeField] GameObject loseCanvas;
    [SerializeField] GameObject invalidCanvas;
    GameObject currentInvalidCanvas;

    //Text Fields to be displayed
    [SerializeField] TMP_InputField userInput;
    [SerializeField] TMP_Text gameScoreText;
    [SerializeField] TMP_Text winScoreText;
    [SerializeField] TMP_Text loseScoreText;

    //Arrays to access game board
    [SerializeField] Button[] guessRowOne;
    [SerializeField] Button[] guessRowTwo;
    [SerializeField] Button[] guessRowThree;
    [SerializeField] Button[] guessRowFour;
    [SerializeField] Button[] guessRowFive;

    //Show start canvas
    public void ShowStartCanvas()
    {
        startCanvas.SetActive(true);
        gameCanvas.SetActive(false);
        winCanvas.SetActive(false);
        loseCanvas.SetActive(false);
        invalidCanvas.SetActive(false);
    }
    
    //Show game canvas
    public void ShowGameCanvas()
    {
        startCanvas.SetActive(false);
        gameCanvas.SetActive(true);
        winCanvas.SetActive(false);
        loseCanvas.SetActive(false);
        invalidCanvas.SetActive(false);
    }

    //Clear text field
    public void ClearUserInput()
    {
        userInput.text = "";
    }
    
    //Show win canvas
    public void ShowWinCanvas()
    {
        startCanvas.SetActive(false);
        gameCanvas.SetActive(false);
        winCanvas.SetActive(true);
        loseCanvas.SetActive(false);
        invalidCanvas.SetActive(false);
    }

    //Show lose canvas
    public void ShowLoseCanvas()
    {
        startCanvas.SetActive(false);
        gameCanvas.SetActive(false);
        winCanvas.SetActive(false);
        loseCanvas.SetActive(true);
        invalidCanvas.SetActive(false);
    }

    //Show invalid canvas
    public void ShowInvalidCanvas()
    {
        if(currentInvalidCanvas == null)
        {
            currentInvalidCanvas = Instantiate(invalidCanvas);
        }
        currentInvalidCanvas.SetActive(true);
    }

    //Hide invalid canvas
    public void HideInvalidCanvas()
    {
        if(currentInvalidCanvas != null)
        {
            currentInvalidCanvas.SetActive(false);
        }
    }

    //Update the score
    public void UpdateScore(int score)
    {
        gameScoreText.text = score.ToString();
        winScoreText.text = score.ToString();
        loseScoreText.text = score.ToString();
    }

    //When user selects submit
    public void SubmitGuess()
    {
        string userGuess = userInput.text;

        if(!string.IsNullOrEmpty(userGuess))
        {
            gameMediator.SubmitGuess(userGuess);
        }

        else
        {
            invalidCanvas.SetActive(true);
        }
    }

    //Display the guessed Letter
    public void GuessLetterDisplay(int guessRow, string[] letters)
    {
        Button[] currentRow = null;

        switch (guessRow)
        {
            case 1: currentRow = guessRowOne; break;
            case 2: currentRow = guessRowTwo; break;
            case 3: currentRow = guessRowThree; break;
            case 4: currentRow = guessRowFour; break;
            case 5: currentRow = guessRowFive; break;
        }

        for (int i = 0; i < currentRow.Length; i++)
        {
            currentRow[i].GetComponentInChildren<TMP_Text>().text = letters[i].ToLower();
        }
    }


    //Change the square color based on condition
    public void ChangeSquareColor(int guessRow, string[] guessResults )
    {
        Button[] currentRow = null;

        switch (guessRow)
        {
            case 1:
                currentRow = guessRowOne;
                break;
            case 2:
                currentRow = guessRowTwo;
                break;
            case 3:
                currentRow = guessRowThree;
                break;
            case 4:
                currentRow = guessRowFour;
                break;
            case 5:
                currentRow = guessRowFive;
                break;
        }

        for (int i = 0; i < currentRow.Length; i++)
        {
            //Default color white
            Color buttonColor = Color.white;

            //Change the color based on the result
            switch (guessResults[i])
            {
                //Correct letter in correct spot
                case "green":
                    buttonColor = Color.green;
                    break;
                //Wrong letter compeletly
                case "gray":
                    buttonColor = Color.gray;
                    break;
                case "yellow":
                    buttonColor = Color.yellow;
                    break;
            }

            //Set the color
            currentRow[i].GetComponent<Image>().color = buttonColor;
        }
    }

    //Reset the board
    public void ResetBoard()
    {
        Button[][] allRows = { guessRowOne, guessRowTwo, guessRowThree, guessRowFour, guessRowFive };

        foreach (Button[] row in allRows)
        {
            foreach(Button button in row)
            {
                button.GetComponent<Image>().color = Color.white;

                button.GetComponentInChildren<TMP_Text>().text = "";
            }
        }
    }
    

}
