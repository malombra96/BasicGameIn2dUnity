using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float jumpForce = 6;
    private Rigidbody2D _rigidbody2D;
    private Animator _animator;
    public LayerMask groundMask;  // para detectar el suelo
    public float running_speet = 2f;
    private Vector3 startPosicion;
    // variables para la animaciones
    const string STATE_ALIVE = "IsAlive";
    const string STATE__ON_THE_GROUND = "InOnTheGround";
    const string STATE__STILL = "still";
    
    // variables para el mana 
    public int healPoints, manaPoints;

    public const int    INICIAL_HEALTH = 100,
                        INICIAL_MANA = 15,
                        MAX_HEALTH = 200,
                        MAX_MANA = 30,
                        MIN_HEALTH = 10,
                        MIN_MANA = 0;

    public const int SUPERJUMP_COST = 5;
    public const float SUPERJUMP_FORCE = 1.5F;
    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    void Start()
    {
        _animator.SetBool(STATE_ALIVE, true);
        _animator.SetBool(STATE__ON_THE_GROUND, false);
        _animator.SetBool(STATE__STILL, true);
        startPosicion = this.transform.position;
        _rigidbody2D.velocity = Vector2.zero;
    }

    public void StartGame()
    {
        this.transform.position = startPosicion;
        _rigidbody2D.velocity = Vector2.zero;
        GameObject main_camera = GameObject.Find("Main Camera");
        main_camera.GetComponent<CameraFollow>().ResetCameraPosition();
        _animator.SetBool(STATE_ALIVE, true);
        _animator.SetBool(STATE__STILL, true);
        healPoints = INICIAL_HEALTH;
        manaPoints = INICIAL_MANA;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.sharedInstance.CurrentGameState == GameState.inGame)
        {
            if (Input.GetButtonDown("Jump")) // ------------- SALTO
            {
                Jump(false);
            }

            if (Input.GetButtonDown("SuperJump"))
            {
                Jump(true);
            }

            if (Input.GetAxis("Horizontal") > 0) // ------------- DERECHA
            {
                _rigidbody2D.velocity = new Vector2(running_speet, _rigidbody2D.velocity.y);
                this.transform.localScale = new Vector3(1,1,1);
                _animator.SetBool(STATE__STILL, false);
            }
        
            if (Input.GetAxis("Horizontal") == 0) // ------------- IZQUIERDA
            {
                _animator.SetBool(STATE__STILL, true);
            }
        
            if (Input.GetAxis("Horizontal") < 0) // ---------- QUIETO (STILL)
            {
                _rigidbody2D.velocity = new Vector2(-running_speet, _rigidbody2D.velocity.y);
                this.transform.localScale = new Vector3(-1,1,1);
                _animator.SetBool(STATE__STILL, false);
            }
        }
        else
        {
            _rigidbody2D.velocity = new Vector2(0, _rigidbody2D.velocity.y);
            _animator.SetBool(STATE__STILL, true);
        }
 
        
        _animator.SetBool(STATE__ON_THE_GROUND, IsTouchingTheGround());
        
        Debug.DrawRay(this.transform.position, Vector2.down* 1.3f, Color.red);
    }

    private void FixedUpdate()
    {
        // if (_rigidbody2D.velocity.x < running_speet)
        // {
        //     _rigidbody2D.velocity = new Vector2(running_speet, _rigidbody2D.velocity.y);
        // }
    }

    void Jump(bool superJump)
    {
        float jumpForceFactor = jumpForce; 
        if (superJump && manaPoints >= SUPERJUMP_COST)
        {
            manaPoints -= SUPERJUMP_COST;
            jumpForceFactor *= SUPERJUMP_FORCE;
        }
        
        if(IsTouchingTheGround())
        {
            _rigidbody2D.AddForce(Vector2.up * jumpForceFactor, ForceMode2D.Impulse);
            
        }
    }

    bool IsTouchingTheGround()
    {
        if(Physics2D.Raycast(this.transform.position, Vector2.down, 1.3f, groundMask))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void Die()
    {
        float travelledDistance = GetTraveledDistance();
        float previousMaxDistance = PlayerPrefs.GetFloat("maxscore", 0); // para guardar datos 
        if (travelledDistance > previousMaxDistance)
        {
            PlayerPrefs.SetFloat("maxscore", travelledDistance); // para guardar datos 
        }
        _animator.SetBool(STATE_ALIVE, false);
        GameManager.sharedInstance.GameOver();  
    }

    public void CollectHelth(int points)
    {
        this.healPoints += points;
        if (this.healPoints >= MAX_HEALTH)
        {
            this.healPoints = MAX_HEALTH;
        }

        if (this.healPoints <= 0)
        {
            Die();
        }
    }

    public void CollectMana(int points)
    {
        this.manaPoints += points;
        if (this.manaPoints >= MAX_MANA)
        {
            this.manaPoints = MAX_MANA;
        }
    }

    public int GetHelth()
    {
        return healPoints;
    }

    public int GetMana()
    {
        return manaPoints;
    }
    
    public float GetTraveledDistance()
    {
        return this.transform.position.x - startPosicion.x;
    }
}
