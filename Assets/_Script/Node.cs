using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    enum State
    {
        None,
        Build,
    }

    [SerializeField] Color selectColor;

    BuildManager buildManager;
    Tower tower;
    GameObject towerObj;

    new Renderer renderer;
    Color origin;

    float offsetY = 0.25f;
    public Tower Tower => tower;
    bool isBuildTower
    {
        get
        {
            return tower == null && Player.Instance.isBuild == true;
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
        renderer.material.color = tower == null ? origin : selectColor;

       
    }

    public void Combine()
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
            GameObject obj = buildManager.BuildTower();
            tower = obj.GetComponent<Tower>();
            Vector3 buildPos = new Vector3(transform.position.x, offsetY, transform.position.z);
            towerObj = Instantiate(obj, buildPos, Quaternion.identity);
            Player.Instance.Money -= 20;
            Player.Instance.ChangeStateNone();
        }
        
        if(Player.Instance.state == Player.PLAYER_STATE.Combine)
        {
            buildManager.Combine(this);
            Player.Instance.state = Player.PLAYER_STATE.None;
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
