using UnityEngine;
using System.Collections;

public class CylinderBehavior : MonoBehaviour {

    public Terrain myTerrain;
    public float Radius;
    RaycastHit hit;
    public GameObject TreePrefab1;
    public int NbTreesCreated = 10;

    void Start() {
        Resize();

        /*
        for (int i = 700; i < 800; i += 4)
            for (int j = 600; j < 700; j += 4) {
                GameObject newTree = (GameObject)Instantiate(TreePrefab1);
                newTree.transform.position = new Vector3(i, 0.2f, j);
            }*/

    }

    void Update() {
        
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (myTerrain.GetComponent<Collider>().Raycast(ray, out hit, Mathf.Infinity)) {
            transform.position = hit.point;
        }
        
        if (Input.GetMouseButtonDown(1)) {
            StartFire();
        }
        if (Input.GetMouseButtonDown(2)) {
            DrawTrees();
        }
    }

    public void Resize() {

        transform.localScale = new Vector3(2 * Radius, 0.5f, 2 * Radius);

    }

    public void StartFire() {

        bool fireStarted = false;
        Collider[] closeColliders = Physics.OverlapSphere(transform.position, Radius);
        foreach (Collider closeCollider in closeColliders) {
            Inflammable closeInflammable = closeCollider.GetComponentInParent<Inflammable>();
            //Si il n'est pas inflammable, passe
            if (closeInflammable == null) {
                continue;
            }

            closeInflammable.Ignite();
            fireStarted = true;
            GlobalVariables.SetFirePoint(transform.position);
        }

        if (fireStarted) {
            this.transform.position = Vector3.zero;
            gameObject.SetActive(false);
            GlobalVariables.NextState();
        }

    }

    public void DrawTrees() {
        for(int i=0; i<NbTreesCreated; i++) {

            float x = hit.point.x + Random.Range(-Radius, Radius);
            float z = hit.point.z + Random.Range(-Radius, Radius);

            GameObject newTree = (GameObject)Instantiate(TreePrefab1);
            newTree.transform.position = new Vector3(x, hit.point.y, z);

        }
    }
    

}
