using UnityEngine;

namespace Game.Weapon
{
    public struct SpawnProjectileEvent
    {
        public GameObject projectilePrefab;
        public Vector3 position;
        public Vector3 speed;
        public float lifeTime;
    }
}
