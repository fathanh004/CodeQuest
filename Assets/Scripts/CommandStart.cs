using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CommandStart : Command
{
    static CommandStart instance;
    public static CommandStart Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<CommandStart>();
            }
            return instance;
        }
    }

    [SerializeField]
    GameObject slot;
    public List<Command> commandList = new List<Command>();
    public int currentIndex = 0;
    public bool isGameFinished = false;
    public bool isExecuting = false;
    public UnityEvent allCommandExecuted;

    private void Awake()
    {
        allCommandExecuted.AddListener(() =>
        {
            if (!isGameFinished)
            {
                UIManager.Instance.ShowWarningPanel(
                    "Goal tidak tercapai, ulangi dan buat algoritma yang benar!"
                );
            }
        });
    }

    public void StartAllCommand()
    {
        isExecuting = true;
        UIManager.Instance.HidePanel();
        commandList.Clear();
        foreach (Transform child in slot.transform)
        {
            Command command = child.GetComponent<Command>();
            commandList.Add(command);
        }

        currentIndex = 0;
        CheckRepeatCommand();
        ExecuteNextCommand();
    }

    public void SetCurrentCommandList()
    {
        commandList.Clear();
        foreach (Transform child in slot.transform)
        {
            Command command = child.GetComponent<Command>();
            commandList.Add(command);
        }
    }

    public void CheckRepeatCommand()
    {
        int repeatCommandIndex = -1;
        int closingCommandIndex = -1;

        for (int i = 0; i < commandList.Count; i++)
        {
            if (commandList[i] is CommandRepeat)
            {
                Debug.Log("Repeat command found");
                repeatCommandIndex = i;
            }

            if (commandList[i] is ClosingCommand && repeatCommandIndex != -1)
            {
                Debug.Log("Closing command found");
                closingCommandIndex = i;
                break;
            }

            if (repeatCommandIndex != -1 && closingCommandIndex != -1)
            {
                break;
            }
        }

        //repeat all command between repeat command and closing command times repeat count then add it to command list and remove repeat command and closing command only
        if (repeatCommandIndex != -1 && closingCommandIndex != -1)
        {
            CommandRepeat repeatCommand = commandList[repeatCommandIndex] as CommandRepeat;
            List<Command> repeatedCommandList = new List<Command>();
            for (int i = repeatCommandIndex + 1; i < closingCommandIndex; i++)
            {
                repeatedCommandList.Add(commandList[i]);
            }

            for (int i = 0; i < repeatCommand.repeatCount; i++)
            {
                commandList.InsertRange(repeatCommandIndex, repeatedCommandList);
            }

            //remove repeat command and closing command
            commandList
                .FindAll(x => x is CommandRepeat || x is ClosingCommand)
                .ForEach(x => commandList.Remove(x));
        }
    }

    public void ExecuteNextCommand()
    {
        if (isGameFinished)
        {
            return;
        }

        if (currentIndex < commandList.Count)
        {
            commandList[currentIndex].Execute();
        }
        else
        {
            allCommandExecuted.Invoke();
            isExecuting = false;
            UIManager.Instance.ShowPanel();
        }
        currentIndex++;
    }
}
