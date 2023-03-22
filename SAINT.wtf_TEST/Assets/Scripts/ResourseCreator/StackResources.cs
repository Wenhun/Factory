using System.Collections.Generic;
using UnityEngine;

public class StackResources : MonoBehaviour
{
    [SerializeField][Range(3, 10)] private int _capacity = 5;
    
    private Dictionary<Transform, ResourceType> _stack = new Dictionary<Transform, ResourceType>();


}