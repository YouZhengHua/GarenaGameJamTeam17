using UnityEngine;
using UnityEngine.Events;

public class HitPlayerControl : MonoBehaviour
{ 
    [SerializeField] MainBattleUIController mainBattleUIController;

    [SerializeField] int PlayerIndex = 1;


    [SerializeField] UnityEvent OnHitEffect;


    private float _hitPoint = 0f;

    public void TriggerHit()
    {
        if (PlayerIndex == 1)
        {

            GameSystem.BeatValue -= _hitPoint;
        }
        else
        {

            GameSystem.BeatValue += _hitPoint;
        }
        if (OnHitEffect != null) OnHitEffect.Invoke();
        mainBattleUIController.UpdateBeatUI();

        ////余涵增加
        //FindObjectOfType<打擊感效果>().播放打擊感();

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("BeatJudge"))
        {
            BeatMoveSystem beatMoveSystem = other.gameObject.GetComponent<BeatMoveSystem>();
            if (beatMoveSystem != null)
            {  // 從?奏物件取得擊中分數
                _hitPoint = beatMoveSystem.GetHitPoint(); // 通知那?物件「你被打了」
                beatMoveSystem.HitPlayer();
                TriggerHit(); // 執行本地玩家被打的??（扣分、特效、UI）
            }
        }
    }



}
