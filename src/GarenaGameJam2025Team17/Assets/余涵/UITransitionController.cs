using UnityEngine;
using System.Collections;

public class UITransitionController : MonoBehaviour
{
    [SerializeField] private CanvasGroup mainCanvasGroup;
    [SerializeField] private float setTime = 1f;
    [SerializeField] private SceneLoader sceneLoader; // 拖進原本的 SceneLoader

    public void StartFunction()
    {
        StartCoroutine(StartTime());
    }

    private IEnumerator StartTime()
    {
        mainCanvasGroup.gameObject.SetActive(true);

        float t = 0f;
        while (t < setTime)
        {
            t += Time.deltaTime;
            mainCanvasGroup.alpha = Mathf.Clamp01(t / setTime);
            yield return null;
        }

        sceneLoader.LoadScene(); // ← 用乾淨的 SceneLoader 執行載入
    }
}
