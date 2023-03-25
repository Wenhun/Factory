using System.Collections;
using UnityEngine;

public class StockTranslationDelay
{
    private float _delayTime;
    private StackResources _stackResources;
    private Stock _stock;

    public StockTranslationDelay(StackResources stackResources, Stock stock, float delayTime)
    {
        _stackResources = stackResources;
        _stock = stock;
        _delayTime = delayTime;
    }

    public IEnumerator TranslationDelay()
    {
        Resource resource;
        do
        {
            yield return new WaitForSeconds(_delayTime);
            resource = _stackResources.ClearSlot(resourceType: _stock.Resource);
            if (resource != null)
            {
                Transform newSlot = _stock.FillSlot(resource);
                resource.TransitionManager.MoveResource(resource.transform.position, newSlot);
            }
        }
        while (resource != null);
    }
}