using UnityEngine;
using Leopotam.Ecs;
using Game.UnityComponents;

namespace Game.LifeTime
{
    public class LifeTimeDestroySystem : IEcsRunSystem
    {
        private readonly EcsFilter<LifeTimeIsOverTag, LifeTimeDestroyableTag> _destroyableFilter = null;

        public void Run()
        {
            foreach (var i in _destroyableFilter)
            {
                var entity = _destroyableFilter.GetEntity(i);
                if (entity.Has<TransformComponent>())
                {
                     var transformComponent = entity.Get<TransformComponent>();
                    if(transformComponent.transform != null)
                    {
                        GameObject.Destroy(transformComponent.transform.gameObject); 
                    }
                }

                entity.Destroy();
            }
        }
    }
}