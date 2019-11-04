
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using Assets.Scripts.Classes;
using UnityEngine.UI;

public class GuiPhoneButtonScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private RectTransform RectTransform = null;
    private RectTransformParameters IdlePositionTransform = new RectTransformParameters();
    private RectTransformParameters HoverPositionTransform = new RectTransformParameters();
    private RectTransformParameters FullscreenPositionTransform = new RectTransformParameters();
    private bool IsSwitching = false;
    public PhonePosition PhonePosition = PhonePosition.IconTray;
    [SerializeField] private float SwitchAnimationSpeed = 5f;
    private float SwitchAnimationTime = 0f;
    [SerializeField] private RawImage PhoneMenu;

    public void Start()
    {
        RectTransform = GetComponent<RectTransform>();
        //Initialize the position and fullscreenPosition rectTransforms
        IdlePositionTransform.Position = new Vector3(-454.2f, -241.2f, 0);
        IdlePositionTransform.Rotation = Quaternion.Euler(new Vector3(0, 0, -26.15f));
        IdlePositionTransform.Size = new Vector2(86.27f, 140.3f);
        HoverPositionTransform.Position = new Vector3(-435.7f, -214.4f, 0);
        HoverPositionTransform.Rotation = Quaternion.Euler(new Vector3(0, 0, -26.15f));
        HoverPositionTransform.Size = new Vector2(86.27f, 140.3f);
        FullscreenPositionTransform.Position = new Vector3(0, 0, 0);
        FullscreenPositionTransform.Rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        FullscreenPositionTransform.Size = new Vector2(230, 640);
    }
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (RectTransform.sizeDelta == HoverPositionTransform.Size)
        {
            RectTransform.localPosition = HoverPositionTransform.Position;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (RectTransform.sizeDelta == HoverPositionTransform.Size)
        {
            RectTransform.localPosition = IdlePositionTransform.Position;
        }
    }

    public void Update()
    {
        if (IsSwitching)
        {
            SwitchAnimationTime += Time.deltaTime * SwitchAnimationSpeed;
            if (PhonePosition == PhonePosition.IconTray)
            {
                MovePhonePosition(HoverPositionTransform, FullscreenPositionTransform, SwitchAnimationTime);
            }
            else if (PhonePosition == PhonePosition.Fullscreen)
            {
                MovePhonePosition(FullscreenPositionTransform, IdlePositionTransform, SwitchAnimationTime);
            }
            CheckIfPhoneReachedPosition();
        }
    }

    /// <summary>
    /// Checks if the switching phone has reached a final position, and sets phonePosition variable accordingly
    /// </summary>
    private void CheckIfPhoneReachedPosition()
    {
        if (RectTransform.localPosition == FullscreenPositionTransform.Position)
        {
            IsSwitching = false;
            PhonePosition = PhonePosition.Fullscreen;
            PhoneMenu.enabled = true;
        }
        else if (RectTransform.localPosition == IdlePositionTransform.Position)
        {
            IsSwitching = false;
            PhonePosition = PhonePosition.IconTray;
        }
    }

    /// <summary>
    /// Lineary Interpolates the phone's rectTransform
    /// </summary>
    private void MovePhonePosition(RectTransformParameters startingTransform, RectTransformParameters endingTransform , float interpolationPercentage)
    {
        RectTransform.localPosition = Vector3.Lerp(startingTransform.Position, endingTransform.Position, interpolationPercentage);
        RectTransform.localRotation = Quaternion.Slerp(startingTransform.Rotation, endingTransform.Rotation, interpolationPercentage);
        RectTransform.sizeDelta = Vector2.Lerp(startingTransform.Size, endingTransform.Size, interpolationPercentage);
    }

    /// <summary>
    /// Starts the switch of the phone's position
    /// </summary>    
    public void BeginPositionSwitch()
    {
        if(RectTransform.sizeDelta == FullscreenPositionTransform.Size || RectTransform.sizeDelta == HoverPositionTransform.Size)
        {
            IsSwitching = true;
            SwitchAnimationTime = 0;
            PhoneMenu.enabled = false;
        }
    }
}
