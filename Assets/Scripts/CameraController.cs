using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform target, farBackground, middleBackground;
    [SerializeField] private float minHeight, maxHeight;
    
    private Vector3 lastPosition;

    // Start is called before the first frame update
    void Start()
    {
        lastPosition = transform.position;
    }

    void Update()
    {
        float clampedY = Mathf.Clamp(target.position.y, minHeight, maxHeight);
        
        transform.position = new Vector3(target.position.x, clampedY, transform.position.z);

        Vector3 amountToMove = transform.position - lastPosition;
        farBackground.position += amountToMove;
        middleBackground.position += amountToMove * .5f;

        lastPosition = transform.position;
    }
}
