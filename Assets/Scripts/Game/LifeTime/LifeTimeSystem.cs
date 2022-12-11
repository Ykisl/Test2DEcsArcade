using Leopotam.Ecs;
using UnityEngine;

namespace Game.LifeTime
{
    public class LifeTimeSystem : IEcsRunSystem
    {
        private readonly EcsFilter<LifeTimeComponent> _lifeTimeFilter = null;

        public void Run()
        {
            foreach (var i in _lifeTimeFilter)
            {
                var lifeTimeComponent = _lifeTimeFilter.Get1Ref(i);
                lifeTimeComponent.Unref().lifeTime += Time.deltaTime;
            }
        }
    }
}
