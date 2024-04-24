using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CommandSlot : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    Image image;
    public UnityEvent onPointerEnter;
    public UnityEvent onPointerExit;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        GameObject droppedObject = eventData.pointerDrag;
        Command command = droppedObject.GetComponent<Command>();
        command.parentAfterDrag = transform;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        GameObject draggedObject = eventData.pointerDrag;
        if (draggedObject != null)
        {
            Debug.Log("OnPointerEnter" + draggedObject.name);
            if (draggedObject.TryGetComponent<Command>(out var command))
            {
                onPointerEnter.Invoke();
            }
        }
    }


    public void OnPointerExit(PointerEventData eventData)
    {
        GameObject draggedObject = eventData.pointerDrag;
        if (draggedObject != null)
        {
            Debug.Log("OnPointerExit" + draggedObject.name);
            if (draggedObject.TryGetComponent<Command>(out var command))
            {
                onPointerExit.Invoke();
            }
        }
    }

    public void Hide()
    {
        image.color = new Color32(255, 255, 255, 0);
    }

    public void Show()
    {
        image.color = new Color32(255, 255, 255, 80);
    }
}
