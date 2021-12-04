using UnityEngine;

namespace Client.Components
{
    public struct SpawnRequest
    {
        public GameObject Prefab;
        public Vector3 Position;
    }

    public struct SpawnResponse
    {
        public GameObject Object;
    }
}