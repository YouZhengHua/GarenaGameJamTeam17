using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class DefendSystem : MonoBehaviour
{
    [SerializeField] GameObject defendOkOBJ;
    [SerializeField] GameObject defendFailOBJ;
    [SerializeField] Transform judgeArea;
    [SerializeField] UnityEvent OndefectSuccess;
    [SerializeField] UnityEvent OndefectFail;
    [SerializeField] private int defendTurn = 1;
    private bool _isDefend = false;
    private float _startDefendTime = 0f;
    private int _judgeBeatIndex = 0;
    
    private MusicController _musicController;

    private void Awake()
    {
        _musicController = GameObject.Find("MusicSystem").GetComponent<MusicController>();
    }

    public void JudgeSuccess(bool isSuccess)
    {
        if (isSuccess)
        {
            defendOkOBJ.SetActive(true);
            if (OndefectSuccess != null) OndefectSuccess.Invoke();
            // 👉 呼叫防禦特效 余涵增加
            //FindObjectOfType<防禦成功效果>()?.播放打擊感();
        }
        else
        {
            defendFailOBJ.SetActive(true);
            if (OndefectFail!= null) OndefectFail.Invoke();
        }
    }
    public void CheckJudge()
    {
       bool isSuccess = false ;
       Collider[] hitCollider = Physics.OverlapBox(judgeArea.position, judgeArea.gameObject.transform.localScale);
        if (hitCollider.Length > 0)
       {
            for (int i = 0; i < hitCollider.Length; i++)
            {
                if (hitCollider[i].CompareTag("BeatJudge"))
                {
                    BeatMoveSystem beatMoveSystem = hitCollider[i].gameObject.GetComponent<BeatMoveSystem>();
                    if (beatMoveSystem != null)
                    {
                        if (beatMoveSystem.GetBeatIndex() != _judgeBeatIndex) continue;
                        beatMoveSystem.SuccessJudge();
                        isSuccess = true;
                    }
                }
            }
       }
       JudgeSuccess(isSuccess);
    }
    private void OnUp(InputValue input)
    {
        if (_isDefend) return;
        _isDefend = true;
        _startDefendTime = Time.time;

        if (_musicController.GetCurrentGameTurn() != defendTurn)
            return;
        _judgeBeatIndex = 0;
        CheckJudge();
    }
    
    private void OnDown(InputValue input)
    {
        if (_isDefend) return;
        _isDefend = true;
        _startDefendTime = Time.time;

        if (_musicController.GetCurrentGameTurn() != defendTurn)
            return;
        _judgeBeatIndex = 1;
        CheckJudge();
    }
    
    private void OnLeft(InputValue input)
    {
        if (_isDefend) return;
        _isDefend = true;
        _startDefendTime = Time.time;

        if (_musicController.GetCurrentGameTurn() != defendTurn)
            return;
        _judgeBeatIndex = 2;
        CheckJudge();
    }
    
    private void OnRight(InputValue input)
    {
        if (_isDefend) return;
        _isDefend = true;
        _startDefendTime = Time.time;

        if (_musicController.GetCurrentGameTurn() != defendTurn)
            return;
        _judgeBeatIndex = 3;
        CheckJudge();
    }
    
    private void Update()
    {
        if (_isDefend && Time.time - _startDefendTime >= GameSystem.BeatDeltaTime * GameSystem.inputDelayFactor)
        {
            _isDefend = false;
        }
    }
}
