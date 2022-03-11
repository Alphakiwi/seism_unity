using UnityEngine;
using System.Collections;

public class ThrowObject : MonoBehaviour {
	public float force = 300f;
	private float update;
	private float direction = 0f;


	void OnGUI()
	{
		force = GUI.HorizontalSlider(new Rect(25, 25, 800, 200), force, 0.0F, 1200.0F);
		if (GUI.Button(new Rect(600,250,180,120),"Lancer le séisme"))

		 {
			while (direction < 1f)
			{
				Throw(direction,0,1-direction);
				Throw(-direction,0,1-direction);
				Throw(direction,0,-(1-direction));
				Throw(-direction,0,-(1-direction));
				direction += 0.05f;
			}
			direction = 0;
		}
	}


	void Update(){
	}

	// Create a sphere and throw it
	void Throw(float x, float y, float z){
		GameObject go = GameObject.CreatePrimitive(PrimitiveType.Sphere);
		go.name = "Onde";
		go.transform.localScale = new Vector3(0.5f,0.5f,0.5f);
		go.transform.position = transform.position + new Vector3(x,y,z);
		go.GetComponent<Renderer>().enabled = false;
		go.AddComponent<Rigidbody>();
		go.GetComponent<Rigidbody>().AddForce(new Vector3(x,y,z)* 5*force);
		go.AddComponent<DWGDestroyer>().force = force;
		Destroy(go,500f);

	}
}
