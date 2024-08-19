using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXButtonManager : MonoBehaviour
{
    public void PlaySFXButton()
    {
        ButtonSFX.Instance.GetComponent<AudioSource>().Play();
    }
}
