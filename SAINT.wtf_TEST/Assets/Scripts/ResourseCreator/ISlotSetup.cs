using UnityEngine;

public interface ISlotSetup
{
    public int capacity { get; }
    public void SetSlots(Transform slot);
}