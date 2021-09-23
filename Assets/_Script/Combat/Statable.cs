using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Statable : MonoBehaviour
{
    public float maxHp;
    public float defense;

    public event System.Action OnDeadEvent;

    new Collider collider;

    float hp;

    public float Hp
    {
        get
        {
            return hp;
        }
        set
        {
            hp = Mathf.Clamp(value, 0, maxHp);
        }
    }
    public float Defense => defense;

    private void Start()
    {
        collider = GetComponent<Collider>();

        hp = maxHp;
    }

    void OnDead()
    {
        collider.enabled = false;
        Destroy(gameObject);
    }
}
