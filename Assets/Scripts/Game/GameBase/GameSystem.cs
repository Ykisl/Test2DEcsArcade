using Game.Components.Events;
using Leopotam.Ecs;
using UnityEngine;

namespace Game.GamePlayer
{
    public class GameSystem : IEcsInitSystem
    {
        private EcsWorld _world;
        private Transform _playerSpawnPosition;

        public GameSystem(Transform playerSpawnPosition)
        {
            _playerSpawnPosition = playerSpawnPosition;
        }

        public void Init()
        {
            ref var playerSpawnEvent = ref _world.NewEntity().Get<PlayerSpawnEvent>();
            playerSpawnEvent.spawnPosition = _playerSpawnPosition.position;
        }
    }
}
