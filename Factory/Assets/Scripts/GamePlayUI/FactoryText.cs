using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Factory.StockManagement;

namespace Factory.GamePlayUI
{
    public class FactoryText : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;

        private readonly Dictionary<Stock, string> _messages = new Dictionary<Stock, string>();

        private void Start()
        {
            if (_text == null)
            {
                Debug.LogError($"{nameof(_text)} is not set in {nameof(FactoryText)}.");
                return;
            }

            foreach (var stock in GetComponentsInChildren<Stock>())
            {
                _messages[stock] = "";
                stock.StatusUpdate += UpdateStockMessage;
            }
        }

        private void UpdateStockMessage()
        {
            var messagesCopy = new Dictionary<Stock, string>(_messages);
            _text.text = "";
            foreach (var kvp in messagesCopy)
            {
                var stock = kvp.Key;
                var message = "";

                if (stock.IsEmpty && stock.Direction != StockDirection.Output)
                {
                    message = $"Stock with {stock.Resource} is Empty!";
                }
                else if (stock.IsFull)
                {
                    message = $"Stock with {stock.Resource} is Full!";
                }

                _messages[stock] = message;
                _text.text += message + "\n";
            }
        }

        private void OnDestroy()
        {
            foreach (var stock in _messages.Keys)
            {
                stock.StatusUpdate -= UpdateStockMessage;
            }
        }
    }
}