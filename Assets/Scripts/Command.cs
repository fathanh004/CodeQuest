using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Command : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
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

    private void Update() {
        //check if the command is overlapping with other commands
        bool overlapping = false;

        
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
    }

    public virtual void Execute()
    {
    
    }
}
