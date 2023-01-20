using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using static IndiceController;

public class EvidenceInventoryController : MonoBehaviour
{
    public IndiceController correspondingIndiceController;
    private DetailPanelController dPC;
    

    // Start is called before the first frame update
    void Start()
    {
        dPC = PointAndClickManager.Instance.detailPanel.GetComponent<DetailPanelController>();
    }

    // Update is called once per frame
    void Update()
    {
        
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

    public void ShowInInventory()
    {
        this.gameObject.SetActive(true);
        transform.localScale = Vector3.zero;

        Sequence appearSequence = DOTween.Sequence();

        appearSequence.Append(transform.DOScale(new Vector3(1.5f, 1.5f, 1.5f), 0.3f));
        appearSequence.Append(transform.DOScale(new Vector3(1f, 1f, 1f), 0.1f));

        appearSequence.Play();
    }
}
