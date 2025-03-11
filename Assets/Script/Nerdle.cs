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

    //Display the letters
    private void DisplayLetters(Button[]rowButtons)
    {
        //Convert to char array
        char[] guessArray = userGuess.ToCharArray();

        for(int i = 0; i < guessArray.Length; i++)
        {
            if(i < rowButtons.Length)
            {
                TMP_Text buttonText = rowButtons[i].GetComponentInChildren<TMP_Text>();
                if(buttonText != null)
                {
                    buttonText.text = guessArray[i].ToString();
                }
            }
        }
    }

    //Check the user guess against chosen random word
    private string[] CheckGuess()
    {
        //Convert both words to char arrays
        char[] guessArray = userGuess.ToCharArray();
        char[] wordArray = chosenWord.ToCharArray();
        string[] result = new string[guessArray.Length];

        for (int i = 0; i < guessArray.Length; i++)
        {
            if(i < wordArray.Length && guessArray[i] == wordArray[i])
            {
                //Letter is correct and in the right spot
                result[i] = "correct";
            }
            else if (chosenWord.Contains(guessArray[i]))
            {
                //Letter is correct but in the wrong spot
                result[i] = "contains";
            }
            else
            {
                //Letter is not in the word
                result[i] = "wrong";
            }
        }
        return result;
    }

    //Color the squares based on result
    private void ColorSquares()
    {
        string[] results = CheckGuess();
    }

    //Play the game if valid
    public void PlayGame(int guessRow)
    {
        if (IsValidGuess(userGuess))
        {
            Button[] currentRowButtons = GetGuessRow(currentAttempt);
            //Displays the users guess in that row
            DisplayLetters(currentRowButtons);
            //Checks users guess against random word
            CheckGuess(currentRowButtons);
            //Colors the squares based on results
            ColorSquares(currentRowButtons);

            currentAttempt++;
        }
    }
}
