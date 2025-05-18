using UnityEngine;
using UnityEngine.Events;

public class HitPlayerControl : MonoBehaviour
{ // 連結 UI 控制器，負責更新畫面上的節奏條
    [SerializeField] MainBattleUIController mainBattleUIController;

    // 玩家編號（1 表示玩家 1，其他可能是 AI 或敵人）


    [SerializeField] int PlayerIndex = 1;

    // 擊中後會觸發的事件（可在 Inspector 綁特效等）


    [SerializeField] UnityEvent OnHitEffect;

    // 被打的分數（從判定區塊拿來的）


    private float _hitPoint = 0f;

    public void TriggerHit()
    {
        if (PlayerIndex == 1)
        {
            // 玩家被打：扣分

            GameSystem.BeatValue -= _hitPoint;

            //余涵增加
            // 只有被打才播放打擊感
            var 扣血特效 = FindObjectOfType<扣血效果>();
            if (扣血特效 != null)
            {
                扣血特效.播放扣血特效();
            }
        }
        else
        {
            // 非玩家被打：加分（可能是打到敵人）

            GameSystem.BeatValue += _hitPoint;
        }
        if (OnHitEffect != null) OnHitEffect.Invoke();
        mainBattleUIController.UpdateBeatUI();

       

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("BeatJudge"))
        {
            BeatMoveSystem beatMoveSystem = other.gameObject.GetComponent<BeatMoveSystem>();
            if (beatMoveSystem != null)
            {  // 從節奏物件取得擊中分數
                _hitPoint = beatMoveSystem.GetHitPoint(); // 通知那個物件「你被打了」
                beatMoveSystem.HitPlayer();
                TriggerHit(); // 執行本地玩家被打的邏輯（扣分、特效、UI）
            }
        }
    }



}
