using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnOpponentsController : MonoBehaviour
{
    [SerializeField] GameObject[] _prefabCars;
    [SerializeField] Transform _spawnPoint;

    void Start()
    {
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        float seconds = Random.Range(3, 5);

        yield return new WaitForSeconds(seconds);
        Instantiate(_prefabCars[Random.Range(0, _prefabCars.Length)], _spawnPoint.position, Quaternion.identity);
        StartCoroutine(Spawn());
    }
}
