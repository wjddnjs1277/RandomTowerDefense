using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerText : Singleton<PlayerText>
{
    [SerializeField] InfoText playerLife;
    [SerializeField] InfoText playerMoney;

    public void ShowLife(string name, string value)
    {
        playerLife.Show(name, value);
    }
    public void ShowMoney(string name, string value)
    {
        playerMoney.Show(name, value);
    }
}
