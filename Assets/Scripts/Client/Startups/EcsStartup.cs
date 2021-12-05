using System;
using Client.Components;
using Client.Systems;
using Client.UnityComponents;
using Leopotam.Ecs;
using UnityEngine;

namespace Client {
    internal sealed class EcsStartup : MonoBehaviour
    {
        [SerializeField] private Configuration configuration;
        [SerializeField] private SceneData sceneData;

        private EcsWorld _world;
        private EcsSystems _systems;
        private EcsSystems _fixedSystems;

        private void Start () {
            _world = new EcsWorld ();
            _systems = new EcsSystems (_world);
            _fixedSystems = new EcsSystems(_world);
#if UNITY_EDITOR
            Leopotam.Ecs.UnityIntegration.EcsWorldObserver.Create (_world);
            Leopotam.Ecs.UnityIntegration.EcsSystemsObserver.Create (_systems);
#endif
            _systems
                .Add(new SpawnSystem())
                .Add(new PlayerSpawnSystem())
                .Add(new PlayerInputSystem())
                .Add(new PlayerInitSystem())
                .Add(new GravitySystem())
                .Add(new PlayerMovementSystem())
                .Add(new AbilityCastSystem())
                .Add(new PlayerCastSystem())
                .Add(new DashAbilitySystem())
                .Add(new CharacterUpdatePositionSystem())
                .Add(new AbilitiesSystem())
                .Add(new PlayerRotationSystem())
                
                .OneFrame<CastRequest>()
                
                .Inject(sceneData)
                .Inject(configuration);

            _systems
                .Init();
            _fixedSystems
                .Init();
        }

        private void Update () {
            _systems?.Run ();
        }

        private void FixedUpdate()
        {
            _fixedSystems?.Run();
        }

        private void OnDestroy () {
            if (_systems != null) {
                _systems.Destroy ();
                _systems = null;
                _world.Destroy ();
                _world = null;
            }
        }
    }
}