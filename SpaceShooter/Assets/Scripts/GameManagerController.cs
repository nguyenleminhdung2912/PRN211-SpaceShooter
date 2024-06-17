
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerController : MonoBehaviour
{
    [Header("GameObject")]
    public GameObject playerShip;
    public GameObject gameOverGo;
    public GameObject TimeCounterGO;
    public GameObject ScoreGameUIGo;
    public GameObject AsteroidsSpawnerGO;
    public GameObject EnemySpawnerGO;
    public enum GameManagerState
    {
        Gameplay,
        GameOver
    }

    public GameManagerState GMState;
    // Start is called before the first frame update
    void Start()
    {
        TimeCounterGO.GetComponent<TimeCounter>().StartTimeCounter();
        StartGamePlay();
    }

    // Update is called once per frame
    void UpdateGameManagerState()
    {
        switch (GMState)
        {
            //case GameManagerState.Opening:
            //    gameOverGo.SetActive(false);
            //    playButton.SetActive(true);

            //    break;
            case GameManagerState.Gameplay:
                ScoreGameUIGo.GetComponent<ScoreDisplay>().Score = 0;
                playerShip.GetComponent<PLayerController>().Init();
                TimeCounterGO.GetComponent<TimeCounter>().StartTimeCounter();
                break;
            case GameManagerState.GameOver:
                TimeCounterGO.GetComponent<TimeCounter>().StopTimeCounter();
                AsteroidsSpawnerGO.GetComponent<AsteroidSpawner>().StopSpawning();
                EnemySpawnerGO.GetComponent<EnemySpawnerController>().StopSpawning();
                break;
        }
    }

    public void SetGameManagerState(GameManagerState state)
    {
        GMState = state;
        UpdateGameManagerState();
    }

    public void StartGamePlay()
    {
        GMState = GameManagerState.Gameplay;
        UpdateGameManagerState();
    }
     
}
