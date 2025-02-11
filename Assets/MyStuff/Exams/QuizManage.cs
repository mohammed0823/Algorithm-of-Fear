using UnityEngine;
using TMPro; // Add this for TextMeshPro
using UnityEngine.UI; // For Button class

public class QuizManage : MonoBehaviour
{
    public GameObject questionUI;
    public Text questionText; // Updated to TextMeshProUGUI
    public Toggle A1;
    public Toggle A2;
    public Toggle A3;
    public Toggle A4;
    public Toggle[] answerToggles = new Toggle[4]; // Array of Toggle components (A1, A2, A3, A4)
    public Button submitButton; // Submit button for checking the answer
    public PlayerMovement playerMovementScript; // Reference to the player's movement script
    public Text text; // for correctness of question

    private Question[] questions; // Array for fixed questions
    private int currentQuestionIndex = 0;
    private string correctAnswer = "test";
    public static bool examActive;

    private static int maxCount = 5;
    public static int count = 0;

    private GameObject exam;

    public static bool won = false;


    void Start()
    {
        
        // Add other questions similarly

       
    }

    public void StartQuiz()
    {

        answerToggles[0] = A1;
        answerToggles[1] = A2;
        answerToggles[2] = A3;
        answerToggles[3] = A4;

        A1.onValueChanged.AddListener(A1Tog);
        A2.onValueChanged.AddListener(A2Tog);
        A3.onValueChanged.AddListener(A3Tog);
        A4.onValueChanged.AddListener(A4Tog);

        // Hard-code questions
        if (count == 0)
        {
            questions = new Question[10];
            questions[0] = new Question("What does HTML stand for?", new string[] { "Hyper Text Markdown Language", "Hyper Text Markup Language", "High Text Machine Language", "Hyperlink Text Modeling Language" }, "Hyper Text Markup Language");
        }
        if (count == 1)
        {
            questions = new Question[10];
            questions[0] = new Question("Which of the following is NOT a programming language?", new string[] { "Python", "Java", "HTML", "Ruby" }, "HTML");
        }
        if (count == 2)
        {
            questions = new Question[10];
            questions[0] = new Question("What does a for loop do?", new string[] { "It executes code only once", "It repeats code a specified number of times", "It checks for syntax errors in the code", "It converts code into machine language" }, "It repeats code a specified number of times");
        }
        if (count == 3)
        {
            questions = new Question[10];
            questions[0] = new Question("What is the output: print(2 + 2 * 3)", new string[] { "12", "8", "10", "4" }, "8");
        }
        if (count == 4)
        {
            questions = new Question[10];
            questions[0] = new Question("What is not a valid Python variable name?", new string[] { "myvar1", "myVar2", "3myVar", "_myVar4" }, "3myVar");
        }

        // Add listener to the submit button
        //submitButton.onClick.AddListener(CheckAnswer);

        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        playerMovementScript.enabled = false;

        //Display the current question
        DisplayQuestion(questions[currentQuestionIndex]);

        // Set the exam state
        examActive = true;
    }

    void DisplayQuestion(Question question)
    {
        questionText.text = question.questionText; // Update question text
        correctAnswer = question.correctAnswer;

        // Set toggle texts for answers
        for (int i = 0; i < answerToggles.Length; i++)
        {
            if (i < question.answers.Length)
            {
                Text toggleText = answerToggles[i].GetComponentInChildren<Text>();
                if (toggleText != null)
                {
                    toggleText.text = question.answers[i];
                }

                answerToggles[i].gameObject.SetActive(true);
                answerToggles[i].isOn = false; // Ensure toggles are reset
            }
            else
            {
                answerToggles[i].gameObject.SetActive(false); // Hide unused toggles
            }
        }
    }

    void A1Tog(bool isOn)
    {
        if ((A2.isOn | A3.isOn | A4.isOn) & isOn)
        {
            A2.isOn = false;
            A3.isOn = false;
            A4.isOn = false;
        }
    }

    void A2Tog(bool isOn)
    {
        if ((A1.isOn | A3.isOn | A4.isOn) & isOn)
        {
            A1.isOn = false;
            A3.isOn = false;
            A4.isOn = false;
        }
    }

    void A3Tog(bool isOn)
    {
        if ((A1.isOn | A2.isOn | A4.isOn) & isOn)
        {
            A1.isOn = false;
            A2.isOn = false;
            A4.isOn = false;
        }
    }

    void A4Tog(bool isOn)
    {
        if ((A1.isOn | A3.isOn | A2.isOn) & isOn)
        {
            A1.isOn = false;
            A3.isOn = false;
            A2.isOn = false;
        }
    }

    public void CheckAnswer()
    {
        // Find the selected answer
        string selectedAnswer = null;
        for (int i = 0; i < answerToggles.Length; i++)
        {
            if (answerToggles[i].isOn) // Check if the toggle is turned on
            {
                Text toggleText = answerToggles[i].GetComponentInChildren<Text>();
                if (toggleText.text != null)
                {
                    selectedAnswer = toggleText.text;
                }
                break;
            }
        }

        // Check if an answer was selected
        if (string.IsNullOrEmpty(selectedAnswer))
        {
            Debug.Log("Please select an answer before submitting.");
            return;
        }

        // Check if the selected answer is correct
        if (selectedAnswer == correctAnswer)
        {
            text.text = "Correct";
            count += 1;
            if (count == maxCount)
            {
                examActive = false;
                playerMovementScript.enabled = true;
                won = true;
            }
            Invoke("EndQuiz", 2);
        }
        else
        {
            text.text = "Incorrect";
           //HideExam();
            examActive = false;
            questionUI.SetActive(false);
            playerMovementScript.enabled = true;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        
    }

    public void EndQuiz()
    {
        text.text = "";
        examActive = false;
        questionUI.SetActive(false); // Hide the question UI
        Debug.Log("Quiz Ended.");
        playerMovementScript.enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        OpenExam.RemoveExam();
    }

    private void HideExam()
    {
        exam.SetActive(false);
        Invoke("ShowExam", 30);
    }

    private void ShowExam()
    {
        exam.SetActive(true);
    }

    public void GetExam(GameObject ex)
    {
        exam = ex;
    }
    

    public bool IsExamActive()
    {
        return examActive;
    }
}
