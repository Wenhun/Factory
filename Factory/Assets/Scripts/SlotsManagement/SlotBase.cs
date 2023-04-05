using System.Linq;
using System;
using System.Collections.Generic;
using UnityEngine;
using Factory.ResourceManagement;

namespace Factory.SlotsManagement
{
    public abstract class SlotBase : MonoBehaviour, ISlotSetup, ISlotController
    {
        private enum CapacityEnum
        {
            four = 4,
            eight = 8,
            twelve = 12,
            sixteen = 16,
            twentyFour = 24
        }

        [SerializeField] private CapacityEnum _capacity = CapacityEnum.twelve;

        //ISlotSetup field
        public int Capacity { get => (int)_capacity; }

        //events
        public event Action StatusUpdate;
        public event Action<int, int> WorkloadUpdate;
        public event Action<Dictionary<Transform, Resource>> SlotsUpdate;

        //status bool
        public bool IsFull => currentWorkload == Capacity;
        public bool IsEmpty => currentWorkload == 0;

        protected Dictionary<Transform, Resource> slots = new Dictionary<Transform, Resource>();
        protected int currentWorkload = 0;

        private void Start()
        {
            if (slots.Count == 0) Debug.Log(transform.parent.name + ": Empty slots!");

            WorkloadUpdate?.Invoke(currentWorkload, (int)_capacity);
            StatusUpdate?.Invoke();
        }

        protected void RaiseEvents()
        {
            WorkloadUpdate?.Invoke(currentWorkload, (int)_capacity);
            SlotsUpdate?.Invoke(slots);
            StatusUpdate?.Invoke();
        }

        public Transform FillSlot(Resource resource)
        {
            if (resource == null) return null;

            foreach (var slot in slots)
            {
                if (slot.Value == null)
                {
                    currentWorkload++;
                    slots[slot.Key] = resource;
                    WorkloadUpdate?.Invoke(currentWorkload, Capacity);
                    StatusUpdate?.Invoke();
                    return slot.Key;
                }
            }

            return null;
        }

        public Resource ClearSlot(Resource resource = null, ResourceType? resourceType = null)
        {
            if (IsEmpty) return null;

            for (int i = slots.Count - 1; i >= 0; i--)
            {
                var slot = slots.Values.ElementAt(i);
                if (slot != null && ((resource != null && slot == resource) ||
                    (resourceType != null && slot.ResourceType == resourceType)))
                {
                    currentWorkload--;
                    Resource removedResource = slot;
                    slots[slots.Keys.ElementAt(i)] = null;
                    RaiseEvents();
                    return removedResource;
                }
            }

            return null;
        }

        public void SetSlots(Transform slot)
        {
            slots.Add(slot, null);
        }
    }
}