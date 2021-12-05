using System;
using Client.Components;
using Client.UnityComponents;
using Client.UnityComponents.Abilities;
using Leopotam.Ecs;

namespace Client.Systems
{
    public class PlayerInitSystem : IEcsRunSystem
    {
        private readonly EcsWorld _world;
        
        private readonly EcsFilter<SpawnResponse> _filter;

        private readonly EcsFilter<Player, Character> _playerFilter;

        private readonly Configuration configuration;
        private readonly SceneData sceneData;

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var response = ref _filter.Get1(i);
                ref var entity = ref _filter.GetEntity(i);

                if (response.Object.TryGetComponent(out CharacterView view))
                {
                    foreach (var pi in _playerFilter)
                    {
                        ref var character = ref _playerFilter.Get2(pi);
                        character.CharacterController = view.GetCharacterController();
                        character.Acceleration = configuration.Acceleration;
                        character.MaxSpeed = configuration.MaxSpeed;
                        var playerEntity = _playerFilter.GetEntity(i);
                        ref var movable = ref playerEntity.Get<Movable>();
                        movable.Transform = character.CharacterController.transform;

                        sceneData.CinemachineCamera.Follow = view.transform;
                        sceneData.CinemachineCamera.LookAt = view.transform;

                        entity.Del<SpawnResponse>();
                    }
                }
            }
        }
        
    }

    public class AbilitiesSystem : IEcsRunSystem
    {
        private readonly EcsWorld _world;
        
        private readonly EcsFilter<CastRequest> _filter;
        private readonly EcsFilter<Caster> _castersFilter;

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var request = ref _filter.Get1(i);

                foreach (var ic in _castersFilter)
                {
                    ref var caster = ref _castersFilter.Get1(ic);
                    ref var casterEntity = ref _castersFilter.GetEntity(ic);

                    if (caster.Equals(request.Caster))
                    {
                        CastAbility(casterEntity, request);
                    }
                }
            }
        }

        private void CastAbility(EcsEntity casterEntity, CastRequest request)
        {
            switch (request.Data)
            {
                case DashData dashData:
                    
                    ref var dash = ref casterEntity.Get<DashAbility>();
                    dash.Distance = dashData.Distance;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(request.Data));
            }
        }
    }
}