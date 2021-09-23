using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerText : Singleton<PlayerText>
{
    [SerializeField] Text playerLife;
    [SerializeField] Text playerMoney;


    public void ShowText(int life, int money)
    {
        playerLife.text = string.Format("Life : {0}", life);
        playerMoney.text = string.Format("Money : {0}", money);
    }
}
