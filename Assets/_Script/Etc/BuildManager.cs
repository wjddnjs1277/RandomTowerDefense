using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : Singleton<BuildManager>
{
    [SerializeField] public GameObject preview;
    [SerializeField] Color unableBuildColor;
    [SerializeField] GameObject[] tower;
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

    public void Combine(Node selectNode)
    {
        for(int i= 0; i < nodeParent.transform.childCount; i++)
        {
            Node node = nodeParent.GetChild(i).GetComponent<Node>();

            if( node.Tower != null)
            {
                if(selectNode!=node && selectNode.Tower == node.Tower)
                {
                    node.Combine();
                    selectNode.Combine();
                    return;
                }    
            }                
        }
    }
}
