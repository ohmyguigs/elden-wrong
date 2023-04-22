using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace OMG
{
    public class WorldSaveManager : MonoBehaviour
    {
       public static WorldSaveManager instance;
       [SerializeField] int worldSceneIndex = 1;

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

        public IEnumerator LoadNewGame()
        {
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(worldSceneIndex);

            yield return null;
        }
    }
}