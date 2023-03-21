using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 dir;
    [SerializeField] private int speed;

    private void Start() {
        controller = GetComponent<CharacterController>();
    }

    private void FixedUpdate() {
        dir.z = speed;
        controller.Move(dir * Time.fixedDeltaTime);
    }
}