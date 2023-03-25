using UnityEngine;

public class ResourceDestroyer : MonoBehaviour
{
    void OnTriggerStay(Collider other)
    {
        if(other.tag == "Resource")
        {
            Destroy(other.gameObject);
        }
    }
}
