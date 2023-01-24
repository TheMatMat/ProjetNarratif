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
    }

    private void OnDisable()
    {
        GameManager.instance.OnLanguageChange -= ChangeTxt;
    }

    private void ChangeTxt()
    {
        switch (GameManager.instance.language)
        {
            case 0:
                txtButton.text = FrTraduction;
                break;
            case 1:
                txtButton.text = EnTraduction;
                break;
        }
    }
}
