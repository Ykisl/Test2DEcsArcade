using UnityEngine;
using Leopotam.Ecs;
using Game.Components.Events;
using Game.UnityComponents;
using Game.Weapon;

namespace Game.GamePlayer
{
    public class PlayerSpawnSystem : IEcsRunSystem
    {
        private readonly EcsWorld _world = null;
        private readonly EcsFilter<PlayerSpawnEvent> _spawnEvents = null;

        private GameObject _playerPrefab;
        private Transform _gameWorldRoot;

        public PlayerSpawnSystem(GameObject playerPrefab, Transform gameWorldRoot)
        {
            _playerPrefab = playerPrefab;
            _gameWorldRoot = gameWorldRoot;
        }

        public void Run()
        {
            foreach (var i in _spawnEvents)
            {
                var playerSpawnEvent = _spawnEvents.Get1(i);
                CreatePlayer(playerSpawnEvent.spawnPosition);

                _spawnEvents.GetEntity(i).Destroy();
            }
        }

        private void CreatePlayer(Vector2 playerPosition)
        {
            var playerGameObject = GameObject.Instantiate(_playerPrefab, playerPosition, Quaternion.identity, _gameWorldRoot);
            var playerEntityController = playerGameObject.GetComponent<PlayerEntityController>();
            var playerEntity = _world.NewEntity();

            var playerWeaponEntity = _world.NewEntity();
            ref var weaponComponent = ref playerWeaponEntity.Get<WeaponComponent>();
            weaponComponent.owner = playerEntity;
            weaponComponent.projectileEmmiter = playerEntityController.weaponProjectileEmiter;
            weaponComponent.projectilePrefab = playerEntityController.weaponProjectilePrefab;
            weaponComponent.projectileSpeed = 10f;

            playerEntity.Get<TransformComponent>().transform = playerEntityController.transform;
            playerEntity.Get<WeaponEntityReferecneComponent>().weponEntity = playerWeaponEntity;
            playerEntity.Get<PlayerTag>();

            playerEntityController.Init(playerEntity);
        }
    }
}