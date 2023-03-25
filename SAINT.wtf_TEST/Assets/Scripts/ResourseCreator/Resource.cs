using UnityEngine;

public enum ResourceType
{
    Resource_1,
    Resource_2,
    Resource_3
}

public class Resource : MonoBehaviour
{
    [SerializeField] private ResourceType _type;
    public ResourceType Type { get => _type; }
}
