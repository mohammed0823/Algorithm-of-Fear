using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Subsystems;

public class OpenExam : MonoBehaviour, Interactable
{
    public GameObject ExamUI;

    private QuizManage quizManager;
   
    private static GameObject exam;


    public void Interact(GameObject obj)
    {
        if (PlayerStats.hasPen)
        {
            exam = obj;

            quizManager = ExamUI.GetComponent<QuizManage>();

            ExamUI.SetActive(true);
            quizManager.StartQuiz();
            //quizManager.GetExam(obj);
            
        }
    }

    public static void RemoveExam()
    {
        Destroy(exam);
    }

    //public void HideExam()
    //{
    //    exam.SetActive(false);
    //    tmp = exam;
    //    Invoke("ShowExam", 30);
    //}

    //public void ShowExam()
    //{
    //    tmp.SetActive(true);
    //}
}
