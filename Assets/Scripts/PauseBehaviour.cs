using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseBehaviour : MonoBehaviour
{
    [SerializeField]
    private GameObject _screenBlur;

    [SerializeField]
    private GameObject _pauseButton;

    [SerializeField]
    private DiscBehaviour _discBehaviour;

    [SerializeField]
    private ArrowBehaviour _arrowBehaviour;

    public void ContinueGame()
    {
        _screenBlur.SetActive(false);
        _pauseButton.SetActive(true);
        _discBehaviour.ResumeGame();
        _arrowBehaviour.ResumeGame();
        gameObject.SetActive(false);
    }
}
