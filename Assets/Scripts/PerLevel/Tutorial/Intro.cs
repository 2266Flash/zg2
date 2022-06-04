using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class Intro : MonoBehaviour
{
    PlayerManager pm;
    public AudioSource click;
    public bool zombies;
    private Random r = new Random();
    public List<GameObject> zombieObjects = new List<GameObject>();
    public Sprite zombiesprite;
    public Animator zombieanimator;
    public AudioSource zombieDeath;
    public Canvas darkScreen;
    public ParticleSystem rain;

    private bool spawnedFire;
    public AudioSource zombieNoise;
    public AudioSource rainNoise;

    public List<Vector2> fireLocations;
    void Start()
    {
        pm = gameObject.GetComponent<PlayerManager>();
        zombies = false;
        pm.gameObject.GetComponent<Movement>().canMove = true;
        darkScreen.enabled = false;
        rain.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        if (zombies)
        {
            if (!spawnedFire)
            {
                foreach (Vector2 i in fireLocations)
                {
                    GameObject fireobj = Instantiate(GameObject.Find("fireexample"));
                    fireobj.transform.position = i;
                }

                spawnedFire = true;

            }
            int cap = 100;

            int no = r.Next(0, Mathf.RoundToInt(1 / Time.deltaTime));
            if (no == 1)
            {
                spawnZombie();
            }


            Vector2 direction;
            direction = transform.TransformDirection(Vector2.left);

            RaycastHit2D hit =
                Physics2D.Raycast(pm.gameObject.GetComponent<Movement>().playerLocation, direction, 0.25f);
            if (hit)
            {
                if (hit.transform.tag == "enemy")
                {
                    zombieObjects.Remove(hit.transform.gameObject);
                    zombieDeath.Play();
                    GameObject.Destroy(hit.transform.gameObject);
                    pm.health--;
                }
                else
                {
                    direction = transform.TransformDirection(Vector2.down);
                    hit = Physics2D.Raycast(pm.gameObject.GetComponent<Movement>().playerLocation, direction, 0.25f);
                    if (hit.transform.tag == "enemy")
                    {
                        zombieObjects.Remove(hit.transform.gameObject);
                        zombieDeath.Play();
                        GameObject.Destroy(hit.transform.gameObject);
                        pm.health--;
                    }
                    else
                    {
                        direction = transform.TransformDirection(Vector2.right);

                        hit = Physics2D.Raycast(pm.gameObject.GetComponent<Movement>().playerLocation, direction,
                            0.25f);
                        if (hit.transform.tag == "enemy")
                        {
                            zombieObjects.Remove(hit.transform.gameObject);
                            zombieDeath.Play();
                            GameObject.Destroy(hit.transform.gameObject);
                            pm.health--;
                        }
                        else
                        {
                            direction = transform.TransformDirection(Vector2.up);

                            hit = Physics2D.Raycast(pm.gameObject.GetComponent<Movement>().playerLocation, direction,
                                0.25f);
                            if (hit.transform.tag == "enemy")
                            {
                                zombieObjects.Remove(hit.transform.gameObject);
                                zombieDeath.Play();
                                GameObject.Destroy(hit.transform.gameObject);
                                pm.health--;
                            }
                        }
                    }
                }

            }
        }
        else
            {
                zombieNoise.Stop();
                rainNoise.Stop();
                rain.Stop();
            }
            
    }

    void spawnZombie()
    {
        GameObject zombie = Instantiate(GameObject.Find("zombieexample"));
        zombie.AddComponent<Zombie>();
        int no = r.Next(0, 3);
        if (no == 0)
        {
            zombie.transform.position = new Vector3(20, 20, 0);
        }
        else if (no == 1)
        {
            zombie.transform.position = new Vector3(-20, 20, 0);
        }
        else if (no == 2)
        {
            zombie.transform.position = new Vector3(20, -20, 0);
        }
        else
        {
            zombie.transform.position = new Vector3(-20, -20, 0);
        }

        zombie.tag = "enemy";
        zombieObjects.Add(zombie);

    }

    public void startZombies()
    {
        darkScreen.enabled = true;
        rain.Play();
        rainNoise.Play();
        zombieNoise.Play();
        zombies = true;
    }
    
}
