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
    private bool _isJudge = false;
    private bool _isJudgeSuccess = false;
    private float _startDefendTime = 0f;
    private int _judgeBeatIndex = 0; 

    IEnumerator JudgeSuccess()
    {
        yield return new WaitForEndOfFrame();
        if (_isJudgeSuccess)
        {
            defendOkOBJ.SetActive(true);
            if (OndefectSuccess != null) OndefectSuccess.Invoke();
        }
        else
        {
            defendFailOBJ.SetActive(true);
            if (OndefectFail!= null) OndefectFail.Invoke();
        }
        _isJudgeSuccess = false ;
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
                    _isJudge = true;
                    StartCoroutine(JudgeSuccess());
                }
                if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    _judgeBeatIndex = 1;
                    _isJudge = true;
                    StartCoroutine(JudgeSuccess());
                }
                if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    _judgeBeatIndex = 2;
                    _isJudge = true;
                    StartCoroutine(JudgeSuccess());
                }
                if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    _judgeBeatIndex = 3;
                    _isJudge = true;
                    StartCoroutine(JudgeSuccess());
                }
            }
            if (inputControl.GetCurrentGameTurn() == 2 && playerID == 1)
            {
                if (Input.GetKeyDown(KeyCode.W))
                {
                    _judgeBeatIndex = 0;
                    _isJudge = true;
                    StartCoroutine(JudgeSuccess());
                }
                if (Input.GetKeyDown(KeyCode.S))
                {
                    _judgeBeatIndex = 1;
                    _isJudge = true;
                    StartCoroutine(JudgeSuccess());
                }
                if (Input.GetKeyDown(KeyCode.A))
                {
                    _judgeBeatIndex = 2;
                    _isJudge = true;
                    StartCoroutine(JudgeSuccess());
                }
                if (Input.GetKeyDown(KeyCode.D))
                {
                    _judgeBeatIndex = 3;
                    _isJudge = true;
                    StartCoroutine(JudgeSuccess());
                }
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!_isJudge) return;
        if (other.gameObject.CompareTag("BeatJudge"))
        {
            BeatMoveSystem beatMoveSystem = other.gameObject.GetComponent<BeatMoveSystem>();
            if (beatMoveSystem != null)
            {
                if (beatMoveSystem.GetBeatIndex() != _judgeBeatIndex) return;
                beatMoveSystem.SuccessJudge();
                _isJudgeSuccess = true;
            }
        }
        _isJudge = false;
    }
}
