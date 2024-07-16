using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{

    //dont destroy on load
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
