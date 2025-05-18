using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class 開始遊戲 : MonoBehaviour
{
    [SerializeField] private int sceneName = 1;
    [SerializeField] private Image 淡出圖;  // 你自行拉進 Inspector
    [SerializeField] private float 淡出時間 = 1.0f;

    /// <summary>
    /// 載入目標場景（含淡出動畫）
    /// </summary>
    public void LoadScene()
    {
        StartCoroutine(淡出然後載入場景());
    }

    private IEnumerator 淡出然後載入場景()
    {
        if (淡出圖 != null)
        {
            Color color = 淡出圖.color;
            color.a = 0f;
            淡出圖.color = color;
            淡出圖.gameObject.SetActive(true);

            float 時間 = 0f;
            while (時間 < 淡出時間)
            {
                float a = 時間 / 淡出時間;
                淡出圖.color = new Color(color.r, color.g, color.b, a);
                時間 += Time.deltaTime;
                yield return null;
            }

            淡出圖.color = new Color(color.r, color.g, color.b, 1f);
        }

        SceneManager.LoadScene(sceneName);
    }

    /// <summary>
    /// 重新載入當前場景
    /// </summary>
    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

