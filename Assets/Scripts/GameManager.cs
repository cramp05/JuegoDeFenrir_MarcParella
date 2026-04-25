using System.Collections;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;
using UnityEngine.Video;

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

    public GameObject[] vida;
    public int _fenrirHealtyh = 5;
    private PlayerControler _playerScript;



    public bool _pause; //para saber si estamo o no en pausa
  //  public bool _winMenu = false;
    public bool _gui = true;

    public SceneLoader _sceneLoader;
    public string gameOverScene;

   // public Button botonWinCanvas;
    public Button botonPause;


    void Awake()
    {
        _playerScript = GameObject.Find("Fenrir").GetComponent<PlayerControler>();
        _sceneLoader = GameObject.Find("SceneLoader").GetComponent<SceneLoader>();
    }

    public void TakeDamage(int damage)
    {
        _fenrirHealtyh -= damage; //resta uno a la variable vida
        _playerScript.RecivirDaño();

        if (_fenrirHealtyh <= 0)
        {
            vida[0].SetActive(false);
            _playerScript.InicioMuerteFenrir();
        }

        if (_fenrirHealtyh <= 4)
        {
            vida[4].SetActive(false);
        }
        if (_fenrirHealtyh <= 3)
        {
            vida[3].SetActive(false);
        }
        if (_fenrirHealtyh <= 2)
        {
            vida[2].SetActive(false);
        }
        if (_fenrirHealtyh <= 1)
        {
            vida[1].SetActive(false);
        }


    }

    public void GameOver()
    {
        _sceneLoader.ChangeScean("Game Over");
    }

    /*public void WinMenu()
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
        pocionAzul.SetActive(true);
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
            _playerScript.VelocidadPocionAzul();
        }

        pocionAzulDuration.text = "0";
        tiempoRestantePocionAzul = 0f;
        pocionAzul.SetActive(false);
        contadorActualPocionAzul = null;
        _playerScript.VelocidadPocionAzulOFF();
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
