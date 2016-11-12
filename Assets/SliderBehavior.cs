using UnityEngine;
using System.Collections;
using UnityEngine.UI; // Required when Using UI elements.

public class SliderBehavior : MonoBehaviour {

    public Slider SpeedSlider;

	// Use this for initialization
	void Start () {
        SpeedSlider.onValueChanged.AddListener(delegate { ValueChanged(); });
	
	}
	
    void ValueChanged() {

        GlobalVariables.Speed = SpeedSlider.value / 2f;

        GameObject[] trees = GameObject.FindGameObjectsWithTag("Tree");
        foreach(GameObject Tree in trees) {
            Tree.GetComponent<ParticleSystem>().startSpeed = (SpeedSlider.value/2f) * 5f;
            Tree.GetComponent<ParticleSystem>().startLifetime = 1.3f / SpeedSlider.value;



        }
    }

}
