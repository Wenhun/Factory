using UnityEngine;
using System.Collections;

public class ResourceCreator : MonoBehaviour
{
    [SerializeField] private Resource resource_1;
    [SerializeField] private Resource resource_2;
    [SerializeField] private Resource resource_3;

    public Resource Get(ResourceType resource)
    {
        switch(resource)
        {
            case ResourceType.Resource_1:
                return resource_1;
            case ResourceType.Resource_2:
                return resource_2;
            case ResourceType.Resource_3:
                return resource_3;
        }
        return null;
    }
}
