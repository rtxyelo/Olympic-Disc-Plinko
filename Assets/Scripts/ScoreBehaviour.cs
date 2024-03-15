using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBehaviour : MonoBehaviour
{
    [SerializeField]
    private Image scoreFillImage;

    private TMP_Text scoreText;

    private Animator scoreTextAnimator;

    private DiscBehaviour discBehaviour;
    
    private GameBehaviour gameBehaviour;

    private DiscMultipleTrigger discMultipleTrigger;

    public int FinalScore => discBehaviour.GameScore * discMultipleTrigger.MultipleValue;

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

        gameBehaviour = FindObjectOfType<GameBehaviour>();
        scoreFillImage.fillAmount = 0f;
    }

    private void ScoreChange()
    {
        scoreTextAnimator.Play("ScoreChange");
        scoreText.text = discBehaviour.GameScore.ToString();

        float currentScore = discBehaviour.GameScore / (float)gameBehaviour.LevelPassScore;
        scoreFillImage.fillAmount = currentScore < 1f ? currentScore : 1f;

    }

    private void GameOver()
    {
        Debug.Log("Game Over (in score script):" +
                    " multiple value is " + discMultipleTrigger.MultipleValue +
                    " game score is " + discBehaviour.GameScore +
                    " RESULT SCORE " + discBehaviour.GameScore * discMultipleTrigger.MultipleValue);
        scoreTextAnimator.Play("ScoreChange");
        scoreText.text = FinalScore.ToString();

        float currentScore = FinalScore / (float)gameBehaviour.LevelPassScore;
        scoreFillImage.fillAmount = currentScore < 1f ? currentScore : 1f;
    }

    private void OnDestroy()
    {
        discBehaviour.GameOverEvent -= GameOver;
    }
}
