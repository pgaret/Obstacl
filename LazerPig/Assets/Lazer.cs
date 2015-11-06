using UnityEngine;
using System.Collections;

public class Lazer : MonoBehaviour {

    public float xScale;
    public float yScale;
    public float speed;

    public GameObject player;
    public GameObject canvas;
    public GameObject mousePointer;

    GameObject testPointer;
    bool inCollision = false;

	// Use this for initialization
	void Start ()
    {
        canvas = GameObject.FindGameObjectWithTag("Canvas");
        player = GameObject.FindGameObjectWithTag("Player");
        transform.position = player.transform.position;

        testPointer = Instantiate(mousePointer);
	}
	
	// Update is called once per frame
	void Update ()
    {
        testPointer.transform.position = screenPosition(Input.mousePosition);
        testPointer.transform.position = Input.mousePosition;

        Vector3 dir = testPointer.transform.position - screenPosition(player.transform.position);
        dir = testPointer.transform.TransformDirection(dir);
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);

        RaycastHit2D[] hits;
        float rayX = Input.mousePosition.x - screenPosition(player.transform.position).x;
        float rayY = Input.mousePosition.y - screenPosition(player.transform.position).y;
        Vector2 normal = Vector3.Normalize(new Vector2(rayX, rayY));
        hits = Physics2D.RaycastAll(player.transform.position, normal);

        if (hits.Length > 0)
        {
            float closestDist = hits[0].fraction;
            GameObject closestObj = hits[0].transform.gameObject;
            for (int i = 1; i < hits.Length; i++)
            {
                if (hits[i].fraction < closestDist && hits[i] != player && hits[i] != gameObject)
                {
                    closestDist = hits[i].fraction;
                    closestObj = hits[i].transform.gameObject;
                }
            }
            transform.localScale = new Vector3(closestDist, .2f, 1);
            canvas.GetComponent<Canvas>().speed += closestObj.GetComponent<Obstalce>().speed;

        }
        else
        {
            transform.localScale = new Vector3(.35f, .2f, 1);
        }
    }

    Vector3 screenPosition(Vector3 pos)
    {
        Vector3 v3 = pos;
        v3.z = 0;
        v3 = Camera.main.WorldToScreenPoint(v3);
        return v3;
    }

    public void DestroySelf()
    {
        Destroy(testPointer.gameObject);
        Destroy(gameObject);
    }
}
