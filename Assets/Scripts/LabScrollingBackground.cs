using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabScrollingBackground : MonoBehaviour
{
    public float scrollSpeed;
    private Vector3 startPos;
    private float backgroundWidth = 25.48f;     // Each background is 2548 pixels wide
    public GameObject fillerPrefab;
    public GameObject endPrefab;
    public GameObject[] backgroundPrefabs;
    private GameObject cloneBackground;
    private SpriteRenderer cloneBackgroundRenderer;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;

        cloneBackground = Instantiate(backgroundPrefabs[0],
                        startPos + Vector3.right * backgroundWidth,
                        Quaternion.identity);

        cloneBackgroundRenderer = cloneBackground.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // Move both backgrounds
        transform.Translate(Vector3.left * scrollSpeed * Time.deltaTime);
        cloneBackground.transform.Translate(Vector3.left * scrollSpeed * Time.deltaTime);

        // Check if the first background(filler) has moved completely off screen
        if (transform.position.x <= startPos.x - backgroundWidth)
        {
            transform.position = cloneBackground.transform.position + Vector3.right * backgroundWidth;
        }

        // Check if the second background(room) has moved completely off screen
        if (cloneBackground.transform.position.x <= startPos.x - backgroundWidth)
        {
            cloneBackground.transform.position = transform.position + Vector3.right * backgroundWidth;

            GameObject selectedPrefab = backgroundPrefabs[Random.Range(0, backgroundPrefabs.Length)];
            SpriteRenderer selectedRenderer = selectedPrefab.GetComponent<SpriteRenderer>();
            cloneBackgroundRenderer.sprite = selectedRenderer.sprite;
        }
    }
}
