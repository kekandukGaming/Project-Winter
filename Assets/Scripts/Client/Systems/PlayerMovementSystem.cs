using Client.Components;
using Client.UnityComponents;
using Leopotam.Ecs;
using UnityEngine;

namespace Client.Systems
{
    public class PlayerMovementSystem : IEcsRunSystem
    {
        private SceneData sceneData;
        
        private readonly EcsFilter<PlayerInputData, Movable, Character> _filter;

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var input = ref _filter.Get1(i);
                ref var movement = ref _filter.Get2(i);
                ref var character = ref _filter.Get3(i);

                if (input.Movement.magnitude < 0.1f)
                {
                    return;
                }
                
                var direction = input.Movement.normalized;

                var targetAngle = Mathf.Atan2(direction.x,  direction.z) * Mathf.Rad2Deg + sceneData.CameraTransform.eulerAngles.y;;
                var movementDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

                movement.Position += movementDirection.normalized * character.MaxSpeed;
            }
        }
    }

    public class PlayerRotationSystem : IEcsRunSystem
    {
        private readonly EcsFilter<PlayerInputData, Movable> _filter;

        private readonly SceneData sceneData;
        private readonly Configuration configuration;

        private float turnSmoothVelocity;
        
        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var input = ref _filter.Get1(i);
                ref var movable = ref _filter.Get2(i);

                var direction = input.Movement.normalized;
                
                if (direction.magnitude < 0.1f)
                {
                    return;
                }

                var targetAngle = Mathf.Atan2(direction.x,  direction.z) * Mathf.Rad2Deg + sceneData.CameraTransform.eulerAngles.y;
                var angle = Mathf.SmoothDampAngle(movable.Transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity,
                    configuration.TurnSmooth);
                
                movable.Transform.rotation = Quaternion.Euler(0f, angle, 0f);

            }
        }
    }
}