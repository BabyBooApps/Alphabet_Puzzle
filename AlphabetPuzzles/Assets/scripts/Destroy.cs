using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Destroy : MonoBehaviour {

	AudioSource source;
	GameObject go;

	public Dictionary<int, Texture> inventoryItems = new Dictionary<int, Texture> ();

	// Use this for initialization
	void Start () {

		//gameObject.GetComponent<Animator> ().enabled = false;

		/**********this gives random velocity**********/
		//Vector3 nw =new Vector3(0,UnityEngine.Random.Range(2.0F,6.0f) ,0);
		Vector3 nw =new Vector3(0,3.0F,0);
		gameObject.GetComponent<Rigidbody2D>().velocity = nw ;


		/**********this gives acceleration**********/
		gameObject.GetComponent<Rigidbody2D> ().gravityScale = -0.025f;

		/*****this gives constant speed********/
		//Vector3 nw =new Vector3(0,1,0);
		//gameObject.GetComponent<Rigidbody2D>().velocity = nw * 2;

		//int balloonId = UnityEngine.Random.Range (1,5);
		//gameObject.GetComponent<SpriteRenderer> ().sprite = Resources.Load<Sprite>("balloon_" +balloonId);

		//source = Camera.main.gameObject.GetComponent<AudioSource>();
			
	}


}
