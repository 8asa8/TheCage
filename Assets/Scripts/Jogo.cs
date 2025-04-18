using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jogo : MonoBehaviour
{
    [SerializeField] Equipa equipa1;
    [SerializeField] Equipa equipa2;
    // Start is called before the first frame update
    void Start()
    {
        foreach (Jogador jogador in equipa1.Jogadores)
        {
            Debug.Log($"Nome: {jogador.Nome}");
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
