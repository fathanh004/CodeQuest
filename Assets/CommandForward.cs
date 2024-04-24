using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CommandForward : Command
{
    public int step;
    public TMP_Text labelText;

    public override void Awake()
    {
        base.Awake();
        labelText = GetComponentInChildren<TMP_Text>();
        labelText.text = "Maju " + step + " langkah";
    }
}
