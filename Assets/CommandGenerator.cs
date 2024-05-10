using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CommandGenerator : MonoBehaviour
{
    public GameObject commandPrefab;

    public UnityEvent onCommandDraggedOut;
    int currentChildCount = 0;

    private void Start()
    {
        GenerateCommand();
        onCommandDraggedOut.AddListener(GenerateCommand);
    }

    private void Update() {
        if (currentChildCount > transform.childCount)
        {
            onCommandDraggedOut.Invoke();
            currentChildCount = transform.childCount;
        }
    }

    private void GenerateCommand()
    {
        Instantiate(commandPrefab, transform);
        currentChildCount++;
    }
}
