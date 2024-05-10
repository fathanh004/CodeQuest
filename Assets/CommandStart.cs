using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    List<Command> commandList = new List<Command>();
    int currentIndex = 0;

    public void StartAllCommand()
    {
        commandList.Clear();
        foreach (Transform child in slot.transform)
        {
            Command command = child.GetComponent<Command>();
            AddListenerToCommand(command);
            commandList.Add(command);
        }

        currentIndex = 0;
        ExecuteNextCommand();
    }

    public void ExecuteNextCommand()
    {
        if (currentIndex < commandList.Count)
        {
            commandList[currentIndex].Execute();
            currentIndex++;
        }
    }

    void AddListenerToCommand(Command command)
    {
        command.onDoneExecuting.RemoveAllListeners();
        command.onDoneExecuting.AddListener(ExecuteNextCommand);
    }
}
