
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;


public class Grapher : MonoBehaviour {
	private Texture2D texture;
	public int width;
	public int height;

	public MyStatistics myStatistics;

	List<int> values;
	public int refreshTempo;
	public int nbTree = 0;

	public Color baseColor;
	public Color curveColor;

	int counter = 0;

	bool draw = false;

	void addValue(int v){
		values.Add (v);
	}

	void Start() {
		texture = new Texture2D(width, height);
		values = new List<int> ();

		clear ();
		//texture.anisoLevel = 0;
			
		GetComponent<RawImage>().texture = texture;
		texture.Apply();
	}

	void Update() {
		if (draw) {
			if (counter != 0) {
				counter--;
				return;
			}
			counter = refreshTempo;
			clear ();
			addValue (myStatistics.GetNbIntactTrees ());

			int nbValues = values.Count;
			double step = (double)(nbValues) / width;
			double recadrage = height / nbTree;
			for (int i = 0; i < width; i++) {
				texture.SetPixel (i, (int)((values[(int)(i * step)] * recadrage)), curveColor);
			}

			texture.Apply();
		}
	}

	void clear(){
		for (int y = 0; y < texture.height; y++) {
			for (int x = 0; x < texture.width; x++) {
				texture.SetPixel (x, y, baseColor);
			}
		}
	}

	public void Reset(){
		values.Clear();
		nbTree = 0;
		draw = false;
	}

	public void launch(){
		draw = true;
		nbTree = myStatistics.GetNbTrees ();
	}
}