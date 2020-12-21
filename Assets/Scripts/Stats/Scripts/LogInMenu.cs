using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

namespace estem
{
    public class LogInMenu : MonoBehaviour
    {
        public GameObject studentLogInMenu;
        public TMP_InputField studentIdInput;

        public Button logInButton;
        public Button lessonButton1;
        public Button lessonButton2;
        public Button lessonButton3;
        public Button lessonButton4;
        public Button lessonButton5;
        public Button lessonButton6;
        public Button lessonButton7;

        public string studentId;
        public string lessonName;

        public GameObject MenuCam;
        public GameObject LoadingCanvas;
        public GameObject LessonSelectCanvas;

        void Start()
        {
            StatTests.testCompleteGameClientDataFlow();
            logInButton.interactable = false;
            lessonButton1.interactable = false;
            lessonButton2.interactable = false;
            lessonButton3.interactable = false;
            lessonButton4.interactable = false;
            lessonButton5.interactable = false;
            lessonButton6.interactable = false;
            lessonButton7.interactable = false;

            LoadingCanvas.SetActive(false);
        }

        void Update()
        {
            if (studentLogInMenu.activeSelf)
            {
                if (logInButton.interactable)
                {
                    if (studentIdInput.text.ToString() == "" /*|| int.Parse(studentIdInput.text.ToString()) > 12*/)
                    {
                        DisableButton();
                    }
                }
            }

            if (SceneManager.GetSceneByName("1").isLoaded || SceneManager.GetSceneByName("2").isLoaded || SceneManager.GetSceneByName("3").isLoaded || SceneManager.GetSceneByName("4").isLoaded || SceneManager.GetSceneByName("5").isLoaded || SceneManager.GetSceneByName("6").isLoaded || SceneManager.GetSceneByName("7").isLoaded)
            {
                LoadingCanvas.SetActive(false);
            }
        }
        public void OnTextFieldNotEmpty()
        {
            logInButton.interactable = true;
        }

        public void DisableButton()
        {
            logInButton.interactable = false;
        }
        public void OnLogInButton()
        {

            // Load Lesson Select Menu
            LessonSelectCanvas.SetActive(true);


            studentId = studentIdInput.text.ToString();
            Debug.Log("Student ID: " + studentId);
            studentLogInMenu.SetActive(false);

            // Log in student
            Debug.Log(ClientUtils.loginUser(studentId));

            // Post log in time
            DateTime now = DateTime.Now;
            Debug.Log(StatUtils.postStat(studentId, Stats.login_time_per_date, now.Date, now));
        }

        public void OnHover(Button currentButton)
        {
            currentButton.interactable = true;
        }

        public void OnNotHover(Button currentButton)
        {
            currentButton.interactable = false;
        }

        public void Quit()
        {
            Application.Quit();
        }

        public void OnLesson1Button()
        {
            LoadingCanvas.SetActive(true);
            LessonSelectCanvas.SetActive(false);
            SceneManager.LoadScene("1", LoadSceneMode.Additive);
            lessonName = "1";
        }

        public void OnLesson2Button()
        {
            LoadingCanvas.SetActive(true);
            LessonSelectCanvas.SetActive(false);
            SceneManager.LoadScene("2", LoadSceneMode.Additive);
            lessonName = "2";
        }

        public void OnLesson3Button()
        {
            LoadingCanvas.SetActive(true);
            LessonSelectCanvas.SetActive(false);
            SceneManager.LoadScene("3", LoadSceneMode.Additive);
            lessonName = "3";
        }

        public void OnLesson4Button()
        {
            LoadingCanvas.SetActive(true);
            LessonSelectCanvas.SetActive(false);
            SceneManager.LoadScene("4", LoadSceneMode.Additive);
            lessonName = "4";
        }

        public void OnLesson5Button()
        {
            LoadingCanvas.SetActive(true);
            LessonSelectCanvas.SetActive(false);
            SceneManager.LoadScene("5", LoadSceneMode.Additive);
            lessonName = "5";
        }

        public void OnLesson6Button()
        {
            LoadingCanvas.SetActive(true);
            LessonSelectCanvas.SetActive(false);
            SceneManager.LoadScene("6", LoadSceneMode.Additive);
            lessonName = "6";
        }

        public void OnLesson7and8Button()
        {
            LoadingCanvas.SetActive(true);
            LessonSelectCanvas.SetActive(false);
            SceneManager.LoadScene("7", LoadSceneMode.Additive);
            lessonName = "7";
        }


    }
}
