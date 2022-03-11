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
		Collider[] colliders = Physics.OverlapSphere(explodePos, force/50);
		foreach (Collider hit in colliders){
			if(hit.GetComponent<Collider>().tag == "Destructible"){
				if(hit.GetComponent<Rigidbody>()){
					hit.GetComponent<Rigidbody>().isKinematic = false;
					hit.GetComponent<Rigidbody>().AddExplosionForce(force, explodePos, force/50);
				}
			}
		}
	}
}
