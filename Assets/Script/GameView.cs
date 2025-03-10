using NUnit.Framework;
using NUnit.Framework.Constraints;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameView : MonoBehaviour
{
    //Game Canvas to be dispalyed
    [SerializeField] GameObject startCanvas;
    [SerializeField] GameObject gameCanvas;
    [SerializeField] GameObject winCanvas;
    [SerializeField] GameObject loseCanvas;

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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Call the start canvas function
        ShowStartCanvas();
    }
    //Show start canvas
    public void ShowStartCanvas()
    {
        startCanvas.SetActive(true);
        gameCanvas.SetActive(false);
        winCanvas.SetActive(false);
        loseCanvas.SetActive(false);
    }
    
    //Show game canvas
    public void ShowGameCanvas()
    {
        startCanvas.SetActive(false);
        gameCanvas.SetActive(true);
        winCanvas.SetActive(false);
        loseCanvas.SetActive(false);
    }
    
    //Show win canvas
    public void ShowWinCanvas()
    {
        startCanvas.SetActive(false);
        gameCanvas.SetActive(false);
        winCanvas.SetActive(true);
        loseCanvas.SetActive(false);
    }

    //Show lose canvas
    public void ShowLoseCanvas()
    {
        startCanvas.SetActive(false);
        gameCanvas.SetActive(false);
        winCanvas.SetActive(false);
        loseCanvas.SetActive(true);
    }

    //Update the score
    public void UpdateScore(int score)
    {
        gameScoreText.text = score.ToString();
        winScoreText.text = score.ToString();
        loseScoreText.text = score.ToString();
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

        for (int i = 0; i <currentRow.Length; i++)
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

}
