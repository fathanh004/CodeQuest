using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    static UIManager instance;
    public static UIManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<UIManager>();
            }
            return instance;
        }
    }

    [SerializeField]
    GameObject panel;

    [SerializeField]
    GameObject missionGoalPanel;

    [Header("Hide or Show Button")]
    [SerializeField]
    GameObject hideOrShowButton;

    [SerializeField]
    Sprite showSprite;

    [SerializeField]
    Sprite hideSprite;

    bool isPanelShowed = true;

    [Header("Warning Panel")]
    [SerializeField]
    GameObject warningPanel;

    [SerializeField]
    TMP_Text warningText;

    public void ShowOrHidePanel()
    {
        var image = hideOrShowButton.GetComponent<Image>();
        if (isPanelShowed)
        {
            HidePanel();
            image.sprite = showSprite;
        }
        else
        {
            ShowPanel();
            image.sprite = hideSprite;
        }
    }

    public void HidePanel()
    {
        panel.GetComponent<RectTransform>().DOAnchorPosX(480, 0.5f);
        isPanelShowed = false;
    }

    public void ShowPanel()
    {
        panel.GetComponent<RectTransform>().DOAnchorPosX(-480, 0.5f);
        isPanelShowed = true;
    }

    public void ShowWarningPanel(string text)
    {
        warningText.text = text;
        warningPanel.SetActive(true);
        StartCoroutine(HideWarningPanel());
    }

    public IEnumerator HideWarningPanel()
    {
        yield return new WaitForSeconds(5);
        warningPanel.SetActive(false);
    }
}
