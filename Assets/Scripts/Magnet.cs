using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Magnet : MonoBehaviour
{
    public enum Polarization { Negative , Positive };
    public Polarization polarization;
    public Material positive, negative;
    public float magnitudeMultiplier = 250;
    readonly float permeability = 1;

    Rigidbody rb;

    List<GameObject> magnets = new List<GameObject>();
    GameObject[] metals;
    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        FindObjects();
        SetMaterial();

    }
    private void FixedUpdate()
    {
        if (magnets.Count != 0)
        {
            for (int i = 0; i < magnets.Count; i++)
            {
                float F;
                Vector3 heading = gameObject.transform.position - magnets[i].transform.position;
                F = magnitudeMultiplier * permeability * magnets[i].GetComponent<Magnet>().magnitudeMultiplier / (4 * Mathf.PI * heading.sqrMagnitude);
                Vector3 direction = heading / heading.magnitude;
                if (polarization != magnets[i].GetComponent<Magnet>().polarization)
                    direction = direction * -1;
                rb.AddForce(F * direction);
            }
        }

        if (metals.Length != 0)
        {
            for (int i = 0; i < metals.Length; i++)
            {
                float F;
                Vector3 heading = gameObject.transform.position - metals[i].transform.position;
                F = magnitudeMultiplier * permeability * metals[i].GetComponent<Metal>().magnitudeMultiplier / (4 * Mathf.PI * heading.sqrMagnitude);
                Vector3 direction = heading / heading.magnitude;
                metals[i].GetComponent<Rigidbody>().AddForce(F * direction);
            }
        }

    }

    void SetMaterial()
    {
        if (polarization == Polarization.Negative)
        {
            gameObject.GetComponent<Renderer>().material = negative;
        }
        else
        {
            gameObject.GetComponent<Renderer>().material = positive;
        }
    }

    void FindObjects()
    {
        metals = GameObject.FindGameObjectsWithTag("Metal");

        foreach (GameObject go in GameObject.FindGameObjectsWithTag("Draggable"))
        {
            if (go.Equals(this.gameObject))
                continue;
            magnets.Add(go);
        }
    }

}
