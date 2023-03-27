using Factory.GamePlayUI;
using Factory.PlayerStackManagement;
using UnityEngine;

namespace Factory.StockManagement
{
    [RequireComponent(typeof(Stock))]
    public class StockTrigger : MonoBehaviour
    {
        private Stock _thisStock;
        private StockTranslationDelay _translationDelay;
        private ProgressBar _progressBar;

        private void Awake()
        {
            _thisStock = GetComponent<Stock>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player") && _thisStock.Direction == StockDirection.Input)
            {
                PlayerStack stackResources = other.GetComponentInChildren<PlayerStack>();

                if (stackResources == null)
                {
                    Debug.LogError($"Object: {nameof(stackResources)} in {nameof(StockTrigger)} not found!");
                    return;
                }

                _progressBar = other.GetComponentInChildren<ProgressBar>();

                if (_progressBar == null)
                {
                    Debug.LogError($"Object: {nameof(_progressBar)} in {nameof(StockTrigger)} not found!");
                    return;
                }

                _translationDelay = new StockTranslationDelay(stackResources, _thisStock, _thisStock.TransitionDelay, _progressBar);
                StartCoroutine(_translationDelay.TranslationDelay());
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (_translationDelay != null)
            {
                StopCoroutine(_translationDelay.TranslationDelay());
                _translationDelay = null;
            }
        }
    }
}