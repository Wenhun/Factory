using System.Collections.Generic;
using UnityEngine;
using Factory.ResourceManagement;
using System.Linq;

namespace Factory.PlayerStackManagement
{
    [RequireComponent(typeof(PlayerStack))]
    public class StackSorter : MonoBehaviour
    {
        private PlayerStack _stackResources;

        private void Start()
        {
            _stackResources = GetComponent<PlayerStack>();
            _stackResources.SlotsUpdate += SortSlots;
        }

        private void SortSlots(Dictionary<Transform, Resource> slots)
        {
            var sortedSlots = slots.Where(x => x.Value != null)
                                   .OrderBy(x => x.Value.transform.GetSiblingIndex())
                                   .ToList();

            for (int i = 0; i < sortedSlots.Count; i++)
            {
                var slot = slots.ElementAt(i);
                var sortedSlot = sortedSlots.ElementAt(i);

                if (slot.Value != sortedSlot.Value)
                {
                    slots[slot.Key] = sortedSlot.Value;
                    slots[sortedSlot.Key] = slot.Value;
                    sortedSlot.Value.TransitionManager.MoveResource(sortedSlot.Value.transform.position, slot.Key);
                }
            }
        }

        private void OnDestroy()
        {
            _stackResources.SlotsUpdate -= SortSlots;
        }
    }
}