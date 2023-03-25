using System.Collections.Generic;
using UnityEngine;

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
        if(_endPoint != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, _endPoint.position, _translateSpeed * Time.deltaTime);
            transform.localRotation = Quaternion.identity;
            if (Vector3.Distance(transform.position, _endPoint.position) <= 0.1f)
            {
                transform.position = _endPoint.position;
                _endPoint = null;
            }
        }
    }
}