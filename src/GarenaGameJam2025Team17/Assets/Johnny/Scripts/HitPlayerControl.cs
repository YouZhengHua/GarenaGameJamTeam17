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
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("BeatJudge"))
        {
            BeatMoveSystem beatMoveSystem = other.gameObject.GetComponent<BeatMoveSystem>();
            if (beatMoveSystem != null)
            {
                _hitPoint = beatMoveSystem.GetHitPoint();
                beatMoveSystem.HitPlayer();
                TriggerHit();
            }
        }
    }



}
