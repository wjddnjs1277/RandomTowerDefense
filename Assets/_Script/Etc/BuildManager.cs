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

    private void Start()
    {
        origin = preview.GetComponent<Renderer>().material.color;
    }

    private void Update()
    {
        preview.SetActive(Player.Instance.isBuild);
    }

    public GameObject BuildTower()
    {
        int index = Random.Range(0, tower.Length);

        return tower[index];
    }
    public void SwitchColor(bool isBuild)
    {
        preview.GetComponent<Renderer>().material.color = isBuild ? origin : unableBuildColor; 
    }

    public bool Combine(Node selectNode)
    {
        if (selectNode.Tower == null)
            return false;

        if (selectNode.Tower.rank == Tower.TOWER_RANK.Epic)
            return false;

        for(int i= 0; i < nodeParent.transform.childCount; i++)
        {
            Node node = nodeParent.GetChild(i).GetComponent<Node>();

            if( node.Tower != null)
            {
                if(selectNode!=node && selectNode.Tower == node.Tower)
                {
                    node.RemoveTower();
                    selectNode.RemoveTower();



                    return true;
                }    
            }                
        }

        return false;
    }

    public GameObject ConbineTower(Tower tower)
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
