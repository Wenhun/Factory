using Factory.SlotsManagement;
using UnityEngine;

namespace Factory.StockManagement
{
    public class CreateStockSlots : SlotCreationBase
    {
        protected override void CreateSlots()
        {
            int capacity = slotContainer.Capacity;
            int rowCount = Mathf.CeilToInt(Mathf.Sqrt(capacity));
            int colCount = Mathf.CeilToInt((float)capacity / rowCount);

            float lengthX = lastSlot.position.x - firstSlot.position.x;
            float lengthY = lastSlot.position.z - firstSlot.position.z;
            float spacingX = lengthX / (rowCount - 1);
            float spacingY = lengthY / (colCount - 1);

            for (int x = 0; x < rowCount; x++)
            {
                for (int y = 0; y < colCount; y++)
                {
                    GameObject newSlot = new GameObject("Slot " + x + " " + y);
                    newSlot.transform.SetParent(transform);
                    newSlot.transform.position = new Vector3(firstSlot.position.x + spacingX * x, 0.5f, firstSlot.position.z + spacingY * y);
                    newSlot.transform.localRotation = Quaternion.identity;
                    slotContainer.SetSlots(newSlot.transform);
                }
            }
        }
    }
}