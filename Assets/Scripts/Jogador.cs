using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Jogador", menuName = "ScriptableObjects/Jogador", order = 1)]
public class Jogador : ScriptableObject
{
    public string Nome;
    public float Def;
    public float Mid;
    public float Att;
    public float TSI;
    
    public Sprite Image;
    //public int pos;
    // Start is called before the first frame update
    public float Especializacao(string stat){
        switch(stat)
        {
            case "DEF": return Def / TSI;
            case "ATT": return Att / TSI;
            case "MID": return Mid / TSI;
            default: return 0;
        }
    }

}





