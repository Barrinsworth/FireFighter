using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using FireFighter.Input;

namespace FireFighter.Water
{
    [UpdateInGroup(typeof(SimulationSystemGroup))]
    [UpdateAfter(typeof(InputSystem))]
    public class CharacterHoseInputSystem : ComponentSystem
    {
        private EntityQuery characterHoseInputEntityQuery;

        protected override void OnCreate()
        {
            characterHoseInputEntityQuery = GetEntityQuery(ComponentType.ReadOnly<CharacterHoseInputComponentData>());
        }

        protected override void OnUpdate()
        {
            CharacterHoseInputComponentData input = characterHoseInputEntityQuery.GetSingleton<CharacterHoseInputComponentData>();

            if(input.Shooting == 1)
            {
                Entities.WithAll<WaterSpawner, PlayerTag>().WithNone<SpawnningTag>().ForEach((Entity entity, ref WaterSpawner waterSpawnner) =>
                    {
                        waterSpawnner.TimeBetweenSpawns = 0.0f;

                        PostUpdateCommands.AddComponent<SpawnningTag>(entity);
                    }
                );
            }
            else
            {
                Entities.WithAll<WaterSpawner, SpawnningTag, PlayerTag>().ForEach((Entity entity) =>
                    {
                        PostUpdateCommands.RemoveComponent<SpawnningTag>(entity);
                    }
                );
            }
        }
    }
}
