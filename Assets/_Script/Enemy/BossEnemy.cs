using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy : EnemyController
{
    public override void OnDead()
    {
        WaveSpawner.EnemiesAlive -= 1;
        Player.Instance.Money += 100;
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "End")
        {
            WaveSpawner.EnemiesAlive -= 1;
            Player.Instance.Life -= 10;
            Destroy(gameObject);
        }
    }
}
