using Client.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Client.Systems
{
    public class PlayerInputSystem : IEcsRunSystem
    {
        private EcsFilter<PlayerInputData> inputFilter;
        
        public void Run()
        {
            foreach (var i in inputFilter)
            {
                ref var input = ref inputFilter.Get1(i);
                
                input.Movement = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
            }
        }
    }
}