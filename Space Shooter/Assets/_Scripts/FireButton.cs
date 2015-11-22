using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class FireButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

    public bool buttonPressed;
    public GameObject player;



    public void OnPointerDown(PointerEventData eventData)
    {
        player.SendMessage("Fire", true);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        player.SendMessage("Fire", false);
    }
}
