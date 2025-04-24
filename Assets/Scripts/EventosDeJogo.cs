using UnityEngine;

[CreateAssetMenu(fileName = "Evento", menuName = "ScriptableObjects/EventosDeJogo", order = 1)]
public class EventosDeJogo: ScriptableObject{
    public int id;
    public string tipo;
    public string text;

}