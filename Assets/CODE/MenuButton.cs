using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MenuButton : MonoBehaviour
{
    public void PlayButton() {
        StartCoroutine(WaitAndGoRoom(0.15f, 1));
    }

    public void SettingsButton() {
        Camera.main.GetComponent<Controller>().settingsUI.SetActive(true);
    }

    public void ExitButton() {
        Application.Quit();
    }

    public void ExitSettingsButton() {
        Camera.main.GetComponent<Controller>().settingsUI.SetActive(false);
    }

    IEnumerator WaitAndGoRoom(float waitTime, int sceneIndex) {
        GameObject.FindGameObjectWithTag("ADMOB").GetComponent<Admob>().ShowAdInit();

        yield return new WaitForSeconds(waitTime);

        SceneManager.LoadScene(sceneIndex);
    }
}
