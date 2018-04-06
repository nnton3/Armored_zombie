using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
	
	//Сцены
	public static int SelectedScene;       //Запущенная сцена

	//Прогресс прохождения
	public static int progress = 1;

	void Awake() {
		DontDestroyOnLoad (transform.gameObject);
	}

	void Start() {
		
	}

	public static void SaveGame() {
		PlayerPrefs.SetInt("progress", progress);
	}

	public static void LoadGame() {
		PlayerPrefs.GetInt("progress", progress);
	}

	public static void LoadNewLvl() {
		SceneManager.LoadScene ("Level" + (SelectedScene + 1));
	}
}
