using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    [SerializeField]
    private PlayerManager player;

    [SerializeField]
    private int poolSize = 25;
    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private Transform bulletParent;

    private List<Bullet> bulletPool = new List<Bullet>();
    private Vector3 initialBulletSize;

    private void Start()
    {
        initialBulletSize = bulletPrefab.transform.localScale;
        ManagePool();
    }

    public void GetBullet()
    {
        for (int b = 0; b < bulletPool.Count; b++)
        {
            if(!bulletPool[b].gameObject.activeSelf)
            {
                FireBullet(bulletPool[b]);
                return;
            }
        }
    }

    private void FireBullet(Bullet bullet)
    {
        bullet.transform.position = player.WeaponTransform.position;
        bullet.transform.rotation = Quaternion.Lerp(player.WeaponTransform.rotation,Random.rotation, 0.015f / GameManager.Instance.Spread);
        bullet.transform.localScale = initialBulletSize * GameManager.Instance.BulletSize;
        bullet.gameObject.SetActive(true);
    }

    private void ManagePool()
    {
        if(bulletPool.Count < poolSize)
        {
            for (int i = 0; i < poolSize - bulletPool.Count; i++)
            {
                Bullet bullet = Instantiate(bulletPrefab, bulletParent).GetComponent<Bullet>();
                bullet.gameObject.SetActive(false);
                bulletPool.Add(bullet);

            }
        }
    }
}
