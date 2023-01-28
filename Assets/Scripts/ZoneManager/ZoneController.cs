using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TeamSeven
{

    public class ZoneController : MonoBehaviour
    {
        [Header("ZONE DETAILS")]
        public int zoneId;
        public string zoneFrName;
        public string zoneEnName;
        public Image zoneSprite;

        [Header("NAVIGATION")]
        public bool canGoPrevious;
        public bool canGoNext;
        public bool isLocked;

        public ZoneController previousZone;
        public ZoneController nextZone;

        [Header("ZONE EVIDENCES")]
        public List<IndiceController> indices = new List<IndiceController>();
    }
}