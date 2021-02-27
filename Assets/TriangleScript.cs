using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using rand = UnityEngine.Random;

public class TriangleScript : MonoBehaviour {

    public KMBombInfo bomb;
    public KMAudio audio;
    public MeshRenderer triangleRenderer;
    public KMSelectable triangle;
    public TextMesh textmesh;

    public MeshRenderer ledRenderer;

    static int moduleIdCounter = 1;
    int moduleId;
    private bool moduleSolved;

    private string[] Alphabet = { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "n", "o",
        "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };

    private Color[] Colors = { new Color(1f, 0f, 0f), new Color(1f, 0.5f, 0f), new Color(1f, 1f, 0f),
        new Color(0f, 1f, 0f), new Color(0f, 0.75f, 1f), new Color(0.25f, 0f, 1f), new Color(0.75f, 0f, 1f), };
    private string[] possibleColors = { "red", "orange", "yellow", "green", "aqua", "purple", "violet" };
    private int intColor;
    private int ledIntColor;


    void Awake()
    {
        moduleId = moduleIdCounter++;
        Randomize();

    }

    void Start ()
    {

        //Making Buttons Work
        triangle.OnInteract += delegate () { StartCoroutine(pressTriangle()); return false; };
        triangle.OnInteractEnded += delegate () { StartCoroutine(releaseTriangle()); };

    }

    private void Randomize()
    {

        //Random Color
        intColor = rand.Range(0, 7);
        triangleRenderer.material.color = Colors[intColor];

        Debug.Log("Generated Color: " + possibleColors[intColor]);

        //Random Letter
        string letter = Alphabet[rand.Range(0, Alphabet.Length)].ToUpper();
        textmesh.text = letter;
        Debug.Log("Generated Letter: " + letter);

        //Random LED
        ledIntColor = rand.Range(0, 7);
        ledRenderer.material.color = Colors[ledIntColor];
        Debug.Log("Generated LED Color: " + possibleColors[ledIntColor]);

    }

    private IEnumerator pressTriangle()
    {

        GetComponent<KMAudio>().PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, transform);
        triangle.AddInteractionPunch();

        for (int i = 0; i < 5; i++)
        {

            triangle.transform.localPosition -= new Vector3(0, .00445f / 5, 0);
            yield return null;

        }

    }

    private IEnumerator releaseTriangle()
    {

        GetComponent<KMAudio>().PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonRelease, transform);

        for (int i = 0; i < 5; i++)
        {

            triangle.transform.localPosition += new Vector3(0, .00445f / 5, 0);
            yield return null;

        }

        GetComponent<KMBombModule>().HandlePass();

    }
	
}
