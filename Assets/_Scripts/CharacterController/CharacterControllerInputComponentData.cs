using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;

namespace FireFighter.CharacterController
{
    public struct CharacterControllerInputComponentData : IComponentData
    {
        public float2 Move;
        public float2 Aim;
        public byte Jump;
    }
}
