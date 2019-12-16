using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;

namespace FireFighter.CharacterController
{
    public struct CharacterControllerComponentData : IComponentData
    {
        public float3 Gravity;
        public float MovementSpeed;
        public float MaxMovementSpeed;
        public float RotationSpeed;
        public float JumpUpwardsSpeed;
        public float MaxSlope; // radians
        public int MaxIterations;
        public float CharacterMass;
        public float SkinWidth;
        public float ContactTolerance;
        public int AffectsPhysicsBodies;
    }
}
