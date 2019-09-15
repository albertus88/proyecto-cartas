using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class PlayButton : MonoBehaviour
{

    void Start()
    {
        transform.DOScale(1.2f, 1.0f).SetLoops(-1, LoopType.Yoyo);
    }

    bool _pressed = false;
    public void Play()
    {
        if(_pressed)
        {
            return;
        }

        _pressed = true;
        StartCoroutine(OnPressed());
    }

    IEnumerator OnPressed()
    {
        transform.DOPunchScale(0.1f * Vector3.one, 0.5f);
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("GameCards", LoadSceneMode.Single);
    }
}
