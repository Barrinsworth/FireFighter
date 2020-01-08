using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;

namespace FireFighter.Water
{
    public class WaterSpawnerAuthoring : MonoBehaviour, IConvertGameObjectToEntity, IDeclareReferencedPrefabs
    {
        [SerializeField] private bool autoSpawn = false;
        [SerializeField] private GameObject waterPrefab;
        [SerializeField] private float waterSpawnRate = 0.0f;
        [SerializeField] private float waterPressure = 0.0f;

        public void DeclareReferencedPrefabs(List<GameObject> gameObjects)
        {
            gameObjects.Add(waterPrefab);
        }

        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
        {
            if(enabled)
            {
                if(autoSpawn)
                {
                    dstManager.AddComponentData(entity, new SpawnningTag());
                }

                WaterSpawner waterSpawnner = new WaterSpawner
                {
                    WaterEntity = conversionSystem.GetPrimaryEntity(waterPrefab),
                    WaterPressure = waterPressure,
                    WaterSpawnRate = waterSpawnRate,
                    TimeBetweenSpawns = 0.0f
                };

                dstManager.AddComponentData(entity, waterSpawnner);
            }
        }
    }
}
