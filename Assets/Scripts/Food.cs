using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    public BoxCollider2D gridArea;
    Vector3 newCell;
    Bounds bounds;
    // Start is called before the first frame update
    void Start()
    {
        Randomize();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Randomize()
    {
        bounds = gridArea.bounds;
        newCell.x = Random.Range(bounds.min.x, bounds.max.x);
        newCell.y = Random.Range(bounds.min.y, bounds.max.y);
        transform.position = new Vector3(Mathf.Round(newCell.x), Mathf.Round(newCell.y));
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Randomize();
        }
    }
}
