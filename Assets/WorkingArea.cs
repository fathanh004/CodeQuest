using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WorkingArea : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        GameObject droppedObject = eventData.pointerDrag;
        Command command = droppedObject.GetComponent<Command>();
        command.parentAfterDrag = transform;
    }
}

