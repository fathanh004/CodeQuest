using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    bool isMateriAlreadyRead = false;

    [SerializeField]
    GameObject blockPanelMulai;

    [SerializeField]
    GameObject materiPanel;

    [SerializeField]
    Button bacaMateriButton;

    [SerializeField]
    Button mulaiButton;

    private void Awake()
    {
        isMateriAlreadyRead = PlayerPrefs.GetInt("isMateriAlreadyRead") == 1;
        bacaMateriButton.onClick.AddListener(BacaMateri);
    }

    private void Start()
    {
        CheckMateriIsRead();
    }

    void CheckMateriIsRead()
    {
        if (isMateriAlreadyRead)
        {
            blockPanelMulai.SetActive(false);
            mulaiButton.interactable = true;
        }
        else
        {
            blockPanelMulai.SetActive(true);
            mulaiButton.interactable = false;
        }
    }

    public void BacaMateri()
    {
        isMateriAlreadyRead = true;
        PlayerPrefs.SetInt("isMateriAlreadyRead", isMateriAlreadyRead ? 1 : 0);
        CheckMateriIsRead();
        materiPanel.SetActive(true);
    }
}
