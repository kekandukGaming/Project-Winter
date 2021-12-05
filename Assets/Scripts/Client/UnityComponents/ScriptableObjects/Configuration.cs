using Client.UnityComponents.Abilities;
using UnityEngine;

namespace Client.UnityComponents
{
    [CreateAssetMenu(fileName = "Configuration")]
    public class Configuration : ScriptableObject
    {
        public CharacterView characterView;
        public AbilitiesData AbilitiesData;
        public float Acceleration = 1f;
        public float MaxSpeed = 5f;
        public float TurnSmooth = 0.1f;
    }
}