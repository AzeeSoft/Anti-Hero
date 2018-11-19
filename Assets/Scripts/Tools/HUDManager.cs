using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    public Image HealthBarFiller;
    public Image DashBarFiller;

    private PlayerModel _playerModel;

    // Use this for initialization
    void Start()
    {
        _playerModel = LevelManager.Instance.GetPlayerGameObject().GetComponent<PlayerModel>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHealthBar();
        UpdateDashBar();
    }

    void UpdateHealthBar()
    {
        HealthBarFiller.fillAmount = _playerModel.GetHealth() / _playerModel.MaxHealth;
    }

    void UpdateDashBar()
    {
        DashBarFiller.fillAmount = _playerModel.GetAvailableDash() / _playerModel.GetTotalDash();
    }
}