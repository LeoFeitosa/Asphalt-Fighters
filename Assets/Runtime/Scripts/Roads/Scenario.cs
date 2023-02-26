using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scenario : MonoBehaviour
{
    public Object[] _prefabDetails;
    float _initialPositionY = 0;


    // Start is called before the first frame update
    void Start()
    {
        _initialPositionY = this.transform.position.y; 
        MountScenario();
    }

    void MountScenario() 
    {
        RandomDetails(_prefabDetails);

        float positionY = this.gameObject.transform.position.y;

        foreach (GameObject _detail in _prefabDetails)
        {
            Vector3 borderSize = _detail.GetComponentInChildren<SpriteRenderer>().bounds.size;
            positionY += borderSize.y;
            
            float positionX = this.gameObject.transform.position.x;
            float currentPositionY = positionY - borderSize.y;

            _detail.GetComponent<Transform>().position = new Vector2(positionX, currentPositionY);
        }
    }

    void RandomDetails(Object[] objects) 
    {
        for (int i = 0; i < objects.Length; i++)
        {
            int randomIndex = Random.Range(0, objects.Length);

            Object temp = objects[i];
            objects[i] = objects[randomIndex];
            objects[randomIndex] = temp;
        }
    }

    void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }
}
