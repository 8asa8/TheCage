using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;



[CreateAssetMenu(fileName = "Equipa", menuName = "ScriptableObjects/Equipa", order = 1)]
public class Equipa : ScriptableObject
{


    public string nome;
    public Sprite logo;
    public List<Jogador> Jogadores;
    public int CaculateOverallDef()
    {
        int def = 0;
        foreach (Jogador jogador in Jogadores)
        {
            def += jogador.Def;
        }
        return def;
    }
    public int CaculateOverallMid()
    {
        int def = 0;
        foreach (Jogador jogador in Jogadores)
        {
            def += jogador.Mid;
        }
        return def;
    }
    public int CaculateOverallAtt()
    {
        int def = 0;
        foreach (Jogador jogador in Jogadores)
        {
            def += jogador.Att;
        }
        return def;
    }

}


