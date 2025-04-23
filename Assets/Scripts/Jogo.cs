using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Jogo : MonoBehaviour
{
    [SerializeField] Equipa equipa1;
    [SerializeField] Equipa equipa2;
    public Campo campo;
    //public float percentagem0;
    //public TextMeshProUGUI percentagem0Text;





    void Start()
    {
        //campo = new Campo(); 
        Debug.Log(equipa1.nome);
        int i = 1;
        int j = 6;
        foreach (Jogador jogador in equipa1.Jogadores)
        {
            Debug.Log($"Nome: {jogador.Nome}");
            campo.AddPlayerToZone(i, jogador);
            i++;
        }


        Debug.Log(equipa2.nome);
        foreach (Jogador jogador in equipa2.Jogadores)
        {
            Debug.Log($"Nome: {jogador.Nome}");
            campo.AddPlayerToZone(j, jogador);
            j--;

        }
    }

    // Update is called once per frame
    void Update()
    {
        campo.CalculaPercentagem();
    }

    /*void CalculaPercentagem()
    {
        //percentagem0.text = campo.relacaoPosicaoJogador[1].Def.ToString();
        //Mudar isto para o CAMPO
        percentagem0 = equipa1.CaculateOverallDef() / (equipa1.CaculateOverallDef() + equipa2.CaculateOverallAtt());
        percentagem0Text.text = percentagem0.ToString();
    }*/
}
