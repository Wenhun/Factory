using UnityEngine;

[RequireComponent(typeof(Resource))]
[RequireComponent(typeof(BoxCollider))]
public class ResourceTrigger : MonoBehaviour
{
    private Resource _resource;
    private BoxCollider _boxCollider;

    void Start()
    {
        _resource = GetComponent<Resource>();
        _boxCollider = GetComponent<BoxCollider>();
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            StackResources stack = other.GetComponentInChildren<StackResources>();
            if(stack.isAvailable())
            {
                _boxCollider.isTrigger = false;
                GetComponentInParent<ISlotController>().ClearSlot(_resource);
                _resource.TransitionManager.MoveResource(transform.position, stack.FillSlot(_resource));
            }
        }
    }
}