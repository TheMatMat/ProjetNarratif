using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using UnityEditor.U2D.Animation;
using UnityEngine;
using UnityEngine.UI;
using static IndiceController;

public class InventoryController : MonoBehaviour
{
    public IndiceController correspondingIndiceController;
    private DetailPanelController dPC;

    public Image blackBG;

    [Header("Inventory")]
    private bool isInventoryVisible = false;
    public GameObject inventoryContainer;
    public GameObject inventoryElements;
    public Transform inventoryVisiblePos, inventoryInvisblePos;

    [Header("Detail")]
    private bool isDetailVisible = false;
    public GameObject evidenceDetail;
    public Transform evidenceVisiblePos, evidenceInvisblePos;

    public Image evidenceDetailSprite;
    public TextMeshProUGUI evidenceDetailText;


    // Start is called before the first frame update
    void Start()
    {
        dPC = PointAndClickManager.Instance.detailPanel.GetComponent<DetailPanelController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InventoryInOut()
    {
        if (isInventoryVisible)
        {
            inventoryContainer.transform.DOMove(inventoryInvisblePos.position, 0.5f);
            evidenceDetail.transform.DOMove(evidenceInvisblePos.position, 0.5f);
            blackBG.DOFade(0f, 0.2f).OnComplete(() => this.gameObject.SetActive(false));
        }
        else
        {
            this.gameObject.SetActive(true);
            inventoryContainer.transform.DOMove(inventoryVisiblePos.position, 0.5f);
            blackBG.DOFade(0.8f, 0.2f);
        }

        isInventoryVisible = !isInventoryVisible;
        isDetailVisible = false;
    }

    public void DetailElementInOut(IndiceController indiceController)
    {
        if (isDetailVisible)
        {
            Sequence changeSprite = DOTween.Sequence();

            changeSprite.Append(evidenceDetailSprite.DOFade(0f, 0.2f).OnComplete(() => evidenceDetailSprite.sprite = indiceController.evidenceData.detailSprite));
            changeSprite.Append(evidenceDetailSprite.DOFade(1.0f, 0.2f));

            changeSprite.Play();
        }
        else
        {
            evidenceDetailSprite.sprite = indiceController.evidenceData.detailSprite;
            evidenceDetail.transform.DOMove(evidenceVisiblePos.position, 0.5f);
            blackBG.DOFade(0.8f, 0.2f);

            isDetailVisible = true;
        }
    }

    public void ShowDetail()
    {
        if (!correspondingIndiceController)
            return;

        dPC.FadeIn(correspondingIndiceController.evidenceData.detailSprite);
        Debug.Log("show detail: " + correspondingIndiceController.evidenceData.evidenceName);
    }

    public void HideDetail()
    {
        dPC.FadeOut();
        Debug.Log("hide detail: " + correspondingIndiceController.evidenceData.evidenceName);
    }
}
