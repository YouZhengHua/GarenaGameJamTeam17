using UnityEngine;

public class InputControl : MonoBehaviour
{
    [SerializeField] GameObject[] player1ShootBeatOBJ;
    [SerializeField] GameObject[] player2ShootBeatOBJ;
    [SerializeField] float player1AttPositionX = -19f;
    [SerializeField] float player2AttPositionX = 19f;
    [SerializeField] float attackY = 1f;
    [SerializeField] float player1Attack = 3f;
    [SerializeField] float player2Attack = 3f;
    private int gameTurn = 1;
    private bool isAttack = false;
    private float _attackStartTime = 0f;

    public int GetCurrentGameTurn() { return gameTurn; }
    private void Start()
    {
        GameSystem.BeatValue = 0f;
    }
    private void Update()
    {
        if (isAttack)
        {
            if (Time.time - _attackStartTime > GameSystem.AttackDelay) isAttack = false;
        }
        else
        {
            if (gameTurn == 1)
            {
                if (Input.GetKeyDown(KeyCode.W))
                {
                    GameObject newBeat = Instantiate(player1ShootBeatOBJ[0], new Vector3(player1AttPositionX, attackY, 0f), new Quaternion(0, 0, 0, 0));
                    BeatMoveSystem beatMoveSystem = newBeat.GetComponent<BeatMoveSystem>();
                    beatMoveSystem.StartMoveBeat(1, player1Attack);
                    _attackStartTime = Time.time;
                    isAttack = true;
                }
                
                if (Input.GetKeyDown(KeyCode.S))
                {
                    GameObject newBeat = Instantiate(player1ShootBeatOBJ[1], new Vector3(player1AttPositionX, attackY, 0f), new Quaternion(0, 0, 0, 0));
                    BeatMoveSystem beatMoveSystem = newBeat.GetComponent<BeatMoveSystem>();
                    beatMoveSystem.StartMoveBeat(1, player1Attack);
                    _attackStartTime = Time.time;
                    isAttack = true;
                }
                
                if (Input.GetKeyDown(KeyCode.A))
                {
                    GameObject newBeat = Instantiate(player1ShootBeatOBJ[2], new Vector3(player1AttPositionX, attackY, 0f), new Quaternion(0, 0, 0, 0));
                    BeatMoveSystem beatMoveSystem = newBeat.GetComponent<BeatMoveSystem>();
                    beatMoveSystem.StartMoveBeat(1, player1Attack);
                    _attackStartTime = Time.time;
                    isAttack = true;
                }
                
                if (Input.GetKeyDown(KeyCode.D))
                {
                    GameObject newBeat = Instantiate(player1ShootBeatOBJ[3], new Vector3(player1AttPositionX, attackY, 0f), new Quaternion(0, 0, 0, 0));
                    BeatMoveSystem beatMoveSystem = newBeat.GetComponent<BeatMoveSystem>();
                    beatMoveSystem.StartMoveBeat(1, player1Attack);
                    _attackStartTime = Time.time;
                    isAttack = true;
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    GameObject newBeat = Instantiate(player2ShootBeatOBJ[0], new Vector3(player2AttPositionX, attackY, 0f), new Quaternion(0, 0, 0, 0));
                    BeatMoveSystem beatMoveSystem = newBeat.GetComponent<BeatMoveSystem>();
                    beatMoveSystem.StartMoveBeat(-1, player2Attack);
                    _attackStartTime = Time.time;
                    isAttack = true;
                }
                if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    GameObject newBeat = Instantiate(player2ShootBeatOBJ[1], new Vector3(player2AttPositionX, attackY, 0f), new Quaternion(0, 0, 0, 0));
                    BeatMoveSystem beatMoveSystem = newBeat.GetComponent<BeatMoveSystem>();
                    beatMoveSystem.StartMoveBeat(-1, player2Attack);
                    _attackStartTime = Time.time;
                    isAttack = true;
                }
                if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    GameObject newBeat = Instantiate(player2ShootBeatOBJ[2], new Vector3(player2AttPositionX, attackY, 0f), new Quaternion(0, 0, 0, 0));
                    BeatMoveSystem beatMoveSystem = newBeat.GetComponent<BeatMoveSystem>();
                    beatMoveSystem.StartMoveBeat(-1, player2Attack);
                    _attackStartTime = Time.time;
                    isAttack = true;
                }
                if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    GameObject newBeat = Instantiate(player2ShootBeatOBJ[3], new Vector3(player2AttPositionX, attackY, 0f), new Quaternion(0, 0, 0, 0));
                    BeatMoveSystem beatMoveSystem = newBeat.GetComponent<BeatMoveSystem>();
                    beatMoveSystem.StartMoveBeat(-1, player2Attack);
                    _attackStartTime = Time.time;
                    isAttack = true;
                }

            }
        }

    }

}
