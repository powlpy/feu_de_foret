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

    public void GoHelicopters() {
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
        GameObject fireHelicopter = (GameObject)Instantiate(FireHelicopter);
        myPosition.y = 15f;
        fireHelicopter.transform.position = myPosition;
        fireHelicopter.transform.LookAt(GlobalVariables.GetFirePoint());
        Quaternion myRotation = fireHelicopter.transform.rotation;
        myRotation.x = 0f;
        myRotation.z = 0f;
        fireHelicopter.transform.rotation = myRotation;
        fireHelicopter.GetComponent<FlyingVehicleBehavior>().SetBombardmentLocation(GlobalVariables.GetFirePoint());

    }

    public void GoPlanes() {
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
        GameObject firePlane = (GameObject)Instantiate(FirePlane);
            myPosition.y = 15f;
            firePlane.transform.position = myPosition;
            firePlane.transform.LookAt(GlobalVariables.GetFirePoint());
            Quaternion myRotation = firePlane.transform.rotation;
            myRotation.x = 0f;
            myRotation.z = 0f;
            firePlane.transform.rotation = myRotation;
            firePlane.GetComponent<FlyingVehicleBehavior>().SetBombardmentLocation(GlobalVariables.GetFirePoint());

    }

}
