
using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class GuiPhoneButtonScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    Vector3 normalPosition = new Vector3(-454.2f, -241.2f, 0);
    Vector3 hoverPosition = new Vector3(-435.7f, -214.4f, 0);
    RectTransform rectTransform = null;

    public void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        rectTransform.localPosition = hoverPosition;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        rectTransform.localPosition = normalPosition;
    }

    public void OpenPhone ()
    {
        Debug.Log("clicked");
    }
}
