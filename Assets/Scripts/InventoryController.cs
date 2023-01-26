using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TeamSeven
{

    public class InventoryController : MonoBehaviour
    {
        public IndiceController correspondingIndiceController;
        //private DetailPanelController dPC;

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
        public TextMeshProUGUI evidenceNameText;


        /*void Start()
        {
            dPC = PointAndClickManager.Instance.detailPanel.GetComponent<DetailPanelController>();
        }*/

        public void InventoryInOut()
        {
            if (DialogueControler.instance.DialoguePanelOpen) return;

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

                changeSprite.Append(evidenceDetailSprite.DOFade(0f, 0.3f).OnComplete(() => UpdateVisibleData(indiceController)));
                changeSprite.Append(evidenceDetailSprite.DOFade(1.0f, 0.3f));

                changeSprite.Play();
            }
            else
            {
                UpdateVisibleData(indiceController);
                evidenceDetail.transform.DOMove(evidenceVisiblePos.position, 0.5f);
                blackBG.DOFade(0.8f, 0.2f);

                isDetailVisible = true;
            }
        }

        private void UpdateVisibleData(IndiceController indiceController)
        {
            evidenceDetailSprite.sprite = indiceController.evidenceData.detailSprite;

            switch (GameManager.instance.language)
            {
                case GameManager.LANGUAGE.FR:
                    evidenceNameText.text = indiceController.evidenceData.name.FR;
                    evidenceDetailText.text = indiceController.evidenceData.description.FR;
                    break;
                case GameManager.LANGUAGE.EN:
                    evidenceNameText.text = indiceController.evidenceData.name.EN;
                    evidenceDetailText.text = indiceController.evidenceData.description.EN;
                    break;
            }
        }
    }
}
