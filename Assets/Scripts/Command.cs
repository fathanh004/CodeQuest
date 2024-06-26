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
