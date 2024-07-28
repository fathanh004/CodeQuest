using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    static BackgroundMusic instance; 

    public static BackgroundMusic Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<BackgroundMusic>();
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
