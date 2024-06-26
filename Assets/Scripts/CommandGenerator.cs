using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class CommandGenerator : MonoBehaviour
{
    public GameObject commandPrefab;

    public GameObject content;

    public TMP_Text allowedCommandsText;

    public UnityEvent onCommandDraggedOut;
    int currentChildCount = 0;

    public int maxCommands = 1;

    public int currentCommands = 0;
    int allowedCommands;

    private void Start()
    {
        allowedCommands = maxCommands;
        GenerateCommand();

        onCommandDraggedOut.AddListener(() => UpdateAllowedCommandsText(-1));
        onCommandDraggedOut.AddListener(GenerateCommand);
        GameManager.Instance.onRestart.AddListener(RefreshCommands);
        UpdateAllowedCommandsText(0);
    }

    public void RefreshCommands()
    {
        currentCommands = 0;
        GenerateCommand();
    }

    private void Update()
    {
        if (currentChildCount > content.transform.childCount)
        {
            onCommandDraggedOut.Invoke();
            currentChildCount = content.transform.childCount;
        }
    }

    public void UpdateAllowedCommandsText(int number)
    {
        allowedCommands += number;
        allowedCommandsText.text = allowedCommands + "/" + maxCommands;
    }

    public void GenerateCommand()
    {
        if (content.transform.childCount > 0)
            return;

        if (currentCommands < maxCommands)
        {
            GameObject command = Instantiate(commandPrefab, content.transform);
            command.GetComponent<Command>().commandGenerator = this;
            command.transform.SetSiblingIndex(0);
            currentCommands++;
            currentChildCount = content.transform.childCount;
        }
    }
}
