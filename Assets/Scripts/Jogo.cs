using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    public Dictionary<int, bool> passaEvento;
    public bool passaRollZona;
    public List<EventosDeJogo> EventosZona;
    public bool passaRollEquipa;
    public List<EventosDeJogo> EventosEquipa;
    public bool passaRollDuelo;
    public List<EventosDeJogo> EventosDuelo;
    public List<EventosDeJogo> EventosDueloPerdido;
    public bool passaRollEquipaReage;
    public List<EventosDeJogo> EventosEquipaReage;
    public List<EventosDeJogo> EventosEquipaReagePerdido;
    public bool passaRollDesfecho;
    public List<EventosDeJogo> EventosDesfecho;
    public List<EventosDeJogo> EventosDesfechoPerdido;

    void Start()
    {
        RegisterEvents();
        SubscribeToEvents();
        equipa1 = GameState.equipa1;
        equipa2 = GameState.equipa2;
        //campo = new Campo(); 
        Debug.Log(equipa1.nome);
        passaEvento = new Dictionary<int, bool>();
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
        coroutine = UpdateText("Jogo Entre as equipas " + equipa1.nome.ToString() + " vs " + equipa2.nome.ToString());
        StartCoroutine(coroutine);
    }
    void CalculosEventos(object sender, object args)
    {
        campo.campoZona = Random.Range(1, 4);
        Debug.Log(campo.campoZona);
        //Recebe event enviado do Relogio
        passaEvento.Clear();
        passaEvento.Add(0, false);
        passaEvento.Add(1, false);
        passaEvento.Add(2, false);
        passaEvento.Add(3, false);
        passaEvento.Add(4, false);


        switch (campo.campoZona)
        {
            case 1:
                //medir a forca das equipas
                float comparaPercent = Random.Range(0, 100);
                passaRollZona = true;
                passaEvento[0] = passaRollZona;
                EventoEquipa(comparaPercent, campo.percentagem0, 1, 6, "DEF", "ATT");
                break;
            case 2:
                //medir a forca das equipas
                comparaPercent = Random.Range(0, 100);
                passaRollZona = true;
                passaEvento[0] = passaRollZona;
                EventoEquipa(comparaPercent, campo.percentagem1, 2, 5, "MID", "MID");
                break;
            case 3:
                //medir a forca das equipas
                comparaPercent = Random.Range(0, 100);
                passaRollZona = true;
                passaEvento[0] = passaRollZona;
                EventoEquipa(comparaPercent, campo.percentagem2, 3, 4, "ATT", "DEF");
                break;
        }
    }
    private void EventoEquipa(float rollEquipa, float percentagemDeCampo, int index1, int index2, string especializacao1, string especializacao2)
    {

        if (rollEquipa < percentagemDeCampo)
        {
            //Equipa1
            passaRollEquipa = true;
            passaEvento[1] = passaRollEquipa;
            Jogador ActivePlayer = campo.GetJogador(index1);
            Jogador Opponent = campo.GetJogador(index2);
            int posicao = campo.GetJogadorPosicao(ActivePlayer);
            campo.SelecionarPosicao(posicao);

            campo.DifPvp = ActivePlayer.Especializacao(especializacao1) - Opponent.Especializacao(especializacao2);
            float forcaEmbate = (float)(campo.percentagem0 + (campo.DifPvp / 2f) + (0.01f * ActivePlayer.Def));
            float rollDuelo = Random.Range(0, 100);
            Debug.Log("A Equipa " + equipa1.nome + " ganhou a posse da bola no ROLL DE EQUIPA: " + rollEquipa);


            if (rollDuelo < forcaEmbate)
            {
                passaRollDuelo = true;
                passaEvento[2] = passaRollDuelo;
                Debug.Log(ActivePlayer.Nome + " da Equipa " + equipa1.nome + " ganha o ROLL DE DUELO: " + forcaEmbate);
                float rollEquipaReage = Random.Range(1, 101);

                if (rollEquipaReage < percentagemDeCampo)
                {
                    passaRollEquipaReage = true;
                    passaEvento[3] = passaRollEquipaReage;
                    Debug.Log("Os " + equipa1.nome + " apoiam o jogador com forca no ROLL DE EQUIPA REAGE: " + rollEquipaReage);
                    float rollDesfecho = Random.Range(1, 101);

                    if (rollDesfecho < forcaEmbate)
                    {
                        passaRollDesfecho = true;
                        passaEvento[4] = passaRollDesfecho;
                        Cantona();
                        Debug.Log($"GOOLOO! de {ActivePlayer.Nome} com ROLL DE DESFECHO:{rollDesfecho}");
                    }
                }
                else
                {
                    // falhou entao nao acontece nada
                    Debug.Log($"Equipa 1 falhou o ataque");
                    Cantona();
                }
                return;
            }
            else
            {
                // falhou entao nao acontece nada
                Debug.Log($"Equipa 1 falhou o ataque");
                Cantona();
                return;
            }
        }
        else
        {
            //Equipa2
            passaRollEquipa = true;
            passaEvento[1] = passaRollEquipa;
            Jogador ActivePlayer = campo.GetJogador(index2);
            Jogador Opponent = campo.GetJogador(index1);
            int posicao = campo.GetJogadorPosicao(ActivePlayer);
            campo.SelecionarPosicao(posicao);
            campo.DifPvp = ActivePlayer.Especializacao(especializacao2) - Opponent.Especializacao(especializacao1);
            float forcaEmbate = (float)(campo.percentagem0 + (campo.DifPvp / 2f) + (0.01f * ActivePlayer.Def));
            float rollDuelo = Random.Range(0, 100);
            Debug.Log("A Equipa " + equipa2.nome + " ganhou a posse da bola com forca no ROLL DE EQUIPA: " + rollEquipa);


            if (rollDuelo < forcaEmbate)
            {
                passaRollDuelo = true;
                passaEvento[2] = passaRollDuelo;
                Debug.Log(ActivePlayer.Nome + " da Equipa " + equipa2.nome + " ganha o ROLL DE DUELO: " + rollDuelo);
                float rollEquipaReage = Random.Range(1, 101);

                if (rollEquipaReage < percentagemDeCampo)
                {
                    passaRollEquipaReage = true;
                    passaEvento[3] = passaRollEquipaReage;
                    Debug.Log("Os " + equipa2.nome + " apoiam o jogador com forca no ROLL DE EQUIPA REAGE: " + rollEquipaReage);
                    float rollDesfecho = Random.Range(1, 101);

                    if (rollDesfecho < forcaEmbate)
                    {
                        passaRollDesfecho = true;
                        passaEvento[4] = passaRollDesfecho;
                        Debug.Log($"GOOLOO! de {ActivePlayer.Nome} com ROLL DE DESFECHO:{rollDesfecho}");
                        Cantona();
                    }
                }
                else
                {
                    // falhou entao nao acontece nada
                    Debug.Log($"Equipa 2 falhou o ataque");
                    Cantona();
                }
                return;
            }
            else
            {
                // falhou entao nao acontece nada
                Debug.Log($"Equipa 2 falhou o ataque");
                Cantona();
                return;
            }
        }
    }

    public void Cantona()
    {

        foreach (int keyEvento in passaEvento.Keys)
        {
            Debug.Log("Valor do evento " + keyEvento + ": " + passaEvento[keyEvento]);
        }
        EventoZona();
    }
    public void EventoZona()
    {
        //Preecher Lista com Scriptable Objects: EventosDeJogo

        // EventosZona = new List<>

        string evento = EventosZona.ElementAt(Random.Range(0, EventosZona.Count)).text;
        IEnumerator coroutine = UpdateText(evento);
        StartCoroutine(coroutine);

        IEnumerator enumerator = WaitForText(0);
        StartCoroutine(enumerator);
    }
    public void EventoEquipa()
    {
        string evento = EventosEquipa.ElementAt(Random.Range(0, EventosEquipa.Count)).text;
        IEnumerator coroutine = UpdateText(evento);
        StartCoroutine(coroutine);

        IEnumerator enumerator = WaitForText(1);
        StartCoroutine(enumerator);
    }
    public void EventoDuelo()
    {
        string evento = EventosDuelo.ElementAt(Random.Range(0, EventosDuelo.Count)).text;
        IEnumerator coroutine = UpdateText(evento);
        StartCoroutine(coroutine);

        IEnumerator enumerator = WaitForText(2);
        StartCoroutine(enumerator);
    }
    public void EventoDueloPerdido()
    {
        Debug.Log("DUELO PERDIDOOOO");
    }
    public void EventoEquipaReage()
    {
        string evento = EventosEquipaReage.ElementAt(Random.Range(0, EventosEquipaReage.Count)).text;
        IEnumerator coroutine = UpdateText(evento);
        StartCoroutine(coroutine);

        IEnumerator enumerator = WaitForText(3);
        StartCoroutine(enumerator);
    }
    public void EventoEquipaReagePerdido()
    {
        Debug.Log("EquipaReage PERDIDOOOO");
    }
    public void EventoDesfecho()
    {
        string evento = EventosDesfecho.ElementAt(Random.Range(0, EventosDesfecho.Count)).text;
        IEnumerator coroutine = UpdateText(evento);
        StartCoroutine(coroutine);

        IEnumerator enumerator = WaitForText(4);
        StartCoroutine(enumerator);
    }
    public void EventoDesfechoPerdido()
    {
        Debug.Log("DesfechoPerdido PERDIDOOOO");
    }

    IEnumerator UpdateText(string text) //Para as caixas de texto
    {
        cantonaText.text = text;
        textObj.text = cantonaText.text;
        Debug.Log("------------------" + textObj.text);
        textObj.maxVisibleCharacters = 0;
        while (textObj.maxVisibleCharacters < 164)
        {
            textObj.maxVisibleCharacters += 1;
            yield return new WaitForSeconds(0.1f);
        }
        StopCoroutine("UpdateText");
    }
    IEnumerator WaitForText(int numeroDeEvento) //Para as caixas de texto
    {
        yield return new WaitForSeconds(10f);
        if (passaEvento[numeroDeEvento])
        {
            switch (numeroDeEvento)
            {
                case 0:
                    EventoEquipa();
                    break;
                case 1:
                    EventoDuelo();
                    break;
                case 2:
                    EventoEquipaReage();
                    break;
                case 3:
                    EventoDesfecho();
                    break;
                case 4:
                    Debug.Log("GOLO");
                    break;
            }
        }
        else
        {
            switch (numeroDeEvento)
            {
                case 1:
                    EventoDueloPerdido();
                    break;
                case 2:
                    EventoEquipaReagePerdido();
                    break;
                case 3:
                    EventoDesfechoPerdido();
                    break;
            }
        }
        StopCoroutine("WaitForText");

    }

    private void RegisterEvents()
    {
        EventRegistry.RegisterEvent("TriggerEvent");
    }
    private void SubscribeToEvents()
    {
        EventSubscriber.SubscribeToEvent("TriggerEvent", CalculosEventos);
    }
    void OnDestroy()
    {
        EventSubscriber.UnsubscribeFromEvent("TriggerEvent", CalculosEventos);
        EventRegistry.UnregisterEvent("TriggerEvent");
    }
}
