using UnityEngine;
using UnityEngine.UI;

public class TeamStatsUI : MonoBehaviour
{
    [SerializeField] private Image[] playerImages;

    public void FillTeamStats(Equipa equipa)
    {
        for (int i = 0; i < equipa.Jogadores.Count; i++)
        {
            playerImages[i].sprite = equipa.Jogadores[i].Image;
        }

    }

    // Update is called once per frame
    void Update()
    {

    }
}
