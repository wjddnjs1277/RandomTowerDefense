using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] float speed;


    Transform wayPointParent;
    Transform target;

    int wayPointIndex;

    private void Start()
    {
        wayPointIndex = 0;

        wayPointParent = GameObject.Find("WayPointParent").transform;

        target = wayPointParent.GetChild(wayPointIndex);
    }

    private void Update()
    {
        Move();
    }


    void Move()
    {
        Vector3 direction = target.position - transform.position;

        transform.rotation = Quaternion.LookRotation(direction);
        transform.eulerAngles = new Vector3(0.0f, transform.eulerAngles.y, 0.0f);

        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        if(Vector3.Distance(transform.position, target.position) < 0.3f)
        {
            NextWayPoint();
        }
    }

    void NextWayPoint()
    {
        wayPointIndex += 1;
        target = wayPointParent.GetChild(wayPointIndex);
    }

    public void OnDamaged()
    {

    }
    public void OnDead()
    {
        WaveSpawner.EnemiesAlive -= 1;
        Player.Instance.Money += 5;
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "End")
        {
            WaveSpawner.EnemiesAlive -= 1;
            Player.Instance.Life -= 1;
            Destroy(gameObject);
        }
    }
}
