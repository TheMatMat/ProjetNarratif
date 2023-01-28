using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TeamSeven
{

    public class ZoneManager : MonoBehaviour
    {
        public List<ZoneController> zones = new List<ZoneController>();
        public ZoneController firstZone;
        public ZoneController currentZone;

        private static ZoneManager instance = null;
        public static ZoneManager Instance => instance;

        public GameObject arrowLeft, arrowRight;
        public Image transitionBlackScreen;
        public Text transitionZoneName;

        private enum TRANSITION_WAY
        {
            PREVIOUS,
            NEXT,
        }

        private void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(this.gameObject);
                return;
            }
            else
            {
                instance = this;
            }
        }

        public void Start()
        {
            currentZone = firstZone;
            foreach (ZoneController zone in zones)
                zone.gameObject.SetActive(false);

            currentZone.gameObject.SetActive(true);
            HideShowArrows(currentZone);
        }

        private void TransitionScreen(string nameToDisplay, TRANSITION_WAY transitionWay)
        {
            Sequence zoneTransitionSequence = DOTween.Sequence();

            float writingTime = nameToDisplay.Length * 0.15f;
            transitionZoneName.text = "";

            zoneTransitionSequence.Append(transitionBlackScreen.DOFade(1.0f, 0.7f));
            zoneTransitionSequence.Append(transitionZoneName.DOFade(1.0f, 0.2f).OnComplete(() => ActivateZone(transitionWay)));
            zoneTransitionSequence.Append(transitionZoneName.DOText(nameToDisplay, writingTime));
            zoneTransitionSequence.AppendInterval(0.5f);
            zoneTransitionSequence.Append(transitionZoneName.DOFade(0.0f, 0.5f));
            zoneTransitionSequence.AppendInterval(0.5f);
            zoneTransitionSequence.Append(transitionBlackScreen.DOFade(0f, 0.7f));

            zoneTransitionSequence.Play();
        }

        private void ActivateZone(TRANSITION_WAY transitionWay)
        {
            transitionZoneName.text = "";

            switch (transitionWay)
            {
                case TRANSITION_WAY.PREVIOUS:
                    currentZone.gameObject.SetActive(false);
                    currentZone.previousZone.gameObject.SetActive(true);
                    currentZone = currentZone.previousZone;
                    break;
                case TRANSITION_WAY.NEXT:
                    currentZone.gameObject.SetActive(false);
                    currentZone.nextZone.gameObject.SetActive(true);
                    currentZone = currentZone.nextZone;
                    break;
            }

            HideShowArrows(currentZone);
        }

        public void HideShowArrows(ZoneController _zone)
        {
            //hide or show arrows
            if (_zone.canGoNext)
                arrowRight.gameObject.SetActive(true);
            else
                arrowRight.gameObject.SetActive(false);

            if (_zone.canGoPrevious)
                arrowLeft.gameObject.SetActive(true);
            else
                arrowLeft.gameObject.SetActive(false);

            if (_zone.nextZone != null && _zone.nextZone.isLocked)
                arrowRight.GetComponent<Image>().DOFade(0.3f, 0.2f);
            else
            {
                arrowRight.GetComponent<Image>().DOFade(1f, 0.2f).OnComplete(
                    () => arrowRight.transform.DOScale(new Vector3(1.5f, 1.5f, 1.5f), 0.3f).SetLoops(6, LoopType.Yoyo)
                    );
            }
                

            if (_zone.previousZone != null && _zone.previousZone.isLocked)
                arrowLeft.GetComponent<Image>().DOFade(0.3f, 0.2f);
            else
            {
                arrowLeft.GetComponent<Image>().DOFade(1f, 0.2f).OnComplete(
                    () => arrowLeft.transform.DOScale(new Vector3(1.5f, 1.5f, 1.5f), 0.3f).SetLoops(6, LoopType.Yoyo)
                    );
            }
                
        }

        public void LoadPreviousZone()
        {
            if (currentZone.previousZone == null)
                return;

            if (currentZone.previousZone.isLocked)
                return;

            TransitionScreen(currentZone.previousZone.zoneName, TRANSITION_WAY.PREVIOUS);
        }

        public void LoadNextZone()
        {
            if (currentZone.nextZone == null)
                return;

            if (currentZone.nextZone.isLocked)
                return;

            TransitionScreen(currentZone.nextZone.zoneName, TRANSITION_WAY.NEXT);
        }
    }
}
