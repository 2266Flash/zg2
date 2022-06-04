using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = System.Random;

public class Beginning : MonoBehaviour
{

    public AudioSource thud;
    public Canvas titleScreen;

    public Canvas playScreen;
    public AudioSource titleMusic;

    public Canvas controls;

    public string currentMenu;
    private bool canSelect;

    public Transform shake;

    public Canvas pickName;

    public List<Image> nameSelection;
    public string[,] nameChars = new string[3,7];
    public string currentName;
    public TextMeshProUGUI nameText;
    public string currentSelection;
    private int currentRow = 0;
    private int currentPosition = 0;
    public Sprite normalButton;
    public Sprite clickedButton;
    public Canvas whiteScreen;
    private string[] alphabet = new string[] {"A","B","C","D","E","F","G","H","I","J","K","L","M","N","O","P","Q","R","S","T","U","V","W","X","Y","Z","_","","",""};

    void Start()
    {
        int total = 0;
        for (int row = 0; row < 3; row++)
        {
            for (int i = 0; i < 7; i++)
            {
                nameChars[row, i] = alphabet[total];
                total++;
            }
        }
        print(alphabet);

        StartCoroutine(startSequence());
        titleScreen.enabled = false;
        playScreen.enabled = false;
        pickName.enabled = false;
        canSelect = false;
        currentMenu = "splash";
        controls.enabled = false;
        whiteScreen.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        checkInput();

        checkName();
    }


