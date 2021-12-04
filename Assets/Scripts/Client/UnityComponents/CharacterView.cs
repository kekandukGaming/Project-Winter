using UnityEngine;

namespace Client.UnityComponents
{
    public class CharacterView : MonoBehaviour
    {
        [SerializeField] private CharacterController characterController;

        public CharacterController GetCharacterController() => characterController;
    }
}