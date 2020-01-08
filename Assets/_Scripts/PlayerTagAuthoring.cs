using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

namespace FireFighter
{
    public class PlayerTagAuthoring : MonoBehaviour, IConvertGameObjectToEntity
    {
        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
        {
            if(enabled)
            {
                dstManager.AddComponentData(entity, new PlayerTag());
            }
        }
    }
}
