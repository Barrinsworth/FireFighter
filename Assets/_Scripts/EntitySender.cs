using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

namespace FireFighter
{
    public struct SentEntity : IComponentData { }

    public class EntitySender : MonoBehaviour, IConvertGameObjectToEntity
    {
        [SerializeField] private GameObject[] EntityReceiverGameObjects = new GameObject[0];

        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
        {
            dstManager.AddComponentData(entity, new SentEntity() { });
            foreach (var EntityReceiver in EntityReceiverGameObjects)
            {
                var potentialReceivers = EntityReceiver.GetComponents<MonoBehaviour>();
                foreach (var potentialReceiver in potentialReceivers)
                {
                    if (potentialReceiver is IEntityReceiver receiver)
                    {
                        receiver.SetReceivedEntity(entity);
                    }
                }
            }
        }
    }
}
