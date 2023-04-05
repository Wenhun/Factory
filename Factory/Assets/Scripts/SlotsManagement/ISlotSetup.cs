using UnityEngine;

namespace Factory.SlotsManagement
{
    public interface ISlotSetup
    {
        public int Capacity { get; }
        public void SetSlots(Transform slot);
    }
}