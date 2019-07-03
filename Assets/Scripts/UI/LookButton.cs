using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class LookButton : MonoBehaviour
{
    public UnityEvent onCompleteLook;
    public TextMeshProUGUI buttonText;


    [SerializeField]
    private Image fillImage;


    private void OnDestroy()
    {
        StopAllCoroutines();
        onCompleteLook.RemoveAllListeners();
    }

    public void StartLook()
    {
        StartCoroutine(Timer());
    }

    public void StopLook()
    {
        StopAllCoroutines();
        fillImage.fillAmount = 0;
    }


    private IEnumerator Timer()
    {
        fillImage.fillAmount = 0;
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime/2)
        {
            fillImage.fillAmount = t;
            yield return null;
        }
        fillImage.fillAmount = 1;
        onCompleteLook?.Invoke();
    }
}
