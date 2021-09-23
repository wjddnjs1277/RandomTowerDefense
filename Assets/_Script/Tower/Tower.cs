using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public enum TOWER_TYPE
    {
        Normal,         // �Ϲ�
        Multiple,       // ����
        Fixing,         // ����
    }
    // ��Ÿ�, ��ǥ, �̻���  ����Ʈ .,...
    // ��Ÿ� ���̸� ��ǥ �ϳ��� ����
    // Ÿ���� �Ÿ��� ����� Ÿ�� �ٽü���
    [SerializeField] float power;           // ���ݷ�
    [SerializeField] float range;           // �Ÿ�
    [SerializeField] float attackRate;      // �����ֱ�
    [SerializeField] string attackParticleName;
    [SerializeField] LayerMask enemyLayer;

    Transform target;

    float nextAttackTime;

    bool isAttack;          // ������ �����Ѱ�

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

    /* Ÿ�� ���� ���尡 �����ϸ� ��Ÿ����� ������� Ÿ�ټ���
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
