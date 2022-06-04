using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public Image blackScreen;
    private int fading;
    public int health;

    public AudioSource tenseNoise;
    public AudioSource deathMusic;
    public GameObject deathScreen;
    public bool onDeathScreen;
    public AudioSource clickSound;
    public TextMeshProUGUI deathText;
    public string deathMessage;

    public Movement moveScript;

    public Vector2 spawnLoc;
    
    public Image[] healthbar;
    public Image whiteScreen;
    public bool disableRespawn;
    public bool firstLevel;

    public TextMeshProUGUI typingText;

    public string name;

    public TextMeshProUGUI dialogueText;
    public Canvas dialogueBox;
    public string dialogueTag;
    public bool attacking;


    void Start()
    {
        globalVars.p = this;
        fadeIn();
        health = 5;
        GetComponent<Animator>().SetBool("isAlive", true);
        moveScript = gameObject.GetComponent<Movement>();
        name = globalVars.playerName;
        attacking = false;
    }

    // Update is called once per frame
    void Update()
    {
        checkFading();
        checkInput();

        checkHealth();
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        print("Got collision");
        if (other.gameObject.tag == "enemy")
        {
            health--;
            print("reduced health");
        }
    }

    void checkHealth()
    {
        if (health == 5)
        {
            for (int i = 0; i < 5; i++)
            {
                healthbar[i].enabled = true;
            }
        }
        else if (health == 4)
        {

            healthbar[0].enabled = true;
            healthbar[1].enabled = true;
            healthbar[2].enabled = true;
            healthbar[3].enabled = true;
            healthbar[4].enabled = false;
        }
        else if (health == 3)
        {

            healthbar[0].enabled = true;
            healthbar[1].enabled = true;
            healthbar[2].enabled = true;
            healthbar[3].enabled = false;
            healthbar[4].enabled = false;
        }
        else if (health == 2)
        {
            healthbar[0].enabled = true;
            healthbar[1].enabled = true;
            healthbar[2].enabled = false;
            healthbar[3].enabled = false;
            healthbar[4].enabled = false;
        }
        else if (health == 1)
        {
            healthbar[0].enabled = true;
            healthbar[1].enabled = false;
            healthbar[2].enabled = false;
            healthbar[3].enabled = false;
            healthbar[4].enabled = false;
        }
        else if (health == 0)
        {
            for (int i = 0; i < 5; i++)
            {
                healthbar[i].enabled = false;
            }

            health = -1;
            killPlayer();
        }
    }

    public void killPlayer()
    {
        GetComponent<Animator>().SetBool("isAlive", false);
        GetComponent<Animator>().SetBool("isDieing", true);
        GetComponent<Animator>().SetBool("isDead", true);

        StartCoroutine(processDeath());
    }


    IEnumerator processDeath()
    {
        if (firstLevel)
        {
            gameObject.GetComponent<Intro>().zombies = false;
            foreach (GameObject i in gameObject.GetComponent<Intro>().zombieObjects)
            {
                GameObject.Destroy(i);
            }
            gameObject.GetComponent<Intro>().zombies = false;
        }
        GetComponent<Animator>().SetBool("isAlive", false);
        gameObject.GetComponent<Movement>().canMove = false;
        tenseNoise.Play();
        yield return new WaitForSeconds(1);
        fadeOut();
        closeDialogueBox();
        yield return new WaitForSeconds(1);
        deathMusic.Play();
        deathScreen.GetComponent<Animator>().SetBool("fadein",true);
        StartCoroutine(globalVars.textTyper(clickSound, deathMessage, deathText));
        yield return new WaitForSeconds(1);
        deathScreen.GetComponent<Animator>().SetBool("fadein",false);
        onDeathScreen = true;

        if (firstLevel)
        {
            StartCoroutine(processFirstLevelDeath());
        }

    }
    
    IEnumerator processRevive()
    {
        onDeathScreen = false;
        moveScript.canMove = true;
        tenseNoise.Play();
        deathMusic.Stop();
        moveScript.playerLocation = spawnLoc;
        deathScreen.GetComponent<Animator>().SetBool("fadeout",true);
        yield return new WaitForSeconds(1);
        deathScreen.GetComponent<Animator>().SetBool("fadeout",false);
        yield return new WaitForSeconds(0.2f);
        clickSound.Play();
        whiteScreen.enabled = true;
        yield return new WaitForSeconds(0.2f);
        whiteScreen.enabled = false;
        clickSound.Play();
        yield return new WaitForSeconds(0.2f);
        GetComponent<Animator>().SetBool("isAlive", true);
        GetComponent<Animator>().SetBool("isDead", false);
        GetComponent<Animator>().SetBool("isDieing", false);
        fadeIn();
        
        

    }
    void checkInput()
    {
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Z))
        {
            processInteraction();
        }

        if (Input.GetKeyDown(KeyCode.L) || Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.Backspace))
        {
            processAttack();
        }
        
    }

    void processAttack()
    {
        if (!attacking)
        {
            gameObject.GetComponent<Animator>().SetBool("isAttacking",true);
            attacking = true;
            StartCoroutine(runAttack());
            
        }
    }

    IEnumerator runAttack()
    {
        yield return new WaitForSeconds(0.5f);
        gameObject.GetComponent<Animator>().SetBool("isAttacking",false);
        attacking = false;
        Vector2 direction;
        RaycastHit2D hit;
        direction = transform.TransformDirection(Vector2.up);
        hit = Physics2D.Raycast(gameObject.GetComponent<Movement>().playerLocation,direction,2);
        if (hit)
        {
            if (hit.transform.tag == "enemy" || hit.transform.tag == "killable")
            {
                StartCoroutine(damage(hit));
            }
        }
        direction = transform.TransformDirection(Vector2.down);
        hit = Physics2D.Raycast(gameObject.GetComponent<Movement>().playerLocation,direction,2);
        if (hit)
        {
            if (hit.transform.tag == "enemy" || hit.transform.tag == "killable")
            {
                StartCoroutine(damage(hit));
            }
        }
        direction = transform.TransformDirection(Vector2.left);
        hit = Physics2D.Raycast(gameObject.GetComponent<Movement>().playerLocation, direction, 2);
        if (hit)
        {
            if (hit.transform.tag == "enemy" || hit.transform.tag == "killable")
            {
                StartCoroutine(damage(hit));
            }
        }
        direction = transform.TransformDirection(Vector2.right);
        hit = Physics2D.Raycast(gameObject.GetComponent<Movement>().playerLocation, direction, 2);
        if (hit)
        {
            if (hit.transform.tag == "enemy" || hit.transform.tag == "killable")
            {
                StartCoroutine(damage(hit));
            }
        }


    }

    IEnumerator damage(RaycastHit2D hit)
    {
        hit.transform.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        hit.transform.gameObject.GetComponent<Killable>().reduceHealth();
        yield return new WaitForSeconds(0.5f);
        hit.transform.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
    }
    

    void processInteraction()
    {
        if (onDeathScreen)
        {
            if (!disableRespawn)
            {
                StartCoroutine(processRevive());
            }
        }

        else if (dialogueBox.enabled)
        {
            closeDialogueBox();
        }
        else
        {
            Vector2 direction;
        
            if (Input.GetKey(KeyCode.W)) { direction = transform.TransformDirection(Vector2.up); }
            else if (Input.GetKey(KeyCode.S)) { direction = transform.TransformDirection(Vector2.down); }
            else if (Input.GetKey(KeyCode.A)) { direction = transform.TransformDirection(Vector2.left); }
            else { direction = transform.TransformDirection(Vector2.right); }

            RaycastHit2D hit = Physics2D.Raycast(gameObject.GetComponent<Movement>().playerLocation,direction,2);
            if (hit)
            {
                if (hit.transform.tag == "Interactable")
                {
                    hit.transform.gameObject.GetComponent<Interactable>().Interact(this);
                }
            }
            else
            {
                direction = transform.TransformDirection(Vector2.up);
                hit = Physics2D.Raycast(gameObject.GetComponent<Movement>().playerLocation,direction,2);
                if (!hit)
                {
                    direction = transform.TransformDirection(Vector2.down);
                    hit = Physics2D.Raycast(gameObject.GetComponent<Movement>().playerLocation,direction,2);
                    if (!hit)
                    {
                        direction = transform.TransformDirection(Vector2.left); 
                        hit = Physics2D.Raycast(gameObject.GetComponent<Movement>().playerLocation,direction,2);
                        if (!hit)
                        {
                            direction = transform.TransformDirection(Vector2.right); 
                            hit = Physics2D.Raycast(gameObject.GetComponent<Movement>().playerLocation,direction,2);
                            if (!hit)
                            {
                                return;
                            }
                        }
                        
                    }
                    
                }
                if (hit.transform.tag == "Interactable")
                {
                    hit.transform.gameObject.GetComponent<Interactable>().Interact(this);
                }
                
            }
        }
        
    }
    void checkFading()
    {
        if (fading >0)
        {
            fading++;
            if (fading > 2)
            {
                fading = 0;
                blackScreen.GetComponent<Animator>().SetBool("fadeout",false);
                blackScreen.GetComponent<Animator>().SetBool("fadein",false);
            }
        }
    }
    public void fadeOut()
    {
        print("processed");
        blackScreen.GetComponent<Animator>().SetBool("fadeout",true);
        fading = 1;
    }
    
    public void fadeIn()
    {
        blackScreen.GetComponent<Animator>().SetBool("fadein",true);
        fading = 1;
    }



    IEnumerator processFirstLevelDeath()
    {
        yield return new WaitForSeconds(10);
        deathMusic.Stop();
        deathScreen.GetComponent<Animator>().SetBool("fadeout",true);
        whiteScreen.enabled = true;
        clickSound.Play();
        yield return new WaitForSeconds(1);
        whiteScreen.enabled = false;
        clickSound.Play();
        yield return new WaitForSeconds(1);
        whiteScreen.enabled = true;
        clickSound.Play();
        yield return new WaitForSeconds(0.5f);
        whiteScreen.enabled = false;
        clickSound.Play();
        yield return new WaitForSeconds(0.5f);
        clickSound.Stop();
        whiteScreen.enabled = true;
        clickSound.Play();
        yield return new WaitForSeconds(0.25f);
        clickSound.Stop();
        whiteScreen.enabled = false;
        clickSound.Play();
        yield return new WaitForSeconds(0.15f);
        clickSound.Stop();
        whiteScreen.enabled = true;
        clickSound.Play();
        yield return new WaitForSeconds(0.1f);
        clickSound.Stop();
        whiteScreen.enabled = false;
        clickSound.Play();
        yield return new WaitForSeconds(0.1f);
        clickSound.Stop();
        whiteScreen.enabled = true;
        clickSound.Play();
        yield return new WaitForSeconds(1f);
        clickSound.Stop();
        whiteScreen.enabled = false;
        clickSound.Play();
        string text = "Death is only the beginning...";
        /*for (int i = 0; i < text.Length+1; i++)
        {
            clickSound.Stop();
            typingText.text = text.Substring(0, i);
            clickSound.Play();
            yield return new WaitForSeconds(0.1f);
        }*/
        StartCoroutine(globalVars.textTyper(clickSound, text, typingText));
        
        yield return new WaitForSeconds(6f);
        clickSound.Stop();
        whiteScreen.enabled = true;
        clickSound.Play();
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }

    public void openDialogueBox(string text,AudioSource typingNoise,string dialogueTagSelect)
    {
        closeDialogueBox();
        dialogueBox.enabled = true;
        dialogueTag = dialogueTagSelect;
        StartCoroutine(runTyping(text, typingNoise, dialogueTagSelect));

        if (GetComponent<Movement>().canMove)
        {
            GetComponent<Movement>().canMove = false;
        }
        


    }

    IEnumerator runTyping(string text, AudioSource noise,string tag)
    {
        for (int i = 0; i < text.Length; i++)
        {
            if (dialogueBox.enabled)
            {
                if (tag == dialogueTag)
                {
                    yield return new WaitForSeconds(0.1f);
                    dialogueText.text = dialogueText.text + text[i];
                    noise.Play();
                }
            }
        }
        
        yield return new WaitForSeconds(5f);
        if (dialogueTag == tag)
        {
            closeDialogueBox();
        }
    }
    public void closeDialogueBox()
    {
        dialogueTag = "";
        dialogueBox.enabled = false;
        dialogueText.text = "";
        
        GetComponent<Movement>().canMove = true;

    }
}
