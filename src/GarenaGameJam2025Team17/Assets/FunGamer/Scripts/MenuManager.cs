using UnityEngine;

public class MenuManager : SceneLoader
{
    [SerializeField] private AudioClip clip;
    [SerializeField] private AudioSource source;
    /// <summary>
    /// 載入目標場景
    /// </summary>
    public override void LoadScene()
    {
        float delay = (clip?.length ?? 1f) * ((double)Time.timeScale < 0.009999999776482582 ? 0.01f : Time.timeScale);
        source.Stop();
        source.PlayOneShot(clip);
        Invoke("DelayLoad", delay);
    }

    private void DelayLoad()
    {
        base.LoadScene();
    }
}
