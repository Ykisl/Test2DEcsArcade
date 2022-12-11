using UnityEngine;
using Leopotam.Ecs;
using Game.UnityComponents;
using Game.Move;
using Game.LifeTime;

namespace Game.Weapon
{
    public class SpawnProjectileSystem : IEcsRunSystem
    {
        private readonly EcsWorld _world = null;
        private EcsFilter<SpawnProjectileEvent> _spawnEventFilter = null;

        private Transform _projectileRoot;

        public SpawnProjectileSystem(Transform projectileRoot)
        {
            _projectileRoot = projectileRoot;
        }

        public void Run()
        {
            foreach(var i in _spawnEventFilter)
            {
                var projectileSpawnEvent = _spawnEventFilter.Get1(i);

                var projectilePrefab = projectileSpawnEvent.projectilePrefab;
                var projectilePosition = projectileSpawnEvent.position;
                var projectileSpeed = projectileSpawnEvent.speed;
                CreateProjectile(projectilePrefab, projectilePosition, projectileSpeed, projectileSpawnEvent.lifeTime);

                _spawnEventFilter.GetEntity(i).Destroy();
            }
        }

        private void CreateProjectile(GameObject prefab, Vector3 position, Vector3 speed, float lifeTime)
        {
            var projectileGameObject = GameObject.Instantiate(prefab, position, Quaternion.identity, _projectileRoot);
            var playerEntity = _world.NewEntity();

            playerEntity.Get<TransformComponent>().transform = projectileGameObject.transform;
            playerEntity.Get<MaxLifeTimeComponent>().maxLifeTime = lifeTime;
            playerEntity.Get<LifeTimeDestroyableTag>();
            playerEntity.Get<MoveComponent>().movingSpeed = speed;
            playerEntity.Get<ProjectileTag>();
        }
    }
}
