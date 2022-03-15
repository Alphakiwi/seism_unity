using UnityEngine;
using System.Collections;
using System.Linq;

public class ThrowObject : MonoBehaviour {
	public float force = 0f;
	public float force_final_gui = 300f;
	public float force_final = 300f;
	private float update;
	private float direction = 0f;
	public float distance_hypocentre = 0f;


	void Start(){
		GameObject epicentre = GameObject.Find("Epicentre");
		epicentre.GetComponent<Renderer>().enabled = false;
	}

	void OnGUI()
	{
		force_final_gui = GUI.HorizontalSlider(new Rect(25, 25, 800, 25), force_final_gui, 0.0F, 1200.0F);
		distance_hypocentre = GUI.HorizontalSlider(new Rect(25, 50, 800, 25), distance_hypocentre, 0.0F, 30.0F);

		GameObject hypocentre = GameObject.Find("Hypocentre");
		hypocentre.transform.position = transform.position + new Vector3(0, -distance_hypocentre, 0);

		if (GUI.Button(new Rect(600,250,180,120),"Lancer le séisme"))
		 {
			force_final += force_final_gui + 300 - distance_hypocentre*10;
			force = force_final/4;
			StartCoroutine(DestroyWallsPause());
			}
	}

	IEnumerator DestroyWallsPause()
    {
			float decalage = 0f;
			var components =  Resources.FindObjectsOfTypeAll<GameObject>().Where(obj => obj.name == "Wall");
			while (force < force_final){
				foreach (GameObject comp in components){
					comp.AddComponent<CameraShake>().shakeAmount = force/1000;
				}
				yield return new WaitForSeconds(1.25f);
				while (direction < 1.0f)
				{
						Throw(direction,0,1-direction);
						Throw(-direction,0,1-direction);
						Throw(direction,0,-(1-direction));
						Throw(-direction,0,-(1-direction));
						direction += 0.1f;
				}
					decalage += 0.03f;
					direction = 0 + decalage;
					force += 100;
					if (force_final < force) { force = force_final;}

			}

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
