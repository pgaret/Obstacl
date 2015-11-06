using UnityEngine;
using System.Collections;

public class GamePlay : MonoBehaviour {

    public GameObject laser;
    public GameObject canvas;
    GameObject ourLaser;

	// Use this for initialization
	void Start ()
	{

	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKeyDown(KeyCode.Space))
        {
            ourLaser = Instantiate(laser);
        }
        else if (Input.GetKeyUp(KeyCode.Space) && ourLaser != null)
        {
            ourLaser.GetComponent<Lazer>().DestroySelf();
            ourLaser = null;
        }
        else
        {
            canvas.GetComponent<Canvas>().speed = canvas.GetComponent<Canvas>().speed * Time.deltaTime / 2;
        }
	}
}
