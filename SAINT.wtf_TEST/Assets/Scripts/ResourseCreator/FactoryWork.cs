using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(ResourceCreator))]
public class FactoryWork : MonoBehaviour
{
    [SerializeField] private Stock _outputStock;
    [SerializeField] private Stock[] _inputStocks;
    [SerializeField] private float _productionTime = 3f;
    [SerializeField]ResourceTransitionManager _resourceTransitionManager;

    private bool _isMonoFactory = false;
    private List<Resource> _resourceForClean = new List<Resource>();
    private ResourceCreator _resourceCreator;
    private float _timer;

    private void Start()
    {
        _outputStock.Direction = StockDirection.Output;

        foreach(Stock inputStock in _inputStocks)
        {
            inputStock.Direction = StockDirection.Input;
        }

        _resourceCreator = GetComponent<ResourceCreator>();

        if(_inputStocks.Length == 0) _isMonoFactory = true;

        _timer = _productionTime;
    }

    private void Update()
    {
        if(IsWorking())
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

    public bool IsWorking()
    {
        if (!_outputStock.isAvailable()) 
            return false;

        if (_isMonoFactory)
            return true;

        foreach (Stock inputStock in _inputStocks)
        {
            if (inputStock.IsEmpty()) return false;
        }

        return true;
    }

    private void Create()
    {
        foreach (Stock inputStock in _inputStocks)
        {
            Resource resource =  inputStock.ClearSlot();
            resource.TransitionManager.MoveResource(resource.transform.position, transform);
            _resourceForClean.Add(resource);
        }

        CreateResource();
    }

    private void CreateResource()
    {
        Resource newResource = Instantiate(_resourceCreator.Get(_outputStock.Resource));
        Transform slot = _outputStock.FillSlot(newResource);
        newResource.TransitionManager.MoveResource(transform.position, slot);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Resource")
        {
            Destroy(other.gameObject);
        }
    }
}
