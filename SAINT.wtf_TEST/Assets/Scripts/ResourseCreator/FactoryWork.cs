using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(ResourceCreator))]
public class FactoryWork : MonoBehaviour
{
    [SerializeField] private float _productionTime = 3f;
    [SerializeField] private List<Stock> _stocks = new List<Stock>();
    [SerializeField] private bool _isMonoFactory;

    private Stock _productionStock;
    private List<Stock> _inputStocks = new List<Stock>();

    private ResourceCreator _resourceCreator;

    private float _timer;

    private void Start()
    {
        _resourceCreator = GetComponent<ResourceCreator>();

        if(!_isMonoFactory)
        {
            foreach (Stock stock in _stocks)
            {
                switch (stock.type)
                {
                    case StockType.Output:
                        _productionStock = stock;
                        continue;
                    case StockType.Input:
                        _inputStocks.Add(stock);
                        continue;
                }
            }
        }
        else
        {
            _productionStock = _stocks[0];
        }

        _timer = _productionTime;
    }

    private void Update()
    {
        if(IsCanCreate())
        {
            _timer -= Time.deltaTime;
            if (_timer <= 0)
            {
                Create();
                _timer = _productionTime;
            }
        }
        else
        {
            _timer = _productionTime;
        }
    }

    private void Create()
    {
        Instantiate(_resourceCreator.Get(_productionStock.resource), _productionStock.transform);
        if(!_isMonoFactory)
        {
            foreach (Stock stock in _inputStocks)
            {
                stock.RemoveResource();
            }
        }
        _productionStock.AddResource();
    }

    private bool IsCanCreate()
    {
        if (_isMonoFactory) return !_productionStock.IsFull;

        return !_productionStock.IsFull && !_inputStocks.Exists(stock => stock.IsEmpty);
    }
}
