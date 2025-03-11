using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Nerdle : MonoBehaviour
{

    //Reference Canvases
    [SerializeField] GameObject startCanvas;
    [SerializeField] GameObject gameCanvas;
    [SerializeField] GameObject invalidCanvas;
    [SerializeField] GameObject winCanvas;
    [SerializeField] GameObject loseCanvas;

    //Reference Text Docs and their Lists
    [SerializeField] TextAsset possibleAnswers;
    [SerializeField] List<string> possibleAnswersList;
    [SerializeField] TextAsset allowedWords;
    [SerializeField] List<string> allowedWordsList;

    //Text Fields to be displayed
    [SerializeField] TMP_InputField userInput;
    string userGuess;
    [SerializeField] TMP_Text gameScoreText;
    [SerializeField] TMP_Text winScoreText;
    [SerializeField] TMP_Text loseScoreText;

    //Arrays to access game board
    [SerializeField] Button[] guessRowOne;
    [SerializeField] Button[] guessRowTwo;
    [SerializeField] Button[] guessRowThree;
    [SerializeField] Button[] guessRowFour;
    [SerializeField] Button[] guessRowFive;

    //Game variables
    string chosenWord;
    int score;
    int currentAttempt;
    int maxAttempt = 5;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ShowStartCanvas();
        TextDocToList();
        ChooseRandomWord();

        score = 0;
        currentAttempt = 0;
    }

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
        startCanvas.SetActive(false);
        gameCanvas.SetActive(false);
        winCanvas.SetActive(false);
        loseCanvas.SetActive(false);
        invalidCanvas.SetActive(true);
    }


    //Create the lists from text docs
    public void TextDocToList()
    {
        possibleAnswersList = new List<string>(possibleAnswers.text.Split('\n'));
        allowedWordsList = new List<string>(allowedWords.text.Split('\n'));
    }

    //Choose a random word from the list
    public void ChooseRandomWord()
    {
        int chosenWordIndex = Random.Range(0, possibleAnswersList.Count);
        chosenWord = possibleAnswersList[chosenWordIndex].Trim();
    }

    //Submit the guess
    public string SubmitGuess(TMP_InputField userInput)
    {
        userGuess = userInput.ToString().ToLower();
        return userGuess;
    }

    //Check for valid user Input
    public bool IsValidGuess(string userGuess)
    {
        if (!possibleAnswersList.Contains(userGuess) && !allowedWordsList.Contains(userGuess))
            return false;
        else
            return true;
    }

    

    //Play the game if valid
    public void PlayGame(int guessRow)
    {
        if(currentAttempt <= maxAttempt)
        {
            currentAttempt++;

            if (IsValidGuess(userGuess))
            {
                //Convert chosenWord into array
                char[] wordArray = chosenWord.ToCharArray();
                //Convert guess into array
                char[] guessArray = userGuess.ToCharArray();

                foreach (char letter in guessArray)
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
                        currentRow[i].GetComponentInChildren<TMP_Text>().text = guessArray[letter].ToString().ToLower();
                    }
                }
                //Loop through the char array to check the guess
                foreach (char letter in guessArray)
                {
                    if (letter == wordArray[0])
                    {
                        //Color the square green
                    }
                    else if (wordArray.Contains(letter))
                    {
                        //Color the square yellow
                    }
                    else
                    {
                        //Color the square gray
                    }

                }

            }
        }
    }
}
