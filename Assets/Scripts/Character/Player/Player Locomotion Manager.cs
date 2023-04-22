using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OMG
{
    public class PlayerLocomotionManager : CharacterLocomotionManager
    {
        PlayerManager player;
        // Movements vem do PlayerInputManager
        public float verticalMovement;
        public float horizontalMovement;
        public float moveAmount;
        private Vector3 moveDirection;
        [SerializeField] float walkingSpeed = 2;
        [SerializeField] float runningSpeed = 5;

        protected override void Awake()
        {
            base.Awake();

            player = GetComponent<PlayerManager>();
        }

        public void HandleAllMovement()
        {
            HandleGroundedMovement();
            // AERIAL MOVEMENT
        }

        private void GetVerticalAndHorizontalInput()
        {
            verticalMovement = PlayerInputManager.instance.verticalInput;
            horizontalMovement = PlayerInputManager.instance.horizontalInput;
            moveAmount = PlayerInputManager.instance.moveAmount;
            // TODO: Clamp movement
        }

        private void HandleGroundedMovement()
        {
            GetVerticalAndHorizontalInput();
            // Move direction is based on camera perspective and inputs
            moveDirection = PlayerCamera.instance.transform.forward * verticalMovement;
            moveDirection += PlayerCamera.instance.transform.right * horizontalMovement;
            moveDirection.Normalize();
            moveDirection.y = 0;

            if (PlayerInputManager.instance.moveAmount > 0.5f)
            {
                // Move player at running speed
                player.characterController.Move(moveDirection * runningSpeed * Time.deltaTime);
            }
            else if (PlayerInputManager.instance.moveAmount <= 0.5f)
            {
                // Move player at walking speed
                player.characterController.Move(moveDirection * walkingSpeed * Time.deltaTime);
            }
        }
    }
}

