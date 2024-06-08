using UnityEngine;
using System.Collections;

public class AutoDestroyParticles : MonoBehaviour {

	private ParticleSystem thisParticleSystem ;


	void Start () {

		thisParticleSystem = this.GetComponent<ParticleSystem>();		
		if (!thisParticleSystem.loop) {
			Destroy(this.gameObject, thisParticleSystem.duration);
		}
	}
	

}
