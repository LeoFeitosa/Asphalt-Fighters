using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scenario : MonoBehaviour
{
    public GameObject _prefabBorder;
    public Object[] _prefabDetails;

    // Start is called before the first frame update
    void Start()
    {
        MountScenario();
    }

    // Update is called once per frame
    void Update()
    {
         
    }

    void MountScenario() 
    {
        RandomDetails(_prefabDetails);

        float positionY = gameObject.transform.position.y;

        foreach (GameObject _detail in _prefabDetails)
        {
            Vector3 borderSize = _detail.GetComponentInChildren<SpriteRenderer>().bounds.size;
            positionY += borderSize.y;
            
            float positionX = gameObject.transform.position.x;
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

    void Mirror(GameObject objectDetail)
    {
        Vector3 scaleDetail = objectDetail.GetComponent<Transform>().localScale;
        objectDetail.GetComponent<Transform>().localScale = new Vector3(
            scaleDetail.x * -1, 
            scaleDetail.y, 
            scaleDetail.z
        );
    }

    void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }
}
