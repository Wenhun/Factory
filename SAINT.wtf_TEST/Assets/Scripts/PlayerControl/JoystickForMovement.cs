using UnityEngine;

namespace Factory.PlayerController
{
    public class JoystickForMovement : JoystickHandler
    {
        [SerializeField] private PlayerMovement playerMovement;

        private void Update()
        {
            if (_inputVector.x != 0 || _inputVector.y != 0)
            {
                playerMovement.MovePlayer(new Vector3(_inputVector.x, 0, _inputVector.y));
            }
            else
            {
                playerMovement.MovePlayer(new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")));
            }
        }
    }
}