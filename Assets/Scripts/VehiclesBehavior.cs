using UnityEngine;
using System.Collections;

public class VehiclesBehavior : MonoBehaviour {

    public GameObject FireTruck;
    public GameObject FireHelicopter;
    public GameObject FirePlane;
    public int NbFireTrucks;
    public int NbFireHelicopters;
    public int NbFirePlanes;

    private float fireDistance = 20;

    public void GoFireTrucks(Vector3 minBounds, Vector3 maxBounds) {
        minBounds -= new Vector3(fireDistance, 0, fireDistance);
        maxBounds += new Vector3(fireDistance, 0, fireDistance);

        float boundsWidth = maxBounds.x - minBounds.x;
        float boundsHeight = maxBounds.z - minBounds.z;
        float perimeterSize = 2 * (boundsWidth + boundsHeight);
        Vector3 boundsCenter = (maxBounds + minBounds) / 2;

        for(int i=0; i<NbFireTrucks; i++) {
            //float myPerimeterPosition = Random.Range(0, perimeterSize);
            float myPerimeterPosition = ((float)i + Random.Range(-0.4f, 0.4f)) / (float)NbFireTrucks * perimeterSize;
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
            fireTruck.transform.LookAt(boundsCenter);
            fireTruck.transform.Rotate(-90f, 0f, -90f);

        }

    }

    public void GoHelicopters(Vector3 minBounds, Vector3 maxBounds) {
        minBounds -= new Vector3(fireDistance, 0, fireDistance);
        maxBounds += new Vector3(fireDistance, 0, fireDistance);

        float boundsWidth = maxBounds.x - minBounds.x;
        float boundsHeight = maxBounds.z - minBounds.z;
        float perimeterSize = 2 * (boundsWidth + boundsHeight);
        Vector3 boundsCenter = (maxBounds + minBounds) / 2;

        for (int i = 0; i < NbFireHelicopters; i++) {
            float myPerimeterPosition = ((float)i + Random.Range(-0.4f, 0.4f)) / (float)NbFireHelicopters * perimeterSize;
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
            fireHelicopter.transform.LookAt(boundsCenter);
            Quaternion myRotation = fireHelicopter.transform.rotation;
            myRotation.x = 0f;
            myRotation.z = 0f;
            fireHelicopter.transform.rotation = myRotation;
            fireHelicopter.GetComponent<FlyingVehicleBehavior>().SetBombardmentLocation(boundsCenter);
        }

    }

    public void GoPlanes(Vector3 minBounds, Vector3 maxBounds) {
        minBounds -= new Vector3(fireDistance, 0, fireDistance);
        maxBounds += new Vector3(fireDistance, 0, fireDistance);

        float boundsWidth = maxBounds.x - minBounds.x;
        float boundsHeight = maxBounds.z - minBounds.z;
        float perimeterSize = 2 * (boundsWidth + boundsHeight);
        Vector3 boundsCenter = (maxBounds + minBounds) / 2;

        for (int i = 0; i < NbFirePlanes; i++) {
            float myPerimeterPosition = ((float)i + Random.Range(-0.4f, 0.4f)) / (float)NbFirePlanes * perimeterSize;
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
            firePlane.transform.LookAt(boundsCenter);
            Quaternion myRotation = firePlane.transform.rotation;
            myRotation.x = 0f;
            myRotation.z = 0f;
            firePlane.transform.rotation = myRotation;
            firePlane.GetComponent<FlyingVehicleBehavior>().SetBombardmentLocation(boundsCenter);
        }

    }

}
