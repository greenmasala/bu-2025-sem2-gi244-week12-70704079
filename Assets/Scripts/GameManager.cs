// notice ... List class requires System.Collections.Generic namespace
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Unity.VisualScripting;

public class GameManager : MonoBehaviour
{
    public List<GameObject> targets;

    [Header("UI Elements")]
    // NOTE: TextMeshProUGUI requires "using TMPro"
    public TextMeshProUGUI scoreText;
    // NOTE: TextMeshProUGUI requires "using TMPro"
    public TextMeshProUGUI gameOverText;
    public Button restartButton;

    public GameObject titleScreen;
    public GameObject gameOverScreen;
    public Button easyButton;
    public Button mediumButton;
    public Button hardButton;

    private int score;
    private bool isGameActive = true;
    private float spawnRate = 1.0f;

    void Awake()
    {

    }

    void Start()
    {
        StartGame();
    }

    void StartGame()
    {
        StartCoroutine(SpawnTargets());
        scoreText.text = "Score: " + score;
    }

    public void UpdateScore(int score)
    {
        this.score += score; //use this to call for variable announced for the entire script
        scoreText.text = "Score: " + this.score;
    }

    IEnumerator SpawnTargets()
    {
        while (true)
        {
            int index = Random.Range(0, targets.Count);
            var prefab = targets[index];
            Instantiate(prefab);
            yield return new WaitForSeconds(spawnRate);
        }
    }
}

