using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class 角色死亡效果 : MonoBehaviour
{
    [Header("畫面變暗設定")]
    public Image 黑幕遮罩;
    public float 淡入時間 = 0.5f;
    public float 最終透明度 = 0.7f;

    [Header("畫面抖動設定")]
    public float 抖動強度 = 0.2f;
    public float 抖動時間 = 0.3f;

    [Header("音效設定")]
    public AudioSource 音效來源;
    public AudioClip 死亡音效;

    [Header("KO圖像")]
    public Image KO_K圖片;
    public Image KO_O圖片;
    public float 彈出時間 = 0.2f;
    public float 彈出間隔 = 0.15f;
    public float 最大縮放 = 1.5f;

    private Vector3 攝影機原始位置;
    private Camera 主攝影機;

    void Start()
    {
        主攝影機 = Camera.main;
        if (主攝影機 != null)
            攝影機原始位置 = 主攝影機.transform.localPosition;

        if (黑幕遮罩 != null)
        {
            Color 原色 = 黑幕遮罩.color;
            黑幕遮罩.color = new Color(原色.r, 原色.g, 原色.b, 0);
        }

        if (KO_K圖片 != null) KO_K圖片.gameObject.SetActive(false);
        if (KO_O圖片 != null) KO_O圖片.gameObject.SetActive(false);
    }

    public void 播放死亡效果()
    {
        Debug.Log("💀 播放角色死亡效果！");

        if (黑幕遮罩 != null)
            StartCoroutine(畫面淡入());

        if (主攝影機 != null)
            StartCoroutine(螢幕抖動());

        if (音效來源 != null && 死亡音效 != null)
            音效來源.PlayOneShot(死亡音效);

        StartCoroutine(播放KO彈字());
    }

    IEnumerator 畫面淡入()
    {
        float 經過 = 0f;
        while (經過 < 淡入時間)
        {
            float alpha = Mathf.Lerp(0f, 最終透明度, 經過 / 淡入時間);
            Color 原色 = 黑幕遮罩.color;
            黑幕遮罩.color = new Color(原色.r, 原色.g, 原色.b, alpha);
            經過 += Time.deltaTime;
            yield return null;
        }

        Color 完成色 = 黑幕遮罩.color;
        黑幕遮罩.color = new Color(完成色.r, 完成色.g, 完成色.b, 最終透明度);
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

    IEnumerator 播放KO彈字()
    {
        if (KO_K圖片 != null)
        {
            KO_K圖片.gameObject.SetActive(true);
            KO_K圖片.transform.localScale = Vector3.zero;
            StartCoroutine(彈出動畫(KO_K圖片));
        }

        yield return new WaitForSeconds(彈出間隔);

        if (KO_O圖片 != null)
        {
            KO_O圖片.gameObject.SetActive(true);
            KO_O圖片.transform.localScale = Vector3.zero;
            StartCoroutine(彈出動畫(KO_O圖片));
        }
    }

    IEnumerator 彈出動畫(Image 圖片)
    {
        float 時間 = 0f;
        while (時間 < 彈出時間)
        {
            float t = 時間 / 彈出時間;
            float scale = Mathf.Lerp(0f, 最大縮放, t);
            圖片.transform.localScale = new Vector3(scale, scale, scale);
            時間 += Time.deltaTime;
            yield return null;
        }

        圖片.transform.localScale = Vector3.one;
    }
}

