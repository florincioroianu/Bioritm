using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hexagon : MonoBehaviour
{
    public Rigidbody2D rb;
    public float shrinkSpeed = 3f;
    LineRenderer Hexagon;

    void Start()
    {
        rb.rotation = Random.Range(0f, 360f);
        transform.localScale = Vector3.one * 10f;
        Hexagon = GetComponent<LineRenderer>();
#pragma warning disable CS0618 // Type or member is obsolete
        Hexagon.SetColors(start: gameManager.color, end: gameManager.color);
#pragma warning restore CS0618 // Type or member is obsolete
    }

    void Update()
    {
        transform.localScale -= Vector3.one * shrinkSpeed * Time.deltaTime;
        if (transform.localScale.x < .05f)
            Destroy(gameObject);
    }
}
