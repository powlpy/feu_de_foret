using UnityEngine;
using System.Collections;

public class VehiclesBehavior : MonoBehaviour {

    public GameObject FireTruck;
    public GameObject FireHelicopter;
    public GameObject FirePlane;

    private float fireDistance = 20;

    public void GoFireTrucks() {
        if (GlobalVariables.State == 0) return;
        Vector3 minBounds = GlobalVariables.GetBoundingBoxMin();
        Vector3 maxBounds = GlobalVariables.GetBoundingBoxMax();
        minBounds -= new Vector3(fireDistance, 0, fireDistance);
        maxBounds += new Vector3(fireDistance, 0, fireDistance);

        float boundsWidth = maxBounds.x - minBounds.x;
        float boundsHeight = maxBounds.z - minBounds.z;
        float perimeterSize = 2 * (boundsWidth + boundsHeight);
        
        float myPerimeterPosition = Random.Range(0, perimeterSize);
        Vector3 myPosition;
        if (myPerimeterPosition < boundsWidth) {
            myPosition = new Vector3(minBounds.x + myPerimeterPosition, 0, minBounds.z);
        } else if (myPerimeterPosition < boundsWidth + boundsHeight) {
            myPosition = new Vector3(maxBounds.x, 0, minBounds.z + myPerimeterPosition - boundsWidth);
        } else if (myPerimeterPosition < 2 * boundsWidth + boundsHeight) {
            myPosition = new Vector3(maxBounds.x - (myPerimeterPosition - boundsWidth - boundsHeight), 0, maxBounds.z);
        } else {
            myPosition = new Vector3(minBounds.x, 0, minBounds.z + (perimeterSize - myPerimeterPosition));
        }
        GameObject fireTruck = (GameObject)Instantiate(FireTruck);
        myPosition.y = 0.25f;
        fireTruck.transform.position = myPosition;
        fireTruck.transform.LookAt(GlobalVariables.GetFirePoint());
        fireTruck.transform.Rotate(-90f, 0f, -90f);
        

    }

    public void GoFlyingVehicle(int type) {

        if (GlobalVariables.State == 0) return;

        GameObject myPrefab;
        if (type == 0) myPrefab = FireHelicopter;
        else myPrefab = FirePlane;

        Vector3 bombardmentPosition = GameObject.Find("Cylinder").transform.position;

        GameObject flyingVehicle = (GameObject)Instantiate(myPrefab);
        Vector3 myPosition = RandomCircle(bombardmentPosition, 75f);
        myPosition.y = 15f;
        flyingVehicle.transform.position = myPosition;
        flyingVehicle.transform.LookAt(bombardmentPosition);
        Quaternion myRotation = flyingVehicle.transform.rotation;
        myRotation.x = 0f;
        myRotation.z = 0f;
        flyingVehicle.transform.rotation = myRotation;
        flyingVehicle.GetComponent<FlyingVehicleBehavior>().SetBombardmentLocation(bombardmentPosition);
    }

    Vector3 RandomCircle(Vector3 center, float radius) {
        float ang = Random.value * 360;
        Vector3 pos;
        pos.x = center.x + radius * Mathf.Sin(ang * Mathf.Deg2Rad);
        pos.y = center.y;
        pos.z = center.z + radius * Mathf.Cos(ang * Mathf.Deg2Rad);
        return pos;
    }

}
