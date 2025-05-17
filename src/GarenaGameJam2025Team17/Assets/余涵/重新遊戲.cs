using UnityEngine;
using UnityEngine.SceneManagement;

public class 重新遊戲 : MonoBehaviour
{
    public void 載入遊戲場景()
    {
        Debug.Log("載入遊戲場景() 被呼叫"); // ← 加這行
        SceneManager.LoadScene("11");
    }
}
