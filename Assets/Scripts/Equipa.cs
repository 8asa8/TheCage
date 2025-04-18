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


        return 0;
    }

}


