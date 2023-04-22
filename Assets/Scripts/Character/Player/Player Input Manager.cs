using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace OMG
{
    public class PlayerInputManager : MonoBehaviour
    {
        public static PlayerInputManager instance;
        PlayerControls controls;
        [SerializeField] Vector2 movementInput;
        public float verticalInput;
        public float horizontalInput;
        public float moveAmount;

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

            // LISTEN TO SCENE CHANGES
            SceneManager.activeSceneChanged += OnSceneChanged;

            instance.enabled = false;
        }

        private void OnSceneChanged(Scene oldScene, Scene newScene)
        {
            // only enable controls if loads in world scene
            if (newScene.buildIndex == WorldSaveManager.instance.GetWorldSceneIndex())
            {
                instance.enabled = true;
            }
            else
            {
                instance.enabled = false;
            }
        }

        private void OnEnable()
        {
            if (controls == null)
            {
                controls = new PlayerControls();
                controls.PlayerMovement.Movement.performed += ctx => movementInput = ctx.ReadValue<Vector2>();
            }
            controls.Enable();
        }

        private void OnDestroy()
        {
            // UNLISTEN TO SCENE CHANGES
            SceneManager.activeSceneChanged -= OnSceneChanged;
            controls.Disable();
        }

        private void OnApplicationFocus(bool focus)
        {
            // PRA EVITAR MOVIMENTAR NOS 2 JOGOS ABERTOS
            // FACILITANDO TESTAR COM 2 EDITORES ABERTOS
            if (enabled)
            {
                if (!focus)
                {
                    controls.Enable();
                }
                else
                {
                    controls.Disable();
                }
            }
            
        }
        private void Update()
        {
            HandleMovementInput();
        }
        private void HandleMovementInput()
        {
            verticalInput = movementInput.y;
            horizontalInput = movementInput.x;

            // IGNORA O SINAL DO INPUT
            moveAmount = Mathf.Clamp01(Mathf.Abs(horizontalInput) + Mathf.Abs(verticalInput));

            // CLAMP (TO GIVE THAT DARK SOULS FEEL)
            if (moveAmount <= 0.5 && moveAmount > 0)
            {
                // walking
                moveAmount = 0.5f;
            }
            else if (moveAmount > 0.5 && moveAmount <= 1)
            {
                // running
                moveAmount = 1;
            }
        }
    }
}
