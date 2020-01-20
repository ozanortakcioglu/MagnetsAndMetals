using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Metal : MonoBehaviour
{
    Rigidbody rb;
    public Material metal;
    public float magnitudeMultiplier = 1;
    GameObject[] magnets;
    readonly float permeability = 1;

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        gameObject.GetComponent<Renderer>().material = metal;
        magnets = GameObject.FindGameObjectsWithTag("Draggable");

    }
    private void FixedUpdate()
    {
        if (magnets.Length != 0)
        {
            for (int i = 0; i < magnets.Length; i++)
            {
                float F;
                Vector3 heading = magnets[i].transform.position - gameObject.transform.position;
                F = magnitudeMultiplier * permeability * magnets[i].GetComponent<Magnet>().magnitudeMultiplier / (4 * Mathf.PI * heading.sqrMagnitude);
                Vector3 direction = heading / heading.magnitude;
                rb.GetComponent<Rigidbody>().AddForce(F * direction);
            }
        }
    }
}
