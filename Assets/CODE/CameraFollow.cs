using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class CameraFollow : MonoBehaviour {
    [SerializeField]
    GameObject target;

    [SerializeField]
    Camera cam;

    [SerializeField]
    public float distance;

    bool moving = false;

    Vector3 previousPosition;
    public bool canRotateCam = false;

    float fingID;
    Touch pressedTouch;

    bool isAndroidSystem = false;
    private void Awake() {
        if (Application.platform == RuntimePlatform.Android) {
            isAndroidSystem = true;
        }
    }

    void Update()
    {
        cam.transform.position = target.transform.position;




        if (!isAndroidSystem) {
            if (Input.GetMouseButtonDown(0)) {
                if (!EventSystem.current.IsPointerOverGameObject()) {
                    previousPosition = cam.ScreenToViewportPoint(Input.mousePosition);


                    canRotateCam = true;
                }
            }

            if (Input.GetMouseButtonUp(0)) {
                canRotateCam = false;
            }

        }else {
            if (Input.touchCount > 0) {
                foreach (Touch t in Input.touches) {
                    if (t.phase == TouchPhase.Began) {
                        if (!EventSystem.current.IsPointerOverGameObject(t.fingerId)) {
                            previousPosition = cam.ScreenToViewportPoint(t.position);
                            //SceneManager.LoadScene(0);
                            pressedTouch = t;
                            fingID = t.fingerId;

                            canRotateCam = true;
                        }
                    }

                    if (t.phase == TouchPhase.Ended && fingID == t.fingerId) {
                        canRotateCam = false;
                    }

                    if (t.phase == TouchPhase.Moved && fingID == t.fingerId) {
                        if (canRotateCam/* && Camera.main.GetComponent<CameraRaycast>().UIElementClicked == false*/) {
                            Vector3 direction = previousPosition - cam.ScreenToViewportPoint(t.position);

                            cam.transform.Rotate(new Vector3(1f, 0, 0), direction.y * 270);
                            cam.transform.Rotate(new Vector3(0, 1f, 0), -direction.x * 270, Space.World);
                            cam.transform.Translate(new Vector3(0, 0, 0));

                            previousPosition = cam.ScreenToViewportPoint(t.position);
                        }
                    }
                }
            }
        }

        if (!isAndroidSystem) {
            if (canRotateCam && Camera.main.GetComponent<CameraRaycast>().UIElementClicked == false) {
                Vector3 direction = previousPosition - cam.ScreenToViewportPoint(isAndroidSystem ? pressedTouch.position : Input.mousePosition);

                cam.transform.Rotate(new Vector3(1f, 0, 0), direction.y * 270);
                cam.transform.Rotate(new Vector3(0, 1f, 0), -direction.x * 270, Space.World);
                cam.transform.Translate(new Vector3(0, 0, 0));

                previousPosition = cam.ScreenToViewportPoint(isAndroidSystem ? pressedTouch.position : Input.mousePosition);
            }
        }


        cam.transform.Translate(new Vector3(0, 0, -9));
    }
}
