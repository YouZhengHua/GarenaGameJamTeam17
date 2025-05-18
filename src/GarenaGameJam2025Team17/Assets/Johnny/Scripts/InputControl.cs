using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;
public class InputControl : MonoBehaviour
{
    [SerializeField] GameObject[] playerShootBeatOBJ; 
    [SerializeField] float playerAttack = 3f;
    [SerializeField] GameObject JusgeArea;
    [SerializeField] UnityEvent OnInputOK;
    [SerializeField] UnityEvent OnInputFail;
    
    private int gameTurn
    {
        get => _musicController.GetCurrentGameTurn();
    }

    public bool IsAttackTurn => gameTurn == attackTurn;
    public int PlayerIndex => attackTurn;

    private bool isAttack = false;
    private float _attackStartTime = 0f;
    [SerializeField] private int moveWay = 1;

    [SerializeField] private int attackTurn = 1;

    private MusicController _musicController;

    private void Awake()
    {
        _musicController = GameObject.Find("MusicSystem").GetComponent<MusicController>();
    }

    private void Start()
    {
        GameSystem.BeatValue = 0f;
        InputUser.PerformPairingWithDevice(Keyboard.current, this.GetComponent<PlayerInput>().user);
    }

    private void OnUp(InputValue input)
    {

        if (gameTurn != attackTurn || !input.isPressed)
            return;
        CreateBeat(0);
    }

    private void OnDown(InputValue input)
    {

        if (gameTurn != attackTurn || !input.isPressed)
            return;
        
        CreateBeat(1);
    }

    private void OnLeft(InputValue input)
    {

        if (gameTurn != attackTurn || !input.isPressed)
            return;
        
        CreateBeat(2);
    }
    
    private void OnRight(InputValue input)
    {

        if (gameTurn != attackTurn || !input.isPressed)
            return;
        
        CreateBeat(3);
    }
    private GameObject JudgeHitTime()
    {
        GameObject attchItem = null;
        Collider[] hitCollider = Physics.OverlapBox(JusgeArea.transform.position, JusgeArea.transform.localScale);
        if (hitCollider.Length > 0)
        {
            for (int i = 0; i < hitCollider.Length; i++)
            {
                EmptyBeatMoveSystem emptyBeatMoveSystem;
                if (hitCollider[i].TryGetComponent<EmptyBeatMoveSystem>(out emptyBeatMoveSystem))
                {
                    if (emptyBeatMoveSystem.GetPlayerIndex() == attackTurn)
                    {
                        attchItem = hitCollider[i].gameObject;
                    }
                }
            }
        }
        return attchItem;
    }
    public void CreateBeat(int beatIndex)
    {
        Debug.Log(GameSystem.BeatDeltaTime * GameSystem.inputDelayFactor);
        if (isAttack) return;
        isAttack = true;
        _attackStartTime = Time.time;
        GameObject attchEmptyBeat = JudgeHitTime();
        if (attchEmptyBeat != null)
        {
            GameObject newBeat = Instantiate(playerShootBeatOBJ[beatIndex], attchEmptyBeat.transform.position, new Quaternion(0, 0, 0, 0));
            BeatMoveSystem beatMoveSystem = newBeat.GetComponent<BeatMoveSystem>();
            beatMoveSystem.StartMoveBeat(moveWay, playerAttack);
            _attackStartTime = Time.time;
            if (OnInputOK != null) OnInputOK.Invoke();
        }
        else 
        {
            if (OnInputFail != null) OnInputFail.Invoke();
        }
        isAttack = true;
    }
    
    private void Update()
    {
        if (isAttack && Time.time - _attackStartTime >= GameSystem.BeatDeltaTime * GameSystem.inputDelayFactor)
        {
            isAttack =false;
        }
    }
}
