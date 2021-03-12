using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float runningSpeet = 1.5f;
    private Rigidbody2D _rigidbody2D;
    public bool faceRight = false;
    private Vector3 startPosicion;
    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        startPosicion = this.transform.position;
    }

    void Start()
    {
        this.transform.position = startPosicion;
    }
    
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        float currentRunningSpeet = runningSpeet;
        if (faceRight)
        {
            currentRunningSpeet = runningSpeet;
            this.transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else
        {
            currentRunningSpeet = -runningSpeet;
            this.transform.eulerAngles = Vector3.zero;
        }

        if (GameManager.sharedInstance.CurrentGameState == GameState.inGame)
        {
            _rigidbody2D.velocity = new Vector2(currentRunningSpeet, _rigidbody2D.velocity.y);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<PlayerController>().CollectHelth(-10);
            other.GetComponent<Over>().luz = true;
            StartCoroutine(StarAnimation(other.gameObject));
            return;
        }
        
        if (other.tag == "coin")
        {
            return;
        }

        // si llegamos aca solo darle el giro 

        faceRight = !faceRight;

    }

    IEnumerator StarAnimation(GameObject other)
    {
        yield return new WaitForSeconds(1);
        other.GetComponent<Over>().luz = false;
    }
}
