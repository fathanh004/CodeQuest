using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Command : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    RectTransform rectTransform;
    Image image;
    CanvasGroup canvasGroup;

    [HideInInspector]
    public Transform parentAfterDrag;

    public virtual void Awake()
    {
        image = GetComponent<Image>();
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        //Change color
        image.color = new Color32(255, 255, 255, 170);
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
        image.color = new Color32(255, 255, 255, 255);
        transform.SetParent(parentAfterDrag);
        canvasGroup.blocksRaycasts = true;
    }

    
}
