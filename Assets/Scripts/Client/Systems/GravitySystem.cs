using Client.Components;
using Leopotam.Ecs;
using UnityEngine;
using Motion = Client.Components.Motion;

namespace Client.Systems
{
    public class GravitySystem : IEcsRunSystem
    {
        private readonly EcsFilter<Character> _filter;

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var character = ref _filter.Get1(i);
                character.CharacterController.Move(Physics.gravity * Time.deltaTime);
            }
        }
    }
}