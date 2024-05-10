using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Animator animator;

    // Kecepatan gerakan pemain
    [SerializeField]
    private float moveSpeed = 5f;

    Vector3 initialPosition;
    Quaternion initialRotation;

    private void Awake()
    {
        initialPosition = transform.position;
        initialRotation = transform.rotation;
    }

    private void Update()
    {
        // Tambahkan input atau kondisi untuk memicu gerakan maju
        if (Input.GetKeyDown(KeyCode.W))
        {
            MoveForward(1f); // Pemanggilan MoveForward dengan jarak 1
        }
    }

    // Metode untuk mengatur ulang posisi pemain dan rotasi
    public void ResetPosition()
    {
        transform.SetPositionAndRotation(initialPosition, initialRotation);
    }
   

    // Metode untuk gerakan maju dan animasi
    public void MoveForward(float distance)
    {
        // Atur animator ke kecepatan 1 (atau sesuai kebutuhan)
        animator.SetFloat("Speed", 1f);

        // Dapatkan arah hadapan pemain
        Vector3 forward = transform.forward;

        // Tentukan posisi akhir berdasarkan jarak yang diinginkan
        Vector3 targetPosition = transform.position + forward * distance;

        // Gerakkan pemain ke depan berdasarkan kecepatan dan waktu
        transform.position = Vector3.MoveTowards(
            transform.position,
            targetPosition,
            moveSpeed * Time.deltaTime
        );
    }

    // Metode untuk menghadap ke arah tertentu
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
