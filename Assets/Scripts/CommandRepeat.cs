using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CommandRepeat : Command
{
    public int repeatCount;
    public TMP_Dropdown dropdown;

    private void Awake()
    {
        base.Awake();
        dropdown = GetComponentInChildren<TMP_Dropdown>();
    }

    public void OnChange()
    {
        repeatCount = dropdown.value;
    }
    

}
