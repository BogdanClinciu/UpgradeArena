using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    [SerializeField]
    private PlayerUI playerUI;
    [SerializeField]
    private Camera playerCamera;

    public Transform Transform => playerTransform;
    public Transform WeaponTransform => weaponTransform;
    public WeaponManager WeaponManager => playerWeapon;
    public float PlayerHP {get => hp;}

    [Space]
    [SerializeField]
    private WeaponManager playerWeapon;
    [SerializeField]
    private Transform playerTransform;
    [SerializeField]
    private Transform weaponTransform;
    [SerializeField]
    private float hp = 100;
    [SerializeField]
    private float hpCap = 100;

    [SerializeField]
    private GraphicRaycaster canvasRaycaster;
    [SerializeField]
    private EventSystem canvasEventSystem;

    private PointerEventData pointerEventData;
    private Vector2 screenCenter;
    private GameObject currentLookButton;


    private void Start()
    {
        screenCenter.x = Screen.width/2;
        screenCenter.y = Screen.height/2;
        playerUI.UpdateHpBar(hp, hpCap);
    }

    private void Update()
    {
        if(!GameManager.Instance.IsPause)
        {
            if(Input.GetMouseButton(0) && hp != 0)
            {
                playerWeapon.Shoot();
            }
            playerUI.ShowWarning(WarningDirection());
        }

        RaycatstForward();
    }

    public void HpUpgrade(float ammount)
    {
        hpCap += ammount;
        hp = Mathf.Clamp(hp + ammount, 0, hpCap);

        playerUI.UpdateHpBar(hp, hpCap);
    }

    public void Heal()
    {
        hp = hpCap;
        playerUI.UpdateHpBar(hp, hpCap);
    }

    public void Damage(float ammount)
    {
        hp -= ammount;
        if(hp - ammount <= 0 && hp != 0)
        {
            hp = 0;
            Die();
        }

        playerUI.UpdateHpBar(hp, hpCap);
        playerUI.ShowHurtOverlay();
    }

    public void Die()
    {
        playerUI.UpdateHpBar(0, hpCap);
        GameManager.Instance.EndGame();
    }

    private void RaycatstForward()
    {
        pointerEventData = new PointerEventData(canvasEventSystem);
        pointerEventData.position = screenCenter;

        List<RaycastResult> results = new List<RaycastResult>();
        canvasRaycaster.Raycast(pointerEventData, results);

        if(currentLookButton != null)
        {
            if(results.Exists(g => g.gameObject == currentLookButton))
            {
                return;
            }
            else
            {
                currentLookButton.GetComponent<LookButton>().StopLook();
                currentLookButton = null;
            }
        }

        foreach (RaycastResult r in results)
        {
            if(r.gameObject.tag == "LookButton")
            {
                currentLookButton = r.gameObject;
                currentLookButton.GetComponent<LookButton>().StartLook();
                break;
            }
        }
    }

    private int WarningDirection()
    {
        foreach (IEnemy e in EnemyManager.LiveEnemies)
        {
            float ang = Vector3.Angle(playerTransform.forward, e.position);
            ang = Mathf.Sign(Vector3.Cross(playerTransform.forward, e.position).y) * ang;
            if(ang > 100 || ang < -100)
            {
                return (int)Mathf.Sign(ang);
            }
        }

        return 0;
    }
}
