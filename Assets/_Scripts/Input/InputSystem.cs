using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Entities;

namespace FireFighter.Input
{
    [AlwaysUpdateSystem]
    [UpdateInGroup(typeof(SimulationSystemGroup))]
    public class InputSystem : ComponentSystem, InputActions.ICharacterControllerActions, InputActions.ICharacterHoseActions
    {
        private InputActions inputActions;

        private byte jumpInput = 0;
        private Vector2 moveInput = Vector2.zero;
        private Vector2 aimInput = Vector2.zero;

        private byte shootInput = 0;

        private EntityQuery characterControllerInputEntityQuery;
        private EntityQuery characterHoseInputEntityQuery;

        #region System Life Cycle
        protected override void OnCreate()
        {
            inputActions = new InputActions();
            inputActions.CharacterController.SetCallbacks(this);
            inputActions.CharacterHose.SetCallbacks(this);

            characterControllerInputEntityQuery = GetEntityQuery(typeof(CharacterControllerInputComponentData));
            characterHoseInputEntityQuery = GetEntityQuery(typeof(CharacterHoseInputComponentData));
        }

        protected override void OnStartRunning()
        {
            inputActions.Enable();
            inputActions.CharacterController.Enable();
            inputActions.CharacterHose.Enable();
        }

        protected override void OnUpdate()
        {
            if (characterControllerInputEntityQuery.CalculateEntityCount() == 0)
            {
                EntityManager.CreateEntity(typeof(CharacterControllerInputComponentData));
            }

            characterControllerInputEntityQuery.SetSingleton(new CharacterControllerInputComponentData
            {
                Move = moveInput,
                Aim = aimInput,
                Jump = jumpInput
            });

            jumpInput = 0;

            if(characterHoseInputEntityQuery.CalculateEntityCount() == 0)
            {
                EntityManager.CreateEntity(typeof(CharacterHoseInputComponentData));
            }

            characterHoseInputEntityQuery.SetSingleton(new CharacterHoseInputComponentData
            {
                Shooting = shootInput
            });
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
            aimInput = context.ReadValue<Vector2>();
        }

        public void OnJump(InputAction.CallbackContext context)
        {
            if(context.started)
            {
                jumpInput = 1;
            }
        }

        public void OnShoot(InputAction.CallbackContext context)
        {
            if(context.started || context.performed)
            {
                shootInput = 1;
            }
            else
            {
                shootInput = 0;
            }
        }
    }
}
