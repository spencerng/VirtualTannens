using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;

public class PlayingCardGenerator : MonoBehaviour
{

    ArrayList allCards;
    GameObject guidingBall;
    bool isPlaying;
    Animator animatorComponent;
    private bool faceUp;
    private bool isRed;

    void Awake() {
        guidingBall = this.gameObject.transform.GetChild(1).gameObject;
        animatorComponent = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        faceUp = true;
        isRed = false;
        isPlaying = false;
        GameObject childCard = this.gameObject.transform.GetChild(0).gameObject;
        

        allCards = new ArrayList();
        allCards.Add(childCard);

        float x, y, z;
        x = childCard.transform.position.x;
        y = childCard.transform.position.y;
        z = childCard.transform.position.z;

        for (int i = 1; i <= 51; i++) {
            GameObject newCard = Instantiate(childCard, gameObject.transform);
            allCards.Add(newCard);
            newCard.transform.position = transform.position + new Vector3(0.03f * i, 0.0001f * i, 0);

            string value = (i % 13 + 1).ToString();

            if (value == "1") {
                value = "A";
            } else if (value == "11") {
                value = "J";
            } else if (value == "12") {
                value = "Q";
            } else if (value == "13") {
                value = "K";
            }

            string suit;
            if (i <= 13) {
                suit = "S";
            } else if (i <= 26) {
                suit = "H";
            } else if (i <= 39) {
                suit = "C";
            } else {
                suit = "D";
            }

            Debug.Log(value + suit);

            Texture cardFace = AssetDatabase.LoadAssetAtPath<Texture>("Assets/Textures/cards_texture/" + value + suit + ".png");
            newCard.GetComponent<Renderer>().materials[2].SetTexture("_MainTex", cardFace);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        if (!animatorComponent.enabled && Input.GetKeyDown(KeyCode.E)) {
            Debug.Log("E Pressed");
            isPlaying = true;
            animatorComponent.enabled = true;
            
            if (faceUp) {
                animatorComponent.SetFloat("Direction", 1.0f);
            } else {
                animatorComponent.SetFloat("Direction", -1.0f);
            }
            animatorComponent.Play("CardLeftToRight");
        }
    }

    public void StartPoint() {
        if (animatorComponent.GetFloat("Direction") == -1.0f) {
            Disable();
        } 
    }

    public void EndPoint() {
        if (animatorComponent.GetFloat("Direction") == 1.0f) {
            Disable();
        } 
    }

    public void Disable() {
        isPlaying = false;
        animatorComponent.enabled = false;
        
        // Transform card back color
        if (!faceUp) {
            isRed = !isRed;
            Texture cardBack;
            if (isRed) {
                cardBack = AssetDatabase.LoadAssetAtPath<Texture>("Assets/Textures/red_rider_back.jpg");
            } else {
                cardBack = AssetDatabase.LoadAssetAtPath<Texture>("Assets/Textures/blue_rider_back.jpg");
            }

            foreach (GameObject card in allCards) {
                card.GetComponent<Renderer>().materials[1].SetTexture("_MainTex", cardBack);
            }

        }
        Debug.Log(faceUp);
        faceUp = !faceUp;
        Debug.Log(faceUp);
    }

    public void Enable() {
        isPlaying = true;
    }
}
