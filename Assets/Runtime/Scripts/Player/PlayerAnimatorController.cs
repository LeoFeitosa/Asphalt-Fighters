using System.Collections;
using UnityEngine;

public class PlayerAnimatorController : MonoBehaviour
{
    [SerializeField] GameObject[] _explosionsPrefab;
    [SerializeField] float _speed;
    [SerializeField] float _timeLimitInclination;
    [SerializeField] float _timeLimitRotate;
    [SerializeField] float _degreesOfIncline;

    public int CountHit { get; private set; }

    float _timeMove;
    int _countHit;
    bool _enableRandom;
    int _direction;

    [Header("Blink on colission")]
    [SerializeField] float _delay = 3f;
    [SerializeField] float _blink = 0.1f;
    [SerializeField] int _count = 7;

    void Start()
    {
        _timeMove = 0;
        _countHit = 0;
        _enableRandom = true;
    }

    void Update()
    {
        CountHit = _countHit;
    }

    void FixedUpdate()
    {
        MoveRotate();
    }

    void MoveRotate()
    {
        if (_countHit > 0)
        {
            _timeMove += Time.fixedDeltaTime;
        }
        if (_countHit == 1)
        {
            RandomizeRotationDirection();
            TiltZAxis();
        }
        else if (_countHit >= 2)
        {
            RodateZ();
        }
    }

    void TiltZAxis()
    {
        if (_timeMove <= _timeLimitInclination)
        {
            transform.Rotate(new Vector3(0f, 0f, _degreesOfIncline * _direction) * Time.fixedDeltaTime);
        }
        else
        {
            StopRotateZ();
        }
    }

    void RodateZ()
    {
        if (_timeMove <= _timeLimitRotate)
        {
            transform.Rotate(new Vector3(0f, 0f, (transform.localRotation.z + _speed) * _direction) * Time.fixedDeltaTime);
        }
        else
        {
            StopRotateZ();
        }
    }

    void StopRotateZ()
    {
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.identity, Time.fixedDeltaTime * _speed);

        if (transform.rotation.z == 0)
        {
            _enableRandom = true;
            _timeMove = 0;
            _countHit = 0;
            CountHit = 0;
        }
    }

    public void Hit()
    {
        _countHit++;
        CountHit = _countHit;
    }

    void RandomizeRotationDirection()
    {
        if (_enableRandom)
        {
            _enableRandom = false;

            Vector3 degreeZ = Vector3.zero;

            if (Random.value >= 0.5f)
            {
                _direction = 1;
            }
            else
            {
                _direction = -1;
            }
        }
    }

    public void Explosion()
    {
        Instantiate(_explosionsPrefab[Random.Range(0, _explosionsPrefab.Length)], transform.position, transform.rotation);

        transform.position = Vector2.zero;

        GetComponentInChildren<SpriteRenderer>().enabled = false;
        GetComponentInChildren<BoxCollider2D>().enabled = false;

        StartCoroutine(Reposition());
    }

    void RemoveExplosions()
    {
        GameObject[] explosions = GameObject.FindGameObjectsWithTag(TagsConstants.Explosions);

        foreach (GameObject explosion in explosions)
        {
            Destroy(explosion);
        }
    }

    IEnumerator Reposition()
    {
        yield return new WaitForSeconds(_delay);

        for (int n = 0; n < _count; n++)
        {
            GetComponentInChildren<SpriteRenderer>().enabled = true;
            yield return new WaitForSeconds(_blink);
            GetComponentInChildren<SpriteRenderer>().enabled = false;
            yield return new WaitForSeconds(_blink);
        }

        GetComponentInChildren<SpriteRenderer>().enabled = true;
        GetComponentInChildren<BoxCollider2D>().enabled = true;

        Debug.Log("Recentralizar");

        RemoveExplosions();

        yield return null;
    }

}