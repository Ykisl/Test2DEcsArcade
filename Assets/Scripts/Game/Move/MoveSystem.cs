using Game.UnityComponents;
using Leopotam.Ecs;
using UnityEngine;

namespace Game.Move 
{
    public class MoveSystem : IEcsRunSystem
    {
        private readonly EcsFilter<MoveComponent, TransformComponent> _movableFilter = null;

        public void Run()
        {
            foreach(var i in _movableFilter)
            {
                var moveComponent = _movableFilter.Get1(i);
                var transformComponent = _movableFilter.Get2(i);

                var transform = transformComponent.transform;
                var position = transform.position;
                position += moveComponent.movingSpeed * Time.deltaTime;
                transform.position = position;
            }
        }
    }
}
