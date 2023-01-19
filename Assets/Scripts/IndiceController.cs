using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IndiceController : MonoBehaviour
{
    public IndiceDataBase indiceDB;
    public IndiceData data;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Image>().sprite = data.sceneSprite;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerDown()
    {
        Debug.Log("indice has been found: " + data.name);

        Sequence disapearSequence = DOTween.Sequence(); 

        disapearSequence.Append(transform.DOScale(new Vector3(1.5f, 1.5f, 1.5f), 0.2f));
        disapearSequence.Append(transform.DOScale(new Vector3(0f, 0f, 0f), 0.3f));

        disapearSequence.Play();

        PointAndClickManager.Instance.NewEvidenceFound(data);
    }
}
