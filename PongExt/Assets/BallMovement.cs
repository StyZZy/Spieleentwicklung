using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class BallMovement : MonoBehaviour {
    public float zSpeed;
    public float xMax;
    public Material brickLife3;
    public Material brickLife2;
    public Material brickLife1;
    public Canvas loseText;

     Vector3 velocity;
	// Use this for initialization
	void Start () {
        velocity = new Vector3(0, 0, -zSpeed);
	}
	
	// Update is called once per frame
	void Update () {
        transform.position += velocity * Time.deltaTime;
        if(transform.position.z < -9.5)
        {
            loseText.gameObject.SetActive(true);
            
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
           velocity = new Vector3(-velocity.x, velocity.y, -velocity.z);
        }
        
        else if (other.tag.Equals("Brick"))
        {
            
            float dist = transform.position.x - other.transform.position.x;
            velocity = new Vector3(velocity.x, velocity.y, -velocity.z);

            if (other.gameObject.GetComponent<Renderer>().material.color.Equals(brickLife1.color))
            {
                Destroy(other.gameObject);
                
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
