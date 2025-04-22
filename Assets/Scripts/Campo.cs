using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Campo : MonoBehaviour
{

    public Dictionary<int, Jogador> relacaoPosicaoJogador;



    public void AddPlayerToZone(int posicao, Jogador jogador)
    {
        if(relacaoPosicaoJogador  == null ) 
            relacaoPosicaoJogador = new Dictionary<int, Jogador>();

        
        relacaoPosicaoJogador.Add(posicao, jogador);
        
        Image[] transformsOfChildren = GetComponentsInChildren<Image>();
        transformsOfChildren[posicao].sprite = jogador.Image;
        Debug.Log($"Pos> {posicao}, Jogador: {jogador.Nome}");

    }
}
