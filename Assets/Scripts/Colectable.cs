using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CollectableType
{
    healPotion,
    manaPotion,
    money
}
public class Colectable : MonoBehaviour
{
    public CollectableType Type = CollectableType.money;
    private SpriteRenderer sprite;
    private CircleCollider2D _collider2D;
    private GameObject player;
    private bool hasBeenCollected = false;
    public int value = 1;

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        _collider2D = GetComponent<CircleCollider2D>();
        player = GameObject.Find("Player");
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Collect();            
        }
    }

    void Show() // metodo para mostrar las monedas
    {
        sprite.enabled = true;
        _collider2D.enabled = true;
        hasBeenCollected = false;
    }

    void Hide() //metodo para ocultar la moneda
    {
        sprite.enabled = false;
        _collider2D.enabled = false;

        switch (this.Type)
        {
            case CollectableType.money:

                GameManager.sharedInstance.CollectObject(this);
                
                break;
            case CollectableType.healPotion:
                player.GetComponent<PlayerController>().CollectHelth(this.value);
                break;
            case CollectableType.manaPotion:
                player.GetComponent<PlayerController>().CollectMana(this.value); 
                break;
        }
    }

    void Collect() // recolectar las monedas
    {
        Hide();
        hasBeenCollected = true;
    }
}
