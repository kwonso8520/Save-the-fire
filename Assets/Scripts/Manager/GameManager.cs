using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    private float time;
    public bool isDead = false;
    [Header("Panel")]
    [SerializeField] private GameObject _gameEnd_Panel;

    [Header("GameLogic")]
    [SerializeField] private Text _timeText;
    [SerializeField] private Text _endTimeText;
    [SerializeField] private Text _bestTimeText;



    void Awake()
    {
        if (null == instance)
        {
            instance = this;
        }
    }

    void Start()
    {
        _gameEnd_Panel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDead)
        {
            time += Time.deltaTime;
            _timeText.text = ((int)time).ToString();
        }
    }

    public void GameEnd_Panel_On()
    {
        _gameEnd_Panel.SetActive(true);
        float bestTime = PlayerPrefs.GetFloat("BestTime");
        _endTimeText.text = ((int)time).ToString();
        if (bestTime < time)
        {
            bestTime = time;
            PlayerPrefs.SetFloat("BestTime", bestTime);
        }
        _bestTimeText.text = ((int)bestTime).ToString();
        Time.timeScale = 0;
    }

    public void GameReStart()
    {
        SceneManager.LoadScene("GamePlay");
        Time.timeScale = 1;
    }

    public void GotoMainMenu()
    {
        SceneManager.LoadScene("GameStart");
        Time.timeScale = 1;
    }
}