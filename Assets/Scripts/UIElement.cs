using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace TeamSeven
{

    public class UIElement : MonoBehaviour
    {
        [SerializeField] private float fadeTime = 0.8f;

        private CanvasGroup canvasGroup;

        private void Start()
        {
            canvasGroup = gameObject.GetComponent<CanvasGroup>();
        }

        public void EnableElement(bool enable)
        {
            if (enable)
            {
                canvasGroup.interactable = true;
                canvasGroup.blocksRaycasts = true;
                canvasGroup.DOFade(1, fadeTime);
            }
            else
            {
                canvasGroup.DOFade(0, fadeTime).OnComplete(CompleteFade);
            }
        }

        public void CompleteFade()
        {
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
        }
    }
}
