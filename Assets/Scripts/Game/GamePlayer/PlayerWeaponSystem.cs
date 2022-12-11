using UnityEngine;
using Leopotam.Ecs;
using Game.GameInput;
using Game.Weapon;

namespace Game.GamePlayer 
{
    public class PlayerWeaponSystem : IEcsRunSystem
    {
        private readonly EcsFilter<PlayerInputComponent, WeaponEntityReferecneComponent, PlayerTag> _playerFilter = null;

        public void Run()
        {
            foreach (var i in _playerFilter)
            {
                var playerInputComponent = _playerFilter.Get1(i);

                if (playerInputComponent.isFire)
                {
                    var weaponReferecneComponent = _playerFilter.Get2(i);
                    var wepaonEntity = weaponReferecneComponent.weponEntity;

                    wepaonEntity.Get<WeaponShootEvent>();
                }
            }
        }
    }
}
