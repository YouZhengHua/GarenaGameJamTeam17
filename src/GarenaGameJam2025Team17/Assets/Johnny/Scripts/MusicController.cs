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
            emptyBeatMoveSystem.StartMoveBeat(1);
        }
        else
        {
            GameObject newEmtyBeat = Instantiate(emptyBeatOBJ, new Vector3(player2EmptyBeatStartPoint, 2f, 0f), new Quaternion(0, 0, 0, 0));
            EmptyBeatMoveSystem emptyBeatMoveSystem = newEmtyBeat.GetComponent<EmptyBeatMoveSystem>();
            emptyBeatMoveSystem.StartMoveBeat(-1);
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

        GameSystem.BeatSpeed = 19/ _currentRoundTime;
        _beatDeltaTime = _currentRoundTime / _currentRoundTotalBeatCount;

        _beatList.Clear();
        foreach (char character in sp1[1])
        {
            _beatList.Add(character.ToString());
        }
        _currentBeatIndex = 0;
        _currentRoundTimes = 0;
        _roundStartTime = Time.time;
        if (!_isGameStart) _isGameStart = true;
        UpdateStatus();
    }
    public void NextRound()
    {
        _currentLevelRoundCount ++;
        _currentActualRound++;
        if (_currentLevelRoundCount > everyLevelRoundCount[_currentLevel])
        {
            NextLevel();
            return;
        }
        RoundInitial();
    }
    public void NextLevel()
    {
        _currentLevel++;
        if (_currentLevel > totalMusicLevel)
        {
            if (OnGameOver != null) OnGameOver.Invoke();
            _isGameStart = false;
            return;
        }
        _currentLevelRoundCount = 0;
        RoundInitial();
    }
    public void ChangeTurn()
    {
        if (_currentGameTurn == 1) _currentGameTurn = 2;
        else _currentGameTurn = 1;
        UpdateAttackWayUI();
    }
    public void ChangeTurnMusic()
    {
        if (_currentGameTurn == 1)
        {
            player2MainBGM.Stop();
            player1MainBGM.Play();
        }
        else
        {
            player1MainBGM.Stop();
            player1MainBGM.Play();
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
    private void Start()
    {
        UpdateStatus();
        UpdateAttackWayUI();
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

        if (_currentRoundTimes == 0 && !LeasonHintOBJ.activeSelf) LeasonHintOBJ.SetActive(true);

        if (Time.time - _roundStartTime >= _beatDeltaTime * (_currentBeatIndex + _currentRoundTotalBeatCount * _currentRoundTimes))
        {
            if (_currentRoundTimes == 1 || _currentRoundTimes == 5)
            {
                float leftTime = (_currentRoundTotalBeatCount - _currentBeatIndex) * _beatDeltaTime;

                if (leftTime>0.49999f && leftTime<0.50001f) threeTwoOneAudio[0].Play();
                if (leftTime > 0.99999f && leftTime < 1.00001f) threeTwoOneAudio[1].Play();
                if (leftTime > 1.49999f && leftTime < 1.50001f) threeTwoOneAudio[2].Play();
                if (leftTime > 1.99999f && leftTime < 2.00001f) threeTwoOneAudio[3].Play();
            }
            if (_beatList[_currentBeatIndex] == "1")
            {
                if (_currentRoundTimes == 1 || _currentRoundTimes == 5)  CreateEmptyBeat();
                emptyAudio.Play();
                beatVisualOBJ.SetActive(true);
            }
            _currentBeatIndex ++;
            
            if (_currentBeatIndex >= _currentRoundTotalBeatCount)
            {
                _currentBeatIndex = 0;
                _currentRoundTimes++;
                if (LeasonHintOBJ.activeSelf) LeasonHintOBJ.SetActive(false);
                if (_currentRoundTimes == 1) ChangeTurnMusic();
                if (_currentRoundTimes == 5)
                {
                    ChangeTurn();
                    ChangeTurnMusic();
                }
                    if (_currentRoundTimes == 9)
                {
                    ChangeTurn();
                    NextRound();
                }
            }
        }


    }
}
