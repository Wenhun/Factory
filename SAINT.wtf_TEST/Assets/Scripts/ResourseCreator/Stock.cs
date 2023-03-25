using System.Collections.Generic;
using UnityEngine;

public enum StockDirection
{
    Input, Output
}

public class Stock : StackResources
{
    [System.Serializable]
    private struct StockType
    {
        public StockDirection direction { get; set; }
        public ResourceType resource;
        public float transitionDelay;
    }

    [SerializeField] private StockType _type; 
    
    public ResourceType Resource {get => _type.resource; }
    public StockDirection Direction {get => _type.direction ; set => _type.direction = value;}
    public float TransitionDelay {get => _type.transitionDelay; }
}
