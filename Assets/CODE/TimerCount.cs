using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerCount : MonoBehaviour
{
    public int timerSeconds = 0;
    public Text timerText;

    void Awake() {

        StartCoroutine(WaitAndCount());
    }

    IEnumerator WaitAndCount() {
        yield return new WaitForSeconds(1f);

        if (Controller.globalFinish == false) {
            timerSeconds++;
            StartCoroutine(WaitAndCount());
        }

        RefreshText();

        
    }

    void RefreshText() {

        var minutes = Mathf.Floor(timerSeconds / 60);
        var seconds = Mathf.Floor(timerSeconds % 60);

        timerText.text = (minutes < 10 ? "0" + minutes.ToString() : minutes.ToString()) + ":"
                           + (seconds < 10 ? "0" + seconds.ToString() : seconds.ToString()) + "s";
    }
}
