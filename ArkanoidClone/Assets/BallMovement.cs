using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class BallMovement : MonoBehaviour {
    public float zSpeed;
    public float xMax;
    public Material brickLife3;
    public Material brickLife2;
    public Material brickLife1;
    public Canvas loseText;
    public Canvas winText;
    private Boolean gameInProgress;
    private Boolean started;
    private int numberOfBricks;
     Vector3 velocity;
	// Use this for initialization
	void Start () {
        started = false;
        gameInProgress = true;
        numberOfBricks = 36;
	}
	
	// Update is called once per frame
	void Update () {
        if(Input.GetKey(KeyCode.Space) && !started)
        {
            velocity = new Vector3(0, 0, zSpeed);
        }
        transform.position += velocity * Time.deltaTime;
        if(transform.position.z < -11.5)
        {
            loseText.gameObject.SetActive(true);
            //Destroy(gameObject);
            gameInProgress = false;
            
        }

        if(Input.GetKey(KeyCode.X) && !gameInProgress)
        {
            
            
            SceneManager.LoadScene("main");
        }

	}

    void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Paddle"))
        {
            float maxDist = transform.localScale.x * 0.5f + other.transform.localScale.x * 0.5f;
            float actualDist = transform.position.x - other.transform.position.x;
            float normalizeDist = actualDist / maxDist;
            velocity = new Vector3(normalizeDist * xMax, velocity.y, -velocity.z);
        }
        else if (other.tag.Equals("Wall")){
            velocity = new Vector3(-velocity.x, velocity.y, velocity.z);
        }
        else if (other.tag.Equals("BackWall"))
        {
           velocity = new Vector3(velocity.x, velocity.y, -velocity.z);
        }
        
        else if (other.tag.Equals("Brick"))
        {
            
            float distz = transform.position.z - other.transform.position.z;
            if (distz >= 0.5f)
            {
                velocity = new Vector3(-velocity.x, velocity.y, velocity.z);
            }
            else
            {
                velocity = new Vector3(velocity.x, velocity.y, -Math.Abs(velocity.z));
            }
            if (other.gameObject.GetComponent<Renderer>().material.color.Equals(brickLife1.color))
            {
                Destroy(other.gameObject);
                numberOfBricks--;
                if(numberOfBricks == 0)
                {
                    winText.gameObject.SetActive(true);
                    velocity = new Vector3(0, 0, 0);
                    gameInProgress = false;
                }
            }
            else if (other.gameObject.GetComponent<Renderer>().material.color.Equals(brickLife2.color))
            {
                other.gameObject.GetComponent<Renderer>().material.color = brickLife1.color;
            }
            else if (other.gameObject.GetComponent<Renderer>().material.color.Equals(brickLife3.color))
            {
                other.gameObject.GetComponent<Renderer>().material.color = brickLife2.color;
            }

        }
        gameObject.GetComponent<AudioSource>().Play();
    }
}
