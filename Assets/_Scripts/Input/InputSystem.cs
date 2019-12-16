using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Entities;

namespace FireFighter.Input
{
    [AlwaysUpdateSystem]
    public class InputSystem : ComponentSystem, InputActions.IGamePlayActions
    {
        private InputActions inputActions;

        protected override void OnCreate()
        {
            inputActions = new InputActions();
            inputActions.GamePlay.SetCallbacks(this);
        }

        protected override void OnStartRunning()
        {
            inputActions.Enable();
            inputActions.GamePlay.Enable();
        }

        protected override void OnUpdate()
        {
            //Debug.Log(inputActions.GamePlay.Move.ReadValue<Vector2>());
        }

        protected override void OnStopRunning()
        {
            inputActions.Disable();
        }

        protected override void OnDestroy()
        {
            inputActions.Dispose();
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            Debug.Log(context.ReadValue<Vector2>());
        }

        public void OnAim(InputAction.CallbackContext context)
        {
            throw new System.NotImplementedException();
        }

        public void OnShoot(InputAction.CallbackContext context)
        {
            throw new System.NotImplementedException();
        }
    }
}
