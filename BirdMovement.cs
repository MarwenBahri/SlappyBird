using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;

public class BirdMovement : Agent
{
    Rigidbody2D rb;
    public float power=50;
    public GameObject GameoverPanel;
    int score = 0;
    public Sprite[] numbers;
    public Image[] ScoreBord;
    public SceneManager sc;
    int current = 0;
    public CircleCollider2D myColl;
    public Collider2D[] otherColliders;
    public Transform[] pipes;
    public override void Initialize()
    {
        rb = GetComponent<Rigidbody2D>();
       
    }
    public override void Heuristic(float[] actionsOut)
    {
        actionsOut[0] = 0;
        if (Input.GetKey(KeyCode.A))
        {
            actionsOut[0] = 1;
        }
    }
    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.position.y);
        sensor.AddObservation(rb.velocity.y);
        sensor.AddObservation(transform.position.x - pipes[current].position.x);
        sensor.AddObservation(transform.position.y - pipes[current].position.y);
        base.CollectObservations(sensor);
    }
    
    public override void OnActionReceived(float[] vectorAction)
    {
        float r = 0.1f;
        if(Mathf.FloorToInt(vectorAction[0])==1)
        {
            rb.velocity = Vector2.zero;
            rb.AddForce(Vector2.up * power);
        }
        foreach (Collider2D cd in otherColliders)
        {
            if (myColl.IsTouching(cd))
            {
                r = -1.0f;
                break;          
             
            }
        }
        AddReward(r);
    }
    public override void OnEpisodeBegin()
    {
        rb.velocity = Vector2.zero;
        transform.localPosition = new Vector3(-7.6f, -3.16f, 10f);
        ScoreBord[0].sprite = ScoreBord[1].sprite = ScoreBord[2].sprite = ScoreBord[3].sprite = numbers[0];
            
        score = 0;
        sc.movementSpeed = 50;
        for (int i = 0; i < 3; i++)
        {
            sc.pipes[i].rb.velocity = Vector2.zero;
            sc.pipes[i].resetPosition(sc.pos[i]);
            sc.pipes[i].rb.AddForce(Vector2.left * 50);
        }   
    }
    public void updatescore(int score)
    {
        int pos=0;
        while(score!=0)
        {
            ScoreBord[pos++].sprite = numbers[score % 10];
            score /= 10;      
        }
    }
    public void resetScene()
    {
            EndEpisode();
            score = 0;
            ScoreBord[0].sprite = ScoreBord[1].sprite = ScoreBord[2].sprite = ScoreBord[3].sprite = numbers[0];
            sc.movementSpeed = 50;
            rb.velocity = Vector2.zero;
            transform.localPosition = new Vector3(-7.6f, -3.16f, 10f);
        
            for (int i = 0; i < 3; i++)
            {
                sc.pipes[i].rb.velocity = Vector2.zero;
                sc.pipes[i].resetPosition(sc.pos[i]);
                sc.pipes[i].rb.AddForce(Vector2.left *sc.movementSpeed);
            }        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
            AddReward(-1.0f);
            resetScene();      
            return;

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        AddReward(1.0f);
            current = (current + 1) % 3;
            score++;
            updatescore(score);
            if (score % 10 == 0)
            {

                for (int i = 0; i < 3; i++)
                {
                    
                    sc.pipes[i].rb.AddForce(Vector2.left * sc.movementSpeed * 0.05f);
                }
                sc.movementSpeed *= 1.1f;
            }

    }
   
}
