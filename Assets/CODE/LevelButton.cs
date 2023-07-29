using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelButton : MonoBehaviour
{
    public void PauseButton() {
        Time.timeScale = 0f;
        Camera.main.GetComponent<Controller>().pauseUI.SetActive(true);
    }

    public void ResumeButton() {
        Time.timeScale = 1f;
        Camera.main.GetComponent<Controller>().pauseUI.SetActive(false);
    }

    public void RetryCompletedButton() {
        StartCoroutine(WaitAndGoRoom(0.15f, 1));
    }

    public void RetryFailedButton() {
        StartCoroutine(WaitAndGoRoom(0.15f, 1));
    }

    public void ExitButton() {
        StartCoroutine(WaitAndGoRoom(0.15f, 0));
    }

    IEnumerator WaitAndGoRoom(float waitTime, int sceneIndex) {
        GameObject.FindGameObjectWithTag("ADMOB").GetComponent<Admob>().ShowAdInit();
        Time.timeScale = 1f;

        yield return new WaitForSecondsRealtime(waitTime);

        SceneManager.LoadScene(sceneIndex);
    }
}
