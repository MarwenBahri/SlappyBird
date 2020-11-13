using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;
using UnityEngine.UI;
public class SceneManager : MonoBehaviour
{
    public float movementSpeed=50;
    public float[] pos;
    public ObstacleManger[] pipes;
    public GameObject agent;
    public void ResetScene()
    {
        agent.transform.localPosition = new Vector3(-7.6f, -3.16f, 10f);
        for (int i = 0; i < 3; i++)
        {
            pipes[i].rb.velocity = Vector2.zero;
            pipes[i].resetPosition(pos[i]);
            pipes[i].rb.AddForce(Vector2.left * movementSpeed);
        }

    }
  
}
