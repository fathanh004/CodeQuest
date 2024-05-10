using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    PlayerController playerController;

    [SerializeField]
    GameObject commandListGameObject;

    [SerializeField]
    Button startButton;

    [SerializeField]
    Button restartButton;

    private void Awake()
    {
        startButton.onClick.AddListener(StartGame);
        restartButton.onClick.AddListener(Restart);
    }

    //reload scene
    public void Restart()
    {
        playerController.ResetPosition();
        foreach (Transform child in commandListGameObject.transform)
        {
            Destroy(child.gameObject);
        }
        startButton.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(false);
    }

    public void StartGame()
    {
        CommandStart.Instance.StartAllCommand();
        startButton.gameObject.SetActive(false);
        restartButton.gameObject.SetActive(true);
    }
}
