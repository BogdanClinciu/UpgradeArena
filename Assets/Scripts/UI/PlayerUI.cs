using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField]
    private GameObject leftWarning;
    [SerializeField]
    private GameObject rightWarning;

    [Space]
    [SerializeField]
    private CanvasGroup hurtOverlay;

    [Space]
    #region Stat ui
        [SerializeField]
        private Text dmgText;
        [SerializeField]
        private Text rateText;
        [SerializeField]
        private Text spreadText;
        [SerializeField]
        private Text sizeText;
        [SerializeField]
        private Text speedText;
        [SerializeField]
        private Text scoreText;

        [Space]
        [SerializeField]
        private Text hpText;
        [SerializeField]
        private Image hpFillBar;

        [Space]
        [SerializeField]
        private Text waveText;
    #endregion

    private float lastAngle = 0;

    public void UpdateHpBar(float hp, float hpCap)
    {
        hpText.text = $"{(int)hp} / {hpCap}";
        hpFillBar.fillAmount = hp/hpCap;
    }

    public void UpdateScore(float score)
    {
        scoreText.text  = score.ToString();
    }

    public void UpdateWave(int wave)
    {
        waveText.text = $"Wave {wave + 1}";
    }

    public void UpdateStatTexts(float dmg, float rate, float spread, float size, float speed)
    {
        dmgText.text    = dmg.ToString();
        rateText.text   = rate.ToString();
        spreadText.text = (1f / spread).ToString("#0.00");
        sizeText.text   = size.ToString();
        speedText.text  = speed.ToString();
    }

    public void ShowWarning(int angle)
    {
        if(lastAngle != angle)
        {
            lastAngle = angle;

            if(angle == 0)
            {
                leftWarning.SetActive(false);
                rightWarning.SetActive(false);
            }

            if(angle > 0)
            {
                leftWarning.SetActive(false);
                rightWarning.SetActive(true);
            }

            if(angle < 0)
            {
                leftWarning.SetActive(true);
                rightWarning.SetActive(false);
            }
        }
    }

    public void ShowHurtOverlay()
    {
        StopAllCoroutines();
        StartCoroutine(HurtOverlay());
    }

    private IEnumerator HurtOverlay()
    {
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime/0.2f)
        {
            hurtOverlay.alpha = Mathf.Sin(t * Mathf.PI);
            yield return null;
        }
        hurtOverlay.alpha = 0;
    }
}
