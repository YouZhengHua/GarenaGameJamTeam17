using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] protected string sceneName = "";
    /// <summary>
    /// 載入目標場景
    /// </summary>
    public virtual void LoadScene()
    {
        SceneManager.LoadScene(sceneName);
    }

    /// <summary>
    /// 重新載入當前場景
    /// </summary>
    public virtual void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
