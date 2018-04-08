using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour {
    public Vector3 speed;
    public Transform floor;
    public Transform wall;
    public Transform ball;
    private bool started;

	// Use this for initialization
	void Start () {
        started = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.Space))
        {
            started = true;
        }
        //if (Input.GetKey(KeyCode.A))
        //    transform.position = transform.position + new Vector3(-0.1f,0,0);
        //if (Input.GetKey(KeyCode.D))
        //    transform.position = transform.position + new Vector3(0.1f, 0, 0);

        float dir = Input.GetAxis("Horizontal");
        float newPos = transform.position.x + dir * Time.deltaTime * speed.x;
        float paddleScale = transform.localScale.x;
        float floorScale = floor.localScale.x;
        float wallScale = wall.localScale.x;
        float maxPos = floorScale * 10 * 0.5f - paddleScale * 1 * 0.5f - wallScale * 1;

        float pos = Mathf.Clamp(newPos, -maxPos, maxPos);
        transform.position = new Vector3(pos, transform.position.y, transform.position.z);
        if (!started && pos != maxPos && pos != -maxPos)
        {
            newPos = ball.position.x + dir * Time.deltaTime * speed.x;
            ball.position = new Vector3(newPos, ball.position.y, ball.position.z);
        }
    }
}
