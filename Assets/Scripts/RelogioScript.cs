using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RelogioScript : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI tempo;
    float tempoDecorrido;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        int minutos = Mathf.FloorToInt(tempoDecorrido / 60);
        int segundos = Mathf.FloorToInt(tempoDecorrido % 60);
        tempoDecorrido = tempoDecorrido + Time.deltaTime;
        tempo.text = string.Format("{0:00}m:{1:00}s", minutos, segundos);

        if (segundos == 10)
        {
            Debug.Log("passaram 10s");
        }
    }
}
