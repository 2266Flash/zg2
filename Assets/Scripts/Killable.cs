using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Killable : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;
    public AudioSource toast;
    private bool deathmsgsent;

    void Start()
    {
        currentHealth = maxHealth;
        deathmsgsent = false;
    }

    public void reduceHealth()
    {
        currentHealth--;
        if (currentHealth == 9 & gameObject.name=="Toast")
        {
            GameObject.Find("Player").GetComponent<PlayerManager>()
                .openDialogueBox("Bit rude.", toast, "toastrude");
        }
        if (currentHealth == 8 & gameObject.name=="Toast")
        {
            GameObject.Find("Player").GetComponent<PlayerManager>()
                .openDialogueBox("Please stop", toast, "toastrude2");
        }
        if (currentHealth == 7 & gameObject.name=="Toast")
        {
            GameObject.Find("Player").GetComponent<PlayerManager>()
                .openDialogueBox("This is getting annoying now.", toast, "toastrude3");
        }
        if (currentHealth == 5 & gameObject.name=="Toast")
        {
            GameObject.Find("Player").GetComponent<PlayerManager>()
                .openDialogueBox("I've only got 5 hp left.", toast, "toastrude4");
        }
        if (currentHealth == 3 & gameObject.name=="Toast")
        {
            GameObject.Find("Player").GetComponent<PlayerManager>()
                .openDialogueBox("STOP PLEASE", toast, "toastrude5");
        }
        if (currentHealth == 1 & gameObject.name=="Toast")
        {
            GameObject.Find("Player").GetComponent<PlayerManager>()
                .openDialogueBox("I'm begging you, please stop.", toast, "toastrude1");
        }
        if (currentHealth <= 0)
        {
            if (gameObject.name == "Toast")
            {
                if (!deathmsgsent)
                {
                    gameObject.GetComponent<Animator>().SetBool("Dead", true);
                    GameObject.Find("Player").GetComponent<PlayerManager>()
                        .openDialogueBox("I'm toast.", toast, "toastistoast");
                    globalVars.cruelty++;
                    StartCoroutine(killToast());
                    deathmsgsent = true;
                }
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }

    IEnumerator killToast()
    {
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }
    
}
