using UnityEngine;

public class CreateStockSlots : MonoBehaviour
{
    [SerializeField] protected Transform _firstSlot;
    [SerializeField] protected Transform _lastSlot;

    protected ISlotSetup _stock;

    private void Start()
    {
        _stock = GetComponent<ISlotSetup>();
        CreateSlots();
    }

    protected virtual void CreateSlots()
    {
        int capacity = _stock.capacity;
        int rowCount = Mathf.CeilToInt(Mathf.Sqrt(capacity));
        int colCount = Mathf.CeilToInt((float)capacity / rowCount);

        float lengthX = _lastSlot.position.x - _firstSlot.position.x;
        float lengthY = _lastSlot.position.z - _firstSlot.position.z;
        float spacingX = lengthX / (rowCount - 1);
        float spacingY = lengthY / (colCount - 1);

        for (int x = 0; x < rowCount; x++)
        {
            for (int y = 0; y < colCount; y++)
            {
                GameObject newSlot = new GameObject("Slot " + x + " " + y);
                newSlot.transform.SetParent(transform);
                newSlot.transform.position = new Vector3(_firstSlot.position.x + spacingX * x, 0.5f, _firstSlot.position.z + spacingY * y);
                _stock.SetSlots(newSlot.transform);
            }
        }
    }
}