using UnityEngine;
using Leopotam.Ecs;

namespace Game.LifeTime 
{
    public class MaxLifeTimeSystem : IEcsRunSystem
    {
        private readonly EcsFilter<MaxLifeTimeComponent> _maxLifeTimeFilter = null;

        public void Run()
        {
            foreach (var i in _maxLifeTimeFilter)
            {
                var maxlifeTimeComponent = _maxLifeTimeFilter.Get1(i);
                var entity = _maxLifeTimeFilter.GetEntity(i);
                var lifeTimeComponent = entity.Get<LifeTimeComponent>();

                if(lifeTimeComponent.lifeTime >= maxlifeTimeComponent.maxLifeTime)
                {
                    entity.Get<LifeTimeIsOverTag>();
                }
            }
        }
    }
}