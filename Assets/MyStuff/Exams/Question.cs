// A simple Question class to hold questions and answers
[System.Serializable]
public class Question
{
    public string questionText;
    public string[] answers;
    public string correctAnswer;

    public Question(string questionText, string[] answers, string correctAnswer)
    {
        this.questionText = questionText;
        this.answers = answers;
        this.correctAnswer = correctAnswer;
    }
}