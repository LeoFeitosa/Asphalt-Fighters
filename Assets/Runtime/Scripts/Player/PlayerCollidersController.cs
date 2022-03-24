using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollidersController : MonoBehaviour
{
    public bool FinishedThePhase { get; private set; }

    void Start()
    {
        FinishedThePhase = false;
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

        if (collision.gameObject.CompareTag(TagsConstants.Finish))
        {
            FinishLevel();
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

    void FinishLevel()
    {
        Debug.Log("Finiallllll");
        FinishedThePhase = true;
    }
}
