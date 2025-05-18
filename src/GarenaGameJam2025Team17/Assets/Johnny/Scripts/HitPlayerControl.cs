using UnityEngine;
using UnityEngine.Events;

public class HitPlayerControl : MonoBehaviour
{ // �B�Y UI ��������ؓ؟���®����ϵĹ����l
    [SerializeField] MainBattleUIController mainBattleUIController;

    // ��Ҿ�̖��1 ��ʾ��� 1������������ AI ���ˣ�


    [SerializeField] int PlayerIndex = 1;

    // ��������|�l���¼������� Inspector ����Ч�ȣ�


    [SerializeField] UnityEvent OnHitEffect;

    // ����ķ֔������ж��^�K�Á�ģ�


    private float _hitPoint = 0f;

    public void TriggerHit()
    {
        if (PlayerIndex == 1)
        {
            // ��ұ��򣺿۷�

            GameSystem.BeatValue -= _hitPoint;

            //�ອ����
            // ֻ�б���Ų��Ŵ����
            var ��Ѫ��Ч = FindObjectOfType<��ѪЧ��>();
            if (��Ѫ��Ч != null)
            {
                ��Ѫ��Ч.���ſ�Ѫ��Ч();
            }
        }
        else
        {
            // ����ұ��򣺼ӷ֣������Ǵ򵽔��ˣ�

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
            {  // �Ĺ������ȡ�Ó��з֔�
                _hitPoint = beatMoveSystem.GetHitPoint(); // ֪ͨ�ǂ�������㱻���ˡ�
                beatMoveSystem.HitPlayer();
                TriggerHit(); // ���б�����ұ����߉݋���۷֡���Ч��UI��
            }
        }
    }



}
