using UnityEngine;

namespace Factory.PlayerController
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed = 1;

        private CharacterController _characterController;

        private void Awake()
        {
            _characterController = GetComponent<CharacterController>();
        }

        public void MovePlayer(Vector3 direction)
        {
            Vector3 lookDirection = transform.forward;
            lookDirection.y = 0;

            lookDirection.Normalize();

            direction = Quaternion.LookRotation(lookDirection) * direction;

            _characterController.Move(direction * _moveSpeed * Time.deltaTime);
        }
    }
}