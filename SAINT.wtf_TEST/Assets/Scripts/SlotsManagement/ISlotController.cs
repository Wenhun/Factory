using Factory.ResourceManagement;
using UnityEngine;

namespace Factory.SlotsManagement
{
    public interface ISlotController
    {
        public Transform FillSlot(Resource resource);
        public Resource ClearSlot(Resource resource = null, ResourceType? resourceType = null);
    }
}