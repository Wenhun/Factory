using System.Collections.Generic;
using UnityEngine;

public class StackResources : MonoBehaviour, ISlotSetup, ISlotController
{
    private enum Capacity
    {
        four = 4,
        eight = 8,
        twelve = 12,
        sixteen = 16,
        twentyFour = 24
    }

    [SerializeField] private Capacity _capacity = Capacity.twelve;
    public int capacity {get => (int)_capacity; }
    
    protected Dictionary<Transform, Resource> _slots = new Dictionary<Transform, Resource>();
    protected int _currentWorkload = 0;

    private void Start()
    {
        if (_slots.Count == 0) Debug.Log(transform.parent.name + ": Empty slots!");
    }

    public bool isAvailable()
    {
        return _currentWorkload < (int)_capacity;
    }

    public bool IsEmpty()
    {
        return _currentWorkload == 0;
    }

    public Transform FillSlot(Resource resource)
    {
        if(resource != null)
        {
            foreach (var slot in _slots)
            {
                if (slot.Value == null)
                {
                    _currentWorkload++;
                    _slots[slot.Key] = resource;
                    return slot.Key;
                }
            }
        }
        return null;
    }

    public Resource ClearSlot()
    {
        if(!IsEmpty())
        {
            foreach (var slot in _slots)
            {
                if (slot.Value != null)
                {
                    _currentWorkload--;
                    Resource removedResource = _slots[slot.Key];
                    _slots[slot.Key] = null;
                    return removedResource;
                }
            }
        }
        return null;
    }

    public Resource ClearSlot(Resource resource = null, ResourceType? resourceType = null)
    {
        if(!IsEmpty())
        {
            foreach (var slot in _slots)
            {
                if (slot.Value != null && ((resource != null && slot.Value == resource) ||
                    (resourceType != null && slot.Value.Type == resourceType)))
                {
                    _currentWorkload--;
                    Resource removedResource = _slots[slot.Key];
                    _slots[slot.Key] = null;
                    return removedResource;
                }
            }
        }
        return null;
    }

    public void SetSlots(Transform slot)
    {
        _slots.Add(slot, null);
    }
}