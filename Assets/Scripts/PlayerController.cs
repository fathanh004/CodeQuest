using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;
using TMPro;

public class PlayerController : MonoBehaviour
{
    static PlayerController instance;
    public static PlayerController Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<PlayerController>();
            }
            return instance;
        }
    }
    [SerializeField]
    private Animator animator;

    [SerializeField]
    private float duration;

    Vector3 initialPosition;
    Quaternion initialRotation;
    bool isWalking = false;
    bool isRotating = false;
    Collider collider;

    public UnityEvent onDoneExecuting;

    private void Awake()
    {
        initialPosition = transform.position;
        initialRotation = transform.rotation;
        collider = GetComponent<Collider>();
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
        if (isWalking)
            return;

        var targetPosition = transform.localPosition + transform.forward;

        if (IsFrontTileTagEqual(targetPosition, "Wall"))
        {
            Debug.Log("Wall detected");
            targetPosition = transform.localPosition;
        }

        isWalking = true;
        animator.SetBool("IsWalking", isWalking);

        transform.DOMove(targetPosition, duration).onKill += () =>
        {
            isWalking = false;
            animator.SetBool("IsWalking", isWalking);
            onDoneExecuting.Invoke();
        };
    }

    public IEnumerator GoalReached()
    {
        yield return new WaitForSeconds(2);
        GameManager.Instance.onGoalReached.Invoke();
    }

    public bool IsFrontTileTagEqual(Vector3 targetPosition, string tag)
    {
        Collider[] hitColliders = Physics.OverlapSphere(targetPosition, 0.1f);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag(tag))
            {
                return true;
            }
        }
        return false;
    }

    // Metode untuk menghadap ke arah tertentu
    public void FacingTowards(string direction)
    {
        if (isRotating)
            return;
        isRotating = true;
        Vector3 targetRotation = transform.eulerAngles;
        if (direction == "kiri")
        {
            targetRotation.y -= 90;
        }
        else if (direction == "kanan")
        {
            targetRotation.y += 90;
        }

        transform.DORotate(targetRotation, duration).onKill += () =>
        {
            isRotating = false;
            onDoneExecuting.Invoke();
        };
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Goal"))
        {
            Debug.Log("Goal reached");
            StartCoroutine(GoalReached());
        }
    }
}
