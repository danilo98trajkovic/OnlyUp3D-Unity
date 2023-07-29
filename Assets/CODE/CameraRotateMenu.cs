using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotateMenu : MonoBehaviour
{
    void Update()
    {
        transform.Rotate(0, 12 * Time.deltaTime, 0);
    }
}
