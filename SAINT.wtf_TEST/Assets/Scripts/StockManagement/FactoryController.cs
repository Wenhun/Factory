using UnityEngine;
using System.Linq;
using Factory.ResourceManagement;

namespace Factory.StockManagement
{
    [RequireComponent(typeof(ResourceCreator))]
    public class FactoryController : MonoBehaviour
    {
        [SerializeField] private Stock _outputStock;
        [SerializeField] private Stock[] _inputStocks;
        [SerializeField] private float _productionTime = 3f;

        private ResourceCreator _resourceCreator;
        private float _timer;
        private bool _isMonoFactory;

        private void Awake()
        {
            if (_outputStock == null || !_outputStock.gameObject.activeInHierarchy)
            {
                Debug.LogError($"Object {_outputStock} in {nameof(FactoryController)} not found or inactive!");
                enabled = false;
                return;
            }

            foreach (Stock inputStock in _inputStocks)
            {
                if (inputStock == null || !inputStock.gameObject.activeInHierarchy)
                {
                    Debug.LogError($"Object {inputStock} in {nameof(FactoryController)} not found or inactive!");
                    enabled = false;
                    return;
                }

                inputStock.Direction = StockDirection.Input;
            }

            _outputStock.Direction = StockDirection.Output;
        }

        private void Start()
        {
            _resourceCreator = GetComponent<ResourceCreator>();
            _isMonoFactory = _inputStocks.Length == 0;
            _timer = _productionTime;
        }

        private void Update()
        {
            if (IsWorking())
            {
                _timer -= Time.deltaTime;
                if (_timer <= 0)
                {
                    Produce();
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
            return !_outputStock.IsFull && (_isMonoFactory || _inputStocks.All(inputStock => !inputStock.IsEmpty));
        }

        private void Produce()
        {
            foreach (Stock inputStock in _inputStocks)
            {
                Resource resource = inputStock.ClearSlot();
                resource.TransitionManager.MoveResource(resource.transform.position, transform);
            }

            Resource newResource = Instantiate(_resourceCreator.Get(_outputStock.Resource));
            Transform slot = _outputStock.FillSlot(newResource);
            newResource.TransitionManager.MoveResource(transform.position, slot);
        }
    }
}