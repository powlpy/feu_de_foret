using UnityEngine;
using System.Collections;

public class Inflammable : MonoBehaviour {

    public float fireValue = 0;
    private ParticleSystem myFireEffect;
    private bool onFire = false;
    private bool isBurnt = false;

    void Awake() {

        myFireEffect = gameObject.GetComponent<ParticleSystem>();

    }
    
    void Start() {
        if(myFireEffect != null) {
            myFireEffect.Stop();
        }

        transform.Find("Normal").gameObject.SetActive(true);
        transform.Find("Burnt").gameObject.SetActive(false);

    }
    
    void Update() {

        UpdateFire();
        RenderFire();

    }

    //Evolue et répand le feu de l'arbre
    void UpdateFire() {

        //Le feu évolue
        fireValue += (0.0015f * fireValue + 0.0005f) * GlobalVariables.Speed;
        Debug.Log(GlobalVariables.Speed);
        if(fireValue > 100f) {
            fireValue = 100f;
        }
        //Le feu se répand
        //Tableau contenant les colliders proches
        Collider[] closeColliders = Physics.OverlapSphere(transform.position, 8);
        //Pour chacun d'entre eux
        foreach(Collider closeCollider in closeColliders) {
            Inflammable closeInflammable = closeCollider.GetComponentInParent<Inflammable>();
            //Si il n'est pas inflammable, passe
            if (closeInflammable == null) {
                continue;
            }
            closeInflammable.PassFire(fireValue);
        }

    }

    void RenderFire() {

        if(fireValue < 10 || isBurnt) {
            return;
        }

        if (!onFire) {
            myFireEffect.Play();
            onFire = true;
        }

        float greenValue = -2f / 185f * fireValue + 49f / 45f;
        myFireEffect.startColor = new Color(1.0f, greenValue, 0.2f, 1.0f);

        float myCutoff = 1f / 180f * fireValue + 4f / 9f;
        transform.Find("Normal").gameObject.GetComponent<Renderer>().materials[4].SetFloat("_Cutoff",myCutoff);

        if (fireValue == 100f) {
            isBurnt = true;
            myFireEffect.Stop();
            transform.Find("Normal").gameObject.SetActive(false);
            transform.Find("Burnt").gameObject.SetActive(true);
        }

    }

    //Reçoit le feu de ses voisins
    public void PassFire(float foreignFire) {

        if(foreignFire < 10 || fireValue > 15) {
            return;
        }

        if(foreignFire > fireValue) {
            fireValue += (foreignFire * 0.0007f) * GlobalVariables.Speed;
        }

    }


}
