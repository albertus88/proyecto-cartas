using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CardAnimation : MonoBehaviour
{
    [SerializeField] float _delay = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        transform.DOLocalMoveY(20.0f, 1.0f).SetLoops(-1, LoopType.Yoyo).SetDelay(_delay);       
    }
}
