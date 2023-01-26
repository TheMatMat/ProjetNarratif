using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace TeamSeven
{

    public class GameManager : MonoBehaviour
    {
        public static GameManager instance { get; private set; }

        public LANGUAGE language { get; private set; }

        [Header("Audio Settings")]
        public AudioMixer audioMixer;

        public delegate void LanguageListner();
        public LanguageListner OnLanguageChange;

        [System.Serializable]
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

        private void Start()
        {
            GoToSnapshot("MenuSnapshot", 0.8f);
        }

        public void ChangeLanguage()
        {
            language = (LANGUAGE)((language.GetHashCode() + 1) % 2);
            OnLanguageChange?.Invoke();
        }

        public void GoToSnapshot(string name, float time = 2)
        {
            audioMixer.FindSnapshot(name)?.TransitionTo(time);
        }
    }
}