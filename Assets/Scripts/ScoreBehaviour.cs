using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreBehaviour : MonoBehaviour
{
    private TMP_Text scoreText;

    private Animator scoreTextAnimator;

    private DiscBehaviour discBehaviour;

    private DiscMultipleTrigger discMultipleTrigger;

    private void Start()
    {
        var gameScoreObj = GameObject.Find("GameScore");
        scoreTextAnimator = gameScoreObj.GetComponent<Animator>();
        scoreText = gameScoreObj.GetComponent<TMP_Text>();
        scoreText.text = "0";
        discBehaviour = FindObjectOfType<DiscBehaviour>();
        discMultipleTrigger = discBehaviour.transform.GetChild(0).GetComponent<DiscMultipleTrigger>();

        if (discBehaviour != null )
        {
            discBehaviour.GameOverEvent += GameOver;
            discBehaviour.GameScoreEvent += ScoreChange;
        }
    }

    private void ScoreChange()
    {
        scoreTextAnimator.Play("ScoreChange");
        scoreText.text = discBehaviour.GameScore.ToString();
    }

    private void GameOver()
    {
        Debug.Log("Game Over (in score script):" +
                    " multiple value is " + discMultipleTrigger.MultipleValue +
                    " game score is " + discBehaviour.GameScore +
                    " RESULT SCORE " + discBehaviour.GameScore * discMultipleTrigger.MultipleValue);
        scoreTextAnimator.Play("ScoreChange");
        scoreText.text = (discBehaviour.GameScore * discMultipleTrigger.MultipleValue).ToString();
    }

    private void OnDestroy()
    {
        discBehaviour.GameOverEvent -= GameOver;
    }
}
