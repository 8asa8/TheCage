using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Jogo : MonoBehaviour
{
    Equipa equipa1;
    Equipa equipa2;
    public Campo campo;
    //public float percentagem0;
    //public TextMeshProUGUI percentagem0Text;

    public TMP_Text textObj;
    public TextMeshProUGUI cantonaText;

 private IEnumerator coroutine;

    void Start()
    {
        RegisterEvents();
        SubscribeToEvents();
        equipa1 = GameState.equipa1;
        equipa2 = GameState.equipa2;
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
        
        campo.CalculaPercentagem();
        coroutine = UpdateText();
        StartCoroutine(coroutine);
    }

    IEnumerator UpdateText() //Para as caixas de texto
    {
        cantonaText.text = "Jogo Entre as equipas "+equipa1.nome.ToString()+" vs "+equipa2.nome.ToString();
        textObj.text = cantonaText.text;
        textObj.maxVisibleCharacters = 0;
        while (textObj.maxVisibleCharacters < 164) 
        {
            textObj.maxVisibleCharacters += 1;
        yield return new WaitForSeconds(0.1f);
        }
    }

    void CalculosEventos(object sender, object args)
    {
        campo.campoZona = Random.Range(1,4);
        Debug.Log(campo.campoZona);
        //Recebe event enviado do Relogio

      

        switch (campo.campoZona)
        {
        case 1:
            //medir a forca das equipas
            float comparaPercent = Random.Range(0,100);
            EventoEquipa(comparaPercent, campo.percentagem0, 1, 6, "DEF", "ATT");
            break;
        case 2:
            //medir a forca das equipas
            comparaPercent = Random.Range(0,100);
            EventoEquipa(comparaPercent, campo.percentagem1, 2, 5, "MID", "MID");
            break;
        case 3:
            //medir a forca das equipas
            comparaPercent = Random.Range(0,100);
            EventoEquipa(comparaPercent, campo.percentagem2, 3, 4, "ATT", "DEF");
            break;
        }
    }

    private void EventoEquipa(float comparaPercent, float percentagemDeCampo, int index1, int index2, string especializacao1, string especializacao2){
            //CENARIO 1
            if(comparaPercent < percentagemDeCampo)
            {
                //Equipa1
                Jogador ActivePlayer = campo.GetJogador(index1); 
                Jogador Opponent = campo.GetJogador(index2); 
                campo.DifPvp = ActivePlayer.Especializacao(especializacao1) - Opponent.Especializacao(especializacao2);
                // E1 DEF + (ActivePlayer.DifPvp /2 ) + (0.01 * ActivePlayer.Def)
                float forcaEmbate = (float)(campo.percentagem0 + (campo.DifPvp / 2f) + (0.01f * ActivePlayer.Def));
                float comparaPercentRoll2 = Random.Range(0,100);
                if (comparaPercentRoll2 < forcaEmbate)
                {
                    Debug.Log($"Equipa 1 conseguiu o ataque com forca de embate: {forcaEmbate}");
                    float equipaReage = Random.Range(1, 101);
                    if(equipaReage < percentagemDeCampo){
                        Debug.Log($"Equipa Reage com: {equipaReage}");
                        float desfecho = Random.Range(1, 101);
                        if(desfecho < forcaEmbate)
                        {
                            Debug.Log($"GOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOLLLLLLLLLLLLLLLLLLLLLLLLLOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOO! de {ActivePlayer.Nome}");
                        }
                    }
                    else{
                    // falhou entao nao acontece nada
                    Debug.Log($"Equipa 1 falhou o ataque");
                    }
                    return;
                }
                else{
                    // falhou entao nao acontece nada
                    Debug.Log($"Equipa 1 falhou o ataque");
                    return;
                    }
            }
            else
            {
                //Equipa2
                Jogador ActivePlayer = campo.GetJogador(index2);
                Jogador Opponent = campo.GetJogador(index1);
                campo.DifPvp = ActivePlayer.Especializacao(especializacao2) - Opponent.Especializacao(especializacao1);
                float forcaEmbate = (float)(campo.percentagem0 + (campo.DifPvp / 2f) + (0.01f * ActivePlayer.Def));
                float comparaPercentRoll2 = Random.Range(0,100);
                if (comparaPercentRoll2 < forcaEmbate)
                {
                    Debug.Log($"Equipa 2 conseguiu o ataque com forca de embate: {forcaEmbate}");
                    float equipaReage = Random.Range(1, 101);
                    if(equipaReage < percentagemDeCampo){
                        Debug.Log($"Equipa Reage com: {equipaReage}");
                        float desfecho = Random.Range(1, 101);
                        if(desfecho < forcaEmbate)
                        {
                            Debug.Log($"GOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOLLLLLLLLLLLLLLLLLLLLLLLLLOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOO! de {ActivePlayer.Nome}");
                        }
                    }
                    else{
                    // falhou entao nao acontece nada
                    Debug.Log($"Equipa 2 falhou o ataque");
                    }
                    return;
                }
                else{
                    // falhou entao nao acontece nada
                    Debug.Log($"Equipa 2 falhou o ataque");
                    return;
                    }
            }
    }

    private void RegisterEvents(){
        EventRegistry.RegisterEvent("TriggerEvent");
    }
    private void SubscribeToEvents(){
        EventSubscriber.SubscribeToEvent("TriggerEvent", CalculosEventos);
    }

    void OnDestroy()
    {
        EventSubscriber.UnsubscribeFromEvent("TriggerEvent", CalculosEventos);
        EventRegistry.UnregisterEvent("TriggerEvent");
    }
}
