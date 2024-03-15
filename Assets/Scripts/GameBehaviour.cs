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

    //                                                     1  2   3   4   5   6    7    8    9    10   11   12   13   14   15   16   17   18   19   20   21   22   23   24   25   26   27   28   29   30   31   32   33   34   35   36   37   38   39   40   41   42   43   44    45    46    47    48    49    50
    private readonly List<int> levelsScoresList = new() { 10, 30, 40, 50, 60, 80, 100, 120, 130, 150, 180, 200, 220, 250, 280, 300, 340, 350, 380, 400, 420, 450, 480, 500, 530, 550, 580, 600, 620, 650, 680, 700, 720, 750, 780, 800, 820, 850, 880, 900, 920, 950, 980, 1000, 1200, 1500, 1600, 1700, 1800, 2000};

    public int LevelPassScore => levelsScoresList[PlayerPrefs.GetInt(currentLevelKey, 1) - 1];

    private readonly string currentLevelKey = "CurrentLevel";

    private readonly string maxLevelKey = "MaxLevel";

    private DiscBehaviour discBehaviour;

    private ScoreBehaviour scoreBehaviour;

    private Button pauseButton;

    private TMP_Text winScoreText;

    public delegate void GameOverEventHandler();

    public event GameOverEventHandler GameWinEvent;

    public event GameOverEventHandler GameLoseEvent;

    private void Start()
    {
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

            GameWinEvent?.Invoke();

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

            GameLoseEvent?.Invoke();

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
