using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CommandForward : MonoBehaviour
{
    public int step;
    public TMP_Text labelText;

    private void Awake()
    {
        labelText = GetComponentInChildren<TMP_Text>();
        labelText.text = "Maju " + step + " langkah";
    }
}
