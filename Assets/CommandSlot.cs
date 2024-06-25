using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CommandSlot : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    Image image;
    public UnityEvent onAddedCommand;
    public UnityEvent onRemovedCommand;
    public UnityEvent onPointerEnter;
    public UnityEvent onPointerExit;

    int currentCommandCount = 0;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    private void Update()
    {
        if (transform.childCount > currentCommandCount)
        {
            onAddedCommand.Invoke();
            currentCommandCount++;
        }

        if (transform.childCount < currentCommandCount)
        {
            onRemovedCommand.Invoke();
            currentCommandCount--;
        }
    }

    private void Start()
    {
        onAddedCommand.AddListener(IncreaseImageHeight);
        onRemovedCommand.AddListener(DecreaseImageHeight);
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
            if (draggedObject.TryGetComponent<Command>(out var command))
            {
                onPointerExit.Invoke();
            }
        }
    }
    

    void IncreaseImageHeight()
    {
        image.rectTransform.sizeDelta = new Vector2(
            image.rectTransform.sizeDelta.x,
            image.rectTransform.sizeDelta.y + 80
        );
        Hide();
    }

    void DecreaseImageHeight()
    {
        image.rectTransform.sizeDelta = new Vector2(
            image.rectTransform.sizeDelta.x,
            image.rectTransform.sizeDelta.y - 80
        );
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
