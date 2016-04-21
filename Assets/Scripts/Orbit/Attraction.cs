using UnityEngine;
using System.Collections;

public class Attraction : MonoBehaviour {

	public float mass1;
	float mass2;
	float force;
	float playerDist;
	float closestDist = Mathf.Infinity;

	Vector3 direction;
	public bool oneSideGravity;

	Rigidbody rb;
	GameObject gs;

	void Awake () {

		//Sets visual gravity radius if GravitySources parent is a star

		if (gameObject.transform.parent.tag == "ObjectNoArrow") {
			GameObject gravitySemiCircle = transform.FindChild("PlaceholderGravityRadiusHalf").gameObject;
			GameObject gravityCircle = transform.FindChild("PlaceholderGravityRadius").gameObject;

			float radius = gameObject.GetComponent<SphereCollider>().radius;
			gravityCircle.transform.localScale = new Vector3(radius / 9.85f, radius / 9.85f, 1); //"radius / 9.85f" to compensate for 2048 resolution sprites with blank space
			gravitySemiCircle.transform.localScale = new Vector3(radius, radius, 1);

			if (oneSideGravity) {
				gravityCircle.SetActive (false);
			} else {
				gravitySemiCircle.SetActive(false);
			}
		}
	}

    void Gravity(GameObject g) {

        rb = g.GetComponent<Rigidbody>();

        if (g.transform.FindChild("GravitySource") != null) {
            gs = g.transform.FindChild("GravitySource").gameObject;
            mass2 = gs.GetComponent<Attraction>().mass1;
        }

        if (gs != null) {
            float dist = Vector2.Distance(g.transform.position, transform.position);
			force = (mass1 * mass2) / (dist * dist);
		}

        direction = (transform.position - g.transform.position).normalized * force;

		if (oneSideGravity) {
			Vector3 gToThis = g.transform.position - transform.position;
			if (gToThis.y < 0) {
				rb.AddForce(direction);
			}
		}

		if (!oneSideGravity) {
			rb.AddForce(direction);
		}
	}
	void OnTriggerEnter (Collider c) {
		if (c.tag == "Star" || c.tag == "Planet" || c.tag == "Player" && c.gameObject != gameObject.transform.parent.gameObject) {
			//Dist reset
			closestDist = Mathf.Infinity;
		}
	}

	void OnTriggerStay (Collider c) {
		if (c.tag == "Star" || c.tag == "Planet" || c.tag == "Player" && c.gameObject != gameObject.transform.parent.gameObject) {
			//Score
			Vector3 playerDistVec = transform.position - c.transform.position;
			playerDist = playerDistVec.magnitude;
			if (playerDist < closestDist) {
				closestDist = playerDist;
			}
			Gravity (c.gameObject);
		}
	}
}
