using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ObjectAlert : MonoBehaviour {

	public GameObject aObject;
	public float arrowPosFix;
	GameObject player;
	public Text arrowText;

	Vector3 aObjectPos;
	Vector2 arrowPosition;
	Vector2 aObjectScreenPos;

	void Awake () {
		player = GameObject.Find("Player");
	}

	void Update () {

		if (aObject != null) {
			aObjectPos = aObject.transform.position;
			aObjectScreenPos = Camera.main.WorldToScreenPoint(aObjectPos);

			Vector3 planetScreenPoint = Camera.main.WorldToScreenPoint(aObjectPos);
			arrowPosition = new Vector2(planetScreenPoint.x, Camera.main.pixelHeight - (Camera.main.pixelHeight / arrowPosFix));

			if (aObjectScreenPos.y < Camera.main.pixelHeight) {
				Destroy(gameObject);
			}
			arrowText.text = "" + Mathf.Round(Vector3.Distance(player.transform.position, aObject.transform.position) / 10) * 10;
			transform.position = arrowPosition;
		} else {
			Destroy(gameObject);
		}
	}
}