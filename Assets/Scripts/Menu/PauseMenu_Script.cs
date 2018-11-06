using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu_Script : MonoBehaviour {

	public static bool GameIsPaused = false;
	
	public string MenuScene;
	
	public GameObject pauseMenuUI;
	
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			if (GameIsPaused)
			{
				Resume();
			}else
			{
				Pause();
			}
		}
	}
	
	public void Resume()
	{
		pauseMenuUI.SetActive(false);
		Time.timeScale = 1f;
		GameIsPaused = false;
	}
	
	void Pause()
	{
		pauseMenuUI.SetActive(true);
		Time.timeScale = 0f;
		GameIsPaused = true;
	}
	
	public void LoadMenu()
	{
		Time.timeScale = 1f;
		GameIsPaused = false;
		SceneManager.LoadScene(MenuScene);
	}
	
	public void QuitGame()
	{
		Debug.Log("You tried to Quit");
		Application.Quit();
	}
}
