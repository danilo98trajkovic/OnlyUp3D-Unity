using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JumpButton : MonoBehaviour, IPointerDownHandler/*, IPointerMoveHandler*/, IPointerUpHandler {

    bool joyPressed = false;

    private void Awake() {
        
    }

    public void OnPointerDown(PointerEventData eventData) {
        if (transform.name == "JumpButton") {
            Camera.main.GetComponent<Controller>().player.GetComponent<BoyPlayer>().Jump();
        }else if (transform.name == "Joystick") {
            joyPressed = true;
            /*Camera.main.GetComponent<CameraFollow>().canRotateCam = false;
            StartCoroutine(WaitAndDisableCameraRotate());*/
        }
    }

    /*void IPointerMoveHandler.OnPointerMove(PointerEventData eventData) {
        if (transform.name == "Joystick" && joyPressed == true) {
            Camera.main.GetComponent<CameraFollow>().canRotateCam = false;
        }
    }*/

    void IPointerUpHandler.OnPointerUp(PointerEventData eventData) {
        if (transform.name == "Joystick" && joyPressed == true) {
            joyPressed = false;
        }
    }

    IEnumerator WaitAndDisableCameraRotate() {
        yield return new WaitForSeconds(0.001f);

        Camera.main.GetComponent<CameraFollow>().canRotateCam = false;
    }
}
