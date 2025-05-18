using UnityEngine;
using UnityEngine.Events;

public class GameSystemSetting : MonoBehaviour
{
    [SerializeField] float judgeShowTime = 0.2f;
    [SerializeField] float inputFactor = 0.5f;

    private float player1Hp = 50f;
    private float player2Hp = 50f;

    public UnityEvent<int> OnPlayerWin;

    [SerializeField] private HpUIController hpUIController;

    private void Awake()
    {
        GameSystem.JudgeShowTime = judgeShowTime;
        GameSystem.inputDelayFactor = inputFactor;
    }

    public void Player1Hurt()
    {
        SetPlayerHp(1, player1Hp - 10f);
        SetPlayerHp(2, player2Hp + 10f);
    }
    
    public void Player2Hurt()
    {
        SetPlayerHp(1, player1Hp + 10f);
        SetPlayerHp(2, player2Hp - 10f);
    } 

    private void SetPlayerHp(int playerIndex, float hp)
    {
        if (playerIndex == 1)
        {
            player1Hp = hp;
            if (hp == 0f)
            {
                OnPlayerWin.Invoke(2);
            }
        }
        else
        {
            player2Hp = hp;
            if (hp == 0f)
            {
                OnPlayerWin.Invoke(1);
            }
        }

        hpUIController.UpdateUI(player1Hp / (player1Hp +player2Hp));
    }
}
