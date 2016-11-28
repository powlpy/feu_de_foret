using UnityEngine;
using System.Collections;

public class FlyingVehicleBehavior : MonoBehaviour {
    public float speed; 
    public GameObject WaterStreamFX;
    private bool isBombarding = false;
    private bool isEmpty = false;
    private Vector3 bombardmentLocation;
    private float bombardmentDistance; 

    void Start() {
        bombardmentDistance = 20f;
    }
    
    void Update() {
        UpdateMovement();
        if (isEmpty)
            CheckDestroyDistance();
        else
            CheckBombardmentDistance();

    }

    void UpdateMovement() {

        transform.position += transform.forward * Time.deltaTime * speed * GlobalVariables.Speed;

    }

    void CheckBombardmentDistance() {
        if (bombardmentLocation == null) return;
        Vector2 myPosition2D = new Vector2(transform.position.x, transform.position.z);
        Vector2 bombardmentPosition2D = new Vector2(bombardmentLocation.x, bombardmentLocation.z);
        float myDistance = Vector2.Distance(myPosition2D, bombardmentPosition2D);
        if (!isBombarding && myDistance < bombardmentDistance)
            StartBombarding();
        else if (isBombarding && myDistance > bombardmentDistance)
            StopBombarding();
    }

    void CheckDestroyDistance() {
        if (bombardmentLocation == null) return;
        Vector2 myPosition2D = new Vector2(transform.position.x, transform.position.z);
        Vector2 bombardmentPosition2D = new Vector2(bombardmentLocation.x, bombardmentLocation.z);
        float myDistance = Vector2.Distance(myPosition2D, bombardmentPosition2D);
        if (myDistance > 3 * bombardmentDistance)
            Destroy(this.gameObject);
    }

    void StartBombarding() {
        isBombarding = true;
        WaterStreamFX.SetActive(true);
        Debug.Log(this);
    }

    public void SetBombardmentLocation(Vector3 bombardmentPoint) {
        bombardmentLocation = bombardmentPoint;
    }

    void StopBombarding() {
        isBombarding = false;
        WaterStreamFX.GetComponentInChildren<EllipsoidParticleEmitter>().emit = false;
        isEmpty = true;
    }

}
