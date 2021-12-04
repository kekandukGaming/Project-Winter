using Client.Components;
using Client.UnityComponents;
using Leopotam.Ecs;
using UnityEngine;
using Motion = Client.Components.Motion;

namespace Client.Systems
{
    public class PlayerMovementSystem : IEcsRunSystem
    {
        private SceneData sceneData;
        
        private readonly EcsFilter<PlayerInputData, Movement, Character> _filter;

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var input = ref _filter.Get1(i);
                ref var movement = ref _filter.Get2(i);
                ref var character = ref _filter.Get3(i);

                var targetSpeed = movement.MaxSpeed * input.Movement.magnitude;
                movement.CurrentSpeed = Mathf.MoveTowards(movement.CurrentSpeed, targetSpeed, movement.Acceleration * Time.deltaTime);

                var cameraTransform = sceneData.Camera.transform;
                var forward = cameraTransform.forward;
                var right = cameraTransform.right;

                forward.y = 0f;
                right.y = 0f;
            
                forward.Normalize();
                right.Normalize();

                Vector3 movementDirection;

                if (targetSpeed != 0f)
                {
                    movementDirection = forward * input.Movement.y + right * input.Movement.x;
                }
                else
                {
                    movementDirection = input.Movement.normalized;
                }

                Debug.Log(movementDirection * character.Speed * Time.deltaTime);
                character.CharacterController.Move(movementDirection * character.Speed * Time.deltaTime);
            }
        }
    }
}