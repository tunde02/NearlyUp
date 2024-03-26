using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public Transform start;
    public Transform end;


    // Start is called before the first frame update
    void Start()
    {
        RaycastHit hit;
        if (Physics.Raycast(start.position, (end.position - start.position).normalized, out hit))
        {
            Debug.Log($"1. {hit.point}, {hit.distance}");
            
        }
        
        if (Physics.Linecast(start.position, end.position, out hit))
        {
            Debug.Log($"2. {hit.point}, {hit.distance}");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
