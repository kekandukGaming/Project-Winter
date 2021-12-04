using Client.Components;
using Client.UnityComponents;
using Leopotam.Ecs;
using UnityEngine;

namespace Client.Systems
{
    public class PlayerSpawnSystem : IEcsInitSystem
    {
        private readonly EcsWorld ecsWorld;
        private readonly SceneData sceneData;
        private readonly Configuration configuration;
        
        public void Init()
        {
            var playerEntity = ecsWorld.NewEntity();
            
            playerEntity.Get<PlayerInputData>();
            playerEntity.Get<Player>();
            playerEntity.Get<Movement>();
            playerEntity.Get<Character>();

            var spawnRequestEntity = ecsWorld.NewEntity();
            ref var spawnRequest = ref spawnRequestEntity.Get<SpawnRequest>();

            spawnRequest.Prefab = configuration.characterView.gameObject;
            spawnRequest.Position = sceneData.SpawnPoint.position;
        }
    }

    public class PlayerInitSystem : IEcsRunSystem
    {
        private readonly EcsFilter<SpawnResponse> _filter;

        private readonly EcsFilter<Player, Character> _playerFilter;

        private readonly Configuration configuration;

        public void Run()
        {
            foreach (var i in _filter)
            {
                var response = _filter.Get1(i);
                var entity = _filter.GetEntity(i);

                if (response.Object.TryGetComponent(out CharacterView view))
                {
                    foreach (var pi in _playerFilter)
                    {
                        ref var character = ref _playerFilter.Get2(pi);
                        character.CharacterController = view.GetCharacterController();
                        character.Speed = configuration.Speed;   
                    
                        entity.Del<SpawnResponse>();
                    }
                }
            }
        }
    }
}