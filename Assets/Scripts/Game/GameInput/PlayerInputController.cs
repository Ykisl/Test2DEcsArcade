using Game.GameBase;
using Game.GamePlayer;
using Leopotam.Ecs;
using UnityEngine;
using Zenject;

namespace Game.GameInput
{
    public class PlayerInputController : MonoBehaviour
    {
        [SerializeField] private const KeyCode moveLeftKeyCode = KeyCode.LeftArrow;
        [SerializeField] private const KeyCode moveRightKeyCode = KeyCode.RightArrow;
        [SerializeField] private const KeyCode fireKeyCode = KeyCode.Space;

        private GameController _gameController;
        private EcsWorld _ecsWorld;

        [Inject]
        private void Construct(GameController gameController)
        {
            _gameController = gameController;
            _gameController.OnGameWorldInitialized += GameWorldInitialized; 
        }

        private void OnDestroy()
        {
            _gameController.OnGameWorldInitialized -= GameWorldInitialized;
        }

        private void GameWorldInitialized(EcsWorld ecsWorld)
        {
            _ecsWorld = ecsWorld;
        }

        private void Update()
        {
            if(_ecsWorld == null)
            {
                return;
            }

            var isMoveLeftKey = Input.GetKey(moveLeftKeyCode);
            var isMoveRightKey = Input.GetKey(moveRightKeyCode);
            var isFireKey = Input.GetKeyDown(fireKeyCode);

            var playerFilter = _ecsWorld.GetFilter(typeof(EcsFilter<PlayerTag>));
            foreach(var player in playerFilter)
            {
                var playerEntity = playerFilter.GetEntity(player);

                ref var playerInput = ref playerEntity.Get<PlayerInputComponent>();
                playerInput.isMoveLeft = isMoveLeftKey;
                playerInput.isMoveRight = isMoveRightKey;
                playerInput.isFire = isFireKey;
            }
        }
    }
}
