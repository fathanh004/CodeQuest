using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Command
    : MonoBehaviour,
        IDragHandler,
        IBeginDragHandler,
        IEndDragHandler,
        IPointerEnterHandler,
        IDropHandler,
        IPointerExitHandler
{
    RectTransform rectTransform;
    Image image;
    CanvasGroup canvasGroup;

    protected PlayerController playerController;

    [HideInInspector]
    public Transform parentAfterDrag;
    Color32 currentColor;

    public CommandGenerator commandGenerator;

    public bool canBeDeleted = true;

    public int indexer = -1;

    public virtual void Awake()
    {
        image = GetComponentInChildren<Image>();
        currentColor = image.color;
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();

        playerController = GameObject
            .FindGameObjectWithTag("Player")
            .GetComponent<PlayerController>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        //Change color
        currentColor.a = 170;
        image.color = currentColor;
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
        canvasGroup.blocksRaycasts = false;
    }

    public void DestroyCommand()
    {
        if (canBeDeleted)
        {
            Destroy(gameObject);
            commandGenerator.currentCommands--;
            commandGenerator.UpdateAllowedCommandsText(1);
            commandGenerator.GenerateCommand();
        }
    }

    public bool IsOverlapping(RectTransform rect1, RectTransform rect2)
    {
        if (rect1 == null || rect2 == null)
            return false;

        Rect rect1World = GetWorldRect(rect1);
        Rect rect2World = GetWorldRect(rect2);

        return rect1World.Overlaps(rect2World);
    }

    public Rect GetWorldRect(RectTransform rectTransform)
    {
        Vector3[] corners = new Vector3[4];
        rectTransform.GetWorldCorners(corners);

        float width = Vector3.Distance(corners[0], corners[3]);
        float height = Vector3.Distance(corners[0], corners[1]);

        return new Rect(corners[0].x, corners[0].y, width, height);
    }

    private bool IsThisCommandInsideCommandSlot()
    {
        //check if this command is a child of a command slot
        if (transform.parent.TryGetComponent<CommandSlot>(out var commandSlot))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        GameObject draggedObject = eventData.pointerDrag;
        if (draggedObject != null)
        {
            if (
                draggedObject.TryGetComponent<Command>(out var command)
                && !this.gameObject.CompareTag("StartCommand")
                && IsThisCommandInsideCommandSlot()
            )
            {
                if (command != this)
                {
                    Debug.Log(
                        draggedObject.name
                            + " is over "
                            + name
                            + " index: "
                            + transform.GetSiblingIndex()
                    );
                    currentColor.a = 100;
                    image.color = currentColor;
                }
            }
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        GameObject draggedObject = eventData.pointerDrag;
        if (draggedObject != null)
        {
            if (
                draggedObject.TryGetComponent<Command>(out var command)
                && !this.gameObject.CompareTag("StartCommand")
                && IsThisCommandInsideCommandSlot()
            )
            {
                if (command != this)
                {
                    Debug.Log(
                        draggedObject.name
                            + " is over "
                            + name
                            + " index: "
                            + transform.GetSiblingIndex()
                    );
                    currentColor.a = 255;
                    image.color = currentColor;
                }
            }
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        GameObject droppedObject = eventData.pointerDrag;
        if (droppedObject != null)
        {
            if (
                droppedObject.TryGetComponent<Command>(out var command)
                && !this.gameObject.CompareTag("StartCommand")
                && IsThisCommandInsideCommandSlot()
            )
            {
                Debug.Log(
                    droppedObject.name
                        + " dropped on "
                        + name
                        + " index: "
                        + transform.GetSiblingIndex()
                );
                command.indexer = transform.GetSiblingIndex();
                command.parentAfterDrag = transform.parent;
                currentColor.a = 255;
                image.color = currentColor;
            }
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //Change color
        currentColor.a = 255;
        image.color = currentColor;
        transform.SetParent(parentAfterDrag);
        canvasGroup.blocksRaycasts = true;
        if (indexer != -1)
        {
            transform.SetSiblingIndex(indexer);
            indexer = -1;
        }
    }

    public virtual void Execute() { }
}
