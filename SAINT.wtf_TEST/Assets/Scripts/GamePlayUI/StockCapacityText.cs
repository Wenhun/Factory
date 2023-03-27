using UnityEngine;
using TMPro;
using Factory.StockManagement;

namespace Factory.GamePlayUI
{
    public class StockCapacityText : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;

        private Stock _stock;

        private void Awake()
        {
            _stock = GetComponentInParent<Stock>();
            if (_stock == null)
            {
                Debug.LogError($"Stock component not found in parents of {nameof(StockCapacityText)}");
                enabled = false;
            }
        }

        private void OnEnable()
        {
            if (_stock != null)
            {
                _stock.WorkloadUpdate += OnWorkloadUpdate;
            }
        }

        private void OnDisable()
        {
            if (_stock != null)
            {
                _stock.WorkloadUpdate -= OnWorkloadUpdate;
            }
        }

        private void OnWorkloadUpdate(int currentWorkload, int capacity)
        {
            _text.text = $"{currentWorkload}/{capacity}";
            _text.color = (currentWorkload == 0 && _stock.Direction != StockDirection.Output) || currentWorkload == capacity ? Color.red : Color.white;
        }
    }
}