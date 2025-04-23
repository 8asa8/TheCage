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


        //Count Down
        if (tempoDecorrido > 0 && inGame == 0)
        {
            tempoDecorrido = tempoDecorrido - Time.deltaTime;
            tempo.text = string.Format("{0:00}m:{1:00}s", minutos, segundos);
        }
        else
        {
            inGame = 1;
        }

        //Tempo In Game
        if (inGame == 1)
        {
            tempoDecorrido = tempoDecorrido + Time.deltaTime;
            tempo.text = string.Format("{0:00}m:{1:00}s", minutos, segundos);
        }
    }


}
