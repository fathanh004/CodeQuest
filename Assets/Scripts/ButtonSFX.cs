using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSFX : MonoBehaviour
{
    static ButtonSFX instance;

    public static ButtonSFX Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<ButtonSFX>();
            }
            return instance;
        }
    }

    //dont destroy on load
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
}
