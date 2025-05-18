using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class 扣血效果 : MonoBehaviour
{
    [Header("畫面閃紅設定")]
    public Image 攻擊閃紅畫面;   // 拖進 UI 紅色遮罩（半透明）
    public float 閃紅持續時間 = 0.2f;

    [Header("畫面抖動設定")]
    public float 抖動強度 = 0.1f;
    public float 抖動時間 = 0.1f;

    [Header("音效設定")]
    public AudioSource 音效來源;
    public AudioClip 打擊音效;

    private Vector3 原始攝影機位置;
    private Camera 主攝影機;

    void Start()
    {
        主攝影機 = Camera.main;
        if (主攝影機 != null)
            原始攝影機位置 = 主攝影機.transform.localPosition;

        // 一開始讓紅色畫面完全透明
        if (攻擊閃紅畫面 != null)
        {
            Color 原色 = 攻擊閃紅畫面.color;
            攻擊閃紅畫面.color = new Color(原色.r, 原色.g, 原色.b, 0);
        }
    }

    // 給隊友調用的打擊感觸發方法
    public void 播放扣血特效()
    {
        Debug.Log("⚡ 播放打擊感() 被呼叫！");

        if (攻擊閃紅畫面 != null)
            StartCoroutine(螢幕閃紅());

        if (主攝影機 != null)
            StartCoroutine(螢幕抖動());

        if (音效來源 != null && 打擊音效 != null)
            音效來源.PlayOneShot(打擊音效);
    }

    IEnumerator 螢幕閃紅()
    {
        float 時長 = 閃紅持續時間;
        float 經過 = 0f;

        // 「閃一下」效果：快速進入透明度 0.5，再慢慢淡出
        攻擊閃紅畫面.color = new Color(1, 0, 0, 0.5f);
        yield return new WaitForSeconds(0.05f); // 快速彈入
        while (經過 < 時長)
        {
            float alpha = Mathf.Lerp(0.5f, 0f, 經過 / 時長);
            Color 原色 = 攻擊閃紅畫面.color;
            攻擊閃紅畫面.color = new Color(原色.r, 原色.g, 原色.b, alpha);
            經過 += Time.deltaTime;
            yield return null;
        }

        Color 清空色 = 攻擊閃紅畫面.color;
        攻擊閃紅畫面.color = new Color(清空色.r, 清空色.g, 清空色.b, 0);
    }

    IEnumerator 螢幕抖動()
    {
        float 經過時間 = 0f;

        while (經過時間 < 抖動時間)
        {
            Vector3 隨機位置 = 原始攝影機位置 + Random.insideUnitSphere * 抖動強度;
            主攝影機.transform.localPosition = 隨機位置;

            經過時間 += Time.deltaTime;
            yield return null;
        }

        主攝影機.transform.localPosition = 原始攝影機位置;
    }
}
