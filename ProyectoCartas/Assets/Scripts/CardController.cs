using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;

[System.Serializable]
public class PairCardController
{
    [SerializeField] public int _cardFirst;
    [SerializeField] public int _cardSecond;
}


public class CardController : MonoBehaviour
{
    [SerializeField] int _id;
    [SerializeField] public int IdPair;
    [SerializeField] GameObject _iconBack;
    [SerializeField] GameObject _iconFront;

    bool _canTap = true;
    Action<int> _callbackTap;
    bool _flipped = false;

    void Start()
    {
        _iconFront.SetActive(false);
    }

    public void Tap()
    {
        if(!_canTap || _flipped)
        {
            return;
        }

        _callbackTap?.Invoke(_id);
    }

    public void SetFlipped(bool flipped)
    {
        _flipped = flipped;
    }

    public void CanTap(bool enabled)
    {
        _canTap = enabled;
    }

    public void OnFlipCard(Action onCardFinished)
    {
        transform.DORotateQuaternion(Quaternion.Euler(0, 180, 0), 0.5f).OnComplete(new TweenCallback(onCardFinished));
        StartCoroutine(OnFlipHalf());
    }

    IEnumerator OnFlipHalf()
    {
        yield return new WaitForSeconds(0.25f);
        _iconBack.SetActive(!_iconBack.activeSelf);
        _iconFront.SetActive(!_iconFront.activeSelf);
    }

    public void SetCallbackTap(Action<int> callbackTap)
    {
        _callbackTap = callbackTap;
    }
}
