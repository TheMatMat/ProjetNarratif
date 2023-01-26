using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamSeven
{
    public class IndiceHandle : MonoBehaviour
    {
        public PNJConfigue[] pnjConfigues;
        public string itemId;

        public void EnableItem()
        {
            foreach(PNJConfigue pnj in pnjConfigues)
            {
                pnj.EnableDialogue(itemId);
            }
        }
    }
}
