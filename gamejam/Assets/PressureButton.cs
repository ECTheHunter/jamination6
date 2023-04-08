using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressureButton : MonoBehaviour
{
    public bool is_pressed;
    public float presscheck_raycastdistance;
    public GameObject raycast_origin;
    public GameObject closed;
    public GameObject open;
    public LayerMask layerMask;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(raycast_origin.transform.position, Vector2.up, presscheck_raycastdistance, ~layerMask);
        if (hit.collider != null)
        {
            is_pressed = true;
        }
        else
        {
            is_pressed = false;
        }
        if(is_pressed)
        {
            open.SetActive(false);
            closed.SetActive(true);
        }
        else
        {
            closed.SetActive(false);
            open.SetActive(true);

        }
    }
}
