using UnityEngine;

public class CreateStackSlots : CreateStockSlots
{
    protected override void CreateSlots()
    {
        int rowCount = _stock.capacity;
        float lengthY = _lastSlot.position.y - _firstSlot.position.y;
        float spacingY = lengthY / (rowCount - 1);

        for (int y = 0; y < rowCount; y++)
        {
            GameObject newSlot = new GameObject("Slot " + y);
            newSlot.transform.SetParent(transform);
            newSlot.transform.localPosition = new Vector3(0, _firstSlot.localPosition.y + spacingY * y, 0);
            _stock.SetSlots(newSlot.transform);
        }
    }
}