using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollidersController : MonoBehaviour
{
    [SerializeField] BoxCollider2D _boxCollider2D;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(TagsConstants.Grease))
        {
            SlipInTheGrease();
        }

        if (collision.gameObject.CompareTag(TagsConstants.Sidewalks))
        {
            HitThePavement();
        }

        if (collision.gameObject.CompareTag(TagsConstants.Cars))
        {
            CrashIntoTheCar();
        }
    }

    void SlipInTheGrease()
    {
        Debug.Log("Escorregou no oleo");
    }

    void HitThePavement()
    {
        Debug.Log("Bateu na calcada");
    }

    void CrashIntoTheCar()
    {
        Debug.Log("Bateu no carro");
    }
}
