using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBar : ProgressBar {

	/*UnitParams playerParams;

	void Start () {
		playerParams = GameManager.player.GetComponent<UnitParams> ();
	}
	
	void Update ()
	{
		if (playerParams.Health != currentPercent) {
			isOn = true;
			if (playerParams.Health < currentPercent && Mathf.Abs(playerParams.Health - currentPercent) > 3f) {
				currentPercent -= speed * Time.deltaTime;
			} else if (playerParams.Health > currentPercent && Mathf.Abs(playerParams.Health - currentPercent) > 3f) {
				currentPercent += speed * Time.deltaTime;
			}
		} else {
			isOn = false;
		}
		loadingBar.GetComponent<Image> ().fillAmount = currentPercent / 100;
	}*/
}
