using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColouredTargets : MonoBehaviour
{
    public Material[] randomMaterials;
    
    void OnCollisionEnter(Collision other)
    {
        if (other.transform.tag == "Object")
        {
            gameObject.GetComponent<Renderer>().material = randomMaterials[Random.Range(0, randomMaterials.Length)];
        }
    }
}
