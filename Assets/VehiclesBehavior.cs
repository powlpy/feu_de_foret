using UnityEngine;
using System.Collections;

public class VehiclesBehavior : MonoBehaviour {

    public GameObject FireTruck;
    public int NbFireTrucks;

    private float fireDistance = 10;

    public void GoFireTrucks(Vector3 minBounds, Vector3 maxBounds) {
        minBounds -= new Vector3(20, 0, 20);
        maxBounds += new Vector3(20, 0, 20);

        float boundsWidth = maxBounds.x - minBounds.x;
        float boundsHeight = maxBounds.z - minBounds.z;
        float perimeterSize = 2 * (boundsWidth + boundsHeight);
        Vector3 boundsCenter = (maxBounds + minBounds) / 2;

        for(int i=0; i<NbFireTrucks; i++) {
            float randomPerimeterPosition = Random.Range(0, perimeterSize);
            Vector3 myPosition;
            if (randomPerimeterPosition < boundsWidth) {
                myPosition = new Vector3(minBounds.x + randomPerimeterPosition, 0, minBounds.z);
            } else if (randomPerimeterPosition < boundsWidth + boundsHeight) {
                myPosition = new Vector3(maxBounds.x, 0, minBounds.z + randomPerimeterPosition - boundsWidth);
            } else if (randomPerimeterPosition < 2 * boundsWidth + boundsHeight) {
                myPosition = new Vector3(maxBounds.x - (randomPerimeterPosition - boundsWidth - boundsHeight), 0, maxBounds.z);
            } else {
                myPosition = new Vector3(minBounds.x, 0, minBounds.z + (perimeterSize - randomPerimeterPosition));
            }
            GameObject fireTruck = (GameObject)Instantiate(FireTruck);
            minBounds.y = 0.25f;
            fireTruck.transform.position = myPosition;
            float myAngle = Vector3.Angle(fireTruck.transform.position, boundsCenter);
            fireTruck.transform.LookAt(boundsCenter);
            fireTruck.transform.Rotate(-90f, 0f, -90f);

        }

    }


}
