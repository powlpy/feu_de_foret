using UnityEngine;
using System.Collections;

public class GlobalVariables : MonoBehaviour {
    
    public static float Speed = 1f;
    public static int State = 0;
    public static Bounds boundingBox;

    public static void NextState() {
        if(State == 0) {
            NextState0to1();
        }
    }


    void OnDrawGizmos() {
        if(State > 0) {
            Gizmos.color = new Color(0.8f, 0.8f, 0.4f, 0.5F);
            Gizmos.DrawCube(boundingBox.center, boundingBox.size);

        }
    }

    static void NextState0to1() {
        State = 1;
        GameObject.Find("Instructions").SetActive(false);
        GameObject.Find("SliderSizeCircle").SetActive(false);
        GameObject.Find("SliderNbTreesCreated").SetActive(false);


        GameObject[] trees = GameObject.FindGameObjectsWithTag("Tree");
        boundingBox.center += trees[0].transform.position;
        foreach (GameObject tree in trees) {
            boundingBox.Encapsulate(tree.transform.position);
        }
        Vector3 newSize = boundingBox.size;
        newSize.y += 5;
        newSize.x += 10;
        newSize.z += 10;
        boundingBox.size = newSize;
        GameObject.Find("Global").GetComponent<VehiclesBehavior>().GoFireTrucks(boundingBox.min, boundingBox.max);
        GameObject.Find("Global").GetComponent<VehiclesBehavior>().GoHelicopters(boundingBox.min, boundingBox.max);
        GameObject.Find("Global").GetComponent<VehiclesBehavior>().GoPlanes(boundingBox.min, boundingBox.max);
    }
    
}
