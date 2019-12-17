using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Physics.Systems;
using FireFighter.Input;

namespace FireFighter.CharacterController
{
    // This input system simply applies the same character input 
    // information to every character controller in the scene
    [UpdateAfter(typeof(ExportPhysicsWorld)), UpdateAfter(typeof(InputSystem)), UpdateBefore(typeof(CharacterControllerSystem))]
    public class CharacterControllerInputSystem : ComponentSystem
    {
        private EntityQuery characterControllerInputEntityQuery;

        #region System Life Cycle
        protected override void OnCreate()
        {
            characterControllerInputEntityQuery = GetEntityQuery(ComponentType.ReadOnly<CharacterControllerInputComponentData>());
        }

        protected override void OnUpdate()
        {
            // Read user input
            var input = characterControllerInputEntityQuery.GetSingleton<CharacterControllerInputComponentData>();
            Entities.ForEach((ref CharacterControllerInternalComponentData ccData) =>
                {
                    ccData.Input.Move = input.Move;
                    ccData.Input.Aim = input.Aim;
                    ccData.Input.Jump = input.Jump;
                }
            );
        }
        #endregion
    }
}
