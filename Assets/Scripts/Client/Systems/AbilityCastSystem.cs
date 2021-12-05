using System;
using Client.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Client.Systems
{
    public class PlayerCastSystem : IEcsRunSystem
    {
        private readonly EcsFilter<Caster, PlayerInputData> _filter;

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var caster = ref _filter.Get1(i);
                ref var input = ref _filter.Get2(i);

                ref var entity = ref _filter.GetEntity(i);

                if (input.AbilityApplied)
                {
                    var ability = caster.Abilities.Abilities[input.AbilityIndex];

                    ref var castRequest = ref entity.Get<CastRequest>();
                    castRequest.Caster = caster;
                    castRequest.Data = ability;

                    input.AbilityApplied = false;
                    input.AbilityIndex = 0;
                }
            }
        }
    }
    
    public class AbilityCastSystem : IEcsRunSystem
    {
        private readonly EcsFilter<Caster, CastRequest> _filter;

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var castRequest = ref _filter.Get2(i);
            }
        }
    }

    public class DashAbilitySystem : IEcsRunSystem
    {
        private readonly EcsFilter<DashAbility> _filter;

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var entity = ref _filter.GetEntity(i);
                
                ref var movable = ref entity.Get<Movable>();
                ref var dash = ref _filter.Get1(i);

                var direction = movable.Transform.forward;

                movable.Position += direction * dash.Distance;
                
                entity.Del<DashAbility>();
            }
        }
    }

    public class CharacterUpdatePositionSystem : IEcsRunSystem
    {
        private readonly EcsFilter<Character, Movable> _filter;

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var character = ref _filter.Get1(i);
                ref var movable = ref _filter.Get2(i);

                Debug.Log("move to " + movable.Position * Time.deltaTime);
                
                character.CharacterController.Move(movable.Position * Time.deltaTime);

                movable.Position = Vector3.zero;
            }
        }
    }

}