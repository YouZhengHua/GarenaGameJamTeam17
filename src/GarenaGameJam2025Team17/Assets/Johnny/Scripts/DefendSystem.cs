using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class DefendSystem : MonoBehaviour
{
    [SerializeField] int playerID = 1;
    [SerializeField] InputControl inputControl;
    [SerializeField] BoxCollider objCollider;
    [SerializeField] GameObject defendOkOBJ;
    [SerializeField] GameObject defendFailOBJ;
    [SerializeField] UnityEvent OndefectSuccess;
    [SerializeField] UnityEvent OndefectFail;

    private bool _isDefend = false;
    private float _startDefendTime = 0f;
    private int _judgeBeatIndex = 0; 

    public void JudgeSuccess(bool isSuccess)
    {
        if (isSuccess)
        {
            defendOkOBJ.SetActive(true);
            if (OndefectSuccess != null) OndefectSuccess.Invoke();
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
       Collider[] hitCollider = Physics.OverlapBox(transform.position, new Vector3(2f, 4f, 1f));
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
    private void Start()
    {
        objCollider = GetComponent<BoxCollider>();
        objCollider.enabled = true;

    }
    private void Update()
    {
        if (_isDefend)
        {
            if (Time.time - _startDefendTime > GameSystem.DefendDealy) _isDefend = false;
        }
        else
        {
            if (inputControl.GetCurrentGameTurn() == 1 && playerID == 2)
            {
                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    _judgeBeatIndex = 0;
                    CheckJudge();
                }
                if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    _judgeBeatIndex = 1;
                    CheckJudge();
                }
                if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    _judgeBeatIndex = 2;
                    CheckJudge();
                }
                if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    _judgeBeatIndex = 3;
                    CheckJudge();
                }
            }
            if (inputControl.GetCurrentGameTurn() == 2 && playerID == 1)
            {
                if (Input.GetKeyDown(KeyCode.W))
                {
                    _judgeBeatIndex = 0;
                    CheckJudge();
                }
                if (Input.GetKeyDown(KeyCode.S))
                {
                    _judgeBeatIndex = 1;
                    CheckJudge();
                }
                if (Input.GetKeyDown(KeyCode.A))
                {
                    _judgeBeatIndex = 2;
                    CheckJudge();
                }
                if (Input.GetKeyDown(KeyCode.D))
                {
                    _judgeBeatIndex = 3;
                    CheckJudge();
                }
            }
        }
    }
}
