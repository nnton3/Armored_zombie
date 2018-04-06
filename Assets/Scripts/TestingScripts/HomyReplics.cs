using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HomyReplics : MonoBehaviour {

	int i = 0;
	string phrase;
	public Text phrasesText;

	public void Phrases() {
		if (i < 3) {
			i++;
		} else
			i = 0;

		switch (i) {
		case 0:
			phrase = "А чо так долго?";
			IncreasePhrase ();
			break;
		case 1:
			phrase = "Когда игру выпустите?";
			IncreasePhrase ();
			break;
		case 2:
			phrase = "Скоро доделаете игру?";
			IncreasePhrase ();
			break;
		case 3:
			phrase = "Игру еще делаете?";
			IncreasePhrase ();
			break;
		}
	}

	void IncreasePhrase() {
		phrasesText.text = phrase;
	}
}
