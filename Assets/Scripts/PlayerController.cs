using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    public float speed = 0.1f;
    public bool HasKey = false;
    Vector3 startingPosition;

    private float ClickTime;
    private bool doubleClicked = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startingPosition = gameObject.transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mouseInSpace = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetMouseButtonDown(0))
        { 
            float timeSinceLastClick = Time.time - ClickTime;
            if (timeSinceLastClick <= .2f)
            {
                doubleClicked = true;
            }
           ClickTime = Time.time;

            if (doubleClicked)
            {
                Debug.Log("Dash!");
                speed = 6f;
                Invoke("ResetSpeed", 1.5f);
            }

            StopAllCoroutines();
            StartCoroutine(MoveTowards(transform.position, mouseInSpace, speed));
          
        }
    }

    IEnumerator MoveTowards(Vector3 start, Vector3 destination, float speed)
    {
        while ((transform.position - destination).sqrMagnitude > 0.01f)
        {
            transform.position = Vector2.MoveTowards(transform.position, destination,
                speed * Time.deltaTime);
            yield return null;
        }
    }

    public void ResetPlayer()
    {
        this.gameObject.transform.position = startingPosition;
    }

    public void ResetSpeed()
    {
        speed = 3f;
    }

}

