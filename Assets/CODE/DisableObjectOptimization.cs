using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableObjectOptimization : MonoBehaviour
{
    GameObject[] staticObjects;
    GameObject player;

    private void Awake() {
        staticObjects = GameObject.FindGameObjectsWithTag("Block");
        player = Camera.main.GetComponent<Controller>().player;

        StartCoroutine(WaitAndEnableDisableObjects());
    }


    IEnumerator WaitAndEnableDisableObjects() {
        yield return new WaitForSeconds(0.2f);

        for (int i = 0; i < staticObjects.Length; i++) {
            if (staticObjects[i].name == "Ground") {
                continue;
            }

            if (Vector3.Distance(staticObjects[i].transform.position, player.transform.position) > 70) {
                staticObjects[i].gameObject.SetActive(false);
            } else {
                staticObjects[i].gameObject.SetActive(true);
            }
        }

        StartCoroutine(WaitAndEnableDisableObjects());
    }

}
