using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;

namespace FireFighter.CharacterController
{
    public struct CharacterControllerInternalComponentData : IComponentData
    {
        public float CurrentRotationAngle;
        public CharacterSupportStateEnum SupportedState;
        public float3 UnsupportedVelocity;
        public float3 LinearVelocity;
        public Entity Entity;
        public bool IsJumping;
        public CharacterControllerInputComponentData Input;
    }
}
