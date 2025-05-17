using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class 防禦成功效果 : MonoBehaviour
{
    [Header("畫面閃光設定")]
    public Image 閃光畫面;   // 拖進 UI 白色或黃色遮罩
    public float 閃光持續時間 = 0.2f;

    [Header("畫面抖動設定")]
    public float 抖動強度 = 0.05f; // 防禦震動弱一點
    public float 抖動時間 = 0.08f;

    [Header("音效設定")]
    public AudioSource 音效來源;
    public AudioClip 防禦音效;

    private Vector3 攝影機原始位置;
    private Camera 主攝影機;

    void Start()
    {
        主攝影機 = Camera.main;
        if (主攝影機 != null)
            攝影機原始位置 = 主攝影機.transform.localPosition;

        // 一開始讓畫面閃光完全透明
        if (閃光畫面 != null)
        {
            Color 原色 = 閃光畫面.color;
            閃光畫面.color = new Color(原色.r, 原色.g, 原色.b, 0);
        }
    }

    public void 播放打擊感()
    {
        Debug.Log("🛡️ 播放防禦成功效果！");

        if (閃光畫面 != null)
            StartCoroutine(螢幕閃紅()); // 方法名不變

        if (主攝影機 != null)
            StartCoroutine(螢幕抖動());

        if (音效來源 != null && 防禦音效 != null)
            音效來源.PlayOneShot(防禦音效);
    }

    IEnumerator 螢幕閃紅()
    {
        float 彈入時間 = 0.05f;
        float 經過 = 0f;

        // 先從透明 → 快速升到 0.5
        while (經過 < 彈入時間)
        {
            float alpha = Mathf.Lerp(0f, 0.5f, 經過 / 彈入時間);
            Color 原色 = 閃光畫面.color;
            閃光畫面.color = new Color(原色.r, 原色.g, 原色.b, alpha);
            經過 += Time.deltaTime;
            yield return null;
        }

        // 保持 0.5 → 再淡出
        float 淡出時間 = 閃光持續時間;
        經過 = 0f;
        while (經過 < 淡出時間)
        {
            float alpha = Mathf.Lerp(0.5f, 0f, 經過 / 淡出時間);
            Color 原色 = 閃光畫面.color;
            閃光畫面.color = new Color(原色.r, 原色.g, 原色.b, alpha);
            經過 += Time.deltaTime;
            yield return null;
        }

        Color 清空色 = 閃光畫面.color;
        閃光畫面.color = new Color(清空色.r, 清空色.g, 清空色.b, 0);
    }

    IEnumerator 螢幕抖動()
    {
        float 經過時間 = 0f;

        while (經過時間 < 抖動時間)
        {
            Vector3 隨機位置 = 攝影機原始位置 + Random.insideUnitSphere * 抖動強度;
            主攝影機.transform.localPosition = 隨機位置;

            經過時間 += Time.deltaTime;
            yield return null;
        }

        主攝影機.transform.localPosition = 攝影機原始位置;
    }
}

