using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZoneController : MonoBehaviour
{
    [Header("zone details")]
    public int zoneId;
    public string zoneName;
    public Image zoneSprite;
    public bool isManuallyChangeable;

    [Header("surronding zones")]
    public ZoneController previousZone;
    public ZoneController nextZone;

    [Header("all zone evidences")]
    public List<IndiceController> indices = new List<IndiceController>();
}
