using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {

	GameObject Characters;
	GameObject MainMenu;
	GameObject LevelMenu;

	void Start() {

	}

	public void OnClickStart() {
		SceneManager.LoadScene ("Level4");
	}

	public void OnClickQuit() {
		Application.Quit ();
	}
}
