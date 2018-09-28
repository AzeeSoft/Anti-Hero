using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {
	public string  NextScene;

	public void StartGame()
	{
		SceneManager.LoadScene(NextScene);
	}
	
	public void QuitGame()
	{
		Debug.Log("You tried to Quit");
		Application.Quit();
	}
}
