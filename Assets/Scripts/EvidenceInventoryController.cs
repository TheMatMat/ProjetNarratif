using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvidenceInventoryController : MonoBehaviour
{
    public IndiceData data;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Appear()
    {
        transform.localScale = Vector3.zero;

        Sequence appearSequence = DOTween.Sequence();

        appearSequence.Append(transform.DOScale(new Vector3(1.5f, 1.5f, 1.5f), 0.3f));
        appearSequence.Append(transform.DOScale(new Vector3(1f, 1f, 1f), 0.1f));

        appearSequence.Play();
    }
}
