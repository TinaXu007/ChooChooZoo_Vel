using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ScreenTestController : MonoBehaviour
{
    public LoadingScene loadingScene; // Loads the train scene
    public GameObject questionObject; // Text version of question
    public GameObject questionPicObject; // Picture
    public GameObject questionBackgroundObject; // The big panel behind the text of the question 
    public Logger logger; // Store the user responses
    public Sprite[] questionPics; // Possible pictures for the question
    public GameObject[] buttonObjects; // Text answer choices
    public Sprite[] normalButtonPics; // Original pictures for the buttons
    public Sprite[] picAnswers; // Picture answer choices
    public AudioClip[] audioClips; // For reading out the questions
    public ScreenTestKeyboardControls keyboardControls; // Script that turns the keyboard controls on or off and handles their input
    public float questionDelay = 1f; // How many seconds between the disappearance of one question and the creation of the next
    public float requiredCorrectAnswerPercentage = 100f; //60f; // Hardcoded value for what percentage of the questions the user must answer correctly to go to the next "tier"

    [SerializeField]
    private Sprite[] possibleColors; // For the question that asks which color is blue
    private AudioSource audioSource; // For reading out the questions
    private ScreenTestQuestion currentQuestion; // Question that is being iterated over in a call of NextQuestion()
    private TextMeshProUGUI questionLabel; // Allows us to alter the text of the question label
    private TextMeshProUGUI currentAnswerLabel; // Similar thing, but for the answer label that's being iterated over in the loop
    private Vector3 currentAnswerPosition; // Stores Vector form of the anchoredPosition for button's RectTransform
    private float correctAnswerPercentage = 0f;
    private int questionCounter = 0; // Stores index of the current question
    private int correctAnswers = 0;
    private int questionSet = 0; // 0 - Easy; 1 - Medium; 2 - Hard
    private bool isAudioPlaying = false; // Check if the clip is playing so Update() doesn't mess up the audio                    
    private ScreenTestQuestion[] questions;
    private System.Random rand; // To pick random questions from a question set
    private List<int> answerIDs; // Represents the int IDs of the possible answers for the currently selected question

    private ScreenTestQuestion[] level1Questions;
    private ScreenTestQuestion[] level2Questions;
    private ScreenTestQuestion[] level3Questions;
    private ScreenTestQuestion[] level4Questions;

    private ScreenTestQuestion[][] questionSets;
        

    // Start is called before the first frame update
    // Define all the possible questions
    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        rand = new System.Random();
        answerIDs = new List<int>();

        {/* questions = new ScreenTestQuestion[]
        {
            new ScreenTestQuestion("Press now!", new string[] { "" }, new string[] { "button" }, 1, null, null),
            new ScreenTestQuestion("Press the left one!", new string[] { "", "" }, new string[] { "left", "right" }, 1, null, null),
            new ScreenTestQuestion("Press the right one!", new string[] { "", "" }, new string[] { "left", "right" }, 2, null, null),
            new ScreenTestQuestion("Which one is blue?", new string[] { "", "" }, new string[] { "blue", "purple" }, 1, null, new Sprite[] {picAnswers[3],  picAnswers[4]})
        };
              
        for (int i = 0; i < questions.Length; i++) // Configure all the questions with their audio readout
        {
            questions[i].SetAudio(audioClips[i]); 
        }
        

        questionSets = new ScreenTestQuestion[][] { 
            questions            
        };*/
        }

        // Parameter format
        // (string questionText, string[] answersText, string[] actualAnswers, int correct, Sprite questionPicInput, Sprite[] answerPicsInput)
        level1Questions = new ScreenTestQuestion[]
        {
            // Color identification
            new ScreenTestQuestion("Is this apple red? Press the green button for 'Yes' or the red button for 'No'.", new string[] { "Yes", "No"}, new string[] { "Yes", "No" }, 1, questionPics[0], null),
            new ScreenTestQuestion("Is this banana yellow? Press the green button for 'Yes' or the red button for 'No'.", new string[] { "Yes", "No" }, new string[] { "Yes", "No" }, 1, questionPics[1], null),
            new ScreenTestQuestion("Is the sky blue? Press the green button for 'Yes' or the red button for 'No'.", new string[] { "Yes", "No" }, new string[] { "Yes", "No" }, 1, null, null),
            new ScreenTestQuestion("Is this strawberry blue? Press the green button for 'Yes' or the red button for 'No'.", new string[] { "Yes", "No" }, new string[] { "Yes", "No" }, 2, questionPics[2], null),
            new ScreenTestQuestion( "Is this leaf green? Press the green button for 'Yes' or the red button for 'No'.", new string[] { "Yes", "No" }, new string[] { "Yes", "No" }, 1, questionPics[3], null),
            
            // Animal identification
            new ScreenTestQuestion( "Is this a picture of a dog or a cat?", new string[] { "Dog", "Cat" }, new string[] { "Dog", "Cat" }, 1, questionPics[4], null),
            new ScreenTestQuestion("Does a cow say 'meow'? Press the green button for 'Yes' or the red button for 'No'.", new string[] { "Yes", "No" }, new string[] { "Yes", "No" }, 2, null, null),
            new ScreenTestQuestion("Is this animal a fish? Press the green button for 'Yes' or the red button for 'No'.", new string[] { "Yes", "No" }, new string[] { "Yes", "No" }, 1, questionPics[5], null),
            new ScreenTestQuestion("Does this animal eat grass? Press the green button for 'Yes' or the red button for 'No'.", new string[] { "Yes", "No" }, new string[] { "Yes", "No" }, 2, questionPics[6], null),
            new ScreenTestQuestion("Is this animal an elephant? Press the green button for 'Yes' or the red button for 'No'.", new string[] { "Yes", "No" }, new string[] { "Yes", "No" }, 1, questionPics[7], null),
            new ScreenTestQuestion("Is this a picture of a lion? Press the green button for 'Yes' or the red button for 'No'.", new string[] { "Yes", "No" }, new string[] { "Yes", "No" }, 1, questionPics[8], null),

            // Number Recognition
            new ScreenTestQuestion("Do you see number 1 or number 2?", new string[] { "1", "2" }, new string[] { "1", "2" }, 2, questionPics[22], null),
            new ScreenTestQuestion("Are there 3 apples here? Press the green button for 'Yes' or the red button for 'No'.", new string[] { "Yes", "No" }, new string[] { "Yes", "No" }, 1, questionPics[9], null),
            new ScreenTestQuestion("Do you see 4 butterflies? Press the green button for 'Yes' or the red button for 'No'.", new string[] { "Yes", "No" }, new string[] { "Yes", "No" }, 1, questionPics[10], null),
            new ScreenTestQuestion("Are there 2 lions here? Press the green button for 'Yes' or the red button for 'No'.", new string[] { "Yes", "No" }, new string[] { "Yes", "No" }, 1, questionPics[11], null),
            new ScreenTestQuestion("Are there 5 bananas on the screen? Press the green button for 'Yes' or the red button for 'No'.", new string[] { "Yes", "No" }, new string[] { "Yes", "No" }, 2, questionPics[12], null),
            new ScreenTestQuestion("Do you see more than 1 moon? Press the green button for 'Yes' or the red button for 'No'.", new string[] { "Yes", "No" }, new string[] { "Yes", "No" }, 2, questionPics[13], null),

            // Shape Recognition
            new ScreenTestQuestion("Is this a circle or a square?", new string[] { "Circle", "Square" }, new string[] { "Circle", "Square" }, 1, questionPics[14], null),
            new ScreenTestQuestion("Is this shape a circle? Press the green button for 'Yes' or the red button for 'No'.", new string[] { "Yes", "No" }, new string[] { "Yes", "No" }, 2, questionPics[15], null),
            new ScreenTestQuestion("Is this a square? Press the green button for 'Yes' or the red button for 'No'.", new string[] { "Yes", "No" }, new string[] { "Yes", "No" }, 2, questionPics[16], null),
            new ScreenTestQuestion("Is this shape round like a ball? Press the green button for 'Yes' or the red button for 'No'.", new string[] { "Yes", "No" }, new string[] { "Yes", "No" }, 1, questionPics[17], null),
            new ScreenTestQuestion("Does this shape look like a box? Press the green button for 'Yes' or the red button for 'No' for a cube.", new string[] { "Yes", "No" }, new string[] { "Yes", "No" }, 1, questionPics[18], null)
        };

        level2Questions = new ScreenTestQuestion[]
        {
            new ScreenTestQuestion("What comes next in this pattern, star or heart? Star, Heart, Star, ..." , new string[] { "Star", "Heart" }, new string[] { "Star", "Heart" }, 2, questionPics[23], null),
            new ScreenTestQuestion("What do we use to cut paper, scissors or a spoon?", new string[] { "Scissors", "Spoon" }, new string[] { "Scissors", "Spoon" }, 1, null, null),
            new ScreenTestQuestion("Do these two pictures match?", new string[] { "Yes", "No" }, new string[] { "Yes", "No" }, 2, questionPics[19], null),
            new ScreenTestQuestion("Are there 2 or 3 ducks here?", new string[] { "2", "3" }, new string[] { "2", "3" }, 2, questionPics[20], null),
            new ScreenTestQuestion("Did we see a banana or apple earlier?", new string[] { "Banana", "Apple" }, new string[] { "Banana", "Apple" }, 1, null, null)
        };

        level3Questions = new ScreenTestQuestion[]
        {
            new ScreenTestQuestion("The pattern is red, blue, red, blue. What comes next? Press the green button for red or the red button for blue.", new string[] { "Red", "Blue" }, new string[] {  "Red", "Blue" }, 1, null, null),
            new ScreenTestQuestion("Did we see a cat earlier? Press the green button for 'Yes' or the red button for 'No'.", new string[] { "Yes", "No" }, new string[] { "Yes", "No" }, 2, null, null),
            new ScreenTestQuestion("What number comes after 3? Press the green button for 4 or the red button for 5.", new string[] { "4", "5" }, new string[] { "4", "5" }, 1, null, null),
            new ScreenTestQuestion("We have 1 cookie and 2 children. Is 1 cookie enough? Press the green button for 'Yes' or the red button for 'No'.", new string[] { "Yes", "No" }, new string[] { "Yes", "No" }, 2, null, null),
            new ScreenTestQuestion("Which is more: 3 apples or 5 apples? Press the green button for 3 or the red button for 5.", new string[] { "3", "5" }, new string[] { "3", "5" }, 2, null, null)
        };

        level4Questions = new ScreenTestQuestion[]
        {
            new ScreenTestQuestion("The pattern is circle, square, circle, square. Which shape is missing in the sequence: circle, __, circle, square? Press the green button for circle or the red button for square.", new string[] { "Circle", "Square" }, new string[] { "Circle", "Square" }, 2, null, null),
            new ScreenTestQuestion("What was the first animal we saw today? Press the green button for a dog or the red button for a cat.", new string[] { "Dog", "Cat" }, new string[] { "Dog", "Cat" }, 1, null, null),
            new ScreenTestQuestion("If we have 5 candies and eat 1, how many are left? Press the green button for 4 or the red button for 3.", new string[] { "4", "3" }, new string[] { "4", "3" }, 1, null, null),
            new ScreenTestQuestion("If you water a plant, will it grow? Press the green button for 'Yes' or the red button for 'No'.", new string[] { "Yes", "No" }, new string[] { "Yes", "No" }, 1, null, null),
            new ScreenTestQuestion("Will the big bear fit in the small box? Press the green button for 'Yes' or the red button for 'No'.", new string[] { "Yes", "No" }, new string[] { "Yes", "No" }, 2, questionPics[21], null)
        };

        questionSets = new ScreenTestQuestion[][] {
            level1Questions,
            level2Questions,
            level3Questions,
            level4Questions
        };

        // Set the AI voiceovers for all the questions
        int audioNum = 0; 
        for (int i = 0; i < questionSets.Length; i++)
        {
            for (int j = 0; j < questionSets[i].Length; j++)
            {
                questionSets[i][j].SetAudio(audioClips[audioNum]);
                audioNum++;
            }
        }

        questionSet = 0; // Begin with the easy questions

        questionLabel = questionObject.GetComponent<TextMeshProUGUI>();
        {/*foreach(ScreenTestQuestion q in questions)
        {
            Debug.Log(q.GetQuestion());
        }*/
        //Debug.Log(questionLabel.text);
        }

        HideEverything();
        NewQuestionSet(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NextQuestion()
    {    
        questionObject.SetActive(true);
        currentQuestion = questions[questionCounter];
        int numAnswers = (currentQuestion.IsPictureAnswers()) ? currentQuestion.GetPics().Length : currentQuestion.GetAnswers().Length; // Length depends on which answer type we're using here (pictures or strings)
        keyboardControls.InitiateControls(buttonObjects, numAnswers, 0);

        // Activate the question and set it up
        if (currentQuestion.IsPictureQuestion()) 
        {
            questionPicObject.SetActive(true);
            questionPicObject.GetComponent<Image>().sprite = currentQuestion.GetQuestionPic();

            if (currentQuestion.IsTextQuestion()) questionPicObject.GetComponent<RectTransform>().localPosition = questionPicObject.GetComponent<RectTransform>().localPosition; // new Vector3(0f, 200f, 0f); // If we have text, move up the picture (NEVERMIND, DON'T NEED TO DO THAT WITH THE NEW UI)
            else questionObject.SetActive(false); // Turn off text object if it's not set
            //numAnswers = currentQuestion.GetPics().Length;
        }
        if (currentQuestion.IsTextQuestion()) // And 0 (otherwise) means it's a text question
        {
            questionBackgroundObject.SetActive(true);
            questionObject.SetActive(true);
            if (!currentQuestion.IsPictureQuestion()) questionPicObject.SetActive(false); // Turn off picture if it's not set
            questionLabel.text = currentQuestion.GetQuestion(); // Gets the text for the question            
        }

        if (currentQuestion.GetAudio() != null)
        {
            //Debug.Log(currentQuestion.GetAudio().name);
            audioSource.clip = currentQuestion.GetAudio();
            audioSource.Play();
        }

        for (int i = 0; i < numAnswers; i++) // This list contains all the indices in the list of answers. Every time the random number generator picks one of the IDs, it removes the answer associated with it. Through this process, we can mix up the order of the answers.
        {
            answerIDs.Add(i);
        }

        for (int i = 0; i < numAnswers; i++)
        {
            int j = answerIDs[rand.Next(answerIDs.Count)]; // Randomly pick one of the answers
            buttonObjects[j].SetActive(true);

            if (currentQuestion.IsPictureAnswers())
            {
                buttonObjects[j].GetComponent<Button>().image.sprite = currentQuestion.GetPic(j); 
            }
            else
            {
                //buttonObjects[j].GetComponent<Button>().image.sprite = normalButtonPics[j];
                currentAnswerLabel = buttonObjects[j].transform.GetChild(0).GetComponent<TextMeshProUGUI>(); // Activate 1 of the answer buttons and get its label component
                currentAnswerLabel.text = currentQuestion.GetAnswer(j); // Replace its text
            }

            // Distribute distances between buttons
            currentAnswerPosition = buttonObjects[j].GetComponent<RectTransform>().anchoredPosition;
            //buttonObjects[j].GetComponent<RectTransform>().anchoredPosition = new Vector3(-960f + (960f * 2f / (numAnswers + 1)) * (i + 1), currentAnswerPosition.y, currentAnswerPosition.z);

            answerIDs.Remove(j); // Remove the randomly selected answer
        }

        // questionCounter++; // Currently we are only sampling 1 question from each question set, so we do not need to increment the counter

        //StartCoroutine(DelayedStateChange());
    }

    public void ButtonSelected(int buttonName)
    {
        if (currentQuestion.IsChoiceCorrect(buttonName))
        {
            Debug.Log("Correct");
            correctAnswers++;
        }
        else
        {
            Debug.Log("Incorrect");
        }
        //Debug.Log(buttonName - 1); for (int i = 0; i < currentQuestion.GetAnswers().Length; i++) Debug.Log(currentQuestion.GetAnswerValue(i));
        logger.LogData(new string[] { currentQuestion.GetQuestion() }, new string[] { "user: ", currentQuestion.GetAnswerValue(buttonName - 1), " correct: ", currentQuestion.GetCorrectAnswerValue() });

        StartCoroutine(DelayedStateChange());
    }

    // Hides everything after question is answered, delays, then pops up with the next question
    private IEnumerator DelayedStateChange()
    {
        keyboardControls.DeactivateControls();
        HideEverything();

        yield return new WaitForSeconds(questionDelay);

        questionObject.SetActive(true);

        /*foreach (GameObject button in buttonObjects)
        {
            button.SetActive(true);
        }*/

        //Debug.Log(questionCounter + " " + questions.Length);
        // If we hit the end, 
        /*if (questionCounter == questions.Length) {
            // Load the next scene
            EndScreenTest();

            // Which is determined by scene flow manager (not me)

            //NewQuestionSet(false); // Uncomment this if you ever want to have multiple question sets again
        }
        else
        {
            NextQuestion();
        }*/

        NewQuestionSet(false); // Always go to the next question set after 1 question
    }

    // Upgrades difficulty if answering many questions. Loads up a new set of questions based off of that.
    private void NewQuestionSet(bool firstTime)
    {        
        if (!firstTime) // Load a harder or easier question set based off the previous one's user performance
        {
            correctAnswerPercentage = (float)correctAnswers / 1f /*(float)questions.Length*/ * 100f; // Divide by 1 for now because we are currently only doing 1 question per question set
            if (correctAnswerPercentage >= requiredCorrectAnswerPercentage)
            {
                if (questionSet != questionSets.Length - 1) questionSet++;
                else loadingScene.LoadScene("Train_Scene");
            }
            else
            {
                if (questionSet != 0) questionSet--; // Go down a question set if we're not at the first one (can't go down at the first bc that'll cause an out of range error)
            }
        }
        // If it's the first question set ever loaded, just set the questions to whatever is the default question set
        questions = questionSets[questionSet];

        // Reset fields for the next set
        questionCounter = rand.Next(questions.Length); // Randomize the question that is drawn from the next set //questionCounter = 0; 
        correctAnswers = 0;
        NextQuestion();
    }

    // Clear the screen
    private void HideEverything()
    {
        questionObject.SetActive(false);
        questionPicObject.SetActive(false);
        foreach (GameObject button in buttonObjects)
        {
            button.SetActive(false);
        }
    }

    private void EndScreenTest()
    {
        HideEverything();
        GameObject.Find("Background").SetActive(false);
        // Load the next scene
    }
}
