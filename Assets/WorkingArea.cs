using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WorkingArea : MonoBehaviour, IDropHandler
{
    private void Start()
    {
        GameManager.Instance.onRestart.AddListener(DeleteAllCommandExceptStart);
    }

    public void OnDrop(PointerEventData eventData)
    {
        GameObject droppedObject = eventData.pointerDrag;
        Command command = droppedObject.GetComponent<Command>();
        command.parentAfterDrag = transform;
    }

    public void DeleteAllCommandExceptStart()
    {
        foreach (Transform child in transform)
        {
            Command command = child.GetComponent<Command>();
            if (command == null)
            {
                continue;
            }
            if (command is CommandStart)
            {
                continue;
            }
            Destroy(command.gameObject);
        }
    }
}
