using UnityEngine;

namespace Client.UnityComponents
{
    [CreateAssetMenu(fileName = "Configuration")]
    public class Configuration : ScriptableObject
    {
        public CharacterView characterView;
        public float Speed;
        public float DashAbilityDistance;
    }
}