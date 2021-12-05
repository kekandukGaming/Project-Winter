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
            playerEntity.Get<Movable>();
;            playerEntity.Get<Character>();
            playerEntity.Get<Caster>().Abilities = configuration.AbilitiesData;

            var spawnRequestEntity = ecsWorld.NewEntity();
            ref var spawnRequest = ref spawnRequestEntity.Get<SpawnRequest>();

            spawnRequest.Prefab = configuration.characterView.gameObject;
            spawnRequest.Position = sceneData.SpawnPoint.position;
        }
    }
}