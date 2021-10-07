using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeManager : MonoBehaviour
{
    [SerializeField] int basicCost;     // 기본 비용
    [SerializeField] int AddCost;       // 추가 비용

    [SerializeField] Text[] upgradeText;

    Player player;

    private void Start()
    {
        player = Player.Instance;

        setup();

    }

    void setup()
    {
        player.myUpgrade.normalCost = basicCost;
        player.myUpgrade.rareCost = basicCost;
        player.myUpgrade.epicCost = basicCost;

        upgradeText[0].text = player.myUpgrade.normalCost.ToString();
        upgradeText[1].text = player.myUpgrade.rareCost.ToString();
        upgradeText[2].text = player.myUpgrade.epicCost.ToString();
    }

    public void Upgrade(int index)
    {
        switch(index)
        {
            case 0:
                if (player.Money < player.myUpgrade.normalCost)
                    return;
                player.myUpgrade.normal++;
                player.Money -= player.myUpgrade.normalCost;
                player.myUpgrade.normalCost += AddCost;
                upgradeText[index].text = player.myUpgrade.normalCost.ToString();
                break;
            case 1:
                if (player.Money < player.myUpgrade.rare)
                    return;
                player.myUpgrade.rare++;
                player.Money -= player.myUpgrade.rareCost;
                player.myUpgrade.rareCost += AddCost;
                upgradeText[index].text = player.myUpgrade.rareCost.ToString();
                break;
            case 2:
                if (player.Money < player.myUpgrade.epicCost)
                    return;
                player.myUpgrade.epic++;
                player.Money -= player.myUpgrade.epicCost;
                player.myUpgrade.epicCost += AddCost;
                upgradeText[index].text = player.myUpgrade.epicCost.ToString();
                break;
        }
    }

}
