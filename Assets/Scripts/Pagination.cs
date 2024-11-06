using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialPanel : MonoBehaviour
{
    int currentPageIndex = 0;

    public GameObject[] pages;

    [SerializeField]
    Button nextButton;

    [SerializeField]
    Button previousButton;

    private void Awake()
    {
        //add listener to next and previous button
        nextButton.onClick.AddListener(NextPage);
        previousButton.onClick.AddListener(PreviousPage);
    }

    private void OnEnable()
    {
        //hide all pages except the first page
        currentPageIndex = 0;
        for (int i = 1; i < pages.Length; i++)
        {
            pages[i].SetActive(false);
        }
        pages[currentPageIndex].SetActive(true);
        CheckButtons();
    }

    public void NextPage()
    {
        if (currentPageIndex < pages.Length - 1)
        {
            pages[currentPageIndex].SetActive(false);
            currentPageIndex++;
            pages[currentPageIndex].SetActive(true);
        }
        CheckButtons();
    }

    public void PreviousPage()
    {
        if (currentPageIndex > 0)
        {
            pages[currentPageIndex].SetActive(false);
            currentPageIndex--;
            pages[currentPageIndex].SetActive(true);
        }
        CheckButtons();
    }

    //function check if current page the last page, hide next button; if current page the first page, hide previous button
    public void CheckButtons()
    {
        if (currentPageIndex == 0)
        {
            previousButton.gameObject.SetActive(false);
        }
        else
        {
            previousButton.gameObject.SetActive(true);
        }

        if (currentPageIndex == pages.Length - 1)
        {
            nextButton.gameObject.SetActive(false);
        }
        else
        {
            nextButton.gameObject.SetActive(true);
        }
    }
}
