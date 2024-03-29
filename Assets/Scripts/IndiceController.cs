using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TeamSeven
{

    public class IndiceController : MonoBehaviour
    {
        [System.Serializable]
        public struct EvidenceData
        {
            //public string evidenceName;
            //public int sceneID;
            public Sprite sceneSprite;
            public Sprite detailSprite;
            //[TextArea] public string evidenceDescription;

            public DialogueTable.Row name;
            public DialogueTable.Row description;

            /*public EvidenceData(string _evidenceName, int _sceneID, Sprite _sceneSprite, Sprite _detailSprite, string _evidenceDescription)
            {
                evidenceName = _evidenceName;
                sceneID = _sceneID;
                sceneSprite = _sceneSprite;
                detailSprite = _detailSprite;
                evidenceDescription = _evidenceDescription;
            }*/

            public EvidenceData(Sprite _scene, Sprite _detail, DialogueTable.Row _name, DialogueTable.Row _description)
            {
                sceneSprite = _scene;
                detailSprite = _detail;
                name = _name;
                description = _description;
            }

            public EvidenceData(Sprite _scene, Sprite _detail)
            {
                sceneSprite = _scene;
                detailSprite = _detail;
                name = new DialogueTable.Row();
                description = new DialogueTable.Row();
            }
        }

        public bool loadSCVatStart = true;

        [Header("Evidence Data")]
        [SerializeField] private TextAsset csvFile;
        public string nameID;
        public string descriptionID;
        public ZoneController unlockedZone;

        [Header("Evidence Sprites")]
        public Sprite sceneSp;
        public Sprite detailSp;

        [HideInInspector] public EvidenceData evidenceData;
        private Image _image;

        private DialogueConfig _dialogueConfig;

        // Start is called before the first frame update
        void Start()
        {
            _image = gameObject.GetComponent<Image>();
            _dialogueConfig = gameObject.GetComponent<DialogueConfig>();

            if (loadSCVatStart)
            {
                try
                {
                    DialogueTable table = new DialogueTable();
                    table.Load(csvFile);
                    if (table.IsLoaded())
                    {
                        DialogueTable.Row nameRow = table.Find_ID(nameID);
                        DialogueTable.Row descRow = table.Find_ID(descriptionID);

                        if (nameRow.FR == "" || descRow.EN == "")
                            Debug.LogWarning("Data evidence may not be good : " + gameObject.name);

                        evidenceData = new EvidenceData(sceneSp, detailSp, nameRow, descRow);
                    }
                    else
                        throw new System.ArgumentException("CSV file could not be loaded " + csvFile.name);
                }
                catch
                {
                    Debug.LogWarning("A evidence data couldn't be loaded : " + gameObject.name);
                    evidenceData = new EvidenceData(sceneSp, detailSp);
                }
            }

            InitSprite(0);
        }

        public void InitSprite(int mode = 0)
        {
            if (_image == null)
                return;

            switch (mode)
            {
                case 0:
                    _image.sprite = evidenceData.sceneSprite;
                    break;
                case 1:
                    _image.sprite = evidenceData.detailSprite;
                    break;
            }
        }

        public void OnPointerDownScene()
        {
            if (unlockedZone != null)
                unlockedZone.isLocked = false;

            Sequence disapearSequence = DOTween.Sequence();

            disapearSequence.Append(transform.DOScale(new Vector3(1.5f, 1.5f, 1.5f), 0.2f));
            disapearSequence.Append(transform.DOScale(new Vector3(0f, 0f, 0f), 0.3f));

            disapearSequence.Play();

            PointAndClickManager.Instance.NewEvidenceFound(this);

            ZoneManager.Instance.HideShowArrows(ZoneManager.Instance.currentZone);

            _dialogueConfig?.StartDialogue();
        }

        public void OnPointerDownInventory()
        {
            PointAndClickManager.Instance.ShowOneEvidenceInfos(this);
        }
    }
}
