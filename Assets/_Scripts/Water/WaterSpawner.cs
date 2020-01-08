using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;

namespace FireFighter.Water
{
    public struct WaterSpawner : IComponentData
    {
        public Entity WaterEntity;
        public float WaterSpawnRate;
        public float WaterPressure;

        public float TimeBetweenSpawns;
    }
}
