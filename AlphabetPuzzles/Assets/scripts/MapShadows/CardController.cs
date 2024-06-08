using UnityEngine;
using System.Collections;

public class CardController : MonoBehaviour {

	void OnMouseDown()
    {
        if (!gameObject.GetComponent<AudioSource>().isPlaying)
        gameObject.GetComponent<AudioSource>().Play();
    }
}
