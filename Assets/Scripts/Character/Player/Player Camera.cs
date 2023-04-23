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
        [SerializeField] Transform cameraPivotTransform;

        // Mudar esses para performance das cameras
        [Header("Camera Settings")]
        private float cameraSmoothSpeed = 0.3f; // LAG DA CAMERA, QUANTO MAIOR DEMORA MAIS
        [SerializeField] float upAndDownRotationSpeed = 220;
        [SerializeField] float leftAndRightRotationSpeed = 220;
        [SerializeField] float minimumPivot = -30; // angulo minimo ao olhar pra baixo
        [SerializeField] float maximumPivot = 60; // Angulo maximo ao olhar pra cima
        [SerializeField] float cameraCollisionRadiusOffset = 0.2f;
        [SerializeField] LayerMask cameraCollisionLayer;

        [Header("Camera Values")]
        private Vector3 cameraVelocity;
        private Vector3 cameraObjectPosition; // used for collision
        [SerializeField] float leftAndRightLookAngle;
        [SerializeField] float upAndDownLookAngle;
        private float cameraZPosition; // used for collision
        private float targetCameraZPosition; // also used for collision

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
            cameraZPosition = cameraObject.transform.localPosition.z;
        }

        public void HandleAllCameraActions()
        {
            if (player != null)
            {
                HandleFollowTarget();
                HandleRotations();
                HandleCollisions();
            }
        }

        private void HandleFollowTarget()
        {
            Vector3 targetCameraZPosition = Vector3.SmoothDamp(transform.position, player.transform.position, ref cameraVelocity, cameraSmoothSpeed);
            transform.position = targetCameraZPosition;
        }

        private void HandleRotations()
        {
            // rotate left and right based on horizontal movement on the right joystick / mouse delta x
            leftAndRightLookAngle += (PlayerInputManager.instance.cameraHorizontalInput * leftAndRightRotationSpeed) * Time.deltaTime;
            // rotate up and down based on vertical movement on the right joystick / mouse delta y
            upAndDownLookAngle -= (PlayerInputManager.instance.cameraVerticalInput * upAndDownRotationSpeed) * Time.deltaTime;
            // Clamp up and down rotation
            upAndDownLookAngle = Mathf.Clamp(upAndDownLookAngle, minimumPivot, maximumPivot);

            Vector3 cameraRotation = Vector3.zero;
            Quaternion targetRotation;
            // rotate this gameobject left and right
            cameraRotation.y = leftAndRightLookAngle;
            targetRotation = Quaternion.Euler(cameraRotation);
            transform.rotation = targetRotation;
            // rotate this gameobject up and down
            cameraRotation = Vector3.zero;
            cameraRotation.x = upAndDownLookAngle;
            targetRotation = Quaternion.Euler(cameraRotation);
            cameraPivotTransform.localRotation = targetRotation;
        }

        private void HandleCollisions()
        {
            targetCameraZPosition = cameraZPosition;
            RaycastHit hit;
            Vector3 direction = cameraObject.transform.position - cameraPivotTransform.position;
            direction.Normalize();

            if (Physics.SphereCast(cameraPivotTransform.position, cameraCollisionRadiusOffset, direction, out hit, Mathf.Abs(targetCameraZPosition), cameraCollisionLayer))
            {
                float distanceFromHitObject = Vector3.Distance(cameraPivotTransform.position, hit.point);
                targetCameraZPosition = -(distanceFromHitObject - cameraCollisionRadiusOffset);
            }

            if (Mathf.Abs(targetCameraZPosition) < cameraCollisionRadiusOffset)
            {
                targetCameraZPosition = -cameraCollisionRadiusOffset;
            }

            cameraObjectPosition.z = Mathf.Lerp(cameraObject.transform.localPosition.z, targetCameraZPosition, 0.2f);
            cameraObject.transform.localPosition = cameraObjectPosition;
        }
    }
}
