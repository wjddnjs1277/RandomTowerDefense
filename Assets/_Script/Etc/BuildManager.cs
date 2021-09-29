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
        preview.SetActive(Player.Instance.isBuild);
    }

    GameObject GetTower()
    {
        int index = Random.Range(0, tower.Length);

        return tower[index];
    }
    public void Build(Node node)
    {
        GameObject obj = GetTower();
        node.tower = obj.GetComponent<Tower>();
        Vector3 buildPos = new Vector3(node.transform.position.x, offsetY, node.transform.position.z);
        node.towerObj = Instantiate(obj, buildPos, Quaternion.identity);
        Player.Instance.Money -= 20;
        Player.Instance.ChangeStateNone();
    }

    public void SwitchColor(bool isBuild)
    {
        preview.GetComponent<Renderer>().material.color = isBuild ? origin : unableBuildColor; 
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
                if(selectNode!=node && selectNode.tower == node.tower)
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
            node.tower = obj.GetComponent<Tower>();
            Vector3 buildPos = new Vector3(node.transform.position.x, offsetY, node.transform.position.z);
            node.towerObj = Instantiate(obj, buildPos, Quaternion.identity);
            Player.Instance.state = Player.PLAYER_STATE.None;
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
