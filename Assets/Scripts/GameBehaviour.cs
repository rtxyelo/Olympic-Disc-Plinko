using System.Collections.Generic;
using UnityEngine;

public class GameBehaviour : MonoBehaviour
{
    [SerializeField]
    private GameObject obtaclePrefab;

    //private List<int> levelsScoresList = new List<int>()[10, 3];

    private readonly string currentLevelKey = "CurrentLevel";

    private void Start()
    {
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
                Debug.Log("Obtacle " + obtacle.gameObject.name);
            }
        }
    }
}
