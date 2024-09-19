using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CommandDump : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    Image image;

    private void Awake() {
        image = GetComponent<Image>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag.TryGetComponent<Command>(out var draggedCommand))
        {
            ChangeColor(false);
            draggedCommand.DestroyCommand();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (eventData.pointerDrag.TryGetComponent<Command>(out var draggedCommand))
        {
            ChangeColor(true);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (eventData.pointerDrag.TryGetComponent<Command>(out var draggedCommand))
        {
            ChangeColor(false);
        }
    }

    public void ChangeColor(bool isOver)
    {
        if (isOver)
        {
            image.color = new Color32(255, 0, 0, 100); 
        }
        else
        {
            image.color = new Color32(0, 0, 0, 100); 
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
