using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }

    public int language { get; private set; }

    public delegate void LanguageListner();
    public LanguageListner OnLanguageChange;

    private void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    private void Start()
    {
        language = 0;
        OnLanguageChange?.Invoke();
    }

    public void ChangeLanguage()
    {
        language = (language + 1) % 1;
        OnLanguageChange?.Invoke();
    }
}
