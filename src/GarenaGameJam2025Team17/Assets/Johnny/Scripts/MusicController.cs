using UnityEngine;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine.Events;
using UnityEngine.UI;

public class MusicController : MonoBehaviour
{
    [SerializeField] MusicLevelList musicLevelList;
    [SerializeField] GameObject emptyBeatOBJ;
    [SerializeField] GameObject beatVisualOBJ;
    [SerializeField] AudioSource emptyAudio;
    [SerializeField] AudioSource allBeatBGM;
    [SerializeField] AudioSource player1MainBGM;
    [SerializeField] AudioSource player2MainBGM;
    [SerializeField] int totalMusicLevel;
    [SerializeField] int[] everyLevelRoundCount;
    [SerializeField] float player1EmptyBeatStartPoint = -39f;
    [SerializeField] float player2EmptyBeatStartPoint = 39f;
    [SerializeField] UnityEvent OnGameOver;
    [SerializeField] List<string> _beatList;
    [SerializeField] List<AudioSource> threeTwoOneAudio;
    [SerializeField] Text roundText;
    [SerializeField] Text levelText;
    [SerializeField] GameObject StartUIGameOBJ;
    [SerializeField] Text startText;
    [SerializeField] GameObject LeasonHintOBJ;
    [SerializeField] GameObject AttackWayRightOBJ;
    [SerializeField] GameObject AttackWayLeftOBJ;

    private float oneTimeDistance = 19f;
    private float _gameStartCountdownTime = 0f;
    private bool _isGameStart = false;
    private bool _isGameStartCountdown = false;
    private int _currentGameTurn = 1;
    private int _currentLevel = 0;
    private int _currentLevelRoundCount = 0;
    private int _currentBeatIndex = 0;
    private int _currentRoundTotalBeatCount = 0;
    private int _currentRoundTimes = 0;
    private float _beatDeltaTime = 0;
    private float _currentRoundTime = 0;
    private float _roundStartTime = 0f;
    private float _currentActualRound = 0f;
    
    
    public int GetCurrentGameTurn() { return _currentGameTurn; }
 
    public void GameStart()
    {
        StartUIGameOBJ.SetActive(false);
        RoundInitial();
    }
    
    public void CreateEmptyBeat()
    {
        if (_currentGameTurn == 1)
        {
            GameObject newEmtyBeat = Instantiate(emptyBeatOBJ, new Vector3(player1EmptyBeatStartPoint, 2f, 0f), new Quaternion(0, 0, 0, 0));
            EmptyBeatMoveSystem emptyBeatMoveSystem = newEmtyBeat.GetComponent<EmptyBeatMoveSystem>();
            emptyBeatMoveSystem.StartMoveBeat(1, _currentGameTurn);
        }
        else
        {
            GameObject newEmtyBeat = Instantiate(emptyBeatOBJ, new Vector3(player2EmptyBeatStartPoint, 2f, 0f), new Quaternion(0, 0, 0, 0));
            EmptyBeatMoveSystem emptyBeatMoveSystem = newEmtyBeat.GetComponent<EmptyBeatMoveSystem>();
            emptyBeatMoveSystem.StartMoveBeat(-1, _currentGameTurn);
        }
    }

    public void RoundInitial()
    {
        MusicStringOBJ currentMusicStringOBJ = musicLevelList.musicStringOBJs[_currentLevel];
        int selectNumber = Random.Range(0, currentMusicStringOBJ.musicStrings.Count - 1);
        string currentRhythm = currentMusicStringOBJ.musicStrings[selectNumber];
        string[] sp1 = currentRhythm.Split(":", System.StringSplitOptions.None);
        _currentRoundTime = int.Parse(sp1[0]);
        _currentRoundTotalBeatCount = sp1[1].Length;

        GameSystem.BeatSpeed = oneTimeDistance / _currentRoundTime;
        _beatDeltaTime = _currentRoundTime / _currentRoundTotalBeatCount;
        GameSystem.BeatDeltaTime = _beatDeltaTime;
        _beatList.Clear();
        foreach (char character in sp1[1])
        {
            _beatList.Add(character.ToString());
        }
        _currentBeatIndex = 0;
        _currentRoundTimes = 0;
        _roundStartTime = Time.time;
        if (!_isGameStart) _isGameStart = true;
        if (!allBeatBGM.isPlaying) allBeatBGM.Play();
        ChangePlayerMusic(0);
        UpdateStatus();
    }
    public void NextRound()
    {
        if (allBeatBGM.isPlaying) allBeatBGM.Stop();
        _currentLevelRoundCount ++;
        _currentActualRound++;
        if (_currentLevelRoundCount > everyLevelRoundCount[_currentLevel])
        {
            _currentLevel++;
            _currentLevelRoundCount = 0;
            if (_currentLevel > totalMusicLevel)
            {
                if (OnGameOver != null) OnGameOver.Invoke();
                _isGameStart = false;
                return;
            }
        }
        ChangeTurn();
        RoundInitial();
    }

