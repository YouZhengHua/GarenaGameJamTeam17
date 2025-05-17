using UnityEngine;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine.Events;
using UnityEngine.UI;

public class MusicController : MonoBehaviour
{
    [SerializeField] MusicLevelList musicLevelList;
    [SerializeField] GameObject emptyBeatOBJ;
    [SerializeField] AudioSource emptyAudio;
    [SerializeField] AudioSource gameBGM;
    [SerializeField] int totalMusicLevel;
    [SerializeField] int[] everyLevelRoundCount;
    [SerializeField] float player1EmptyBeatStartPoint = -39f;
    [SerializeField] float player2EmptyBeatStartPoint = 39f;
    [SerializeField] UnityEvent OnGameOver;
    [SerializeField] List<string> _beatList;
    [SerializeField] Text roundText;
    [SerializeField] Text levelText;

    private bool _isGameStart = true;
    private int _currentGameTurn = 1;
    private int _currentLevel = 0;
    private int _currentLevelRoundCount = 0;
    private int _currentBeatIndex = 0;
    private int _currentRoundTotalBeatCount = 0;
    private int _currentRoundTimes = 0;
    private float _beatDeltaTime = 0;
    private float _currentRoundTime = 0;
    private float _roundStartTime = 0f;
    
    public int GetCurrentGameTurn() { return _currentGameTurn; }
    public void GameStart()
    {
        if (gameBGM != null) gameBGM.Play();
        _isGameStart = true;
    }
    public void CreateEmptyBeat()
    {
        if (_currentGameTurn == 1)
        {
            GameObject newEmtyBeat = Instantiate(emptyBeatOBJ, new Vector3(player1EmptyBeatStartPoint, 2f, 5f), new Quaternion(0, 0, 0, 0));
            EmptyBeatMoveSystem emptyBeatMoveSystem = newEmtyBeat.GetComponent<EmptyBeatMoveSystem>();
            emptyBeatMoveSystem.StartMoveBeat(1);
        }
        else
        {
            GameObject newEmtyBeat = Instantiate(emptyBeatOBJ, new Vector3(player2EmptyBeatStartPoint, 2f, 5f), new Quaternion(0, 0, 0, 0));
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
    }
    public void NextRound()
    {
        _currentLevelRoundCount ++;
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
    private void Start()
    {
        if (gameBGM != null) gameBGM.Play();
        _currentLevel = 0;
        _currentLevelRoundCount = 0;
        RoundInitial();
    }
    private void Update()
    {
        if (!_isGameStart) return;

        if (Time.time - _roundStartTime >= _beatDeltaTime * (_currentBeatIndex + _currentRoundTotalBeatCount * _currentRoundTimes))
        {
            if (_beatList[_currentBeatIndex] == "1")
            {
                if (_currentRoundTimes == 0) CreateEmptyBeat();
                emptyAudio.Play();
            }
            _currentBeatIndex ++;
            if (_currentBeatIndex >= _currentRoundTotalBeatCount)
            {
                _currentBeatIndex = 0;
                _currentRoundTimes++;
                if (_currentRoundTimes > 4)
                {
                    NextRound();
                    return;
                }
            }
        }
        roundText.text = "Round:" + _currentLevelRoundCount.ToString();
        levelText.text = "Level:" + _currentLevel.ToString();

    }





}
