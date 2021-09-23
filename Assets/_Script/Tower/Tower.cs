using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public enum TOWER_TYPE
    {
        Normal,         // 일반
        Multiple,       // 다중
        Fixing,         // 고정
    }
    // 사거리, 목표, 미사일  이펙트 .,...
    // 사거리 안이면 목표 하나를 공격
    // 타겟이 거리를 벗어나면 타겟 다시설정
    [SerializeField] float power;           // 공격량
    [SerializeField] float range;           // 거리
    [SerializeField] float attackRate;      // 공격주기
    [SerializeField] string attackParticleName;
    [SerializeField] LayerMask enemyLayer;

    Transform target;

    float nextAttackTime;

    bool isAttack;          // 공격이 가능한가

    public float Power => power;

    private void Start()
    {
        target = null;
        nextAttackTime = 0;
    }

    private void Update()
    {
        TargetSetting();
        TargetLook();

        if (target != null)
            Attack();
    }

    /* 타겟 없고 라운드가 시작하면 사거리내에 있을경우 타겟설정
     */

    void TargetLook()
    {
        if (target == null)
            return;

        Vector3 direction = target.position - transform.position;

        transform.rotation = Quaternion.LookRotation(direction);
        transform.eulerAngles = new Vector3(0.0f, transform.eulerAngles.y, 0.0f);
    }

    void TargetSetting()
    {
        if(target != null && Vector3.Distance(transform.position, target.position) > range)
        {
            target = null;
        }

        if(target == null)
        {
            Collider[] hits = Physics.OverlapSphere(transform.position, range, enemyLayer);

            if (hits.Length == 0)
                return;

            target = hits[Random.Range(0, hits.Length)].transform;
        }

    }

    void Attack()
    {
        if( nextAttackTime <= Time.time)
        {
            nextAttackTime = Time.time + attackRate;

            Damageable damager = target.GetComponent<Damageable>();
            Vector3 dir = transform.position - target.position;
            dir.Normalize();
            Vector3 contactPos = target.transform.position + Vector3.up;
            Contact(damager, contactPos);
        }
    }
    void Contact(Damageable target, Vector3 contactPoint)
    {
        Damageable.DamageMessage message;

        message.attacker = gameObject;
        message.attackerName = gameObject.name;
        message.contactPoint = contactPoint;
        message.amount = power;
        message.damageEffectName = attackParticleName;

        target.OnDamaged(message);
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
