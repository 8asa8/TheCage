using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jogo : MonoBehaviour
{
    [SerializeField] Equipa equipa;
    // Start is called before the first frame update
    void Start()
    {
        foreach (Jogador jogador in equipa.Jogadores)
        {
            Debug.Log($"Nome: {jogador.Nome}");
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
