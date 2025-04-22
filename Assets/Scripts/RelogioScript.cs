using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RelogioScript : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI tempo;
    float tempoDecorrido;
    int inGame = 0;

    // Start is called before the first frame update
    void Start()
    {
        tempoDecorrido = 5;
    }

    // Update is called once per frame
    void Update()
    {
        int minutos = Mathf.FloorToInt(tempoDecorrido / 60);
        int segundos = Mathf.FloorToInt(tempoDecorrido % 60);
        
        //tempoDecorrido = tempoDecorrido + Time.deltaTime;
        //tempoDecorrido = tempoDecorrido + Time.deltaTime;
        
        Debug.Log("---1---");
        if(tempoDecorrido > 0 && inGame == 0)
        {
            tempoDecorrido = tempoDecorrido - Time.deltaTime;
            tempo.text = string.Format("{0:00}m:{1:00}s", minutos, segundos);
        }else {
        Debug.Log("---2---");
        inGame = 1;
        tempoDecorrido = 0;
        }

        //REVER!!!!

         if(inGame == 1) 
        {
            //Debug.Log("entrei no ZERO");
            Debug.Log("IN GAME: "+inGame.ToString());
            Debug.Log("TEMPO GAME: "+tempoDecorrido.ToString());
            Debug.Log("---3---");
            tempoDecorrido = tempoDecorrido + Time.deltaTime;
            tempo.text = string.Format("{0:00}m:{1:00}s", minutos, segundos);
        }
    }


}
