using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text.RegularExpressions;
using TMPro;
using Unity.Burst.Intrinsics;
using Unity.VisualScripting;
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
    public TextMeshProUGUI scoreEquipa1;
    public TextMeshProUGUI scoreEquipa2;
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
    Jogador ActivePlayer;
    Jogador Opponent;

    public void Start()
    {
        RegisterEvents();
        SubscribeToEvents();
        equipa1 = GameState.equipa1;
        equipa2 = GameState.equipa2;
        //campo = new Campo(); 
        passaEvento = new Dictionary<int, bool>();
        int i = 1;
        int j = 4;


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
            j++;

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
            ActivePlayer = campo.GetJogador(index1);
            Opponent = campo.GetJogador(index2);
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
            ActivePlayer = campo.GetJogador(index2);
            Opponent = campo.GetJogador(index1);
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

                    if (rollDesfecho < forcaEmbate + 0.05)
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

        string evento = AdicionarVariaveis(EventosZona.ElementAt(Random.Range(0, EventosZona.Count)).text);

        IEnumerator coroutine = UpdateText(evento);
        StartCoroutine(coroutine);

        IEnumerator enumerator = WaitForText(0);
        StartCoroutine(enumerator);
    }
    public void EventoEquipa()
    {
        string evento = AdicionarVariaveis(EventosEquipa.ElementAt(Random.Range(0, EventosEquipa.Count)).text);
        IEnumerator coroutine = UpdateText(evento);
        StartCoroutine(coroutine);

        IEnumerator enumerator = WaitForText(1);
        StartCoroutine(enumerator);
    }
    public void EventoDuelo()
    {
        string evento = AdicionarVariaveis(EventosDuelo.ElementAt(Random.Range(0, EventosDuelo.Count)).text);
        IEnumerator coroutine = UpdateText(evento);
        StartCoroutine(coroutine);

        IEnumerator enumerator = WaitForText(2);
        StartCoroutine(enumerator);
    }
    public void EventoDueloPerdido()
    {
        string evento = AdicionarVariaveis(EventosDueloPerdido.ElementAt(Random.Range(0, EventosDueloPerdido.Count)).text);
        IEnumerator coroutine = UpdateText(evento);
        StartCoroutine(coroutine);

        //IEnumerator enumerator = WaitForText(2);
        //StartCoroutine(enumerator);
    }
    public void EventoEquipaReage()
    {
        string evento = AdicionarVariaveis(EventosEquipaReage.ElementAt(Random.Range(0, EventosEquipaReage.Count)).text);
        IEnumerator coroutine = UpdateText(evento);
        StartCoroutine(coroutine);

        IEnumerator enumerator = WaitForText(3);
        StartCoroutine(enumerator);
    }
    public void EventoEquipaReagePerdido()
    {
        string evento = AdicionarVariaveis(EventosEquipaReagePerdido.ElementAt(Random.Range(0, EventosEquipaReagePerdido.Count)).text);
        IEnumerator coroutine = UpdateText(evento);
        StartCoroutine(coroutine);

        //IEnumerator enumerator = WaitForText(3);
        //StartCoroutine(enumerator);
        Debug.Log("EquipaReage PERDIDOOOO");
    }
    public void EventoDesfecho()
    {
        string evento = AdicionarVariaveis(EventosDesfecho.ElementAt(Random.Range(0, EventosDesfecho.Count)).text);
        IEnumerator coroutine = UpdateText(evento);
        StartCoroutine(coroutine);

        IEnumerator enumerator = WaitForText(4);
        StartCoroutine(enumerator);
    }
    public void EventoDesfechoPerdido()
    {
        string evento = AdicionarVariaveis(EventosDesfechoPerdido.ElementAt(Random.Range(0, EventosDesfechoPerdido.Count)).text);
        IEnumerator coroutine = UpdateText(evento);
        StartCoroutine(coroutine);
        Debug.Log("DesfechoPerdido PERDIDOOOO");
    }
    public void FimDoJogo()
    {
        if (equipa1.Jogadores.Contains(ActivePlayer))
        {
            Debug.Log("Fim do Jogo. Os " + equipa1.nome + " venceram a partida!");
            scoreEquipa1.text = (int.Parse(scoreEquipa1.text) + 1).ToString();
        }
        else
        {
            Debug.Log("Fim do Jogo. Os " + equipa2.nome + " venceram a partida!");
            scoreEquipa2.text = (int.Parse(scoreEquipa2.text) + 1).ToString();
        }
    }
    IEnumerator UpdateText(string text) //Para as caixas de texto
    {
        cantonaText.text = text;
        textObj.text = cantonaText.text;
        Debug.Log("------------------" + textObj.text);
        textObj.maxVisibleCharacters = 0;
        while (textObj.maxVisibleCharacters < textObj.text.Length)
        {
            textObj.maxVisibleCharacters += 1;
            yield return new WaitForSeconds(0.05f);
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
                    FimDoJogo();

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

    public string AdicionarVariaveis(string texto)
    {
        string zona = "";

        switch (campo.campoZona)
        {
            case 1:
                zona = "de defesa";
                break;
            case 2:
                zona = "de meio campo";
                break;
            case 3:
                zona = "de ataque";
                break;
            default:
                zona = "";
                break;
        }
        Dictionary<string, string> replacements = new Dictionary<string, string>();
        replacements.Add("[zona]", zona);
        replacements.Add("[activeplayer]", ActivePlayer.Nome);
        replacements.Add("[equipa]", equipa1.nome);
        replacements.Add("[equipa2]", equipa2.nome);
        replacements.Add("[opponent]", Opponent.Nome);

        // Loop through each key-value pair in the dictionary and replace in the input string
        foreach (var replacement in replacements)
        {
            texto = texto.Replace(replacement.Key, replacement.Value);
        }
        return texto;
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
