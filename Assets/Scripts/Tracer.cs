using UnityEngine;
using System.Collections;

public class Tracer : MonoBehaviour {

	public Sprite output;

	void Update () {
		if (Input.GetKeyDown(KeyCode.Space)) {
			TraceRays();
		}
	}

	void TraceRays () {
		for (int x = 0; x < output.texture.width; x++) {
			for (int y = 0; y < output.texture.height; y++) {
				var screenPos = new Vector2(Camera.main.pixelWidth / output.texture.width * x, Camera.main.pixelHeight / output.texture.height * y);
				var pos = Camera.main.ScreenToWorldPoint(screenPos);
				//Debug.DrawLine(pos, new Vector3(pos.x, pos.y, -pos.z * 10f), Color.red, 10f);
				RaycastHit hit;
				if (Physics.Raycast(pos, new Vector3(pos.x, pos.y, -pos.z * 10f), out hit)) {
					var col = hit.transform.GetComponent<Renderer>().material.color;
					var normalColor = new Color(hit.normal.x, hit.normal.y, hit.normal.z);

					output.texture.SetPixel(x, y, normalColor);
				} else {
					output.texture.SetPixel(x, y, Color.black);
				}
			}
		}
		output.texture.Apply();
	}
}
