using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CommandFacing : Command
{
    public string direction;

    public TMP_Dropdown dropdown;

    public override void Awake()
    {
        base.Awake();
        dropdown = GetComponentInChildren<TMP_Dropdown>();
    }

    public void OnChange()
    {
        if (dropdown.options[dropdown.value].text == "Kanan")
        {
            direction = "kanan";
        }
        else if (dropdown.options[dropdown.value].text == "Kiri")
        {
            direction = "kiri";
        }
    }
}
