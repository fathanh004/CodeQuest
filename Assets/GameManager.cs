using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{

    static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();
            }
            return instance;
        }
    }
    [SerializeField]
    PlayerController playerController;

    [SerializeField]
    GameObject commandListGameObject;

    [SerializeField]
    Button startButton;

    [SerializeField]
    Button restartButton;
    
    public UnityEvent onGoalReached;

    public UnityEvent onRestart;

    private void Awake()
    {
        startButton.onClick.AddListener(StartGame);
        restartButton.onClick.AddListener(Restart);
    }

    //reload scene
    public void Restart()
    {
        onRestart.Invoke();
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
