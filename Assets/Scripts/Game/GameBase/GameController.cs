using Game.GamePlayer;
using Game.LifeTime;
using Game.Move;
using Game.Weapon;
using LeoEcsPhysics;
using Leopotam.Ecs;
using Leopotam.Ecs.UnityIntegration;
using System;
using UnityEngine;

namespace Game.GameBase
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private GameObject playerPrefab;
        [SerializeField] private Transform playerSpawnPosition;
        [SerializeField] private Transform gameWorldRoot;
        [Space]
        [SerializeField] private Transform projectileRoot;

        public event Action<EcsWorld> OnGameWorldInitialized;

        private EcsWorld _world;
        private EcsSystems _systems;

        private void Start()
        {
            InitGame();
        }

        private void InitGame()
        {
            _world = new EcsWorld();

#if UNITY_EDITOR
            EcsWorldObserver.Create(_world);
#endif

            _systems = new EcsSystems(_world);
            AddOneFrame(_systems);
            AddSystems(_systems);

#if UNITY_EDITOR
            EcsSystemsObserver.Create(_systems);
#endif

            _systems.Init();
            EcsPhysicsEvents.ecsWorld = _world;

            OnGameWorldInitialized?.Invoke(_world);
        }

        private void AddSystems(EcsSystems systems)
        {
            systems.Add(new LifeTimeSystem());
            systems.Add(new MaxLifeTimeSystem());
            systems.Add(new LifeTimeDestroySystem());
            systems.Add(new MoveSystem());
            systems.Add(new SpawnProjectileSystem(projectileRoot));
            systems.Add(new WeaponShootSystem());
            systems.Add(new PlayerMoveSystem());
            systems.Add(new PlayerWeaponSystem());
            systems.Add(new PlayerSpawnSystem(playerPrefab, gameWorldRoot));
            systems.Add(new GameSystem(playerSpawnPosition));
        }

        private void AddOneFrame(EcsSystems systems)
        {
            systems.OneFramePhysics();
        }

        private void Update()
        {
            _systems.Run();
        }

        private void OnDestroy()
        {
            EcsPhysicsEvents.ecsWorld = null;
            _world?.Destroy();
            _world = null;

            _systems?.Destroy();
            _systems = null;
        }
    }
}
