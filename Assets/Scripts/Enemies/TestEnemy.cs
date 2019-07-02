using UnityEngine;
using UnityEngine.Events;

public class TestEnemy : Enemy, IEnemy
{

    public Vector3 position => transform.position;
    private Vector3 lookTarget = Vector3.zero;

    public UnityAction onKill;
    public UnityAction OnKill
    {
        set
        {
            onKill = value;
        }
    }

    private float lastAttackTime = 0;


    private void Update()
    {
        Move();
    }

    public void Damage(float ammount)
    {
        baseHp -= ammount;
        if(baseHp <= 0)
        {
            Kill();
        }
    }

    public void Kill()
    {
        Destroy(gameObject);
    }

    public void Move()
    {
        if (Vector3.Distance(transform.position, Vector3.zero) > attackRange)
        {
            lookTarget.y = transform.position.y;
            transform.LookAt(lookTarget, Vector3.up);
            transform.Translate(0,0,movementSpeed, Space.Self);
        }
        else
        {
            Attack();
        }
    }


    public void Attack()
    {
        if(lastAttackTime + attackSpeed < Time.time)
        {
            baseAnimator.SetTrigger("Attack");
            lastAttackTime = Time.time;
            GameManager.Instance.DamagePlayer(baseDamage);
        }
    }

    private void OnDestroy()
    {
        onKill?.Invoke();
        GameManager.Instance.Score += baseDificulty;
    }

    public void SetMultiliers(float hpMul, float dmgMul, float speedMul, float rangeMul)
    {
        baseHp *= hpMul;
        baseDamage *= dmgMul;
        movementSpeed *= speedMul;
        attackRange *= rangeMul;
    }
}
