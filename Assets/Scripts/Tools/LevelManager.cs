﻿using System.Collections;
using System.Collections.Generic;
using EasyButtons;
using UnityEngine;

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

    /// <summary>
    /// Although not needed, a private reference to the GameManager, just to display the GameManager's data in the inspector.
    /// </summary>
    [SerializeField]
    private GameManager _gameManager = GameManager.Instance;

    private bool _isPlaying;
    private GameObject _playerGameObject;

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
    }

    // Use this for initialization
    protected void Start ()
    {

    }

    // Update is called once per frame
    protected void Update () {
		
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
}