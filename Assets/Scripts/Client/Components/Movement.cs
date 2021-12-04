using UnityEngine;

namespace Client.Components
{
    public struct Movement
    {
        public Vector3 Position;
        public float CurrentSpeed;
        public float MaxSpeed;
        public float Acceleration;
    }
}