using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameView : MonoBehaviour
{
    //Reference other scripts
    [SerializeField] GameMediator gameMediator;

    //Reference for all canvases
    [SerializeField] GameObject startCanvas;
    [SerializeField] GameObject gameCanvas;
    [SerializeField] GameObject winCanvas;
    [SerializeField] GameObject loseCanvas;
    [SerializeField] GameObject invalidCanvas;
    
    //Stores current invalid canvas object
    GameObject currentInvalidCanvas;

    //Text Fields to be displayed
    [SerializeField] TMP_InputField userInput;
    [SerializeField] TMP_Text gameScoreText;
    [SerializeField] TMP_Text winScoreText;
    [SerializeField] TMP_Text loseScoreText;
    [SerializeField] TMP_Text chosenWordLoseCanvas;
    [SerializeField] TMP_Text chosenWordWinCanvas;

    //Arrays to access game board rows of letter buttons
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
    //Show win canvas
    public void ShowWinCanvas(string chosenWord)
    {
        startCanvas.SetActive(false);
        gameCanvas.SetActive(false);
        winCanvas.SetActive(true);
        loseCanvas.SetActive(false);
        invalidCanvas.SetActive(false);
        chosenWordWinCanvas.text = chosenWord.ToString();
    }

    //Show lose canvas
    public void ShowLoseCanvas(string chosenWord)
    {
        startCanvas.SetActive(false);
        gameCanvas.SetActive(false);
        winCanvas.SetActive(false);
        loseCanvas.SetActive(true);
        invalidCanvas.SetActive(false);
        chosenWordLoseCanvas.text = chosenWord.ToString();
    }

    //Instantiate and show invalid canvas
    public void ShowInvalidCanvas()
    {
        //If the invalid canvas is already displayed destroy it before showing another
        if (currentInvalidCanvas != null)
        {
            currentInvalidCanvas.SetActive(false);
            Destroy(currentInvalidCanvas);
        }
        currentInvalidCanvas = Instantiate(invalidCanvas);
        currentInvalidCanvas.SetActive(true);
    }

    //Hide and destroy invalid canvas
    public void HideInvalidCanvas()
    {
        if (currentInvalidCanvas != null)
        {
            Destroy(currentInvalidCanvas);
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

    //Clear text field
    public void ClearUserInput()
    {
        userInput.text = "";
    }

    //Display the guessed Letter in the correct row 
    public void GuessLetterDisplay(int guessRow, string[] letters)
    {
        Button[] currentRow = null;

        //Select the correct row based on the current attempt
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
        //Set the text of each button in the row to the right letter
        for (int i = 0; i < currentRow.Length; i++)
        {
            currentRow[i].GetComponentInChildren<TMP_Text>().text = letters[i].ToLower();
        }
    }


    //Change the square color based on guess results
    public void ChangeSquareColor(int guessRow, string[] guessResults)
    {
        Button[] currentRow = null;

        //Select the correct row based on the current attempt
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

        //Set the color of each button based on guess results
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
                //Right letter in wrong spot
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

        //Resets the color and text for all buttons
        foreach (Button[] row in allRows)
        {
            foreach (Button button in row)
            {
                button.GetComponent<Image>().color = Color.white;

                button.GetComponentInChildren<TMP_Text>().text = "";
            }
        }

        //Clear user input
        ClearUserInput();
    }
}


