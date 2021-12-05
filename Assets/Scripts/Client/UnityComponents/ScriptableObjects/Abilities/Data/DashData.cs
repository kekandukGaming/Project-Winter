using UnityEngine;

namespace Client.UnityComponents.Abilities
{
    [CreateAssetMenu(menuName = "Abilities/Dash", order = 0)]
    public class DashData : AbilityData
    {
        public float Distance;
    }
}