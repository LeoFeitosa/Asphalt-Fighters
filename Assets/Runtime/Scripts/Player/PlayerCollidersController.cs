using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollidersController : MonoBehaviour
{
    [SerializeField] float _timeToRevive;

    PlayerAnimatorController _animatorController;
    public bool Dead { get; private set; }
    public bool FinishedThePhase { get; private set; }

    void Awake()
    {
        _animatorController = GetComponent<PlayerAnimatorController>();
    }

    void Start()
    {
        Dead = false;
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
            if (_animatorController.CountHit == 2)
            {
                _animatorController.Explosion();
                StartCoroutine(TimeToDead(_timeToRevive));
            }
            else
            {
                CrashIntoTheCar();
            }
        }

        if (collision.gameObject.CompareTag(TagsConstants.Finish))
        {
            FinishLevel();
        }
    }

    void SlipInTheGrease()
    {
        _animatorController.Hit();
        Debug.Log("Escorregou no oleo");
    }

    void HitThePavement()
    {
        _animatorController.Explosion();
        StartCoroutine(TimeToDead(_timeToRevive));
        Debug.Log("Bateu na calcada");
    }

    void CrashIntoTheCar()
    {
        _animatorController.Hit();
        Debug.Log("Bateu no carro");
    }

    void FinishLevel()
    {
        FinishedThePhase = true;
    }

    IEnumerator TimeToDead(float time)
    {
        Dead = true;
        yield return new WaitForSeconds(time);
        Dead = false;
        yield return null;
    }
}
