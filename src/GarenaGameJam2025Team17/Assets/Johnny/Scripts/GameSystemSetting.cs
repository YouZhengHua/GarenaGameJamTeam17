using UnityEngine;

public class GameSystemSetting : MonoBehaviour
{
    [SerializeField] float judgeShowTime = 0.2f;
    [SerializeField] float inputFactor = 0.5f;

    private void Awake()
    {
        GameSystem.JudgeShowTime = judgeShowTime;
        GameSystem.inputDelayFactor = inputFactor;
    }

}
