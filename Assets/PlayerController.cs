using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Animator animator;

    [SerializeField]
    private float duration;

    Vector3 initialPosition;
    Quaternion initialRotation;
    bool isWalking = false;

    public UnityEvent onDoneExecuting;

    private void Awake()
    {
        initialPosition = transform.position;
        initialRotation = transform.rotation;
    }

    private void Start()
    {
        onDoneExecuting.RemoveAllListeners();
        onDoneExecuting.AddListener(() =>
        {
            CommandStart.Instance.ExecuteNextCommand();
            Debug.Log("Done executing");
        });
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            MoveForward();
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            FacingTowards("kiri");
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            FacingTowards("kanan");
        }
    }

    // Metode untuk mengatur ulang posisi pemain dan rotasi
    public void ResetPosition()
    {
        transform.SetPositionAndRotation(initialPosition, initialRotation);
    }

    public void MoveForward()
    {
        if (isWalking) return;
        isWalking = true;
        animator.SetBool("IsWalking", isWalking);
        var targetPosition = transform.localPosition + transform.forward;
        Debug.Log("Move forward to " + targetPosition);
        transform
            .DOMove(targetPosition, duration)
            .onKill += () =>
            {
                isWalking = false;
                animator.SetBool("IsWalking", isWalking);
                onDoneExecuting.Invoke();
            };
    }

    // Metode untuk menghadap ke arah tertentu
    public void FacingTowards(string direction)
    {
        Vector3 targetRotation = transform.eulerAngles;
        if (direction == "kiri")
        {
            targetRotation.y -= 90;
        }
        else if (direction == "kanan")
        {
            targetRotation.y += 90;
        }

        transform
            .DORotate(targetRotation, duration)
            .onKill += () =>
            {
                onDoneExecuting.Invoke();
            };
    }
}