    void checkName()
    {
        nameText.text = currentName;
    }
    void checkInput()
    {
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Z))
        {
            if (currentMenu == "pressEnter")
            {
                playScreen.enabled = false;
                controls.enabled = true;
                currentMenu = "controls";
            }
            else if (currentMenu == "controls")
            {
                controls.enabled = false;
                pickName.enabled = true;
                currentMenu = "pickName";
            }
            else if (currentMenu == "pickName")
            {
                if (canSelect)
                {
                    if (currentSelection == "ENTER")
                    {
                        canSelect = false;
                        pickName.enabled = false;
                        thud.pitch = 1.2f;
                        globalVars.playerName = currentName;
                        thud.Play();
                        StartCoroutine(itBegins());
                    }
                    else if (currentSelection == "_")
                    {
                        string newcurrentName = "";
                        for (int i = 0; i < currentName.Length-1; i++)
                        {
                            newcurrentName = newcurrentName + currentName[i];
                        }
                        currentName = newcurrentName;

                    }
                    else
                    {
                        currentName += currentSelection;
                    }
                }
            }
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            int currentSelectionloc = (9 * currentRow) + currentPosition;
            if (currentPosition != 0 & currentRow != 3)
            {
                nameSelection[currentSelectionloc].sprite = normalButton;
                nameSelection[currentSelectionloc].color = Color.white;
                currentPosition--;
                int currentSelectionloc2 = (9 * currentRow) + currentPosition;
                currentSelection = alphabet[currentSelectionloc2];
                nameSelection[currentSelectionloc2].sprite = clickedButton;
                nameSelection[currentSelectionloc2].color = Color.red;
            }
            else
            {
                thud.Play();
            }
            
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            int currentSelectionloc = (9 * currentRow) + currentPosition;
            if (currentPosition != 8 & currentRow!=3)
            {
                nameSelection[currentSelectionloc].sprite = normalButton;
                nameSelection[currentSelectionloc].color = Color.white;
                currentPosition++;
                int currentSelectionloc2 = (9 * currentRow) + currentPosition;
                currentSelection = alphabet[currentSelectionloc2];
                nameSelection[currentSelectionloc2].color = Color.red;
                nameSelection[currentSelectionloc2].sprite = clickedButton;
            }
            else
            {
                thud.Play();
            }
            
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            int currentSelectionloc = (9 * currentRow) + currentPosition;
            if (currentRow != 0 & currentRow != 3)
            {
                nameSelection[currentSelectionloc].sprite = normalButton;
                nameSelection[currentSelectionloc].color = Color.white;
                currentRow--;
                int currentSelectionloc2 = (9 * currentRow) + currentPosition;
                currentSelection = alphabet[currentSelectionloc2];
                nameSelection[currentSelectionloc2].color = Color.red;
                nameSelection[currentSelectionloc2].sprite = clickedButton;
            }
            else if (currentRow == 3)
            {
                nameSelection[27].color = Color.white;
                currentRow--;
                currentPosition = 0;
                int currentSelectionloc2 = (9 * currentRow) + currentPosition;
                currentSelection = alphabet[18];
                nameSelection[18].color = Color.red;
                nameSelection[18].sprite = clickedButton;
            }
            else
            {
                thud.Play();
            }
            
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            int currentSelectionloc = (9 * currentRow) + currentPosition;
            if (currentRow != 2 & currentRow != 3)
            {
                nameSelection[currentSelectionloc].sprite = normalButton;
                nameSelection[currentSelectionloc].color = Color.white;
                currentRow++;
                int currentSelectionloc2 = (9 * currentRow) + currentPosition;
                currentSelection = alphabet[currentSelectionloc2];
                nameSelection[currentSelectionloc2].color = Color.red;
                nameSelection[currentSelectionloc2].sprite = clickedButton;
            }
            else if (currentRow == 2)
            {
                nameSelection[currentSelectionloc].sprite = normalButton;
                nameSelection[currentSelectionloc].color = Color.white;
                currentRow++;
                currentPosition = 0;
                int currentSelectionloc2 = (9 * currentRow) + currentPosition;
                currentSelection = "ENTER";
                nameSelection[27].color = Color.red;
            }
            else
            {
                thud.Play();
            }
            
        }
        
    }

    
    
    IEnumerator startSequence()
    {
        yield return new WaitForSeconds(1);
        thud.Play();
        titleScreen.enabled = true;
        yield return new WaitForSeconds(4);
        titleScreen.enabled = false;
        yield return new WaitForSeconds(1);
        titleMusic.Play();
        playScreen.enabled = true;
        canSelect = true;
        currentMenu = "pressEnter";
    }

    IEnumerator itBegins()
    {
        titleMusic.Stop();
        yield return new WaitForSeconds(3);
        thud.Play();
        whiteScreen.enabled = true;
        yield return new WaitForSeconds(3);
        thud.Play();
        whiteScreen.enabled = false;
        yield return new WaitForSeconds(2);
        thud.Play();
        thud.pitch = thud.pitch + 0.05f;
        whiteScreen.enabled = true;
        yield return new WaitForSeconds(2);
        thud.Play();
        thud.pitch = thud.pitch + 0.05f;
        whiteScreen.enabled = false;
        yield return new WaitForSeconds(1);
        thud.Play();
        thud.pitch = thud.pitch + 0.05f;
        whiteScreen.enabled = true;
        yield return new WaitForSeconds(1);
        thud.Play();
        thud.pitch = thud.pitch + 0.05f;
        whiteScreen.enabled = false;
        yield return new WaitForSeconds(1);
        thud.Play();
        thud.pitch = thud.pitch + 0.05f;
        whiteScreen.enabled = true;
        yield return new WaitForSeconds(0.5f);
        thud.Play();
        thud.pitch = thud.pitch + 0.05f;
        whiteScreen.enabled = false;
        for (int i = 0; i < 20; i++)
        {
            yield return new WaitForSeconds(0.2f);
            thud.Play();
            thud.pitch = thud.pitch + 0.05f;
            if ((i % 2) > 0)
            {
                whiteScreen.enabled = true;
            }
            else
            {
                whiteScreen.enabled = false;
            }
        }

        whiteScreen.GetComponentInChildren<Shake>().enabled = false;
        whiteScreen.GetComponentInChildren<Transform>().position = new Vector3(0, 0, 0);
        thud.pitch = 0.5f;
        whiteScreen.enabled = false;
        yield return new WaitForSeconds(3f);
        whiteScreen.enabled = true;
        thud.Play();
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
