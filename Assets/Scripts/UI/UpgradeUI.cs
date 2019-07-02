using System.Collections;
using UnityEngine;

public class UpgradeUI : MonoBehaviour
{
    [SerializeField]
    private Transform cameraTransform;
    [SerializeField]
    private Transform canvasTransform;

    [Space]
    [SerializeField]
    private CanvasGroup upgradeButtonCanvas;
    [SerializeField]
    private LookButton[] upgradeButtons;

    private Vector3 targetRotation = Vector3.zero;

    private void Update()
    {
        canvasTransform.rotation = Quaternion.Lerp(canvasTransform.rotation, LookRotationTarget(), Time.deltaTime * 5);
    }

    public void ShowNewUpgrades()
    {
        foreach (LookButton lb in upgradeButtons)
        {
            lb.onCompleteLook.RemoveAllListeners();
            lb.StopLook();

            var u = GameManager.Instance.RandomUpgrade();
            lb.buttonText.text = u.upgradeName;
            lb.onCompleteLook.AddListener(u.upgradeAction);
            lb.onCompleteLook.AddListener(HideUpgrades);
        }

        StartCoroutine(ShowHide(true));
    }

    public void HideUpgrades()
    {
        StartCoroutine(ShowHide(false));
    }

    private IEnumerator ShowHide(bool isShow)
    {
        upgradeButtonCanvas.blocksRaycasts = false;

        float fromAlpha = upgradeButtonCanvas.alpha;
        float toAlpha = isShow ? 1 : 0;

        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime/0.5f)
        {
            upgradeButtonCanvas.alpha = Mathf.Lerp(fromAlpha, toAlpha, t);
            yield return null;
        }
        upgradeButtonCanvas.alpha = toAlpha;
        upgradeButtonCanvas.blocksRaycasts = isShow;
    }

    private Quaternion LookRotationTarget()
    {
        return Quaternion.Euler(canvasTransform.eulerAngles.x, Quaternion.LookRotation(cameraTransform.forward, canvasTransform.up).eulerAngles.y ,canvasTransform.eulerAngles.z);
    }
}
