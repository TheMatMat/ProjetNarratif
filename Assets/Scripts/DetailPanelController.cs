using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DetailPanelController : MonoBehaviour
{

    public Image blackBG;
    public Image detailSprite;

    public Transform endPos, startPosUp, startPosRight;

    public bool isFading = false;

    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.SetActive(false);
        detailSprite.transform.position = startPosUp.transform.position;
    }

    public void FadeIn(Sprite spriteToDisplay)
    {
        if(isFading)
            return;

        isFading = true;

        detailSprite.sprite = spriteToDisplay;

        this.gameObject.SetActive(true);

        blackBG.DOFade(0.8f, 0.2f);

        detailSprite.DOFade(1.0f, 0.8f);
        detailSprite.gameObject.transform.DOMove(endPos.transform.position, 0.8f).OnComplete(() => SetState(true));
    }

    public void FadeOut(bool isFirstTime = true)
    {
        if (isFading)
            return;

        isFading = true;

        blackBG.DOFade(0f, 0.2f);

        detailSprite.DOFade(0f, 0.4f);

        detailSprite.gameObject.transform.DOMove(startPosUp.transform.position, 0.8f).OnComplete(() => SetState(false));
    }

    public void SetState(bool newState)
    {
        isFading = false;
        this.gameObject.SetActive(newState);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
