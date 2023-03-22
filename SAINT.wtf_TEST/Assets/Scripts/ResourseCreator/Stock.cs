using UnityEngine;

public enum StockType
{
    Input, Output
}

public class Stock : MonoBehaviour
{
    [SerializeField] StockSlot _stockSlot;

    [System.Serializable]
    private class StockSlot
    {
        public StockType type;
        public ResourceType resource;
        [Range(5, 30)] public int capacity = 10;
    }

    [SerializeField] private int _currentWorkload = 0;
    private bool _isEmpty, _isFull;

    public StockType type { get => _stockSlot.type; }
    public ResourceType resource { get => _stockSlot.resource; }
    public bool IsEmpty { get => _isEmpty; }
    public bool IsFull { get => _isFull; }

    public void AddResource()
    {
        _currentWorkload++;
        _currentWorkload = Mathf.Min(_currentWorkload, _stockSlot.capacity);
    }

    public void RemoveResource()
    {
        _currentWorkload--;
        _currentWorkload = Mathf.Max(_currentWorkload, 0);
    }

    private void Update()
    {
        StockChecker();
    }

    void StockChecker()
    {
        if (_currentWorkload == _stockSlot.capacity)
        {
            _isFull = true;
        }
        else
        {
            _isFull = false;
        }

        if(_currentWorkload == 0)
        {
            _isEmpty = true;
        }
        else
        {
            _isEmpty = false;
        }
    }
}
