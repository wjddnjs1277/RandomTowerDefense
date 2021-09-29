using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{

    [SerializeField] Color[] rankColors;

    BuildManager buildManager;
    public Tower tower;
    public GameObject towerObj;

    new Renderer renderer;
    Color origin;

    bool isBuildTower
    {
        get
        {
            return tower == null && Player.Instance.isBuild == true;
        }
    }
    bool isCombineTower
    {
        get
        {
            return tower != null && Player.Instance.state == Player.PLAYER_STATE.Combine;
        }
    }

    void Start()
    {
        buildManager = BuildManager.Instance;
        renderer = GetComponent<Renderer>();
        origin = renderer.material.color;
    }
    private void Update()
    {
        SwitchColor();

    }

    void SwitchColor()
    {
        int index = 0;

        if (tower != null)
        {
            Tower.TOWER_RANK rank = tower.GetComponent<Tower>().rank;

            switch (rank)
            {
                case Tower.TOWER_RANK.Normal:
                    index = 0;
                    break;
                case Tower.TOWER_RANK.Rare:
                    index = 1;
                    break;
                case Tower.TOWER_RANK.Epic:
                    index = 2;
                    break;
            }
        }

        renderer.material.color = tower == null ? origin : rankColors[index];
    }

    public void RemoveTower()
    {
        tower = null;
        Destroy(towerObj);
    }

    private void OnMouseEnter()
    {
        Vector3 buildPos = new Vector3(transform.position.x, 0.75f, transform.position.z);
        buildManager.preview.transform.position = buildPos;
    }

    private void OnMouseDown()
    {
        if(isBuildTower)
        {
            buildManager.Build(this);
        }
        
        if(isCombineTower)
        {
            buildManager.Combine(this);
        }
    }

    private void OnMouseOver()
    {
        if (tower == null)
        {
            buildManager.SwitchColor(true);
        }
        else
        {
            buildManager.SwitchColor(false);
        }
    }

    private void OnMouseExit()
    {

    }
}
