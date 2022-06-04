using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public bool canMove;
    public bool isMoving;
    public Vector2 playerLocation;
    public GameObject player;
    public float speed;
    
    //0 being up, 1 being right, 2 being down, 3 being left
    public int moveDirection;
    public void Start()
    {
        canMove = true;
        playerLocation = new Vector2(0,0);
        speed = 2;
        player = gameObject;
    }

    
    public void Update()
    {
        checkMovement();
        player.transform.position = playerLocation;
    }
    
    void checkMovement()
    {
        isMoving = false;
        if (canMove)
        {
            float horizontal = 0;
            float vertical = 0; 
            if (Input.GetKey(KeyCode.W))
            {
                if (checkCanMove("up"))
                {
                    vertical = 1;
                    isMoving = true;
                }
            }
            else if (Input.GetKey(KeyCode.S))
            {
                if (checkCanMove("down"))
                {
                    vertical = -1;
                    isMoving = true;
                }
            }
            if (Input.GetKey(KeyCode.A))
            {
                if (checkCanMove("left")) {
                    horizontal = -1;
                    gameObject.GetComponent<SpriteRenderer>().flipX = true;
                    isMoving = true;
                }
            }
            else if (Input.GetKey(KeyCode.D))
            {
                if (checkCanMove("right"))
                {
                    horizontal = 1;
                    gameObject.GetComponent<SpriteRenderer>().flipX = false;
                    isMoving = true;
                }
            }

            if (isMoving)
            {
                gameObject.GetComponent<Animator>().SetBool("isRunning",true);
                    
            }
            else
            {
                gameObject.GetComponent<Animator>().SetBool("isRunning",false);
            }

            horizontal = horizontal * Time.deltaTime * speed;
            vertical = vertical * Time.deltaTime * speed;
            transform.Translate(new Vector3(horizontal,vertical));
            playerLocation = new Vector2(playerLocation.x + horizontal, playerLocation.y + vertical);
        }
        else
        {
            isMoving = false;
            gameObject.GetComponent<Animator>().SetBool("isRunning",false);
            
        }
    }

    bool checkCanMove(string s)
    {
        Vector2 direction;
        
        if (s == "up") { direction = transform.TransformDirection(Vector2.up); }
        else if (s == "down") { direction = transform.TransformDirection(Vector2.down); }
        else if (s == "left") { direction = transform.TransformDirection(Vector2.left); }
        else if (s == "right") { direction = transform.TransformDirection(Vector2.right); }
        else { return false;}
        
        RaycastHit2D hit = Physics2D.Raycast(playerLocation,direction,1);
        if (hit)
        {
            if (hit.transform.tag == "Wall"||hit.transform.tag == "Interactable")
            {
                return false;
            }
        }
        
        
        print("no hit");
        return true;
    }
    
    
}
