using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public bool is_dead;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (is_dead)
        {
            StartCoroutine(YouShouldKillYourselfNOW());
        }
    }
    public IEnumerator YouShouldKillYourselfNOW()
    {
        yield return null;
    }
}
