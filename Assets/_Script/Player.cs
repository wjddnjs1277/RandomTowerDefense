using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Singleton<Player>
{
    [System.Serializable]
    public struct Upgrade
    {
        public int normal;
        public int rare;
        public int epic;

        public int normalCost;
        public int rareCost;
        public int epicCost;
    }
    public enum PLAYER_STATE
    {
        None,       // 아무것도 하지않음
        Building,   // 건설
        Combine,    // 합성
        Upgrade,    // 강화
        Selling,
    }

    const int MaxLife = 20;

    [SerializeField] int life;      // 목숨
    [SerializeField] int money;
    [SerializeField] public Upgrade myUpgrade;

    public int Life
    {
        get
        {
            return life;
        }
        set
        {
            life = Mathf.Clamp(value, 0, 100);
            PlayerText.Instance.ShowLife("LIFE", life.ToString());
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
            PlayerText.Instance.ShowMoney("MONEY", money.ToString());
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
            ButtonManager.Instance.SwitchWindow();
        }
        if(state == PLAYER_STATE.None)
        {
            if (Input.GetKeyDown(KeyCode.B))
            {
                state = PLAYER_STATE.Building;
                ButtonManager.Instance.SwitchWindow();
            }

            if (Input.GetKeyDown(KeyCode.C))
            {
                state = PLAYER_STATE.Combine;
                ButtonManager.Instance.SwitchWindow();
            }

            if (Input.GetKeyDown(KeyCode.U))
            {
                state = PLAYER_STATE.Upgrade;
                ButtonManager.Instance.SwitchWindow();
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                state = PLAYER_STATE.Selling;
                ButtonManager.Instance.SwitchWindow();
            }
        }
    }

    public void ChangeState(int index)
    {
        state = (PLAYER_STATE)index;
    }
}
