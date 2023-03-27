using Factory.SlotsManagement;
using UnityEngine;

namespace Factory.PlayerStackManagement
{
    public class PlayerStack : SlotBase
    {
        private void Start()
        {
            if (slots.Count == 0) Debug.Log(transform.parent.name + ": Empty slots!");
        }
    }
}