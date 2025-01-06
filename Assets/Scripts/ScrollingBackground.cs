using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBackground : MonoBehaviour
{
    public float scrollSpeed;
    private Vector3 startPos;
    private float backgroundWidth = 25.48f;     // Each background is 2548 pixels wide
    public GameObject backgroundPrefab;
    private GameObject cloneBackground;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;

        cloneBackground = Instantiate(backgroundPrefab,
                        startPos + Vector3.right * backgroundWidth,
                        Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        // Move both backgrounds
        transform.Translate(Vector3.left * scrollSpeed * Time.deltaTime);
        cloneBackground.transform.Translate(Vector3.left * scrollSpeed * Time.deltaTime);

        // Check if the first background has moved completely off screen
        if (transform.position.x <= startPos.x - backgroundWidth)
        {
            transform.position = cloneBackground.transform.position + Vector3.right * backgroundWidth;
        }

        // Check if the second background has moved completely off screen
        if (cloneBackground.transform.position.x <= startPos.x - backgroundWidth)
        {
            cloneBackground.transform.position = transform.position + Vector3.right * backgroundWidth;
        }
    }
}
