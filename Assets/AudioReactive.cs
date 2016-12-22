using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioReactive : MonoBehaviour {

	public float mult;
	public Transform[] blocks;

	float[] samples = new float[64];
	AudioSource source;

	void Start () {
		source = GetComponent<AudioSource>();
		print(samples.Length / blocks.Length);
	}
	
	void Update () {
		SetHeight();
	}

	void SetHeight () {
		source.GetSpectrumData(samples, 0, FFTWindow.BlackmanHarris);
		for (int i = 0; i < blocks.Length; i++) {
			float blockH = 0;
			for (int j = 0; j < samples.Length / blocks.Length; j++) {
				blockH += samples[i + j];
			}
			blockH *= mult;
			blocks[i].localScale = new Vector3(1, blockH, 1);
		}
	}
}
