using UnityEngine;
using System.Collections;

public class CanvasHandler : MonoBehaviour {

    public GameObject SliderGlobalSpeed;
    public GameObject SliderSizeCircle;
    public GameObject SliderTreesCreated;
    public GameObject Firefighters;
    public GameObject Setups;

    void Start () {
        Firefighters.SetActive(false);
        SliderGlobalSpeed.SetActive(false);


    }
	
	public void StartSimulation() {
        Firefighters.SetActive(true);
        SliderGlobalSpeed.SetActive(true);
        SliderSizeCircle.SetActive(false);
        SliderTreesCreated.SetActive(false);
        Setups.SetActive(false);


    }
}
