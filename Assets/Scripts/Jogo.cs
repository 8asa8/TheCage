using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jogo : MonoBehaviour
{
    [SerializeField] Equipa equipa1;
    [SerializeField] Equipa equipa2;

     
     public Campo campo;
    // Start is called before the first frame update
    void Start()
    {
        //campo = new Campo(); 
        Debug.Log(equipa1.nome);
        int i = 1;
        int j = 6;
        foreach (Jogador jogador in equipa1.Jogadores)
        {
            Debug.Log($"Nome: {jogador.Nome}");
            campo.AddPlayerToZone(i,jogador);
            i++;
        }

        
        Debug.Log(equipa2.nome);
        foreach (Jogador jogador in equipa2.Jogadores)
        {
            Debug.Log($"Nome: {jogador.Nome}");
            campo.AddPlayerToZone(j,jogador);
            j--;

        }

        //Atribuir posicoes
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}
