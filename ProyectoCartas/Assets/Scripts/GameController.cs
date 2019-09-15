using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    [SerializeField] TMP_Text _text;
    [SerializeField] TMP_Text _finalText;
    [SerializeField] List<CardController> _cardsController;
    [SerializeField] GameObject _containerCards;
    [SerializeField] GameObject _containerEnd;

    float _deltaTime = 0.0f;
    int _numberPairs;

    int _currentNumberPars;

    int _currentTaps;
    int _idFirstCard = -1;
    int _idSecondCard = -1;
    bool _finish = false;

    private void Start()
    {
        _numberPairs = _cardsController.Count / 2;
        for (int i = 0; i < _cardsController.Count; i++)
        {
            _cardsController[i].SetCallbackTap(OnCardTap);
        }
    }

    void OnCardTap(int index)
    {
        if(_idFirstCard == -1)
        {
            _idFirstCard = index;
            _cardsController[_idFirstCard].OnFlipCard(OnCardFinished);
            return;
        }

        if(_idSecondCard == -1 && _idFirstCard != index)
        {
            _idSecondCard = index;
            _cardsController[_idSecondCard].OnFlipCard(OnCardFinished);
            for (int i = 0; i < _cardsController.Count; i++)
            {
                _cardsController[i].CanTap(false);
            }
        }
    }

    void OnCardFinished()
    {
        if(_idFirstCard == -1 || _idSecondCard == -1)
        {
            return;
        }

        if(_idFirstCard != -1 && _idSecondCard != -1)
        {
            if(_cardsController[_idFirstCard].IdPair == _cardsController[_idSecondCard].IdPair)
            {
                _currentNumberPars++;
                _cardsController[_idFirstCard].SetFlipped(true);
                _cardsController[_idSecondCard].SetFlipped(true);

                for (int i = 0; i < _cardsController.Count; i++)
                {
                    _cardsController[i].CanTap(true);
                }

                if (_currentNumberPars == _numberPairs)
                {
                    _finish = true;
                    SetFinishCounter();
                }
            }
            else
            {
                _cardsController[_idFirstCard].OnFlipCard(() => OnCardTurnArround(false));
                _cardsController[_idSecondCard].OnFlipCard(() => OnCardTurnArround(true));
            }

            _idFirstCard = -1;
            _idSecondCard = -1;
        }

    }

    void OnCardTurnArround(bool enableTaps)
    {
        if(enableTaps)
        {
            for (int i = 0; i < _cardsController.Count; i++)
            {
                _cardsController[i].CanTap(true);
            }
        }
    }

    void SetFinishCounter()
    {
        _containerCards.SetActive(false);
        _containerEnd.SetActive(true);
        _finalText.text = ((int)_deltaTime / 60).ToString() + "min " + ((int)(_deltaTime % 60)).ToString() + " seg";
    }

    void Update()
    {
        if(_finish)
        {
            return;

        }

        _deltaTime += Time.deltaTime;

        _text.text = ((int)_deltaTime / 60).ToString() + "min " + ((int)(_deltaTime % 60)).ToString() + " seg";
    }
}
