using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CommandGenerator : MonoBehaviour
{
    public GameObject commandPrefab;

    public UnityEvent onCommandDraggedOut;
    int currentChildCount = 0;

    public bool isMaxOneCommand = false;

    private void Start()
    {
        GenerateCommand();
        if (!isMaxOneCommand)
        {
            onCommandDraggedOut.AddListener(GenerateCommand);
        }
        else
        {
            GameManager.Instance.onRestart.AddListener(GenerateCommand);
        }
    }

    private void Update()
    {
        if (currentChildCount > transform.childCount)
        {
            onCommandDraggedOut.Invoke();
            currentChildCount = transform.childCount;
        }

        if (transform.childCount > 1)
        {
            Destroy(transform.GetChild(0).gameObject);
            currentChildCount = transform.childCount;
        }
    }

    public void GenerateCommand()
    {
        Instantiate(commandPrefab, transform);
        currentChildCount++;
    }
}
