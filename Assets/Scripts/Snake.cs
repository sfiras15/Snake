using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class Snake : MonoBehaviour
{
    Rigidbody2D rb2D;
    Vector3 direction = Vector3.right;
    public Transform bodyPrefab;
    List<Transform> bodies;
    int foodScore = 0;
    bool secretDoor = false;
    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        bodies = new List<Transform>();
        bodies.Add(this.transform);
        transform.position = new Vector3(Mathf.Round(transform.position.x) + direction.x,
        Mathf.Round(transform.position.y) + direction.y);
        for (int i = 0; i < 3; i++)
        {
            Grow();
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.UpArrow) && direction != Vector3.down)
        {
            direction = Vector3.up;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) && direction != Vector3.up)
        {
            direction = Vector3.down;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) && direction != Vector3.left)
        {
            direction = Vector3.right;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) && direction != Vector3.right)
        {
            direction = Vector3.left;
        }
    }
    private void FixedUpdate()
    {
        for (int i = bodies.Count - 1; i > 0 ; i--)
        {
            bodies[i].position = bodies[i - 1].position;
        }

        transform.position = new Vector3(Mathf.Round(transform.position.x) + direction.x,
        Mathf.Round(transform.position.y) + direction.y);
    }
        
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Food")
        {
            Grow();
            foodScore++;
            if (foodScore >= 3 & !secretDoor)
            {
                Debug.Log("GG");
                secretDoor = true;
            }
        }
        if (collision.gameObject.tag == "Body")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
    public void Grow()
    {
        Transform body = Instantiate(bodyPrefab);
        body.position = bodies[bodies.Count - 1].position;
        bodies.Add(body);
    }
}
