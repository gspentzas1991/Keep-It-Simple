
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using Assets.Scripts.Classes;
using UnityEngine.UI;

public class GuiPhoneButtonScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private RectTransform rectTransform = null;
    private RectTransformParameters hoverPositionTransform = new RectTransformParameters();
    private RectTransformParameters fullscreenPositionTransform = new RectTransformParameters();
    private Vector3 idlePhonePosition = new Vector3(-454.2f, -241.2f, 0);
    private bool isSwitching = false;
    public PhonePosition phonePosition = PhonePosition.IconTray;
    [SerializeField] private float switchAnimationSpeed = 5f;
    private float switchAnimationTime = 0f;
    [SerializeField] private RawImage phoneMenu;

    public void Start()
    {
        rectTransform = GetComponent<RectTransform>();

        //Initialize the hoverPosition and fullscreenPosition rectTransforms
        hoverPositionTransform.Position = new Vector3(-435.7f, -214.4f, 0);
        hoverPositionTransform.Rotation = Quaternion.Euler(new Vector3(0, 0, -26.15f));
        hoverPositionTransform.Size = new Vector2(86.27f, 140.3f);

        fullscreenPositionTransform.Position = new Vector3(0, 0, 0);
        fullscreenPositionTransform.Rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        fullscreenPositionTransform.Size = new Vector2(230, 640);

    }
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (rectTransform.sizeDelta == hoverPositionTransform.Size)
        {
            rectTransform.localPosition = hoverPositionTransform.Position;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (rectTransform.sizeDelta == hoverPositionTransform.Size)
        {
            rectTransform.localPosition = idlePhonePosition;
        }
    }

    public void Update()
    {
        if (isSwitching)
        {
            switchAnimationTime += Time.deltaTime * switchAnimationSpeed;
            if (phonePosition == PhonePosition.IconTray)
            {
                MovePhonePosition(hoverPositionTransform, fullscreenPositionTransform, switchAnimationTime);
            }
            else if (phonePosition == PhonePosition.Fullscreen)
            {
                MovePhonePosition(fullscreenPositionTransform, hoverPositionTransform, switchAnimationTime);
            }
            CheckIfPhoneReachedPosition();
        }
    }

    /// <summary>
    /// Checks if the switching phone has reached a final position, and sets phonePosition variable accordingly
    /// </summary>
    private void CheckIfPhoneReachedPosition()
    {
        if (rectTransform.sizeDelta == fullscreenPositionTransform.Size)
        {
            isSwitching = false;
            phonePosition = PhonePosition.Fullscreen;
            phoneMenu.enabled = true;
        }
        else if (rectTransform.sizeDelta == hoverPositionTransform.Size)
        {
            isSwitching = false;
            phonePosition = PhonePosition.IconTray;
        }
    }

    /// <summary>
    /// Lineary Interpolates the phone's rectTransform
    /// </summary>
    private void MovePhonePosition(RectTransformParameters startingTransform, RectTransformParameters endingTransform , float interpolationPercentage)
    {
        rectTransform.localPosition = Vector3.Lerp(startingTransform.Position, endingTransform.Position, interpolationPercentage);
        rectTransform.localRotation = Quaternion.Slerp(startingTransform.Rotation, endingTransform.Rotation, interpolationPercentage);
        rectTransform.sizeDelta = Vector2.Lerp(startingTransform.Size, endingTransform.Size, interpolationPercentage);
    }

    //Fired when the user clicks on the phone 
    public void ClickedOnPhone ()
    {
        if(rectTransform.sizeDelta == fullscreenPositionTransform.Size || rectTransform.sizeDelta == hoverPositionTransform.Size)
        {
            isSwitching = true;
            switchAnimationTime = 0;
            phoneMenu.enabled = false;
        }
    }
}
