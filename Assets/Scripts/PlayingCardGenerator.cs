using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;

public class PlayingCardGenerator : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject childCard = this.gameObject.transform.GetChild(0).gameObject;

        float x, y, z;
        x = childCard.transform.position.x;
        y = childCard.transform.position.y;
        z = childCard.transform.position.z;

        for (int i = 0; i <= 51; i++) {
            GameObject newCard = Instantiate(childCard, gameObject.transform);
            newCard.transform.position = transform.position + new Vector3(0.03f * i, 0.03f * i, 0);

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
            newCard.GetComponent<Renderer>().materials[1].SetTexture("_MainTex", cardFace);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
