using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cardgeon.Dungeon{
	public class PlayerMovementController : MonoBehaviour
	{
        [Header("Movement")]
        [SerializeField] private float walkSpeed = 6f;
        [SerializeField] private float gravity = -13f;
        [SerializeField] [Range(0.0f, 0.5f)] private float moveSmoothTime = .3f;
        [SerializeField] private float slopeForce;
        [SerializeField] private float slopeForceRayLength;

        [Header("References")]
        [SerializeField] private CharacterController controller;


        private float velocityY = 0f;

        private bool isJumping = false;

        Vector2 currentDirection;
        Vector2 currentDirectionVelocity;

        private void Start()
        {
            controller = GetComponent<CharacterController>();
        }

        private void Update()
        {
            UpdateMovement();
        }

        private bool OnSlope()
        {
            if (isJumping)
                return false;

            RaycastHit hit;

            if (Physics.Raycast(transform.position, Vector3.down, out hit, controller.height / 2 * slopeForceRayLength))
            {
                if (hit.normal != Vector3.up)
                {
                    return true;
                }
            }

            return false;
        }

        void UpdateMovement()
        {
            Vector2 targetDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            targetDir.Normalize();

            velocityY = 0f;

            currentDirection = Vector2.SmoothDamp(currentDirection, targetDir, ref currentDirectionVelocity, moveSmoothTime);

            Vector3 velocity = (transform.forward * currentDirection.y + transform.right * currentDirection.x) * walkSpeed + Vector3.up * velocityY;

            controller.Move(velocity * Time.deltaTime);

            if (currentDirection != Vector2.zero && OnSlope())
            {
                controller.Move(Vector3.down * controller.height / 2 * slopeForce * Time.deltaTime);
            }
        }
    }
}
