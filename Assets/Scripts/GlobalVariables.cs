using UnityEngine;
using System.Collections;

public class GlobalVariables : MonoBehaviour {

    public static bool HighQuality = true;
    public static float Speed = 1f;
    public static int State = 0;
    public static Bounds boundingBox;

    public static float windPower = 40f; // in km
    public static float windDirection = 0f; // in radian
    public static float minRadiusFire = 10f; // minimal distance of fire radius

    private static Vector3 boundingBoxMin;
    private static Vector3 boundingBoxMax;

    private static Vector3 firePoint;

    public static void NextState() {
        if (State == 0) {
            NextState0to1();
        }
    }

    /*
    void OnDrawGizmos() {
        if (State > 0) {
            Gizmos.color = new Color(0.8f, 0.8f, 0.4f, 0.5F);
            Gizmos.DrawCube(boundingBox.center, boundingBox.size);

        }
    }*/

    static void NextState0to1() {
        State = 1;
        GameObject.Find("Canvas").GetComponentInChildren<CanvasHandler>().StartSimulation();


        GameObject[] trees = GameObject.FindGameObjectsWithTag("Tree");
        boundingBox.center += trees[0].transform.position;
        foreach (GameObject tree in trees)
            boundingBox.Encapsulate(tree.transform.position);

        boundingBoxMin = boundingBox.min;
        boundingBoxMax = boundingBox.max;
    }

    public static Vector3 GetBoundingBoxMin() {
        return boundingBoxMin;
    }


    public static Vector3 GetBoundingBoxMax() {
        return boundingBoxMax;
    }

    public static void SetFirePoint(Vector3 fp) {
        firePoint = fp;
    }

    public static Vector3 GetFirePoint() {
        return firePoint;
    }

}

