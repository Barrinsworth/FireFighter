using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

namespace FireFighter.Input
{
    public struct CharacterHoseInputComponentData : IComponentData
    {
        public byte Shooting; 
    }
}
