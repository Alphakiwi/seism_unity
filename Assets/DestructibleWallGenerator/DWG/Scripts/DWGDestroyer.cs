using UnityEngine;
using System.Collections;

public class DWGDestroyer : MonoBehaviour {

	public float force;

	void OnCollisionEnter(Collision col){
			ExplodeForce();
			Destroy(GetComponent<DWGDestroyer>());
	}

	// Explode force by radius only if a destructible tag is found
	void ExplodeForce(){
		Vector3 explodePos = transform.position;
		Collider[] colliders = Physics.OverlapSphere(explodePos, force/100);
		Collider[] collidersWood = Physics.OverlapSphere(explodePos, force/30);
		Collider[] collidersRock = Physics.OverlapSphere(explodePos, force/60);
		foreach (Collider hit in collidersRock){
			if(hit.GetComponent<Collider>().tag == "Destructible" & hit.GetComponent<Collider>().name == "Rock"){
				if(hit.GetComponent<Rigidbody>()){
					hit.GetComponent<Rigidbody>().isKinematic = false;
					hit.GetComponent<Rigidbody>().AddExplosionForce(force, explodePos, force/30);
				}
			}
		}
		foreach (Collider hit in collidersWood){
			if(hit.GetComponent<Collider>().tag == "Destructible" & hit.GetComponent<Collider>().name == "Wood"){
				if(hit.GetComponent<Rigidbody>()){
					hit.GetComponent<Rigidbody>().isKinematic = false;
					hit.GetComponent<Rigidbody>().AddExplosionForce(force, explodePos, force/10);
				}
			}
		}
		foreach (Collider hit in colliders){
		if(hit.GetComponent<Collider>().tag == "Destructible" & hit.GetComponent<Collider>().name == "Brick"){
			if(hit.GetComponent<Rigidbody>()){
				hit.GetComponent<Rigidbody>().isKinematic = false;
				hit.GetComponent<Rigidbody>().AddExplosionForce(force, explodePos, force/50);
		}
			}
		}
	}
}
