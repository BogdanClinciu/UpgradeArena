using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    protected float baseHp = 10;
    [SerializeField]
    protected float baseDamage = 10;
    [SerializeField]
    protected float baseDificulty = 1;
    [SerializeField]
    protected float movementSpeed = 0.01f;
    [SerializeField]
    protected float attackRange = 2.5f;
    [SerializeField]
    protected float attackSpeed = 2.0f;

    [Space]
    [SerializeField]
    protected MeshRenderer baseRenderer;
    [SerializeField]
    protected Animator baseAnimator;
    [SerializeField]
    protected AudioSource baseAttackSound;
    [SerializeField]
    protected AudioSource baseDieSound;
}
