using UnityEngine;
using TMPro; // Add this for TextMeshPro
using UnityEngine.UI; // For Button class

public class QuizManager : MonoBehaviour
{
    public GameObject questionUI;
    public TextMeshProUGUI questionText; // Updated to TextMeshProUGUI
    public Toggle A1;
    public Toggle A2;
    public Toggle A3;
    public Toggle A4;
    public Toggle[] answerToggles = new Toggle[4]; // Array of Toggle components (A1, A2, A3, A4)
    public Button submitButton; // Submit button for checking the answer
    public PlayerMovement playerMovementScript; // Reference to the player's movement script
    public Text text; // for correctness of question

    private float counter = 0;
    private Question[] questions; // Array for fixed questions
    private int currentQuestionIndex = 0;
    private string correctAnswer = "test";
    private bool examActive;

    void Start()
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
        questions = new Question[10];
        questions[0] = new Question("What is 2 + 2?", new string[] { "3", "4", "5", "6" }, "4");
        // Add other questions similarly

        // Add listener to the submit button
        submitButton.onClick.AddListener(CheckAnswer);
    }

    public void StartQuiz()
    {
        // Display the current question
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

    void CheckAnswer()
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
            text.text = "Corrext";
            while (counter <= 3)
            {
                counter += Time.deltaTime;
            }
            EndQuiz();
        }
        else
        {
            text.text = "Incorrect";
            while (counter <= 3)
            {
                counter += Time.deltaTime;
            }
            EndQuiz();
        }
    }

    public void EndQuiz()
    {
        examActive = false;
        questionUI.SetActive(false); // Hide the question UI
        Debug.Log("Quiz Ended.");
        playerMovementScript.enabled = true;
    }

    public bool IsExamActive()
    {
        return examActive;
    }
}
