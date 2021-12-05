using Client.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Client.Systems
{
    public class GravitySystem : IEcsRunSystem
    {
        private readonly EcsFilter<Movable> _filter;

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var movable = ref _filter.Get1(i);
                movable.Position += Physics.gravity;
            }
        }
    }
}