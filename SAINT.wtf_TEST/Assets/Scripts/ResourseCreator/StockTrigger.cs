using UnityEngine;

[RequireComponent(typeof(StockTranslationDelay))]
[RequireComponent(typeof(Stock))]
public class StockTrigger : MonoBehaviour
{
    private Stock _thisStock;
    private StockTranslationDelay _translationDelay;

    void Start()
    {
        _thisStock = GetComponent<Stock>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && _thisStock.Direction == StockDirection.Input)
        {
            StackResources stackResources = other.GetComponentInChildren<StackResources>();
            _translationDelay = new StockTranslationDelay(stackResources, _thisStock, _thisStock.TransitionDelay);
            StartCoroutine(_translationDelay.TranslationDelay());
        }
    }

    void OnTriggerExit(Collider other)
    {
        StopAllCoroutines();
        _translationDelay = null;
    }
}