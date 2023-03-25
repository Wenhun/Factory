using System.Collections.Generic;
using UnityEngine;

public class ResourceTransitionManager : MonoBehaviour
{
    [SerializeField] private float _translateSpeed = 5f;

    Dictionary<Resource, Transform> _transitionResources = new Dictionary<Resource, Transform>();
    List<Resource> _resourceForClear = new List<Resource>();

    public void MoveResource(Resource resource, Vector3 start, Transform end)
    {
        if (_transitionResources.ContainsKey(resource))
        {
            _transitionResources[resource] = end;
        }
        else
        {
            _transitionResources.Add(resource, end);
        }
        resource.transform.position = start;
        resource.transform.SetParent(end);
    }

    void Update()
    {
        if (_transitionResources.Count != 0)
        {
            foreach(var resource in _transitionResources)
            {
                resource.Key.transform.position = Vector3.MoveTowards(resource.Key.transform.position, resource.Value.position, _translateSpeed * Time.deltaTime);
                if (Vector3.Distance(resource.Key.transform.position, resource.Value.position) <= 0.1f)
                {
                    resource.Key.transform.position = resource.Value.position;
                    _resourceForClear.Add(resource.Key);
                }
            }
            foreach(Resource resource in _resourceForClear)
            {
                _transitionResources.Remove(resource);
            }
            _resourceForClear.Clear();
        }
    }
}