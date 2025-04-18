using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TeamStatsUI : MonoBehaviour
{
    [SerializeField] private Image[] playerImages;

    [SerializeField] private TextMeshProUGUI[] playerDefLabels;
    [SerializeField] private TextMeshProUGUI[] playerMidLabels;
    [SerializeField] private TextMeshProUGUI[] playerAttLabels;
    [SerializeField] private TextMeshProUGUI[] playerNameLabels;
    [SerializeField] private Image teamLogo;

    [SerializeField] private TextMeshProUGUI overallDef;
    [SerializeField] private TextMeshProUGUI overallMid;
    [SerializeField] private TextMeshProUGUI overallAtt;

    public void FillTeamStats(Equipa equipa)
    {
        for (int i = 0; i < equipa.Jogadores.Count; i++)
        {
            playerImages[i].sprite = equipa.Jogadores[i].Image;
            playerDefLabels[i].text = equipa.Jogadores[i].Def.ToString();
            playerMidLabels[i].text = equipa.Jogadores[i].Mid.ToString();
            playerAttLabels[i].text = equipa.Jogadores[i].Att.ToString();
            playerNameLabels[i].text = equipa.Jogadores[i].Nome;

        }

        teamLogo.sprite = equipa.logo;
        //overallDef = equipa.CaculateOverallDef();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
