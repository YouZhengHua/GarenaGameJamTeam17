using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private string sceneName = "";
    /// <summary>
    /// 載入目標場景
    /// </summary>
    public void LoadScene()
    {
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
