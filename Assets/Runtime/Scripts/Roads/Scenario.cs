using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scenario : MonoBehaviour
{
    public Object _prefabBorder;
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
        // var borderSize = _prefabBorder;

        Debug.Log("borderSize");

        // Instantiate(_prefabRoad, new Vector3(0, this.transform.position.y + _fullSize * _numberOfChildObjects, 0), Quaternion.identity);
    }
}
