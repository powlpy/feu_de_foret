using UnityEngine;
using System.Collections;

public class FiretruckBehavior : MonoBehaviour {

    public float speed;
    private bool isMoving = true;
    public GameObject WaterStreamFX;
    private bool isFighting = false;
    Vector3 fightingLocation;

    void Awake() {

    }

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        //if (GlobalVariables.State == 0) return;
        if (isMoving)
            UpdateMovement();
        /*   fun
        else 
            WaterStreamFX.transform.Rotate(new Vector3(0, 0, 1));
            */
	}

    void UpdateMovement() {
        if (GlobalVariables.State == 0) return;
        if (!isMoving) return;

        transform.position += transform.right * Time.deltaTime * speed * GlobalVariables.Speed;


    }

    void OnTriggerEnter(Collider collider) {
        Debug.Log(Vector3.Distance(fightingLocation, transform.position));
        if (Vector3.Distance(fightingLocation, transform.position) < 18f)
            FightFire(fightingLocation);
        else if (collider.transform.parent != null)
            if (collider.transform.parent.tag == "Tree")
                FightFire(collider.transform.position);
    }


    void FightFire(Vector3 tree) {
        if (isFighting) return;
        isFighting = true;
        isMoving = false;
        WaterStreamFX.SetActive(true);

        //orienter le jet d'eau vers l'arbre le plus proche
        float myAngle = Vector3.Angle(transform.right, tree - transform.position);
        if (Vector3.Cross(transform.right, tree - transform.position).y < 0)
            myAngle = -myAngle;
        WaterStreamFX.transform.Rotate(new Vector3(0, 0, myAngle));

        //Vector3 myCenter = transform.TransformPoint(WaterStreamFX.GetComponent<SphereCollider>().center);
        Vector3 myCenter = WaterStreamFX.GetComponentInChildren<SphereCollider>().transform.position;
        foreach (Collider collider in Physics.OverlapSphere(myCenter, 7f)) {
            if (collider.transform.parent != null)
                if (collider.transform.parent.tag == "Tree")
                    collider.GetComponentInParent<Inflammable>().Watered();
        }
        

    }

    public void SetFightingLocation(Vector3 location) {
        fightingLocation = location;
    }

}
