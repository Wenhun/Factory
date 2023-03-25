using System.Collections.Generic;
using UnityEngine;

public class StackResources : MonoBehaviour, ISlotSetup
{
    [SerializeField][Range(3, 10)] private int _capacity = 5;
    [SerializeField] ResourceTransitionManager _resourceTransitionManager;
    
    private Dictionary<Transform, Resource> _stack = new Dictionary<Transform, Resource>();
    private List<Transform> _slotsForClear = new List<Transform>(); 
    public int capacity {get => _capacity; }
    private Stock _enteredStock;
    private int _currentWorkload = 0;

    public bool isAvailable()
    {
        return _currentWorkload < _capacity;
    }

    public void AddToStack(Resource resource)
    {
        if(resource != null)
        {
            foreach (var slot in _stack)
            {
                if (slot.Value == null)
                {
                    _enteredStock.ClearSlot(resource);
                    _resourceTransitionManager.MoveResource(resource, resource.transform.position, slot.Key.transform);
                    _stack[slot.Key] = resource;
                    _currentWorkload++;
                    break;
                }
            }
        }
    }

    public void RemoveFromStack(ResourceType resource)
    {
        if(_enteredStock != null)
        {
            foreach (var slot in _stack)
            {
                if (slot.Value.Type == resource)
                {
                    _slotsForClear.Add(slot.Key);
                    Transform newPoint = _enteredStock.FillSlot(slot.Value);
                    _resourceTransitionManager.MoveResource(slot.Value, slot.Value.transform.position, newPoint);
                }
            }

            if (_slotsForClear.Count != 0)
            {
                foreach (Transform slot in _slotsForClear)
                {
                    _stack.Remove(slot);
                }
                _slotsForClear.Clear();
            }
        }
    }

    public void SetSlots(Transform slot)
    {
        _stack.Add(slot, null);
    }

    public void SetStock(Stock stock)
    {
        _enteredStock = stock;
    }

}