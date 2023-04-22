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

    }
}