    public void GameEnd()
    {
        _isGameStart = false;
        emptyAudio.Stop();
        allBeatBGM.Stop();
        player1MainBGM.Stop();
        player2MainBGM.Stop();
    }
    
    public void ChangeTurn()
    {
        if (_currentGameTurn == 1) _currentGameTurn = 2;
        else _currentGameTurn = 1;
        UpdateAttackWayUI();
    }
    public void ChangePlayerMusic(int playerID)
    {
        if (playerID == 0)
        {
            player2MainBGM.Stop();
            player1MainBGM.Stop();
        }
        if (playerID == 1)
        {
            player2MainBGM.Stop();
            player1MainBGM.Play();
        }
        if (playerID == 2)
        {
            player1MainBGM.Stop();
            player2MainBGM.Play();
        }
    }
    public void UpdateStatus()
    {
        roundText.text = (_currentActualRound + 1).ToString();
        levelText.text = (_currentLevel + 1).ToString();
    }
    public void UpdateAttackWayUI()
    {
        if (_currentGameTurn == 1)
        {
            AttackWayRightOBJ.SetActive(true);
            AttackWayLeftOBJ.SetActive(false);   
        }
        else
        {
            AttackWayRightOBJ.SetActive(false);
            AttackWayLeftOBJ.SetActive(true);
        }
    }
    private void Awake()
    {
        _currentGameTurn = GameSystem.startGameTurn;
    }
    private void Start()
    {
        UpdateStatus();
        UpdateAttackWayUI();
        oneTimeDistance = (player2EmptyBeatStartPoint - player1EmptyBeatStartPoint) / 4;
        totalMusicLevel = everyLevelRoundCount.Length - 1;
        _gameStartCountdownTime = Time.time;
        _isGameStartCountdown = true;
    }
    private void Update()
    {
        if (_isGameStartCountdown)
        {
            startText.text = (3 - (Time.time - _gameStartCountdownTime)).ToString("0");
            if (Time.time - _gameStartCountdownTime > 3)
            {
                GameStart();
                _isGameStartCountdown = false;
            }
        }
        if (!_isGameStart) return;

        if (Time.time - _roundStartTime >= _beatDeltaTime * (_currentBeatIndex + _currentRoundTotalBeatCount * _currentRoundTimes))
        {
            if (_currentRoundTimes == 0)
            {
                float leftTime = (_currentRoundTotalBeatCount - _currentBeatIndex) * _beatDeltaTime;

                if (leftTime > 0.49999f && leftTime < 0.50001f) threeTwoOneAudio[0].Play();
                if (leftTime > 0.99999f && leftTime < 1.00001f) threeTwoOneAudio[1].Play();
                if (leftTime > 1.49999f && leftTime < 1.50001f) threeTwoOneAudio[2].Play();
                if (leftTime > 1.99999f && leftTime < 2.00001f) threeTwoOneAudio[3].Play();
            }
            if (_currentRoundTimes > 0 && _beatList[_currentBeatIndex] == "1")
            {
                if (_currentRoundTimes == 1)  CreateEmptyBeat();
                emptyAudio.Play();
                beatVisualOBJ.SetActive(true);
            }
            _currentBeatIndex ++;
            
            if (_currentBeatIndex >= _currentRoundTotalBeatCount)
            {
                _currentBeatIndex = 0;
                _currentRoundTimes++;
                if (_currentRoundTimes == 1)
                {
                    if (!LeasonHintOBJ.activeSelf) LeasonHintOBJ.SetActive(true);
                    ChangePlayerMusic(_currentGameTurn);
                }
                if (_currentRoundTimes == 2) if (LeasonHintOBJ.activeSelf) LeasonHintOBJ.SetActive(false);
                if (_currentRoundTimes == 3) ChangePlayerMusic(0);
                if (_currentRoundTimes == 4)
                {
                    if (_currentGameTurn == 1) ChangePlayerMusic(2);
                    else ChangePlayerMusic(1);
                }
                if (_currentRoundTimes == 5) NextRound();
            }
        }


    }
}
