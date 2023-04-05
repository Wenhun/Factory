using System.Collections;
using Factory.GamePlayUI;
using Factory.PlayerStackManagement;
using Factory.ResourceManagement;
using UnityEngine;

namespace Factory.StockManagement
{
    public class StockTranslationDelay
    {
        private readonly float _delayTime;
        private readonly PlayerStack _stackResources;
        private readonly Stock _stock;
        private readonly ProgressBar _progressBar;

        public StockTranslationDelay(PlayerStack stackResources, Stock stock, float delayTime, ProgressBar progressBar)
        {
            _stackResources = stackResources;
            _stock = stock;
            _delayTime = delayTime;
            _progressBar = progressBar;
        }

        public IEnumerator TranslationDelay()
        {
            Resource resource;
            do
            {
                _progressBar.BarChange(_delayTime);
                yield return new WaitForSeconds(_delayTime);
                resource = _stackResources.ClearSlot(resourceType: _stock.Resource);
                if (resource != null)
                {
                    MoveToNewSlot(resource);
                }
            } while (resource != null);
        }

        private void MoveToNewSlot(Resource resource)
        {
            var newSlot = _stock.FillSlot(resource);
            resource.TransitionManager.MoveResource(resource.transform.position, newSlot);
        }
    }
}