using UnityEngine;
using System;

public class PlayerController : MonoBehaviour {
    private CharacterController controller;
    private Vector3 dir;
    [SerializeField] private int speed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float gravity;

    private int lineToMove = 1;
    public float lineDistance = 6; 

    void Start() {
        controller = GetComponent<CharacterController>();
    }
    System.Random rand = new System.Random();

    private void Update() {
        if (SwipeController.swipeRight && lineToMove < 2 ) {
            lineToMove++;
        }

        if (SwipeController.swipeLeft && lineToMove > 0) {
            lineToMove--;
        }

        if (SwipeController.swipeUp && controller.isGrounded){
            Jump();
        }

        Vector3 targetPosition = transform.position.z * transform.forward + transform.position.y * transform.up;

        if (lineToMove == 0){
            targetPosition += Vector3.left * lineDistance;
        }
        else if (lineToMove == 2) {
            targetPosition += Vector3.right * lineDistance;
        }
        transform.position = targetPosition;
    }

    private void Jump() {
        dir.y = jumpForce;
    }

    void FixedUpdate() {
        dir.z = speed;
        dir.y += gravity * Time.fixedDeltaTime;
        controller.Move(dir * Time.fixedDeltaTime);
    }
}