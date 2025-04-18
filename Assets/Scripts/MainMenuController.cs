using UnityEngine;
using static GameState;
public class MainMenuController : MonoBehaviour
{
    [SerializeField] Canvas mainMenuCanvas;
    [SerializeField] Canvas teamSelectionCanvas;

    [SerializeField] TeamStatsUI teamStats;
    private int estadoSelecao = 0;

    public void Start()
    {
        mainMenuCanvas.gameObject.SetActive(true);
        teamSelectionCanvas.gameObject.SetActive(false);
    }
    public void OpenSelectTeamPopup()
    {
        // Abrir popup das equipas
        mainMenuCanvas.gameObject.SetActive(false);
        teamSelectionCanvas.gameObject.SetActive(true);
    }
    public void TeamSelected(Equipa equipaSelecionada)
    {
        if (estadoSelecao == 0)
        {
            equipa1 = equipaSelecionada;
            Debug.Log($"EQUIPA 1 SELECIONADA: {equipa1.nome}");
        }
        else
        {
            equipa2 = equipaSelecionada;
            Debug.Log($"EQUIPA 2 SELECIONADA: {equipa2.nome}");
        }
        teamStats.FillTeamStats(equipaSelecionada);

    }

    public void OnPlayPressed()
    {
        if (estadoSelecao > 0)
        {
            // GameStart - todas as equipas foram selecionadas
        }
        estadoSelecao++;
    }



    public void QuitGame()
    {
        Application.Quit();
    }
}
