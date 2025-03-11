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

    //Update the score
    public void UpdateScore(int score)
    {
        gameScoreText.text = score.ToString();
        winScoreText.text = score.ToString();
        loseScoreText.text = score.ToString();
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
    public void SubmitGuess()
    {
        userGuess = userInput.text.ToLower();
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
        if (string.IsNullOrEmpty(userGuess))
        {
            ShowInvalidCanvas();
            return new string[0];
        }
        string[] result = new string[userGuess.Length];

        if(userGuess == chosenWord)
        {
            score++;
            UpdateScore(score);
            ShowWinCanvas();
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = "correct";
            }
            return result;
        }
        else
        {
            //Convert both words to char arrays
            char[] guessArray = userGuess.ToCharArray();
            char[] wordArray = chosenWord.ToCharArray();

            for (int i = 0; i < guessArray.Length; i++)
            {
                if (i < wordArray.Length && guessArray[i] == wordArray[i])
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
    }

    //Color the squares based on result
    private void ColorSquares(Button[]rowButtons)
    {
        //Get the results from checkguess function
        string[] results = CheckGuess();

        for(int i = 0; i < results.Length; i++)
        {
            //Get the button image component
            Image buttonImage = rowButtons[i].GetComponent<Image>();
            
            //Color green if correct
            if (results[i] == "correct")
            {
                buttonImage.color = Color.green;
            }
            
            //Color yellow if contains
            else if (results[i] == "contains")
            {
                buttonImage.color = Color.yellow;
            }

            //Color gray if incorrect
            else
            {
                buttonImage.color = Color.gray;
            }
        }
    }

    //Get Guess Row for use
    private Button[] GuessRow
    {
        get
        {
            switch (currentAttempt)
            {
                case 0:
                    return guessRowOne;
                case 1:
                    return guessRowTwo;
                case 2:
                    return guessRowThree;
                case 3:
                    return guessRowFour;
                case 4:
                    return guessRowFive;
                default:
                    return new Button[0];
            }
        }
    }

    //Play the game if valid
    public void PlayGame()
    {
        if(currentAttempt <= maxAttempt)
        {
            SubmitGuess();
            
            if(string.IsNullOrEmpty(userGuess) || !IsValidGuess(userGuess))
            {
                ShowInvalidCanvas();
                return;
            }

            else if (IsValidGuess(userGuess))
            {
                Button[] currentRowButtons = GuessRow;
                //Displays the users guess in that row
                DisplayLetters(currentRowButtons);
                //Check users guess
                CheckGuess();
                //Colors the squares based on results
                ColorSquares(currentRowButtons);


                currentAttempt++;
            }
            else
            {
                ShowInvalidCanvas();
            }
        }
        else
        {
            ShowLoseCanvas();
        }
        
    }
}
