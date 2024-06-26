using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DirectionalButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Direction direction;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (direction == Direction.Left)
        {
            CameraController.Instance.leftButtonPressed = true;
        }
        else
        {
            CameraController.Instance.rightButtonPressed = true;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (direction == Direction.Left)
        {
            CameraController.Instance.leftButtonPressed = false;
        }
        else
        {
            CameraController.Instance.rightButtonPressed = false;
        }
    }
}

[Serializable]
public enum Direction
{
    Left,
    Right
}
