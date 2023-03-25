using UnityEngine;

public interface ISlotController
{
    public Transform FillSlot(Resource resource);
    public Resource ClearSlot();
    public Resource ClearSlot(Resource resource = null, ResourceType? resourceType = null);
    public bool isAvailable();
    public bool IsEmpty();
}