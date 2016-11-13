using UnityEngine;
using System.Collections;

public class CylinderBehavior : MonoBehaviour {

    public Terrain myTerrain;
    public float Radius;

    void Start() {
        Resize();

    }

    void Update() {

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (myTerrain.GetComponent<Collider>().Raycast(ray, out hit, Mathf.Infinity)) {
            transform.position = hit.point;
        }

        if (Input.GetMouseButton(1)) {

            bool fireStarted = false;
            Collider[] closeColliders = Physics.OverlapSphere(transform.position, Radius);
            foreach(Collider closeCollider in closeColliders) {
                Inflammable closeInflammable = closeCollider.GetComponentInParent<Inflammable>();
                //Si il n'est pas inflammable, passe
                if (closeInflammable == null) {
                    continue;
                }

                closeInflammable.fireValue = GlobalVariables.FireStartValue;
                fireStarted = true;
            }

            if (fireStarted) {
                gameObject.SetActive(false);
                GlobalVariables.NextState();
            }
        }
    }

    public void Resize() {

        transform.localScale = new Vector3(2 * Radius, 0.5f, 2 * Radius);

    }

}
