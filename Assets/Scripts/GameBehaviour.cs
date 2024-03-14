using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameBehaviour : MonoBehaviour
{
    [SerializeField]
    private GameObject obtaclePrefab;

    [SerializeField]
    private GameObject winPanel;
    
    [SerializeField]
    private GameObject losePanel;

    [SerializeField]
    private GameObject screenBlur;

    [SerializeField]
    private TMP_Text pointsInfoText;

    private readonly List<int> levelsScoresList = new() { 10, 30, 40, 50, 60, 80, 100, 150, 180, 200 };

    public int LevelPassScore => levelsScoresList[PlayerPrefs.GetInt(currentLevelKey, 1) - 1];

    private readonly string currentLevelKey = "CurrentLevel";

    private readonly string maxLevelKey = "MaxLevel";

    private DiscBehaviour discBehaviour;

    private ScoreBehaviour scoreBehaviour;

    private Button pauseButton;

    private TMP_Text winScoreText;
    
    private void Start()
    {
        Debug.Log("Level pass score " + LevelPassScore);
        pointsInfoText.text = "Collect " + LevelPassScore.ToString() + " points to pass level.";
        var obtaclesClonesObj = GameObject.Find("ObtaclesClones");
        var lvl = PlayerPrefs.GetInt(currentLevelKey, 1);
        var levelObj = GameObject.Find($"Level {lvl}");
        Transform[] levelObtaclesObj = levelObj.GetComponentsInChildren<Transform>();
        foreach (var obtacle in levelObtaclesObj)
        {
            if (obtacle.gameObject != levelObj)
            {
                var obtc = Instantiate(obtaclePrefab, obtacle.transform.position, obtacle.transform.rotation);
                obtc.transform.SetParent(obtaclesClonesObj.transform);
            }
        }

        pauseButton = GameObject.Find("PauseButton").GetComponent<Button>();

        scoreBehaviour = FindObjectOfType<ScoreBehaviour>();

        winScoreText = winPanel.transform.GetChild(1).GetChild(1).GetComponent<TMP_Text>();

        discBehaviour = FindObjectOfType<DiscBehaviour>();
        if (discBehaviour != null)
        {
            discBehaviour.GameOverEvent += GameOver;
        }
    }

    private void GameOver()
    {
        if (scoreBehaviour.FinalScore >= LevelPassScore)
        {
            Debug.Log("Game win");

            if (!winPanel.activeSelf)
            {
                if (PlayerPrefs.GetInt(currentLevelKey, 1) + 1 > PlayerPrefs.GetInt(maxLevelKey, 1))
                {
                    PlayerPrefs.SetInt(maxLevelKey, PlayerPrefs.GetInt(maxLevelKey, 1) + 1);
                    Debug.Log("Max Level Key " + PlayerPrefs.GetInt(maxLevelKey, 0));
                }
                pauseButton.enabled = false;
                screenBlur.SetActive(true);
                winScoreText.text = "Score: " + scoreBehaviour.FinalScore.ToString();
                winPanel.SetActive(true);
            }
        }
        else
        {
            Debug.Log("Game lose");

            if (!losePanel.activeSelf)
            {
                pauseButton.enabled = false;
                screenBlur.SetActive(true);
                losePanel.SetActive(true);
            }
        }
    }

    public void PlayNextLevel()
    {
        if (PlayerPrefs.GetInt(currentLevelKey, 1) != 50)
            PlayerPrefs.SetInt(currentLevelKey, PlayerPrefs.GetInt(currentLevelKey, 1) + 1);
        else
            PlayerPrefs.SetInt(currentLevelKey, 1);
    }

    private void OnDestroy()
    {
        discBehaviour.GameOverEvent -= GameOver;
    }
}
