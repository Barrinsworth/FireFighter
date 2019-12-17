using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Entities;

namespace FireFighter.Input
{
    [AlwaysUpdateSystem]
    public class InputSystem : ComponentSystem, InputActions.ICharacterControllerActions
    {
        private InputActions inputActions;
        private bool jumpInput = false;
        private Vector2 moveInput = Vector2.zero;
        private Vector2 aimInput = Vector2.zero;

        private EntityQuery characterControllerInputEntityQuery;

        #region System Life Cycle
        protected override void OnCreate()
        {
            inputActions = new InputActions();
            inputActions.CharacterController.SetCallbacks(this);

            characterControllerInputEntityQuery = GetEntityQuery(typeof(CharacterControllerInputComponentData));
        }

        protected override void OnStartRunning()
        {
            inputActions.Enable();
            inputActions.CharacterController.Enable();
        }

        protected override void OnUpdate()
        {
            if (characterControllerInputEntityQuery.CalculateEntityCount() == 0)
                EntityManager.CreateEntity(typeof(CharacterControllerInputComponentData));

            characterControllerInputEntityQuery.SetSingleton(new CharacterControllerInputComponentData
            {
                Move = moveInput,
                Aim = aimInput,
                Jump = jumpInput
            });

            jumpInput = false;
        }

        protected override void OnStopRunning()
        {
            inputActions.Disable();
        }

        protected override void OnDestroy()
        {
            inputActions.Dispose();
        }
        #endregion

        public void OnMove(InputAction.CallbackContext context)
        {
            moveInput = context.ReadValue<Vector2>();
        }

        public void OnAim(InputAction.CallbackContext context)
        {
            //aimInput = context.ReadValue<Vector2>();
        }

        public void OnJump(InputAction.CallbackContext context)
        {
            if(context.started)
            {
                jumpInput = true;
            }
        }
    }
}
