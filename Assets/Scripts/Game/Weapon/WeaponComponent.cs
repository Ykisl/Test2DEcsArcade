using Leopotam.Ecs;
using UnityEngine;

namespace Game.Weapon 
{
    public struct WeaponComponent
    {
        public EcsEntity owner;
        public GameObject projectilePrefab;
        public Transform projectileEmmiter;
        public float projectileSpeed;
    }
}
