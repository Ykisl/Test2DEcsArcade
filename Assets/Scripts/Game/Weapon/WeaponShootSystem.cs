using Leopotam.Ecs;
using UnityEngine;

namespace Game.Weapon
{
    public class WeaponShootSystem : IEcsRunSystem
    {
        private readonly EcsFilter<WeaponComponent, WeaponShootEvent> _weaponFilter = null;
        private EcsWorld _ecsWorld;

        public void Run()
        {
            foreach (var i in _weaponFilter)
            {
                var weaponEntity = _weaponFilter.GetEntity(i);
                var weaponComponent = _weaponFilter.Get1(i);

                var projectileSpawnEventEntity = _ecsWorld.NewEntity();
                ref var projectileSpawnEvent = ref projectileSpawnEventEntity.Get<SpawnProjectileEvent>();
                projectileSpawnEvent.position = weaponComponent.projectileEmmiter.position;
                projectileSpawnEvent.projectilePrefab = weaponComponent.projectilePrefab;
                projectileSpawnEvent.speed = weaponComponent.projectileEmmiter.up * weaponComponent.projectileSpeed;
                projectileSpawnEvent.lifeTime = 1;

                weaponEntity.Del<WeaponShootEvent>();
            }
        }
    }
}
