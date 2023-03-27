using UnityEngine;

namespace Factory.ResourceManagement
{
    public class ResourceDestroyer : MonoBehaviour
    {
        void OnTriggerEnter(Collider other)
        {
            if (other.tag == "IsCreated")
            {
                other.tag = "IsTrigger";
                return;
            }

            if (other.tag == "IsTrigger")
            {
                Destroy(other.transform.parent.gameObject);
            }
        }
    }
}
