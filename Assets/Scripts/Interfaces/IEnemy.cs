using UnityEngine;
using UnityEngine.Events;

public interface IEnemy
{
    Vector3 position { get; }
    UnityAction OnKill { set;}

    void SetMultiliers(float hp = 1, float dmg = 1, float speed = 1, float attackRange = 1);
    void Move();
    void Attack();
    void Damage(float ammount);
    void Kill();
}
