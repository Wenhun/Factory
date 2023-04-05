using Factory.SlotsManagement;
using UnityEngine;

namespace Factory.PlayerStackManagement
{
    public class CreateStackSlots : SlotCreationBase
    {
        protected override void CreateSlots()
        {
            int rowCount = slotContainer.Capacity;
            float spacingY = (lastSlot.position.y - firstSlot.position.y) / (rowCount - 1);

            for (int y = 0; y < rowCount; y++)
            {
                GameObject newSlot = new GameObject($"Slot {y}");
                newSlot.transform.SetParent(transform, false);
                newSlot.transform.localPosition = firstSlot.localPosition + new Vector3(0, spacingY * y, 0);
                slotContainer.SetSlots(newSlot.transform);
            }
        }
    }
}