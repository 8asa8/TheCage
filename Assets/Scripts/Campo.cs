using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Campo : MonoBehaviour
{

    public Dictionary<int, Jogador> relacaoPosicaoJogador;
    public TextMeshProUGUI percentagem0Text;
    public TextMeshProUGUI percentagem1Text;
    public TextMeshProUGUI percentagem2Text;
    public int campoZona;
    public float percentagem0;
    public float percentagem0Inv;
    public float percentagem1;
    public float percentagem1Inv;
    public float percentagem2;
    public float percentagem2Inv;
    public float DifPvp;
    public float Equipa1Def;
    public float Equipa1Mid;
    public float Equipa1Att;
    public float Equipa2Def;
    public float Equipa2Mid;
    public float Equipa2Att;


    public void Start()
    {
                    relacaoPosicaoJogador = new Dictionary<int, Jogador>();
    }
    public void AddPlayerToZone(int posicao, Jogador jogador)
    {
        relacaoPosicaoJogador.Add(posicao, jogador);
        Image playerImage = GameObject.Find($"/Canvas/Campo/Pos{posicao}/PosImage").GetComponent<Image>(); 
        playerImage.sprite = jogador.Image;
        Debug.Log($"Pos> {posicao}, Jogador: {jogador.Nome}");

    }

    public void SelecionarPosicao(int posicao){
        string borderPath = $"/Canvas/Campo/Pos{posicao}/PosBorder";

        Debug.Log(borderPath);
        GameObject playerBorder = GameObject.Find(borderPath); 
        playerBorder.SetActive(true);
    }
    public void DeselecionarPosicao(int posicao){
        GameObject playerBorder = GameObject.Find($"/Canvas/Campo/Pos{posicao}/PosBorder"); 
        playerBorder.SetActive(false);
    }

    public Jogador GetJogador(int posicao){
        return relacaoPosicaoJogador[posicao];
    }

    public int GetJogadorPosicao(Jogador jogador){
        return relacaoPosicaoJogador.FirstOrDefault(posJogador => posJogador.Value == jogador).Key;
    }

    public void CalculaPercentagem()
    {
        
       Equipa1Def = relacaoPosicaoJogador[1].Def + relacaoPosicaoJogador[2].Def + relacaoPosicaoJogador[3].Def;
       Equipa2Att = relacaoPosicaoJogador[4].Att + relacaoPosicaoJogador[5].Att + relacaoPosicaoJogador[6].Att;

        percentagem0 = Equipa1Def * 100 / (Equipa1Def + Equipa2Att);
        percentagem0Inv = 100 - percentagem0;
        percentagem0Text.text = percentagem0.ToString("#.") + " / " + percentagem0Inv.ToString("#.");


        
       Equipa1Mid = relacaoPosicaoJogador[1].Mid + relacaoPosicaoJogador[2].Mid + relacaoPosicaoJogador[3].Mid;
       Equipa2Mid = relacaoPosicaoJogador[4].Mid + relacaoPosicaoJogador[5].Mid + relacaoPosicaoJogador[6].Mid;

        percentagem1 = Equipa1Mid * 100 / (Equipa1Mid + Equipa2Mid);
        percentagem1Inv = 100 - percentagem1;
        percentagem1Text.text = percentagem1.ToString("#.") + " / " + percentagem1Inv.ToString("#.");



        Equipa1Att = relacaoPosicaoJogador[1].Att + relacaoPosicaoJogador[2].Att + relacaoPosicaoJogador[3].Att;
        Equipa2Def = relacaoPosicaoJogador[4].Def + relacaoPosicaoJogador[5].Def + relacaoPosicaoJogador[6].Def;

        percentagem2 = Equipa1Att * 100 / (Equipa1Att + Equipa2Def);
        percentagem2Inv = 100 - percentagem2;
        percentagem2Text.text = percentagem2.ToString("#.") + " / " + percentagem2Inv.ToString("#.");
    }

}
