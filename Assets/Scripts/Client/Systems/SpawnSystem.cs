using Client.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Client.Systems
{
    public class SpawnSystem : IEcsRunSystem
    {
        private EcsWorld _world;
        
        private readonly EcsFilter<SpawnRequest> _filter;

        public void Run()
        {
            foreach (var i in _filter)
            {
                var request = _filter.Get1(i);
                var requestEntity = _filter.GetEntity(i);

                var gameObject = Object.Instantiate(request.Prefab, request.Position, Quaternion.identity);

                var entity = _world.NewEntity();
                ref var spawnResponse = ref entity.Get<SpawnResponse>();

                spawnResponse.Object = gameObject;
                
                requestEntity.Del<SpawnRequest>();
            }
        }
    }
}