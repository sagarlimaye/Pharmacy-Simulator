using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float walkSpeed;
    public float gravity;
    public const int DEFAULT_WALK_SPEED = 5;
	CharacterController controller;
	Vector3 inputDirection = Vector3.zero;
	Vector3 horizontal = Vector3.zero;
	Vector3 vertical = Vector3.zero;
	Vector3 moveDirection = Vector3.zero;
	float verticalVelocity;
	float verticalVelocityMax = 50.0f;
	float verticalVelocityMin = -50.0f;

    public float WalkSpeed
    {
        get
        {
            return walkSpeed;
        }

        set
        {
            walkSpeed = value;
        }
    }

    void Awake() {
		controller = GetComponent<CharacterController>();
        walkSpeed = DEFAULT_WALK_SPEED;
	}

	void Update() {
		// Update is for non-physics-related code

		// Get player inputs for x and z axis
		inputDirection = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
	}

	void FixedUpdate() {
		// Update is for physics-related code
        if(!MonitorInteraction.MainTerminalPanel.activeInHierarchy)
        {
		    MovePlayer();
		    ApplyGravity();
        }
	}

	void MovePlayer() {
		// Split horizontal (x, z) and vertical (y) values to normalize diagonal movement without changing aerial physics
		Vector3 transformDirection = transform.TransformDirection(inputDirection);
		horizontal = new Vector3(transformDirection.x, 0.0f, transformDirection.z);
		horizontal = Vector3.ClampMagnitude(horizontal, 1);
		vertical = new Vector3(0.0f, verticalVelocity, 0.0f);

		// Apply walk speed to normalized horizontal axis
		horizontal.x *= WalkSpeed;
		horizontal.z *= WalkSpeed;

		// Combine normalized horizontal movement with vertical movement before moving
		moveDirection = horizontal + vertical;
		controller.Move(moveDirection * Time.deltaTime);
	}

	void ApplyGravity() {
		if (controller.isGrounded == true) {
			verticalVelocity = -1;
		} else {
			verticalVelocity -= gravity * Time.deltaTime;
			verticalVelocity = Mathf.Clamp(verticalVelocity, verticalVelocityMin, verticalVelocityMax);
		}
	}
}
