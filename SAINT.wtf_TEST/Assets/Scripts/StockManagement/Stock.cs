using Factory.ResourceManagement;
using Factory.SlotsManagement;
using UnityEngine;

namespace Factory.StockManagement
{
    public enum StockDirection
    {
        Input,
        Output
    }

    public class Stock : SlotBase
    {
        [SerializeField] private StockType _type;

        public ResourceType Resource => _type.resource;
        public StockDirection Direction { get => _type.direction; set => _type.direction = value; }
        public float TransitionDelay => _type.transitionDelay;

        [System.Serializable]
        private struct StockType
        {
            public StockDirection direction;
            public ResourceType resource;
            public float transitionDelay;
        }

        public Resource ClearSlot()
        {
            if (IsEmpty) return null;

            foreach (var slot in slots)
            {
                if (slot.Value != null)
                {
                    currentWorkload--;
                    Resource removedResource = slots[slot.Key];
                    slots[slot.Key] = null;
                    RaiseEvents();
                    return removedResource;
                }
            }

            return null;
        }
    }
}