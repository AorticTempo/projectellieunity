using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class QuickTimeRush : MonoBehaviour
{
    public static int score;
    private Keyboard keyboard;
    private bool isCollide;
    private bool updated;
    private Collider2D collision;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        keyboard = Keyboard.current;
    }
    private void FixedUpdate()
    {
        if (!isCollide)
        {
            if (keyboard.zKey.IsPressed() || 
                keyboard.xKey.IsPressed() || 
                keyboard.cKey.IsPressed() || 
                keyboard.bKey.IsPressed() || 
                keyboard.mKey.IsPressed())
            {
                if (!updated)
                {
                    score -= 50;
                    updated = true;
                }
            }
            else
            {
                updated = false;
            }
        }
        else
        {
            if (collision != null)
            {
                switch (collision.gameObject.name)
                {
                    case "QTE1(Clone)":
                        if (keyboard.zKey.IsPressed())
                        {
                            collision.gameObject.SetActive(false);
                            score += 100;
                            break;
                        }
                        break;
                    case "QTE2(Clone)":
                        if (keyboard.xKey.IsPressed())
                        {
                            collision.gameObject.SetActive(false);
                            score += 100;
                            break;
                        }
                        break;
                    case "QTE3(Clone)":
                        if (keyboard.cKey.IsPressed())
                        {
                            collision.gameObject.SetActive(false);
                            score += 100;
                            break;
                        }
                        break;
                    case "QTE4(Clone)":
                        if (keyboard.bKey.IsPressed())
                        {
                            collision.gameObject.SetActive(false);
                            score += 100;
                            break;
                        }
                        break;
                    case "QTE5(Clone)":
                        if (keyboard.nKey.IsPressed())
                        {
                            collision.gameObject.SetActive(false);
                            score += 100;
                            break;
                        }
                        break;
                    case "QTE6(Clone)":
                        if (keyboard.mKey.IsPressed())
                        {
                            collision.gameObject.SetActive(false);
                            score += 100;
                            break;
                        }
                        break;
                    default:
                        break;
                }
            }
        }
    }
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        isCollide = true;
        this.collision = collision;
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        score -= 50;
    }
}
