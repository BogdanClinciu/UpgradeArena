using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float enableTime;

    private void OnEnable()
    {
        enableTime = Time.fixedTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
            other.GetComponentInParent<IEnemy>().Damage(GameManager.Instance.Damage);
        }

        gameObject.SetActive(false);
        transform.position = Vector3.zero;
    }

    private void FixedUpdate()
    {
        if(gameObject.activeInHierarchy)
        {
            transform.Translate(0, 0, GameManager.Instance.BulletSpeed, Space.Self);
            if(Time.fixedTime - enableTime > 2f)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
