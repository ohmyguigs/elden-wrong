using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OMG
{
    public class PlayerCamera : MonoBehaviour
    {
        public static PlayerCamera instance;
        public PlayerManager player;
        public Camera cameraObject;

        [Header("Camera Settings")]
        private Vector3 cameraVelocity;
        private float cameraSmoothSpeed = 1; // LAG DA CAMERA, QUANTO MAIOR DEMORA MAIS

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            DontDestroyOnLoad(gameObject);
        }

        public void HandleAllCameraActions()
        {
            if (player != null)
            {
                // FOLLOW THE PLAYER
                FollowTarget();
                // ROTATE AROUND THE PLAYER
                // COLIDE WITH OBJECTS
            }
        }

        private void FollowTarget()
        {
            Vector3 targetCameraPosition = Vector3.SmoothDamp(transform.position, player.transform.position, ref cameraVelocity, cameraSmoothSpeed);
            transform.position = targetCameraPosition;
        }
    }
}
