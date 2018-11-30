using System.Collections;
using System.Collections.Generic;
using EasyButtons;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// To be placed on every game level.
///    
/// Not implemented as a Singleton because Unity allows multiple scenes to be loaded simultaneously.
/// A singleton implementation could become catastrophic!
/// </summary>
public class LevelManager : MonoBehaviour
{
    /// <summary>
    /// A static reference to the last loaded LevelManager
    /// </summary>
    [HideInInspector] public static LevelManager Instance;

    public string MainMenuSceneName = "";
    public GameObject GameWonScreen;
    public GameObject GameLostScreen;

    public AudioClip WinSequenceAudioClip;
    public float WinSequenceAudioVolume = 0.01f;
    public float WinSequenceAudioStart = 0.8f;


    /// <summary>
    /// Although not needed, a private reference to the GameManager, just to display the GameManager's data in the inspector.
    /// </summary>
    [SerializeField]
    private GameManager _gameManager = GameManager.Instance;

    public bool _isPlaying;
    private GameObject _playerGameObject;

    private AudioSource audioSource;

    #region Unity API

    protected void OnValidate()
    {
        _gameManager.GetProfileList();  // Just to force the profile list to be loaded in the inspector.
    }

    protected void Awake()
    {
        Instance = this;

        Time.timeScale = 1;

        LoadGameData();
        _gameManager.GetProfileList();  // Just to force the profile list to be loaded in the inspector.

        _isPlaying = true;

        _playerGameObject = GameObject.FindWithTag("Player");
        audioSource = GetComponent<AudioSource>();
    }

    // Use this for initialization
    protected void Start ()
    {

    }

    // Update is called once per frame
    protected void Update ()
    {
        float playerHealth = _playerGameObject.GetComponent<PlayerModel>().GetHealth();
        if (playerHealth <= 0)
        {
            GameLost();
        }
    }
    #endregion

    public GameObject GetPlayerGameObject()
    {
        return _playerGameObject;
    }

    public void LogMessage(string msg)
    {
        Debug.Log(msg);
    }

    [Button]
    void SaveGameData()
    {
        _gameManager.SaveGameData();
    }

    [Button]
    void LoadGameData()
    {
        _gameManager.LoadGameData();
    }

    [Button]
    void ClearGameData()
    {
        _gameManager.ClearGameData(true);
    }

    public void GameWon()
    {
        if (_isPlaying)
        {
            StartCoroutine(PlayGameWonSequence());
        }
    }

    void GameLost()
    {
        if (_isPlaying)
        {
            _isPlaying = false;

            GameLostScreen.SetActive(true);
            Time.timeScale = 0;

            StartCoroutine(WaitAndGoToMainMenu(3f));
        }
    }

    IEnumerator PlayGameWonSequence()
    {
        _isPlaying = false;

        audioSource.Stop();
        audioSource.clip = WinSequenceAudioClip;
        audioSource.volume = WinSequenceAudioVolume;
        audioSource.time = WinSequenceAudioStart;
        audioSource.Play();

        yield return new WaitForSecondsRealtime(7.5f);

        Time.timeScale = 0;
        GameWonScreen.SetActive(true);

        StartCoroutine(WaitAndGoToMainMenu(6.5f));
    }

    IEnumerator WaitAndGoToMainMenu(float sec)
    {
        yield return new WaitForSecondsRealtime(sec);
        SceneManager.LoadScene(MainMenuSceneName);
    }
}
