using UnityEngine;
using System.Collections;

public class OneLinePerFunction : MonoBehaviour {
	
	void Update () { Move(); }
	void Move () { transform.position += new Vector3(Left() + Right(), 0, Forward() + Back()) * Time.deltaTime * MovementSpeed(); }

	float MovementSpeed() { return 10f; }

	float Left		() { return Input.GetKey(KeyCode.A) ? -1 : 0; }
	float Right		() { return Input.GetKey(KeyCode.D) ? 1 : 0; }
	float Forward	() { return Input.GetKey(KeyCode.W) ? 1 : 0; }
	float Back		() { return Input.GetKey(KeyCode.S) ? -1 : 0; }
}
