using UnityEngine;
using System.Collections;

public class PlanetProperties : MonoBehaviour {
	
	public bool orbiting;
	public GameObject orbitingAround;
    public float orbitAngle;

	//public for debug
	public float startDist;
	public float currentDist;
	public float maxDist;
	public float minDist;
	public float avarageDist;

	Rigidbody rb;


	void Awake () {

		rb = GetComponent<Rigidbody>();

		if (GameObject.Find("Star") != null) {
			orbitingAround = GameObject.Find("Star").transform.FindChild("GravitySource").gameObject;
		}
	}

    void Start() {

        if (orbiting) {
            Orbit();
        }
    }

    void Update() {

        if (Input.GetKeyDown(KeyCode.G)) {
            Orbit();
        }
    }

    void Orbit() {

        float mass = orbitingAround.GetComponent<Attraction>().mass1;

        Vector3 dir = (orbitingAround.transform.position - transform.position).normalized;
        startDist = Vector2.Distance(transform.position, orbitingAround.transform.position);
        Vector3 newDir = Quaternion.AngleAxis(orbitAngle, Vector3.forward) * dir * Mathf.Sqrt(mass / startDist);

        rb.velocity = newDir;

        minDist = startDist;

    }

	void FixedUpdate () {

		if (orbiting) {

            //Debug.DrawRay(transform.position, newDir, Color.blue);

			currentDist = Vector2.Distance(transform.position, orbitingAround.transform.position);

			if (maxDist < currentDist) {
				maxDist = currentDist;
			}
			if (currentDist < minDist) {
				minDist = currentDist;
			}
			avarageDist = (minDist + maxDist) / 2;
		}
	}

    void OnTriggerStay(Collider c) {
        //Workaround for OnTriggerStay not working on child objects
    }
}
