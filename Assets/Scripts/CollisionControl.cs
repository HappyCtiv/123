using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionControl : MonoBehaviour
{
    AudioManager audi;
    void Start()
    {
        audi = FindObjectOfType<AudioManager>();
    }

    void OnCollisionEnter(Collision col) 
    {
        if (col.collider.tag == "Dirt")
        {
            audi.Play("Dirt");
        }
        if (col.collider.tag == "Grass")
        {
            audi.Play("Grass");
        }
        if (col.collider.tag == "Metal")
        {
            audi.Play("Metal");
        }
        if (col.collider.tag == "Stone")
        {
            audi.Play("Stone");
        }
    }

}
