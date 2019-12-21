using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Transforms;

namespace FireFighter
{
    public class CopyEntityTranslation : MonoBehaviour, IEntityReceiver
    {
        private Entity entityToTrack = Entity.Null;

        #region Unity Life Cycle
        private void Update()
        {
            if(entityToTrack == Entity.Null)
            {
                return;
            }

            EntityManager entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;

            transform.position = entityManager.GetComponentData<Translation>(entityToTrack).Value;
        }
        #endregion

        public void SetReceivedEntity(Entity entity)
        {
            entityToTrack = entity;
        }
    }
}
