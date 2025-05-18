using UnityEngine;
using UnityEngine.Video;

public class 強制播放影片 : MonoBehaviour
{
    private VideoPlayer player;

    void Start()
    {
        player = GetComponent<VideoPlayer>();
        player.Play();
    }
}
