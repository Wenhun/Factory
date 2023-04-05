using Factory.PlayerStackManagement;
using Factory.SlotsManagement;
using UnityEngine;

namespace Factory.ResourceManagement
{
    [RequireComponent(typeof(Resource), typeof(BoxCollider))]
    public class ResourceTrigger : MonoBehaviour
    {
        private Resource resource;
        private BoxCollider boxCollider;

        private void Start()
        {
            resource = GetComponent<Resource>();
            boxCollider = GetComponent<BoxCollider>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                var stack = other.GetComponentInChildren<PlayerStack>();

                if (stack == null)
                {
                    Debug.LogError($"Object: {nameof(stack)} in {nameof(ResourceTrigger)} Not Found!");
                    return;
                }

                if (stack.IsFull)
                {
                    return;
                }

                PickupResource(stack);
            }
        }

        private void PickupResource(PlayerStack stack)
        {
            boxCollider.enabled = false;
            GetComponentInParent<ISlotController>().ClearSlot(resource);
            resource.TransitionManager.MoveResource(transform.position, stack.FillSlot(resource));
        }
    }
}