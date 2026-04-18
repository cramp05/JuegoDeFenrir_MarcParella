using System.Collections;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject pauseCanvas;
    //public GameObject winCanvas;

    //public int coins = 0; //para contar monedas

    // public int KilledEnemies = 0;

 
    public GameObject pocionAzul;
    private Coroutine contadorActualPocionAzul;
    public float tiempoRestantePocionAzul = 0f;
    public float tiempoExtraPocionAzul = 10f;
    public Text pocionAzulDuration;


    public bool _pause; //para saber si estamo o no en pausa
  //  public bool _winMenu = false;
    public bool _gui = true;

    //public SceanLoader _sceneLoader;
    //public string gameOverScene;

   // public Button botonWinCanvas;
    public Button botonPause;


    void Awake()
    {

        //_sceneLoader = GameObject.Find("Scean Loader").GetComponent<SceanLoader>();
    }

    /*public void GameOver()
    {
        _sceneLoader.ChangeScean(gameOverScene);
    }

    public void WinMenu()
    {
        if (_winMenu == false)
        {
            Time.timeScale = 0;
            botonWinCanvas.Select();
            _winMenu = true;
            _gui = false;

        }
        else
        {
            Time.timeScale = 1;
            _winMenu = false;
            _gui = true;

        }
        winCanvas.SetActive(_winMenu);
    }*/

    void Start()
    {

    }


    void Update()
    {

    }

    public void TimePocionAzul()
    {
        tiempoRestantePocionAzul += tiempoExtraPocionAzul;

        if (contadorActualPocionAzul == null)
        {
            contadorActualPocionAzul = StartCoroutine(ContadorPocionAzul());
        }
    }

    public IEnumerator ContadorPocionAzul()
    {
        pocionAzul.SetActive(true);

        while (tiempoRestantePocionAzul > 0)
        {
            tiempoRestantePocionAzul -= Time.unscaledDeltaTime;
            pocionAzulDuration.text = Mathf.Ceil(tiempoRestantePocionAzul).ToString();
            yield return null;
        }

        pocionAzulDuration.text = "0";
        tiempoRestantePocionAzul = 0f;
        pocionAzul.SetActive(false);
        contadorActualPocionAzul = null;
    }
    /*public void Addkill()
    {
        KilledEnemies++;
        goombaText.text = KilledEnemies.ToString();
    }*/

    public void Pause()
    {
        if (_pause == false)
        {
            Time.timeScale = 0;
            botonPause.Select();
            _pause = true;
        }
        else
        {
            Time.timeScale = 1;
            _pause = false;

        }
        pauseCanvas.SetActive(_pause);
    }

}
