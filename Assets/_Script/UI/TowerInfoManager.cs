using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerInfoManager : Singleton<TowerInfoManager>
{
    [SerializeField] InfoText prefab;
    [SerializeField] GameObject parent;
    [SerializeField] int count;
    [SerializeField] public Tower tower;

    InfoText[] texts;

    private void Start()
    {
        texts = new InfoText[count];
        for (int i = 0; i < count; i++)
        {
            texts[i] = Instantiate(prefab, parent.transform);
        }
        Switch(false);
    }

    private void Update()
    {
        if (parent.activeSelf == true)
            ShowTower(tower);
    }

    public void ShowTower(Tower tower)
    {
        texts[0].Show("Name", tower.name);
        texts[1].Show("Rank", tower.rank.ToString());
        texts[2].Show("Power", tower.Power.ToString());
        texts[3].Show("Range", tower.Range.ToString());
        texts[4].Show("AttackRate", tower.AttackRate.ToString());
        texts[5].Show("Upgrade", tower.Upgrade.ToString());
        texts[6].Show("Attack", (tower.Power + (tower.Upgrade * tower.AddPower)).ToString());
    }

    public void Switch(bool isOn)
    {
        parent.SetActive(isOn);
    }
}
