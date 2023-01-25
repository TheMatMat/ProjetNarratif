using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace TeamSeven
{

    public class TraductionTxtTMP : MonoBehaviour
    {
        [SerializeField] private string FrTraduction;
        [SerializeField] private string EnTraduction;
        [SerializeField] private Image img;

        [Header("Img settings")]
        [SerializeField] private Sprite francaisSprite;
        [SerializeField] private Sprite englishSprite;

        private TextMeshProUGUI txtButton;

        private void Start()
        {
            txtButton = gameObject.GetComponent<TextMeshProUGUI>();
            GameManager.instance.OnLanguageChange += ChangeTxt;
            ChangeTxt();
        }

        private void OnDisable()
        {
            GameManager.instance.OnLanguageChange -= ChangeTxt;
        }

        private void ChangeTxt()
        {
            switch (GameManager.instance.language)
            {
                case GameManager.LANGUAGE.FR:
                    txtButton.text = FrTraduction;
                    if (img)
                        img.sprite = francaisSprite;
                    break;

                case GameManager.LANGUAGE.EN:
                    txtButton.text = EnTraduction;
                    if (img)
                        img.sprite = englishSprite;
                    break;
            }
        }
    }
}