using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[CreateAssetMenu(fileName = "Equipa", menuName = "ScriptableObjects/Equipa", order = 1)]
public class Equipa : ScriptableObject
{

    public string nome;
    public Image logo;

    public List<Jogador> Jogadores;

}