using Game.GameInput;
using Game.Move;
using Leopotam.Ecs;
using UnityEngine;

namespace Game.GamePlayer
{
    public class PlayerMoveSystem : IEcsRunSystem
    {
        private readonly EcsFilter<PlayerInputComponent, PlayerTag> _playerFilter = null;

        private const float PLAYER_SPEED = 5f;

        public void Run()
        {
            foreach (var i in _playerFilter)
            {
                var playerEntity = _playerFilter.GetEntity(i);
                ref var moveComponent = ref playerEntity.Get<MoveComponent>();
                var playerInputComponent = _playerFilter.Get1(i);

                var playerHorizontalSpeed = 0f;
                if (playerInputComponent.isMoveLeft)
                {
                    playerHorizontalSpeed -= PLAYER_SPEED;
                }

                if (playerInputComponent.isMoveRight)
                {
                    playerHorizontalSpeed += PLAYER_SPEED;
                }

                moveComponent.movingSpeed.x = playerHorizontalSpeed;
            }
        }
    }
}
