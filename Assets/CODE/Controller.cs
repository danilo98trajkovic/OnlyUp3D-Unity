using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Controller : MonoBehaviour
{
    public static string globalGame;
    public static bool globalSound;
    public static bool globalFinish;
    public static int secondsHighscore;

    public GameObject player;

    public GameObject eventSystem;
    public GameObject UICanvas;
    public GameObject timerUI;
    public GameObject settingsUI;

    public GameObject pauseUI;

    public GameObject completedUI;
    public GameObject failedUI;

    public GameObject joy;
    public GameObject jumpButton;
    public GameObject pauseButton;


    private void Awake() {
        Application.targetFrameRate = 60;

        globalGame = "";
        globalSound = PlayerPrefs.GetInt("globalSound", 1) == 1 ? true : false;
        globalFinish = false;
        secondsHighscore = PlayerPrefs.GetInt("highscore", 1000000000);

    }

    public void ShowFailedUI() {
        joy.SetActive(false);
        jumpButton.SetActive(false);
        pauseButton.SetActive(false);
        timerUI.SetActive(false);

        failedUI.transform.GetChild(4).GetComponent<Text>().text = timerUI.transform.GetChild(1).GetComponent<Text>().text;

        if (secondsHighscore == 1000000000) {
            failedUI.transform.GetChild(5).GetComponent<Text>().text = "--:--";
        } else {
            var minutes = Mathf.Floor(secondsHighscore / 60);
            var seconds = Mathf.Floor(secondsHighscore % 60);

            failedUI.transform.GetChild(5).GetComponent<Text>().text = (minutes < 10 ? "0" + minutes.ToString() : minutes.ToString()) + ":"
                               + (seconds < 10 ? "0" + seconds.ToString() : seconds.ToString()) + "s";
        }

        player.GetComponent<BoyPlayer>().move = false;
        player.GetComponent<BoyPlayer>().anim.SetTrigger("stay");

        failedUI.SetActive(true);
    }

    public void ShowCompletedUI() {
        if (secondsHighscore > Camera.main.GetComponent<TimerCount>().timerSeconds) {
            PlayerPrefs.SetInt("highscore", Camera.main.GetComponent<TimerCount>().timerSeconds);
            secondsHighscore = Camera.main.GetComponent<TimerCount>().timerSeconds;
        }

        joy.SetActive(false);
        jumpButton.SetActive(false);
        pauseButton.SetActive(false);
        timerUI.SetActive(false);

        completedUI.transform.GetChild(4).GetComponent<Text>().text = timerUI.transform.GetChild(1).GetComponent<Text>().text;

        if (secondsHighscore == 1000000000) {
            completedUI.transform.GetChild(5).GetComponent<Text>().text = "--:--s";
        } else {
            var minutes = Mathf.Floor(secondsHighscore / 60);
            var seconds = Mathf.Floor(secondsHighscore % 60);

            completedUI.transform.GetChild(5).GetComponent<Text>().text = (minutes < 10 ? "0" + minutes.ToString() : minutes.ToString()) + ":"
                               + (seconds < 10 ? "0" + seconds.ToString() : seconds.ToString()) + "s";
        }

        player.GetComponent<BoyPlayer>().move = false;
        player.GetComponent<BoyPlayer>().anim.SetTrigger("stay");

        completedUI.SetActive(true);
    }
}
