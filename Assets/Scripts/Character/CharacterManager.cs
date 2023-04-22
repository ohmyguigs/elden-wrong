using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OMG
{
    public class CharacterManager : MonoBehaviour
    {
        protected virtual void Awake()
        {
            DontDestroyOnLoad(this);
        }

        protected virtual void Update()
        {
            // shared character updates
        }
    }
}
