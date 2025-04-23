using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Campo : MonoBehaviour
{

    public Dictionary<int, Jogador> relacaoPosicaoJogador;


    public TextMeshProUGUI percentagem0Text;
    public TextMeshProUGUI percentagem1Text;
    public TextMeshProUGUI percentagem2Text;


    public void AddPlayerToZone(int posicao, Jogador jogador)
    {
        if (relacaoPosicaoJogador == null)
            relacaoPosicaoJogador = new Dictionary<int, Jogador>();


        relacaoPosicaoJogador.Add(posicao, jogador);

        Image[] transformsOfChildren = GetComponentsInChildren<Image>();
        transformsOfChildren[posicao].sprite = jogador.Image;
        Debug.Log($"Pos> {posicao}, Jogador: {jogador.Nome}");

    }

    public void CalculaPercentagem()
    {
        float percentagem0;
        float percentagem0Inv;
        float Equipa1Def = relacaoPosicaoJogador[1].Def + relacaoPosicaoJogador[2].Def + relacaoPosicaoJogador[3].Def;
        float Equipa2Att = relacaoPosicaoJogador[4].Att + relacaoPosicaoJogador[5].Att + relacaoPosicaoJogador[6].Att;

        percentagem0 = Equipa1Def * 100 / (Equipa1Def + Equipa2Att);
        percentagem0Inv = 100 - percentagem0;
        percentagem0Text.text = percentagem0.ToString("#.") + " / " + percentagem0Inv.ToString("#.");


        float percentagem1;
        float percentagem1Inv;
        float Equipa1Mid = relacaoPosicaoJogador[1].Mid + relacaoPosicaoJogador[2].Mid + relacaoPosicaoJogador[3].Mid;
        float Equipa2Mid = relacaoPosicaoJogador[4].Mid + relacaoPosicaoJogador[5].Mid + relacaoPosicaoJogador[6].Mid;

        percentagem1 = Equipa1Mid * 100 / (Equipa1Mid + Equipa2Mid);
        percentagem1Inv = 100 - percentagem1;
        percentagem1Text.text = percentagem1.ToString("#.") + " / " + percentagem1Inv.ToString("#.");


        float percentagem2;
        float percentagem2Inv;
        float Equipa1Att = relacaoPosicaoJogador[1].Att + relacaoPosicaoJogador[2].Att + relacaoPosicaoJogador[3].Att;
        float Equipa2Def = relacaoPosicaoJogador[4].Def + relacaoPosicaoJogador[5].Def + relacaoPosicaoJogador[6].Def;

        percentagem2 = Equipa1Att * 100 / (Equipa1Att + Equipa2Def);
        percentagem2Inv = 100 - percentagem2;
        percentagem2Text.text = percentagem2.ToString("#.") + " / " + percentagem2Inv.ToString("#.");
    }

}
