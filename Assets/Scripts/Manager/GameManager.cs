using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamSeven
{

    public class GameManager : MonoBehaviour
    {
        public static GameManager instance { get; private set; }

        public LANGUAGE language { get; private set; }

        public delegate void LanguageListner();
        public LanguageListner OnLanguageChange;

        public enum LANGUAGE
        {
            FR,
            EN
        }

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

        public void ChangeLanguage()
        {
            language = (LANGUAGE)((language.GetHashCode() + 1) % 2);
            OnLanguageChange?.Invoke();
        }
    }
}