using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TxtButtonMenu : MonoBehaviour
{
    [SerializeField] private string FrTraduction;
    [SerializeField] private string EnTraduction;

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
                break;
            case GameManager.LANGUAGE.EN:
                txtButton.text = EnTraduction;
                break;
        }
    }
}