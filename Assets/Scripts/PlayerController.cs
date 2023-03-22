using UnityEngine;
using System;

public class PlayerController : MonoBehaviour {
    private CharacterController controller;
    private Vector3 dir;
    [SerializeField] private int speed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float gravity;
    [SerializeField] private GameObject losePanel;

    private int lineToMove = 1;
    public float lineDistance = 6.5f; 

    void Start() {
        controller = GetComponent<CharacterController>();
    }

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
        
        if (transform.position == targetPosition) return;

        Vector3 diff = targetPosition - transform.position;
        Vector3 moveDir = diff.normalized * 25 * Time.deltaTime;

        if (moveDir.sqrMagnitude < diff.sqrMagnitude) {
            controller.Move(moveDir);
        }
        else {
            controller.Move(diff);
        }
    }

    private void Jump() {
        dir.y = jumpForce;
    }

    void FixedUpdate() {
        dir.z = speed;
        dir.y += gravity * Time.fixedDeltaTime;
        controller.Move(dir * Time.fixedDeltaTime);
    }

    private void OnControllerColliderHit(ControllerColliderHit hit) {
        if (hit.gameObject.tag == "obstacle") {
            losePanel.SetActive(true);
            Time.timeScale = 0;
        }
    }
}