using UnityEngine;

public class AudioController : MonoBehaviour
{
    public AudioSource audioSource;
    public int sampleDataLength = 1024;
    private float[] clipSampleData;

    private bool isTrigger = false;
    [SerializeField] private float border = 0.5f;
    void Start() {
        clipSampleData = new float[sampleDataLength];
    }

    void Update() {
        
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            audioSource.Play();
        }

        if (!audioSource.isPlaying)
            return;
        
        audioSource.clip.GetData(clipSampleData, audioSource.timeSamples);
        float loudness = 0f;
        foreach (var sample in clipSampleData) {
            loudness += Mathf.Abs(sample);
        }
        loudness /= sampleDataLength; // 計算平均振幅
        bool result = loudness >= border;
        if (result == isTrigger) return;
        isTrigger = result;
        if(isTrigger)
            Debug.Log("音量強度: " + loudness);
    }
}
