using UnityEngine;

namespace Factory.ResourceManagement
{
    public enum ResourceType
    {
        Resource1,
        Resource2,
        Resource3
    }

    [RequireComponent(typeof(ResourceTransitionManager))]
    public class Resource : MonoBehaviour
    {
        [SerializeField] private ResourceType _resourceType;

        public ResourceType ResourceType { get => _resourceType; }
        public ResourceTransitionManager TransitionManager { get; private set; }

        void Awake()
        {
            TransitionManager = GetComponent<ResourceTransitionManager>();
        }
    }
}
