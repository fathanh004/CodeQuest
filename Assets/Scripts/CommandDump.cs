using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CommandDump : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        Command draggedCommand = eventData.pointerDrag.GetComponent<Command>();
        if (draggedCommand.canBeDeleted)
        {
            Destroy(draggedCommand.gameObject);
        }
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
