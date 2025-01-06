using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseObstacle : MonoBehaviour
{
    public float speed;
    private float despawnX = -15f;
    // Start is called before the first frame update
    public void Start()
    {
        
    }

    // Update is called once per frame
    public void FixedUpdate()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime);

        if (transform.position.x < despawnX)
        {
            // Call the ObjectPooler's method to return this object to the pool
            ObjectPooler.Instance.ReturnObjectToPool(gameObject);  // Make sure to disable the object
        }
    }
}
