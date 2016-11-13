using UnityEngine;
using System.Collections;
using UnityEngine.UI; // Required when Using UI elements.

public class SliderBehavior : MonoBehaviour {

    public Slider SpeedSlider;
    public Slider SizeCircleSlider;

    // Use this for initialization
    void Start() {
        SpeedSlider.onValueChanged.AddListener(delegate { SpeedValueChanged(); });
        SizeCircleSlider.onValueChanged.AddListener(delegate { SizeCircleValueChanged(); });

    }

    void SpeedValueChanged() {

        GlobalVariables.Speed = SpeedSlider.value / 2f;

        GameObject[] trees = GameObject.FindGameObjectsWithTag("Tree");
        foreach (GameObject Tree in trees) {
            Tree.GetComponent<ParticleSystem>().startSpeed = (SpeedSlider.value / 2f) * 5f;
            Tree.GetComponent<ParticleSystem>().startLifetime = 1.3f / SpeedSlider.value;
        }
    }

    void SizeCircleValueChanged() {
        CylinderBehavior cylinderBehavior = GameObject.Find("Cylinder").GetComponent<CylinderBehavior>();
        cylinderBehavior.Radius = SizeCircleSlider.value;
        cylinderBehavior.Resize();

    }

}
