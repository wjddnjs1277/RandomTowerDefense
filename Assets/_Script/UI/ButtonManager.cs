using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] GameObject[] buttonWindows;

    Player player;

    private void Start()
    {
        player = Player.Instance;
    }

    public void SwitchWindow()
    {
       switch(player.state)
       {
            case Player.PLAYER_STATE.None:
                ChangeWindow(1);
                break;
            case Player.PLAYER_STATE.Building:
                ChangeWindow(0);
                break;
            case Player.PLAYER_STATE.Combine:
                ChangeWindow(0);
                break;
       }
    }
    public void ChangeWindow(int index)
    {
        for(int i=0; i < buttonWindows.Length;i++)
        {
            buttonWindows[i].SetActive(false);
        }

        buttonWindows[index].SetActive(true);
    }

}
