using UnityEngine;
using System.Collections;

public class Rocket : MonoBehaviour {

	GameObject go;
	void Awake()
	{
		gameObject.GetComponent<Animator> ().enabled = false;
	}
	
	void Start () {
		
		Vector3 nw =new Vector3(0,1.5F,0);
		gameObject.GetComponent<Rigidbody2D>().velocity = nw * 2;
	}
}
