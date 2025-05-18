using UnityEngine;
using System.Collections;

public class UITransitionController : MonoBehaviour
{
    [SerializeField] private CanvasGroup 黑幕;
    [SerializeField] private float 淡出時間 = 1f;
    [SerializeField] private SceneLoader sceneLoader; // 拖進原本的 SceneLoader

    public void 播放轉場切場景()
    {
        StartCoroutine(淡出後切場景());
    }

    private IEnumerator 淡出後切場景()
    {
        黑幕.gameObject.SetActive(true);

        float t = 0f;
        while (t < 淡出時間)
        {
            t += Time.deltaTime;
            黑幕.alpha = Mathf.Clamp01(t / 淡出時間);
            yield return null;
        }

        sceneLoader.LoadScene(); // ← 用乾淨的 SceneLoader 執行載入
    }
}
