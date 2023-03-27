using UnityEngine;

namespace Factory.PlayerController
{
    public class FollowCamera : MonoBehaviour
    {
        [Header("Object for Following")]
        [SerializeField] private GameObject _player;

        [Header("Camera properties")]
        [SerializeField] private float _returnSpeed;
        [SerializeField] private float _height;
        [SerializeField] private float _rearDistance;

        private Vector3 _currentVector;

        bool _isCorrectConfigured = false;

        private void Start()
        {
            if (_player != null) _isCorrectConfigured = true;
            else
            {
                Debug.LogError("Object: " + nameof(_player) + " in " + nameof(FollowCamera) + "  Not Set!");
            }

            transform.position = new Vector3(_player.transform.position.x, _player.transform.position.y + _height, _player.transform.position.z - _rearDistance);
            transform.rotation = Quaternion.LookRotation(_player.transform.position - transform.position);
        }

        private void Update()
        {
            if (!_isCorrectConfigured) return;

            CameraMove();
        }

        private void CameraMove()
        {
            _currentVector = new Vector3(_player.transform.position.x, _player.transform.position.y + _height, _player.transform.position.z - _rearDistance);
            transform.position = Vector3.Lerp(transform.position, _currentVector, _returnSpeed * Time.deltaTime);
        }
    }
}
