using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : Singleton<ButtonManager>
{
    [SerializeField] GameObject[] buttonWindows;
    [SerializeField] public GameObject GuideObj;
    [SerializeField] Text title;
    [SerializeField] Text content;

    Player player;


    private void Start()
    {
        player = Player.Instance;

        GuideObj.SetActive(false);
        title.text = string.Empty;
        content.text = string.Empty;
    }

    public void ShowGuide(string title, string content)
    {
        GuideObj.SetActive(true);
        this.title.text = title;
        this.content.text = content;
    }
    public void CloseGuide()
    {
        GuideObj.SetActive(false);
        title.text = string.Empty;
        content.text = string.Empty;
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
            case Player.PLAYER_STATE.Upgrade:
                ChangeWindow(2);
                break;
            case Player.PLAYER_STATE.Selling:
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
