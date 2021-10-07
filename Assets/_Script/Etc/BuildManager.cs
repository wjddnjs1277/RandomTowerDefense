using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : Singleton<BuildManager>
{
    [SerializeField] public GameObject preview;
    [SerializeField] Color unableBuildColor;
    [SerializeField] GameObject[] tower;
    [SerializeField] GameObject[] rareTower;
    [SerializeField] GameObject[] epicTower;
    [SerializeField] Transform nodeParent;

    Color origin;
    float offsetY = 0.25f;

    private void Start()
    {
        origin = preview.GetComponent<Renderer>().material.color;
    }

    private void Update()
    {
        preview.SetActive(Player.Instance.state == Player.PLAYER_STATE.Building);
        preview.GetComponent<Renderer>().material.color = Player.Instance.Money >= 20 ? origin : unableBuildColor;
    }

    GameObject GetTower()
    {
        int index = Random.Range(0, tower.Length);

        return tower[index];
    }
    public void Build(Node node)
    {
        GameObject obj = GetTower();
        Vector3 buildPos = new Vector3(node.transform.position.x, offsetY, node.transform.position.z);
        node.towerObj = Instantiate(obj, buildPos, Quaternion.identity);
        node.tower = node.towerObj.GetComponent<Tower>();
        Player.Instance.Money -= 20;
        Player.Instance.ChangeState(0);
        ButtonManager.Instance.SwitchWindow();
    }

    public void SwitchColor(bool isBuild)
    {
        preview.GetComponent<Renderer>().material.color = isBuild ? origin : unableBuildColor; 
    }

    bool IsSell(Node selectNode)
    {
        if (selectNode.tower.rank != Tower.TOWER_RANK.Epic)
            return true;

        return false;
    }
    public void SellTower(Node selectNode)
    {
        int sellPrice = 0;
        if(IsSell(selectNode))
        {
            switch(selectNode.tower.rank)
            {
                case Tower.TOWER_RANK.Normal:
                    sellPrice = 10;
                    break;
                case Tower.TOWER_RANK.Rare:
                    sellPrice = 20;
                    break;
            }
            selectNode.RemoveTower();
            Player.Instance.Money += sellPrice;
            Player.Instance.ChangeState(0);
        }
    }

    bool IsCombine(Node selectNode)
    {
        if (selectNode.tower == null)
            return false;

        if (selectNode.tower.rank == Tower.TOWER_RANK.Epic)
            return false;

        for(int i= 0; i < nodeParent.transform.childCount; i++)
        {
            Node node = nodeParent.GetChild(i).GetComponent<Node>();

            if( node.tower != null)
            {
                if(selectNode!=node && selectNode.towerObj.name == node.towerObj.name)
                {
                    node.RemoveTower();
                    selectNode.RemoveTower();

                    return true;
                }    
            }                
        }

        return false;
    }

    public void Combine(Node node)
    {
        GameObject obj = ConbineTower(node.tower);
        if (IsCombine(node))
        {
            Debug.Log("buildemanager combine");
            Vector3 buildPos = new Vector3(node.transform.position.x, offsetY, node.transform.position.z);
            node.towerObj = Instantiate(obj, buildPos, Quaternion.identity);
            node.tower = node.towerObj.GetComponent<Tower>();
            Player.Instance.state = Player.PLAYER_STATE.None;
            ButtonManager.Instance.SwitchWindow();
        }
    }

    GameObject ConbineTower(Tower tower)
    {
        if (tower == null)
            return null;

        int index;
        switch (tower.rank)
        {
            case Tower.TOWER_RANK.Normal:
                index = Random.Range(0, rareTower.Length);
                return rareTower[index];
            case Tower.TOWER_RANK.Rare:
                index = Random.Range(0, epicTower.Length);
                return epicTower[index];
        }
        return null;
    }
}
