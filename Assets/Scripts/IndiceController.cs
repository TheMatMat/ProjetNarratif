using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IndiceController : MonoBehaviour
{
    [System.Serializable]
    public struct EvidenceData
    {
        public string evidenceName;
        public int sceneID;
        public Sprite sceneSprite;
        public Sprite detailSprite;
        public Sprite inventorySprite;

        public EvidenceData(string _evidenceName, int _sceneID, Sprite _sceneSprite, Sprite _detailSprite, Sprite _inventorySprite)
        {
            evidenceName = _evidenceName;
            sceneID = _sceneID;
            sceneSprite = _sceneSprite;
            detailSprite = _detailSprite;
            inventorySprite = _inventorySprite;
        }
    }

    [Header("evidence data")]
    public EvidenceData evidenceData;
    private Image _sprite;

    // Start is called before the first frame update
    void Start()
    {
        _sprite = this.GetComponent<Image>();
        InitSprite(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitSprite(int mode = 0)
    {
        if (_sprite == null)
            return;

        switch (mode)
        {
            case 0:
                this.GetComponent<Image>().sprite = evidenceData.sceneSprite;
                break;
            case 1:
                this.GetComponent<Image>().sprite = evidenceData.inventorySprite;
                break;
        }
    }

    public void OnPointerDownScene()
    {
        Debug.Log("indice has been found: " + evidenceData.evidenceName);

        Sequence disapearSequence = DOTween.Sequence(); 

        disapearSequence.Append(transform.DOScale(new Vector3(1.5f, 1.5f, 1.5f), 0.2f));
        disapearSequence.Append(transform.DOScale(new Vector3(0f, 0f, 0f), 0.3f));

        disapearSequence.Play();

        PointAndClickManager.Instance.NewEvidenceFound(this);
    }

    public void OnPointerDownInventory()
    {
        PointAndClickManager.Instance.ShowOneEvidenceInfos(this);
        Debug.Log("this indice is: " + evidenceData.evidenceName);
    }
}
