using DG.Tweening;
using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointAndClickManager : MonoBehaviour
{
    public GameObject inventory;
    public GameObject EvidenceIconPrefab;

    private static PointAndClickManager instance = null;
    public static PointAndClickManager Instance => instance;
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }

    public void NewEvidenceFound(IndiceData data)
    {
        GameObject newIcon = Instantiate(EvidenceIconPrefab, inventory.transform);

        EvidenceInventoryController eic = newIcon.GetComponent<EvidenceInventoryController>();

        eic.Appear();

        eic.data = data;
    }
}
