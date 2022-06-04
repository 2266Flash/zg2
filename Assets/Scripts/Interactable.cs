using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Interactable : MonoBehaviour
{
    public string type;
    public int interactioncount;
    public AudioSource audio1;
    public AudioSource audio2;
    public AudioSource audio3;

    public PlayerManager playermanager;
    void Start()
    {
        playermanager = GameObject.Find("Player").GetComponent<PlayerManager>();
        interactioncount = 0;
    }
    
    public void Interact(PlayerManager pm)
    {
        playermanager = pm;
        if (type == "Them")
        {
            runThem();
        }
        else if (type == "TentSleep")
        {
            tentSleep();
        }
        else if (type == "tree") {if (interactioncount == 0) { pm.openDialogueBox("This... is a tree.", audio1, "tree"); } 
            else {pm.openDialogueBox("Nothing to see here.", audio1, "tree1");} }
        
        else if (type == "bush") {if (interactioncount == 0) { pm.openDialogueBox("This... is a bush.", audio1, "bush"); } 
            else {pm.openDialogueBox("Nothing to see here.", audio1, "bush");} }
        
        else if (type == "tent") {if (interactioncount == 0) { pm.openDialogueBox("This tent is poorly designed, and thus is unsuitable.", audio1, "badtent"); } 
            else {pm.openDialogueBox("You can't sleep here. Try somewhere else.", audio1, "badtent");} }
        
        else if (type == "largetent") {if (interactioncount == 0) { pm.openDialogueBox("This tent is already taken", audio1, "takentent"); } 
            else {pm.openDialogueBox("You can't sleep here. Try somewhere else.", audio1, "takentent");} }
        
        else if (type == "nextscene") { SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);}
        else if (type == "toast") { pm.openDialogueBox("Hi, I'm toast!",audio1,"ToastFirst");}
        
        
        interactioncount++;
        
    }


    void tentSleep()
    {
        if (interactioncount == 0)
        {
            playermanager.fadeOut();
            StartCoroutine(tentSleepc());
        }
    }

    IEnumerator tentSleepc()
    {
        yield return new WaitForSeconds(1);
        playermanager.tenseNoise.Play();
        yield return new WaitForSeconds(3);
        playermanager.fadeIn();
        playermanager.tenseNoise.Stop();
        playermanager.GetComponentInParent<Intro>().startZombies();
    }
    void runThem()
    {
        if (interactioncount == 0)
        {
            string msg = "Hi " + playermanager.name + "! My name is...";
            playermanager.openDialogueBox(msg, audio1, "them1");
        }
        else if (interactioncount == 1)
        {
            string msg = "Nevermind. You seem lost, and it will be getting dark soon. Stay here for the night.";
            playermanager.openDialogueBox(msg, audio1, "them2");
        }
        else if (interactioncount == 2)
        {
            string msg = "There is a free tent just behind you, head there.";
            playermanager.openDialogueBox(msg, audio1, "them3");
            GameObject.Find("Arrow").GetComponent<SpriteRenderer>().enabled = true;
            GameObject.Find("Arrow").GetComponent<PointArrow>().enabled = true;
        }
    }
}
