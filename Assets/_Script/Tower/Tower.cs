using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public enum TOWER_RANK
    {
        Normal,
        Rare,
        Epic,
    }

    [SerializeField] float power;           // 공격량
    [SerializeField] float range;           // 거리
    [SerializeField] float attackRate;      // 공격주기
    [SerializeField] int upgradeCount;
    [SerializeField] float addPower;
    [SerializeField] string attackParticleName;
    [SerializeField] LayerMask enemyLayer;

    Transform target;
    public TOWER_RANK rank;


    float nextAttackTime;

    public float Power => power;
    public float Range => range;
    public float AttackRate => attackRate;
    public int Upgrade => upgradeCount;
    public float AddPower => addPower;

    private void Start()
    {
        target = null;
        nextAttackTime = 0;
    }

    private void Update()
    {
        UpdateTower();
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
        if (target.GetComponent<Statable>().Hp == 0)
            return;

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
        message.amount = power + (upgradeCount * addPower);
        message.damageEffectName = attackParticleName;

        target.OnDamaged(message);
    }

    public void UpdateTower()
    {
        switch (rank)
        {
            case TOWER_RANK.Normal:
                upgradeCount = Player.Instance.myUpgrade.normal;
                break;
            case TOWER_RANK.Rare:
                upgradeCount = Player.Instance.myUpgrade.rare;
                break;
            case TOWER_RANK.Epic:
                upgradeCount = Player.Instance.myUpgrade.epic;
                break;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
