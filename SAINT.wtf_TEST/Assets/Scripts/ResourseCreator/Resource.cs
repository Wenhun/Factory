using UnityEngine;

public enum ResourceType
{
    Resource_1,
    Resource_2,
    Resource_3
}

[RequireComponent(typeof(ResourceTransitionManager))]
public class Resource : MonoBehaviour
{
    [SerializeField] private ResourceType _type;
    private ResourceTransitionManager _resourceTransitionManager;
    public ResourceType Type { get => _type; }
    public ResourceTransitionManager TransitionManager {get => _resourceTransitionManager; }

    void Awake()
    {
        _resourceTransitionManager = GetComponent<ResourceTransitionManager>();
    }
}
