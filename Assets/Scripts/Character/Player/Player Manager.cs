using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OMG
{
    public class PlayerManager : CharacterManager
    {
        PlayerLocomotionManager playerLocomotionManager;

        protected override void Awake()
        {
            base.Awake();

            playerLocomotionManager = GetComponent<PlayerLocomotionManager>();
        }

        protected override void Update()
        {
            base.Update();

            playerLocomotionManager.HandleAllMovement();
        }
    }
}
