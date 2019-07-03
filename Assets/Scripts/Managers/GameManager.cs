using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance => instance;
    private static GameManager instance;

    [SerializeField]
    private UpgradeUI upgradeUI;
    [SerializeField]
    private PlayerUI playerUI;
    [SerializeField]
    private PlayerManager player;
    [SerializeField]
    private EnemyManager enemyManager;

    public int waveNumber = 0;
    public float Score
    {
        get => score;
        set
        {
            score = value;
            playerUI.UpdateScore(score);
        }
    }
    private float score = 0;

    public bool IsPause {get => isPause;}
    private bool isPause = false;

    private bool hasStarted = false;

    #region Upgrade

        public struct UpgradeAction
        {
            public string upgradeName;
            public UnityAction upgradeAction;
        }

        public enum UpgradeType
        {
            Dmg = 0,
            Rate = 1,
            Spread = 2,
            Size = 3,
            Speed = 4,
            Health = 5
        }

        public float Damage {get => damageMultiplier;}
        public float FireRate {get => fireRateMultiplier;}
        public float Spread {get => spreadMultiplier;}
        public float BulletSize {get => bulletSizeMultiplier;}
        public float BulletSpeed {get => bulletSpeedMultiplier;}

        private float damageMultiplier = 1.0f;
        private float fireRateMultiplier = 1.0f;
        private float spreadMultiplier = 1.0f;
        private float bulletSizeMultiplier = 1.0f;
        private float bulletSpeedMultiplier = 1.0f;

    #endregion


    private void Start()
    {
        instance = this;
        hasStarted = false;
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0) && !hasStarted)
        {
            hasStarted = true;
            Begin();
            playerUI.FadeIn();
        }
    }

    public void Begin()
    {
        playerUI.UpdateStatTexts(Damage, FireRate, Spread, BulletSize, BulletSpeed);
        enemyManager.CreateWave(3);
    }

    public void RestartGame()
    {
        damageMultiplier = 1.0f;
        fireRateMultiplier = 1.0f;
        spreadMultiplier = 1.0f;
        bulletSizeMultiplier = 1.0f;
        bulletSpeedMultiplier = 1.0f;
        waveNumber = 0;
        Score = 0;
        isPause = false;
        playerUI.UpdateScore(score);
        playerUI.UpdateWave(waveNumber);
        SceneManager.LoadScene(0);
    }

    public void QuitApp()
    {
        Application.Quit();
    }

    public void EndWave()
    {
        isPause = true;
        upgradeUI.ShowNewUpgrades();
    }

    public void DamagePlayer(float ammount)
    {
        player.Damage(ammount);
    }

    public void EndGame()
    {
        isPause = true;
        enemyManager.KillAll();
        upgradeUI.ShowEndgame();
    }

    public UpgradeAction RandomUpgrade()
    {
        UpgradeType type = (UpgradeType)Random.Range(0,6);
        UpgradeAction u = new UpgradeAction();
        u.upgradeAction = () => AddMultiplier(type);
        switch (type)
        {
            case UpgradeType.Dmg:
                u.upgradeName = "+Damage";
                break;
            case UpgradeType.Rate:
                u.upgradeName = "+Rate";
                break;
            case UpgradeType.Spread:
                u.upgradeName = "-Spread";
                break;
            case UpgradeType.Size:
                u.upgradeName = "+Size";
                break;
            case UpgradeType.Speed:
                u.upgradeName = "+Speed";
                break;
            case UpgradeType.Health:
                u.upgradeName = "+Hitpoints";
                break;
            default:
                u.upgradeName = "Should no be here";
                break;
        }

        return u;
    }

    private void AddMultiplier(UpgradeType type)
    {
        switch (type)
        {
            case UpgradeType.Dmg:
                damageMultiplier += 0.2f;
                break;
            case UpgradeType.Rate:
                fireRateMultiplier += 0.1f;
                break;
            case UpgradeType.Spread:
                spreadMultiplier += 0.25f;
                break;
            case UpgradeType.Size:
                bulletSizeMultiplier += 0.5f;
                break;
            case UpgradeType.Speed:
                bulletSpeedMultiplier += 0.1f;
                break;
            case UpgradeType.Health:
                player.HpUpgrade(10 + (int)(waveNumber/5) * 10);
                break;
            default:
                break;
        }

        playerUI.UpdateStatTexts(Damage, FireRate, Spread, BulletSize, BulletSpeed);
        isPause = false;
        NextWave();
    }

    private void NextWave()
    {
        waveNumber ++;
        player.Heal();
        enemyManager.CreateWave(Random.Range(2,4) + waveNumber/3);
        playerUI.UpdateWave(waveNumber);
    }

}
