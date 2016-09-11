using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Minimap : MonoBehaviour {

	public GameObject enemiesParent;
	public GameObject enemyImage;
	public GameObject playerImage;
	public GameObject player;
	public float zoom;
	public float zoomSpeed;
	public bool fixedMap;

	GameObject[] enemies;
	List<Image> images = new List<Image>();

	Quaternion enemiesParentStart;
	Quaternion playerStart;
	
	void Start () {
		enemies = GameObject.FindGameObjectsWithTag("Enemy");
		for (int i = 0; i < enemies.Length; i++) {
			GameObject enemyImageIns = (GameObject)Instantiate(enemyImage);
			enemyImageIns.transform.SetParent(enemiesParent.transform, false);
			images.Add(enemyImageIns.GetComponent<Image>());
		}
		enemiesParentStart = enemiesParent.transform.rotation;
		playerStart = playerImage.transform.rotation;
	}
	
	void Update () {

		zoom += Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
		zoom = Mathf.Clamp(zoom, 1, 10);

		for (int i = 0; i < images.Count; i++) {
			Vector3 pos = enemies[i].transform.position - player.transform.position;
			images[i].rectTransform.localPosition = new Vector3(pos.x, pos.z) * zoom;
		}
		if (!fixedMap) {
			enemiesParent.transform.rotation = Quaternion.Euler(0, 0, player.transform.rotation.eulerAngles.y);
			playerImage.transform.rotation = playerStart;
		} else {
			playerImage.transform.rotation = Quaternion.Euler(180, 0, player.transform.rotation.eulerAngles.y);
			enemiesParent.transform.rotation = enemiesParentStart;
		}
	}
}
