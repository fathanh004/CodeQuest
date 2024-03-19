using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CommandFacing : MonoBehaviour
{
    public string direction;

    public TMP_Dropdown dropdown;

    private void Awake()
    {
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
