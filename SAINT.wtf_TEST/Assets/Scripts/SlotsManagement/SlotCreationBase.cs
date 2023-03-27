using UnityEngine;

namespace Factory.SlotsManagement
{
    public abstract class SlotCreationBase : MonoBehaviour
    {
        [SerializeField] protected Transform firstSlot;
        [SerializeField] protected Transform lastSlot;

        protected ISlotSetup slotContainer;

        private void Awake()
        {
            if (firstSlot == null || lastSlot == null)
            {
                Debug.LogError($"Slots in {name} are not set!");
                enabled = false;
                return;
            }

            slotContainer = GetComponent<ISlotSetup>();

            if (slotContainer == null)
            {
                Debug.LogError($"Object: {slotContainer} in {name} is not found!");
                enabled = false;
                return;
            }

            CreateSlots();
        }

        protected abstract void CreateSlots();
    }
}