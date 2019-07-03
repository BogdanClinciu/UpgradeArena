using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource shootSound;
    [SerializeField]
    private ParticleSystem muzzleFlash;
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private PlayerManager player;
    [SerializeField]
    private BulletPool bulletPool;

    [Space]
    [SerializeField]
    private float basedamage = 1.0f;
    [SerializeField]
    private float basefireRate = 1.0f;
    [SerializeField]
    private float basespread = 1.0f;
    [SerializeField]
    private float basebulletSize = 1.0f;
    [SerializeField]
    private float basebulletSpeed = 1.0f;


    private float lastShotTime = 0.0f;


    public void Shoot()
    {
        if(Time.fixedTime >= lastShotTime + basefireRate / GameManager.Instance.FireRate)
        {
            shootSound.Play();
            animator.Play("PistolFire",0);
            lastShotTime = Time.fixedTime;
            muzzleFlash.Emit(1);
            bulletPool.GetBullet();
        }
    }
}
