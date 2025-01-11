using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    // inherits tge HitPoints through the Character Script
    public HealthBar healthBarPrefab;
    HealthBar healthBar;


    // Start is called before the first frame update
    void Start()
    {
        hitPoints.value = startingHitPoints;
        healthBar = Instantiate(healthBarPrefab);
        healthBar.character = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Pickup"))
        {
            Item hitObject = collision.gameObject.GetComponent<Pickup>().item;
            if (hitObject != null)
            {
                bool shouldDisappear = false;
                switch (hitObject.itemType)
                {
                    case Item.ItemType.COIN:
                        shouldDisappear = true;
                        break;
                    case Item.ItemType.HEALTH:
                        shouldDisappear = true;
                        break;
                    default:
                        shouldDisappear = false;
                        break;
                }
                if (shouldDisappear)
                {
                    collision.gameObject.SetActive(false);
                }
            }
        }
    }
}
