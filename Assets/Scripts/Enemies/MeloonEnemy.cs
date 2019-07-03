using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class MeloonEnemy : Enemy, IEnemy
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
    private Material materialInstance;
    private bool isDead = false;
    private bool isAttacking = false;


    private void Start()
    {
        materialInstance = new Material(baseRenderer.sharedMaterial);
        baseRenderer.material = materialInstance;
    }

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
        isDead = true;
        StartCoroutine(Dissolve());
    }

    private IEnumerator Dissolve()
    {
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime)
        {
            materialInstance.SetFloat("Vector1_D197B0F1", t);
            yield return null;
        }
        Destroy(gameObject);
    }

    public void Move()
    {
        if(!isAttacking)
        {
            if (Vector3.Distance(transform.position, Vector3.zero) > attackRange)
            {
                lookTarget.y = transform.position.y;
                transform.LookAt(lookTarget, Vector3.up);
                transform.Translate(0,0,movementSpeed, Space.Self);
            }
            else if(!isDead)
            {
                isAttacking = true;
                Attack();
            }
        }
        else
        {
            if(lastAttackTime + attackSpeed < Time.time)
            {
                Attack();
            }
            else if (Vector3.Distance(transform.position, Vector3.zero) > attackRange && lastAttackTime + attackSpeed/2 < Time.time)
            {
                lookTarget = Vector3.zero;
                lookTarget.y = transform.position.y;
                transform.LookAt(lookTarget, Vector3.up);
                transform.Translate(0,0,movementSpeed, Space.Self);
            }
            else
            {
                lookTarget = transform.position * 2;
                lookTarget.y = transform.position.y;
                transform.LookAt(lookTarget, Vector3.up);
                transform.Translate(0,0,movementSpeed, Space.Self);
            }
        }
    }


    public void Attack()
    {

        lastAttackTime = Time.time;
        GameManager.Instance.DamagePlayer(baseDamage);
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
