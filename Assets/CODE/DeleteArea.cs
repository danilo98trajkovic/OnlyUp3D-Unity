using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeleteArea : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision) {
        if (collision.transform.tag == "Player") {
            if (Controller.globalFinish == false) {

                StartCoroutine(WaitAndShowFailed());

                Controller.globalFinish = true;
            }
        }
    }

    IEnumerator WaitAndShowFailed() {
        yield return new WaitForSeconds(0.5f);

        Camera.main.GetComponent<Controller>().ShowFailedUI();
    }
}
