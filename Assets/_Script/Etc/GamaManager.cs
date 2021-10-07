using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamaManager : Singleton<GamaManager>
{
    [SerializeField] GameObject GameOverUI;
    [SerializeField] GameObject GameClearUI;
    [SerializeField] Text clearTimeText;

    public bool isGameOver;

    float clearTime;

    private void Start()
    {
        isGameOver = false;
        clearTime = 0f;
        Time.timeScale = 1;
    }

    private void Update()
    {
        if (isGameOver)
            return;

        if (Player.Instance.Life <= 0)
            GameOver();

        clearTime += Time.deltaTime;
    }

    public void GameOver()
    {
        isGameOver = true;
        Time.timeScale = 0;
        GameOverUI.SetActive(true);
    }
    public void GameClear()
    {
        isGameOver = true;
        Time.timeScale = 0;
        ShowClearTime();
        GameClearUI.SetActive(true);
    }
    void ShowClearTime()
    {
        int minute = (int)(clearTime / 60);
        float seconds = clearTime % 60;
        clearTimeText.text = string.Format("CLEAR TIME  {0}:{1:00.00}", minute, seconds);
        clearTimeText.enabled = true;
    }
}
