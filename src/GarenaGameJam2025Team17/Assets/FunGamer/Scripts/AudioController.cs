using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class AudioController : MonoBehaviour
{
    public Action OnTrigger;
    
    public AudioSource audioSource;
    public int sampleDataLength = 1024;
    private float[] clipSampleData;

    private bool isTrigger = false;
    [SerializeField] private float border = 0.5f;

    private float totalTime = 0f;
    private float singleTime = 0f;
    
    private void Start() {
        clipSampleData = new float[sampleDataLength];
    }

    private void Update() 
    {
        if (!audioSource.isPlaying )
            return;
        
        CheckVolume();
    }

    private float GetLoudness
    {
        get
        {
            audioSource.clip.GetData(clipSampleData, audioSource.timeSamples);
            float loudness = 0f;
            foreach (var sample in clipSampleData) {
                loudness += Mathf.Abs(sample);
            }
        
            return loudness / sampleDataLength; // 計算平均振幅
        }
    }

    private void CheckVolume()
    {
        float loudness = GetLoudness;
        bool result = loudness >= border;
        totalTime += Time.deltaTime;
        if (result == isTrigger)
        {
            singleTime += Time.deltaTime;
            return;
        }
        
        isTrigger = result;
        if (isTrigger)
        {
            singleTime = 0f;
            OnTrigger?.Invoke();
        }
    }
}
