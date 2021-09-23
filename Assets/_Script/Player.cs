using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Singleton<Player>
{
    public enum PLAYER_STATE
    {
        None,       // 아무것도 하지않음
        Building,   // 건설
        Combine,    // 합성
    }

    const int MaxLife = 20;

    [SerializeField] int life;      // 목숨
    [SerializeField] int money;

    public int Life
    {
        get
        {
            return life;
        }
        set
        {
            life = Mathf.Clamp(value, 0, 100);
            PlayerText.Instance.ShowText(life, money);
        }
    }
    public int Money
    {
        get
        {
            return money;
        }
        set
        {
            money = Mathf.Clamp(value, 0, 100000);
            PlayerText.Instance.ShowText(life, money);
        }
    }

    public PLAYER_STATE state;
    public bool isBuild
    {
        get
        {
            return money >= 20 && state == PLAYER_STATE.Building;
        }
    }

    private void Start()
    {
        state = PLAYER_STATE.None;

        Life = MaxLife;
        Money = 200;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            state = PLAYER_STATE.None;
        }
    }

    public void ChangeBuildState()
    {
        state = PLAYER_STATE.Building;
    }
    public void ChangeCombineState()
    {
        state = PLAYER_STATE.Combine;
    }
    public void ChangeStateNone()
    {
        if(!isBuild)
            state = PLAYER_STATE.None;
    }
}
