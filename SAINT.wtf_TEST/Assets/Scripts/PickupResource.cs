using UnityEngine;

public class PickupResource : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            StackResources stack = other.GetComponentInChildren<StackResources>();
            if(stack.isAvailable())
            {
                GetComponent<BoxCollider>().isTrigger = false;
                stack.AddToStack(this.GetComponent<Resource>());
            }
        }
    }
}