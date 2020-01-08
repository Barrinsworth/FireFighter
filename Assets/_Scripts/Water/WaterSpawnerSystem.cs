using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Physics.Systems;
using Unity.Transforms;

namespace FireFighter.Water
{
    [UpdateInGroup(typeof(SimulationSystemGroup))]
    [UpdateAfter(typeof(ExportPhysicsWorld)), UpdateAfter(typeof(CharacterHoseInputSystem)), UpdateBefore(typeof(EndFramePhysicsSystem))]
    public class WaterSpawnerSystem : ComponentSystem
    {
        private const int WATER_PARTICLE_DELTA = 4;

        protected override void OnUpdate()
        {
            Entities.WithAll(ComponentType.ReadOnly<SpawnningTag>(), ComponentType.ReadOnly<LocalToWorld>(),
                ComponentType.ReadWrite<WaterSpawner>()).ForEach((ref WaterSpawner waterSpawnner, ref LocalToWorld localToWorld) =>
                {
                    waterSpawnner.TimeBetweenSpawns += Time.DeltaTime;

                    if(waterSpawnner.TimeBetweenSpawns >= waterSpawnner.WaterSpawnRate)
                    {
                        Entity water = PostUpdateCommands.Instantiate(waterSpawnner.WaterEntity);
                        Translation translation = new Translation { Value = localToWorld.Position };

                        PhysicsVelocity velocity = new PhysicsVelocity
                        {
                            Linear = localToWorld.Forward * waterSpawnner.WaterPressure
                        };

                        PostUpdateCommands.SetComponent(water, translation);
                        PostUpdateCommands.SetComponent(water, velocity);
                    }
                }
            );
        }
    }
}
