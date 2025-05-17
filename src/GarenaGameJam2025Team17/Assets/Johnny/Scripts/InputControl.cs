using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;
public class InputControl : MonoBehaviour
{
    [SerializeField] GameObject[] playerShootBeatOBJ; 
    [SerializeField] float playerAttPositionX = -19f;
    [SerializeField] float attackY = 1f;
    [SerializeField] float playerAttack = 3f;
    [SerializeField] private int gameTurn = 1;
    private bool isAttack = false;
    private float _attackStartTime = 0f;
    [SerializeField] private int moveWay = 1;

    [SerializeField] private int attackTurn = 1;

    public int GetCurrentGameTurn() { return gameTurn; }

    public void ChangeTurn()
    {
        if (gameTurn == 1) gameTurn = 2;
        else gameTurn = 1;
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

    public void CreateBeat(int beatIndex)
    {
        GameObject newBeat = Instantiate(playerShootBeatOBJ[beatIndex], new Vector3(playerAttPositionX, attackY, 0f), new Quaternion(0, 0, 0, 0));
        BeatMoveSystem beatMoveSystem = newBeat.GetComponent<BeatMoveSystem>();
        beatMoveSystem.StartMoveBeat(moveWay, playerAttack);
        _attackStartTime = Time.time;
        isAttack = true;
    }
    
    private void Update()
    {
        if (isAttack)
        {
            if (Time.time - _attackStartTime > GameSystem.AttackDelay) isAttack = false;
        }
        else
        {
        }
    }
}
