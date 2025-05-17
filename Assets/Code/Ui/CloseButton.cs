using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class CloseButton : MonoBehaviour, IPointerDownHandler
{
    public GameObject targetObject;

    public void OnPointerDown(PointerEventData eventData)
    {
        targetObject.SetActive(false);
    }

}
