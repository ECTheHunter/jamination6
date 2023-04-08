using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickedUpItem : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.parent != null)
        {
            transform.position = transform.parent.position;
        }
    }
}
