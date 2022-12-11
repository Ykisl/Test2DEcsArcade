using Leopotam.Ecs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.GamePlayer
{
    public class PlayerEntityController : MonoBehaviour, IEntityReference
    {
        public Transform weaponProjectileEmiter;
        public GameObject weaponProjectilePrefab;

        private EcsEntity _entity;

        public void Init(EcsEntity entity)
        {
            _entity = entity;
        }

        public EcsEntity GetEntity()
        {
            return _entity;
        }
    }
}

