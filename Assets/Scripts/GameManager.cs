using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using Unity.VisualScripting;

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

    [SerializeField]
    WinPanel winPanel;

    int starCount = 0;
    float timePassed = 0;
    bool isGameFinished = false;

    public UnityEvent onGoalReached;

    public UnityEvent onStarCollected;

    public UnityEvent onRestart;

    public UnityEvent onStart;

    private void Awake()
    {
        startButton.onClick.AddListener(StartGame);
        restartButton.onClick.AddListener(Restart);
        onStart.AddListener(() =>
        {
            CommandStart.Instance.StartAllCommand();
            startButton.gameObject.SetActive(false);
            restartButton.gameObject.SetActive(true);
        });
        onGoalReached.AddListener(() =>
        {
            isGameFinished = true;
            winPanel.ShowWinPanel(starCount, timePassed);
        });
        onStarCollected.AddListener(() =>
        {
            starCount++;
        });
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
        timePassed = 0;
        if (RepeatCommandChecker())
        {
            Debug.Log(RepeatCommandChecker());
            onStart.Invoke();
        }
    }

    private void Update()
    {
        if (!isGameFinished)
        {
            timePassed += Time.deltaTime;
        }
    }

    //
    // Summary:
    // Check whether there is a repeat command in the command list, if there is, check whether the repeat command has a closing command or not.
    // Return true if there is a repeat command and the repeat command has a closing command OR there is no repeat command at all.
    public bool RepeatCommandChecker()
    {
        int repeatCommandIndex = -1;
        int closingCommandIndex = -1;

        CommandStart.Instance.SetCurrentCommandList();
        for (int i = 0; i < CommandStart.Instance.commandList.Count; i++)
        {
            if (CommandStart.Instance.commandList[i] is CommandRepeat)
            {
                repeatCommandIndex = i;
            }
            else if (CommandStart.Instance.commandList[i] is ClosingCommand)
            {
                closingCommandIndex = i;
            }
        }

        if (repeatCommandIndex != -1 && closingCommandIndex == -1)
        {
            UIManager.Instance.ShowWarningPanel(
                "Gunakan \"Batas Perulangan\" untuk menutup perulangan!"
            );
            return false;
        }
        else if (repeatCommandIndex == -1 && closingCommandIndex != -1)
        {
            UIManager.Instance.ShowWarningPanel(
                "\"Batas Perulangan\" tidak dapat digunakan tanpa perulangan!"
            );
            return false;
        }
        else if (repeatCommandIndex > closingCommandIndex)
        {
            UIManager.Instance.ShowWarningPanel(
                "Batas perulangan harus diletakkan setelah perulangan!"
            );
            return false;
        }

        Debug.Log(repeatCommandIndex + " " + closingCommandIndex);
        return true;
    }
}
