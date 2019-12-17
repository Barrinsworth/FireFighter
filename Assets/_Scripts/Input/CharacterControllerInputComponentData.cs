using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;

namespace FireFighter.Input
{
    public struct CharacterControllerInputComponentData : IComponentData
    {
        public float2 Move;
        public float2 Aim;
        public bool Jump;
    }
}
