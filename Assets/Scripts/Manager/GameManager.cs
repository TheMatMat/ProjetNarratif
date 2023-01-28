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
        public AnimationCurve audioSettingsCurve;

        public delegate void LanguageListner();
        public LanguageListner OnLanguageChange;


        private const string MUSIC_VOLUME = "MusicVolume";
        private const string SFX_VOLUME = "SfxVolume";
        private const string VOICE_VOLUME = "VoiceVolume";


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

        public void OnMusicVolumeChange(float value)
        {
            if (value == 0)
                audioMixer.SetFloat(MUSIC_VOLUME, 0);
            else
                audioMixer.SetFloat(MUSIC_VOLUME, ParseToDebit20(value));
        }

        public void OnSfxVolumeChange(float value)
        {
            if (value == 0)
                audioMixer.SetFloat(SFX_VOLUME, 0);
            else
                audioMixer.SetFloat(SFX_VOLUME, ParseToDebit20(value));
        }

        public void OnVoiceVolumeChange(float value)
        {
            if (value == 0)
                audioMixer.SetFloat(VOICE_VOLUME, 0);
            else
                audioMixer.SetFloat(VOICE_VOLUME, ParseToDebit20(value));
        }

        private float ParseToDebit20(float value)
        {
            float parse = Mathf.Lerp(-80, 20, audioSettingsCurve.Evaluate(Mathf.Clamp01(value)));
            return parse;
        }
    }
}