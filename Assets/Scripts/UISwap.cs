using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISwap : MonoBehaviour {

	//переменная для определения направления персонажа вправо/влево
	[HideInInspector]
	public bool isFacingRight = true;
	public miniBossOgr Ogre;

	void Start () {
		if (Ogre.GetComponent<Flip>().isFacingRight != isFacingRight) {
			Flip ();
		}
	}
	
	void Update () {
		if (Ogre.GetComponent<Flip>().isFacingRight != isFacingRight) {
			Flip ();
		}
	}

	public void Flip()
	{
		//меняем направление движения персонажа
		isFacingRight = !isFacingRight;
		//получаем размеры персонажа
		Vector3 theScale = transform.localScale;
		//зеркально отражаем персонажа по оси Х
		theScale.x *= -1;
		//задаем новый размер персонажа, равный старому, но зеркально отраженный
		transform.localScale = theScale;
	}
}
