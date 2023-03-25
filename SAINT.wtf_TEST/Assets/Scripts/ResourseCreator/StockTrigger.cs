using UnityEngine;

public class StockTrigger : MonoBehaviour
{
    private Stock _thisStock;

    void Start()
    {
        _thisStock = GetComponent<Stock>();
    }
    
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            StackResources stack = other.GetComponentInChildren<StackResources>();
            stack.SetStock(_thisStock);
            if(_thisStock.Direction == StockDirection.Input)
            {
                stack.RemoveFromStack(_thisStock.Resource);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        other.GetComponentInChildren<StackResources>().SetStock(null);
    }
}