using UnityEngine;

public class GameSystemSetting : MonoBehaviour
{
    [SerializeField] float musicSecond = 8f;
    [SerializeField] int roundTimes = 2;
    [SerializeField] float distance = 38f;
    [SerializeField] float attackDelay= 0.1f;
    [SerializeField] float defendDelay = 0.1f;
    [SerializeField] float judgeShowTime = 0.2f;

    private void Awake()
    {
        GameSystem.BeatSpeed = distance / (musicSecond * roundTimes);
        GameSystem.AttackDelay = attackDelay;
        GameSystem.DefendDealy = defendDelay;
        GameSystem.JudgeShowTime = judgeShowTime;
    }

}
