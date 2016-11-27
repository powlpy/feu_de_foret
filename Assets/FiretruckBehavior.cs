using UnityEngine;
using System.Collections;

public class FiretruckBehavior : MonoBehaviour {

    Rigidbody myBody;
    public bool isMoving = true;
    public GameObject WaterStreamFX;

    void Awake() {
        myBody = GetComponent<Rigidbody>();
    }

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        //if (GlobalVariables.State == 0) return;
        if (isMoving)
            UpdateMovement();

	}

    void UpdateMovement() {
        if (GlobalVariables.State == 0) return;
        if (!isMoving) return;

        transform.position += transform.right * Time.deltaTime * 5 * GlobalVariables.Speed;

        transform.Rotate(new Vector3(0, 0, 0.2f));


    }

    void OnTriggerEnter(Collider collider) {
        if (collider.transform.parent != null)
            if (collider.transform.parent.tag == "Tree")
                FightFire();
    }


    void FightFire() {
        isMoving = false;
        WaterStreamFX.SetActive(true);
        Collider waterCollider = WaterStreamFX.GetComponentInChildren<BoxCollider>();
        foreach (Collider collider in Physics.OverlapSphere(transform.position + transform.right * 20, 7f)) {
            if (collider.transform.parent != null)
                if (collider.transform.parent.tag == "Tree")
                    collider.GetComponentInParent<Inflammable>().Watered();
        }

    }

}
