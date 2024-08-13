using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedLevel : MonoBehaviour
{
    [SerializeField]
    List<Level> levelList = new();

    private void Start()
    {
        // Check if the previous level is cleared
        for (int i = 1; i < levelList.Count; i++)
        {
            if (!levelList[i - 1].isCleared)
            {
                levelList[i].lockedPanel.SetActive(true);
            }
            else
            {
                levelList[i].lockedPanel.SetActive(false);
            }
        }
    }
}
