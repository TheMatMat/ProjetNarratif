using DG.Tweening;
using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointAndClickManager : MonoBehaviour
{
    public GameObject inventory;
    public GameObject detailPanel;
    public GameObject EvidenceIconPrefab;

    public EvidenceInventoryController tempEIC = null;

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


    public void NewEvidenceFound(IndiceController indiceController)
    {
        GameObject newIcon = Instantiate(EvidenceIconPrefab, inventory.transform);
        newIcon.SetActive(false);

        EvidenceInventoryController eIC = newIcon.GetComponent<EvidenceInventoryController>();
        eIC.correspondingIndiceController = indiceController;

        tempEIC = eIC;
        detailPanel.GetComponent<DetailPanelController>().FadeIn(indiceController.evidenceData.detailSprite);
    }
}
