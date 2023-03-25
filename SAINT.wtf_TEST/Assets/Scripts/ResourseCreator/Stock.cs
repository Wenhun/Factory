using System.Collections.Generic;
using UnityEngine;

public enum StockDirection
{
    Input, Output
}

[System.Serializable]
public struct StockType
{
    private enum Capacity
    {
        four = 4,
        eight = 8,
        twelve = 12,
        sixteen = 16,
        twentyFour = 24
    }
    
    [SerializeField] private Capacity capacity;

    public StockDirection direction {get; set;}
    public ResourceType resource;
    public int GetCapacity {get => (int)capacity; }
}

public class Stock : MonoBehaviour, ISlotSetup
{
    [SerializeField] private StockType _type; 

    public int capacity {get => _type.GetCapacity; }
    public ResourceType Resource {get => _type.resource; }
    public StockDirection Direction {get => _type.direction ; set => _type.direction = value;}
    
    private Dictionary<Transform, Resource> _slots = new Dictionary<Transform, Resource>();
    private int _currentWorkload = 0;

    public void SetSlots(Transform slot)
    {
        _slots.Add(slot, null);
    }

    void Start()
    {
        if(_slots.Count == 0) Debug.Log(transform.parent.name + ": Empty slots!");
    }

    public bool IsAvailable()
    {
        return _currentWorkload < _slots.Count;
    }

    public bool IsEmpty()
    {
        return _currentWorkload == 0;
    }

    public Transform FillSlot(Resource resource) 
    {
        foreach(var slot in _slots)
        {
            if(slot.Value == null)
            {
                _currentWorkload++;
                _slots[slot.Key] = resource;
                return slot.Key;
            }
        }
        return null;
    }

    public Resource ClearSlot()
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

        return null;
    }

    public Resource ClearSlot(Resource resource)
    {
        foreach (var slot in _slots)
        {
            if (slot.Value == resource)
            {
                _currentWorkload--;
                Resource removedResource = _slots[slot.Key];
                _slots[slot.Key] = null;
                return removedResource;
            }
        }
        return null;
    }
}
