using DG.Tweening;
using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointAndClickManager : MonoBehaviour
{
    public GameObject inventoryElements;
    public GameObject detailElement;
    public GameObject detailPanel;
    public GameObject EvidenceIconPrefab;

    public InventoryController inventoryController;

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
        GameObject newIcon = Instantiate(EvidenceIconPrefab, inventoryController.inventoryElements.transform);
        newIcon.GetComponent<IndiceController>().evidenceData = indiceController.evidenceData;
        newIcon.GetComponent<IndiceController>().InitSprite(1);

        detailPanel.GetComponent<DetailPanelController>().FadeIn(indiceController.evidenceData.detailSprite);
    }

    public void ShowOneEvidenceInfos(IndiceController indiceController)
    {
        inventoryController.DetailElementInOut(indiceController);
    }
}
