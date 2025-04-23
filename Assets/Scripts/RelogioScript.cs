using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Runtime.CompilerServices;


public class RelogioScript : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI tempo;
    float tempoDecorrido;
    int inGame = 0;
    bool eventoExecutado = false;

    // Start is called before the first frame update
    void Start()
    {
        tempoDecorrido = 15;
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
            if( Mathf.RoundToInt(tempoDecorrido) % 5 == 0 && !eventoExecutado)
            {
                Debug.Log("sao 5s");
                EventRegistry.GetEventPublisher("TriggerEvent").RaiseEvent(this);
                eventoExecutado = true;
                StartCoroutine("WaitForASecond");
            }

        
        }
    }
    IEnumerator WaitForASecond(){
        yield return new WaitForSeconds(1f);
        eventoExecutado = false;
        StopCoroutine("WaitForASecond");

    } 




}
