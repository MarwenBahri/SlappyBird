using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class ObstacleManger : MonoBehaviour
{
    public Transform bottom;
    [HideInInspector]
    public Rigidbody2D rb;
    Vector2 initialPos ;
    void Start()
    {
        float rnd = Random.Range(-1f,1f);
        initialPos = bottom.localPosition;
        bottom.localPosition = new Vector2(initialPos.x, initialPos.y+rnd);
        rb = GetComponent<Rigidbody2D>();
    }
    public void resetPosition(float pp=0f)
    {
        transform.localPosition = new Vector2(pp,0);
        float rnd = Random.Range(-1f, 1f);
        bottom.localPosition = new Vector2(initialPos.x, initialPos.y + rnd);
    }
    private void Update()
    {
        if (transform.localPosition.x <= -11.45f)
        {
            resetPosition();
        }

    }

}
