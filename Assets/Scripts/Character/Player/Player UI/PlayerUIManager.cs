using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

namespace OMG
{
    public class PlayerUIManager : MonoBehaviour
    {
        public static PlayerUIManager instance;
        [Header("NETWORK JOIN")]
        [SerializeField] bool startGameAsClient;
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
        private void Update()
        {
            if (startGameAsClient)
            {
                startGameAsClient = false;
                // DESLIGA O HOST, PRA LOGAR COMO CLIENT
                NetworkManager.Singleton.Shutdown();
                // RESTART COMO CLIENT
                NetworkManager.Singleton.StartClient();
            }
        }
    }
}

