using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;
using TMPro;
namespace estem
{
    public class DashboardTriggers : MonoBehaviour
    {
        public GameObject menuCanvas;
        public GameObject loadingCanvas;
        public GameObject lessonStatsText;

        public string lessonName;
        public string startLessonTime;
        public float lessonTime = 0;
        public bool lessonStarted;
        void Start()
        {
            Debug.Log("Student Logged In");
            menuCanvas = GameObject.Find("Menu Canvas");

            menuCanvas.GetComponent<LogInMenu>().MenuCam.SetActive(false);
            menuCanvas.GetComponent<LogInMenu>().LoadingCanvas.SetActive(false);

            startLessonTime = System.DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss");
            lessonName = menuCanvas.GetComponent<LogInMenu>().lessonName;

            loadingCanvas = menuCanvas.GetComponent<LogInMenu>().LoadingCanvas;
            StartLesson();

        }


        void Update()
        {
            if (lessonStarted)
            {
                AddTime();
            }

            if (GetComponent<PlaceTraps>().ShowStats)
            {
                GetComponent<PlaceTraps>().ShowStats = false;
                EndLesson();
            }

            if (!GetComponent<PlaceTraps>().LessonCompleted)
            {
                lessonStatsText.GetComponent<TextMeshProUGUI>().text = "Student ID: " + menuCanvas.GetComponent<LogInMenu>().studentId +
                                                    "\n\nCurrent Lesson: " + lessonName +
                                                    "\n\nLesson Duration : " + Mathf.Round((lessonTime / 60) * 100) / 100 + " Minutes" +
                                                    "\n\nStart Time: " + startLessonTime +
                                                    "\n\nCurrent Time: " + System.DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss");
            }
            
        }


        void AddTime()
        {
            lessonTime += Time.deltaTime;
        }

        void StartLesson()
        {
            Debug.Log("Lesson " + int.Parse(lessonName) + " Started");
            lessonTime = 0;
            lessonStarted = true;
        }


        public void EndLesson()
        {
            if (GetComponent<PlaceTraps>().LessonCompleted)
            {
                lessonStarted = false;
                lessonStatsText.GetComponent<TextMeshProUGUI>().text = "Student ID: " + menuCanvas.GetComponent<LogInMenu>().studentId +
                                                                    "\n\nLesson Completed: " + lessonName +
                                                                    "\n\nLesson Duration : " + Mathf.Round((lessonTime / 60) * 100) / 100 + " Minutes" +
                                                                    "\n\nStart Time: " + startLessonTime +
                                                                    "\n\nFinish Time: " + System.DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss");
            }

            else
            {
                lessonStatsText.GetComponent<TextMeshProUGUI>().text = "Student ID: " + menuCanvas.GetComponent<LogInMenu>().studentId +
                                                    "\n\nCurrent Lesson: " + lessonName +
                                                    "\n\nLesson Duration : " + Mathf.Round((lessonTime / 60) * 100) / 100 + " Minutes" +
                                                    "\n\nStart Time: " + startLessonTime +
                                                    "\n\nCurrent Time: " + System.DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss");

            }



            Debug.Log("Lesson " + int.Parse(lessonName) + " Ended");

            // Post Lesson Name and Lesson Time
            Debug.Log(StatUtils.postStat(menuCanvas.GetComponent<LogInMenu>().studentId, Stats.gameplay_seconds_per_lesson, int.Parse(lessonName), lessonTime));

            // Post Lesson Name and number of completions per Lesson 
            Debug.Log(StatUtils.postStat(menuCanvas.GetComponent<LogInMenu>().studentId, Stats.completions_per_lesson, int.Parse(lessonName), 1));
        }

        public void MainMenu(Button currentButton)
        {
            loadingCanvas.SetActive(true);
            SceneManager.LoadScene("Log In", LoadSceneMode.Single);
            currentButton.interactable = false;
        }
        public void QuitGame(Button currentButton)
        {
            Debug.Log("Application ending after " + Time.time + " seconds");

            // Post Sudent Log Off Date/Time
            DateTime now = DateTime.Now;
            Debug.Log(StatUtils.postStat(menuCanvas.GetComponent<LogInMenu>().studentId, Stats.logout_time_per_date, now.Date, now));

            // Post amount of time student has played today
            Debug.Log(StatUtils.postStat(menuCanvas.GetComponent<LogInMenu>().studentId, Stats.gameplay_seconds_per_date, now, Time.time));
            Application.Quit();
            currentButton.interactable = false;
        }

        public void Back(Button currentButton)
        {
            GetComponent<PlaceTraps>().ShowStats = false;
            GetComponent<PlaceTraps>().EndGameCanvas.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            currentButton.interactable = false;
        }

        public void OnHover(Button currentButton)
        {
            currentButton.interactable = true;
        }

        public void OnNotHover(Button currentButton)
        {
            currentButton.interactable = false;
        }

    }
}
