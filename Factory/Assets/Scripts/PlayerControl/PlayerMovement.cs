using System;
using UnityEngine;

namespace Factory.PlayerController
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed = 1f;
        [SerializeField] private float _rotateSpeed = 1f;
        
        private float _currentAttractionPlayer = 0;
        [SerializeField] private float _gravityForce = 20;

        private CharacterController _characterController;

        private void Start()
        {
            _characterController = GetComponent<CharacterController>();
        }

        void Update()
        {
            GravityHandling();
        }

        private void GravityHandling()
        {
            if(!_characterController.isGrounded)
            {
                _currentAttractionPlayer -= _gravityForce * Time.deltaTime;
            }
            else
            {
                _currentAttractionPlayer = 0;
            }
            
        }

        public void MovePlayer(Vector3 moveDirection)
        {
            moveDirection = moveDirection * _moveSpeed;
            moveDirection.y = _currentAttractionPlayer;
            _characterController.Move(moveDirection * Time.deltaTime);
        }

        public void RotatePlayer(Vector3 moveDirection)
        {
            if(Vector3.Angle(transform.forward, moveDirection) > 0)
            {
                Vector3 newDirection = Vector3.RotateTowards(transform.forward, moveDirection, _rotateSpeed, 0);
                transform.rotation = Quaternion.LookRotation(newDirection);
            }
        }
    }
}