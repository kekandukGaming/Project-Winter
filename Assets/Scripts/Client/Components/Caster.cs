using System.Collections.Generic;
using Client.UnityComponents.Abilities;

namespace Client.Components
{
    public struct Caster
    {
        public AbilitiesData Abilities;
    }

    public struct Ability
    {
        public Caster Owner;
        public bool Casted;
    }
    
    public struct DashAbility
    {
        public float Distance;
    }

    public struct CastRequest
    {
        public Caster Caster;
        public AbilityData Data;
    }

    public enum AbilityOperation
    {
        Add,
        Remove
    }
}