using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RunButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField]
    GameObject player;

    public void OnPointerDown(PointerEventData eventData) {
        player.GetComponent<Animator>().SetTrigger("run");
        player.GetComponent<BoyPlayer>().move = true;
    }

    public void OnPointerUp(PointerEventData eventData) {
        player.GetComponent<Animator>().SetTrigger("stay");
        player.GetComponent<BoyPlayer>().move = false;
    }
}
