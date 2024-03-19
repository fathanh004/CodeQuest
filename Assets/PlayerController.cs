using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public void MoveForward(int step)
    {
        transform.position += transform.forward * step;
    }

    public void FacingTowards(string direction)
    {
        if (direction == "kiri")
        {
            transform.Rotate(0, -90, 0);
        }
        else if (direction == "kanan")
        {
            transform.Rotate(0, 90, 0);
        }
    }
}
