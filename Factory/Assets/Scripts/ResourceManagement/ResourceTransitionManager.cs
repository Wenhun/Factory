using UnityEngine;

namespace Factory.ResourceManagement
{
    public class ResourceTransitionManager : MonoBehaviour
    {
        [SerializeField] private float _translateSpeed = 5f;

        private Transform _endPoint;

        public void MoveResource(Vector3 start, Transform end)
        {
            transform.position = start;
            transform.SetParent(end);
            _endPoint = end;
        }

        void Update()
        {
            if (_endPoint != null)
            {
                transform.position = Vector3.MoveTowards(transform.position, _endPoint.position, _translateSpeed * Time.deltaTime);
                transform.localRotation = Quaternion.identity;

                if (Vector3.Distance(transform.position, _endPoint.position) <= 0.1f)
                    {
                        ResetPosition();
                    }
                }
        }
        
        private void ResetPosition()
        {
            transform.position = _endPoint.position;
            _endPoint = null;
        }
    }
}