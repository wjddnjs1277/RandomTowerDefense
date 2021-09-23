using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Damageable : MonoBehaviour
{
    public struct DamageMessage
    {
        public GameObject attacker;
        public string attackerName;
        public float amount;
        public Vector3 contactPoint;
        public string damageEffectName;

        public DamageMessage(Tower tower)
        {
            attacker = tower.gameObject;
            attackerName = tower.name;
            amount = tower.Power;
            contactPoint = Vector3.zero;
            damageEffectName = string.Empty;
        }
    }

    public UnityEvent<DamageMessage> OnDamagedEvent;
    public UnityEvent OnDeadEvent;

    Statable stat;

    private void Start()
    {
        stat = GetComponent<Statable>();
    }
    public void OnDamaged(DamageMessage message)
    {
        if (stat.Hp <= 0)
            return;

        float damage = Mathf.Clamp(message.amount - (stat.defense / 10), 0, 1000);
        stat.Hp -= damage;

        if (EffectManager.Instance != null && !string.IsNullOrEmpty(message.damageEffectName))
        {
            GameObject effect = EffectManager.Instance.GetPool(message.damageEffectName);
            effect.transform.position = message.contactPoint;
        }

        OnDamagedEvent?.Invoke(message);

        if (stat.Hp <= 0)
            OnDead();
    }
    void OnDead()
    {
        gameObject.layer = LayerMask.NameToLayer("Dead");
        OnDeadEvent?.Invoke();
    }
}
