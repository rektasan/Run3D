using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private float rotationAngle = 45f;

    private Vector3 targetRotation;

    void Start()
    {
        targetRotation = Vector3.zero;
    }

    void Update()
    {
        HandleTouchInput();
        MoveForward();
        RotateCharacter();
    }

    void HandleTouchInput()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved)
            {
                if (touch.position.x < Screen.width / 2)
                {
                    RotateLeft();
                }
                else if (touch.position.x > Screen.width / 2)
                {
                    RotateRight();
                }
            }
        }
        else
        {
            ResetRotation();
        }
    }

    private void MoveForward()
    {
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
    }

    private void RotateCharacter()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(targetRotation), rotationSpeed * Time.deltaTime);
    }

    private void RotateLeft()
    {
        targetRotation = new Vector3(0, -rotationAngle, 0);
    }

    private void RotateRight()
    {
        targetRotation = new Vector3(0, rotationAngle, 0);
    }

    private void ResetRotation()
    {
        targetRotation = Vector3.zero;
    }
}
