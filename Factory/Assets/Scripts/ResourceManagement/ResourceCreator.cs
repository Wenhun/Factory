using UnityEngine;

namespace Factory.ResourceManagement
{
    public class ResourceCreator : MonoBehaviour
    {
        [SerializeField] private Resource _resource_1_Prefab;
        [SerializeField] private Resource _resource_2_Prefab;
        [SerializeField] private Resource _resource_3_Prefab;

        void Start()
        {
            if (_resource_1_Prefab == null || _resource_2_Prefab == null || _resource_3_Prefab == null)
            {
                Debug.LogError($"Resources prefabs in {nameof(name)} not found or inactive!");
            }
        }

        public Resource Get(ResourceType resource)
        {
            switch (resource)
            {
                case ResourceType.Resource1:
                    return _resource_1_Prefab;
                case ResourceType.Resource2:
                    return _resource_2_Prefab;
                case ResourceType.Resource3:
                    return _resource_3_Prefab;
            }
            return null;
        }
    }

}