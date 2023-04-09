using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour
{
    public Vector3 offset = new Vector3(0f, 0f, -10f);
    public float smoothTime;
    private Vector3 velocity = Vector3.zero;

    [SerializeField] private Transform target;
  
    void Update()
    {
        target = GameManager.Instance.player.transform;
        Vector3 targetPosition = target.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime); 
    }
}
