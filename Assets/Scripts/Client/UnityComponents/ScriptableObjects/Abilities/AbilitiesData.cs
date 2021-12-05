using System.Collections.Generic;
using UnityEngine;

namespace Client.UnityComponents.Abilities
{
    [CreateAssetMenu(menuName = "Abilities", order = 0)]
    public class AbilitiesData : ScriptableObject
    {
        public List<AbilityData> Abilities = new List<AbilityData>();
    }
}