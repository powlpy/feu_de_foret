
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;


public class Grapher : MonoBehaviour {
	private Texture2D texture;
	public int width;
	public int height;

	public MyStatistics myStatistics;

	List<int> valuesIntact;
	List<int> valuesDamaged;
	List<int> valuesBurnt;
	List<int> valuesTotalDamages;
	public int refreshTempo;
	public int nbTree;

	public Color baseColor;
	public Color IntactTreeColor;
	public Color DamagedTreeColor;
	public Color BurntTreeColor;
	public Color TotalDamagesColor;

	float counter;

	bool draw;


	void Start() {
		nbTree = 0;
		counter = 0f;
		draw = false;

		texture = new Texture2D(width, height);
		valuesIntact = new List<int> ();
		valuesDamaged = new List<int> ();
		valuesBurnt = new List<int> ();
		valuesTotalDamages = new List<int> ();

		clear ();
		//texture.anisoLevel = 0;
			
		GetComponent<RawImage>().texture = texture;
		texture.Apply();
	}

	void Update() {
		if (draw) {
			if (counter > 0f) {
				counter -= GlobalVariables.Speed;
				return;
			}
			counter = (float)refreshTempo;
			clear ();
			/*
			valuesIntact.Add(myStatistics.GetNbIntactTrees ());
			valuesDamaged.Add (myStatistics.GetNbDamagedTrees ());
			valuesBurnt.Add (myStatistics.GetNbBurnTrees ());
			//*/
			valuesTotalDamages.Add ((int)myStatistics.GetTotalDamage ());

			int nbValues = valuesTotalDamages.Count;
			float step = (float)(nbValues) / width;
			float recadrage = (float)(height - 2) / nbTree;
			for (int i = 0; i < width; i++) {
				/*
				texture.SetPixel (i, (int)((valuesIntact[(int)(i * step)] * recadrage) + 1), IntactTreeColor);
				texture.SetPixel (i, (int)((valuesDamaged[(int)(i * step)] * recadrage) + 1), DamagedTreeColor);
				texture.SetPixel (i, (int)((valuesBurnt[(int)(i * step)] * recadrage) + 1), BurntTreeColor);
				//*/
				texture.SetPixel (i, (int)((valuesTotalDamages[(int)(i * step)] * recadrage / 1000) + 1), TotalDamagesColor);
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
		valuesIntact.Clear ();
		valuesDamaged.Clear ();
		valuesBurnt.Clear ();
		valuesTotalDamages.Clear ();
		nbTree = 0;
		draw = false;
	}

	public void launch(){
		draw = true;
		nbTree = myStatistics.GetNbTrees ();
	}
}