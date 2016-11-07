using UnityEngine;

/*
 * Classe che si occupa della gestione della risoluzione della camera in base all'aspect ratio.
 * Per raggiungere tale scopo, la classe crea delle letterboxes (bande nere).
 * */

public class Resolution : MonoBehaviour 
{
    public Vector2 nativeAspect;        //aspect ratio nativo del gioco

	private float windowAspect;         //valore dell'aspect ratio della scena
	private float aspectRatioNative;    //rapporto dell'aspect ratio nativo
    private float windowActual;         //valore dell'aspect ratio corrente

    /*
	 * Metodo Start
	 * */
    private void Awake () 
	{
        aspectRatioNative = nativeAspect.x / nativeAspect.y;
		windowAspect = (float)Screen.width / (float)Screen.height;

        if (!OtherMath.Similar(aspectRatioNative, windowAspect))
        {
            ScaleCamera(); //scalo la camera in base all'aspect ratio corrente
        }
    }

	/*
	 * Se si gioca in window mode, l'aspect ratio può variare dinamicamente, quindi si deve controllare in real time.
	 * */
	private void FixedUpdate()
	{
        if (!Screen.fullScreen)
        {
            ControlScale(); //se non sono a schermo intero controllo periodicamente il mio aspect ratio
        }
    }

	/*
	 * Funzione che mi permette di scalare la camera in funzione del mio aspectRatio corrente
	 * */
	private void ScaleCamera()
	{
		float scaleHeight = windowAspect / aspectRatioNative;  // valore che serve per sistemare il view port della camera
		Rect rect = Camera.main.rect;                         //salvo la view port della camera

		// Se il mio aspect ratio è minore del mio aspect ratio nativo, attivo le bande nere verticale (se è uguale non cambio nulla)
		if (windowAspect < aspectRatioNative)
		{  	
			//sistemo la view tramite dei valori trovati via Internet
			rect.width = 1.0f;
			rect.height = scaleHeight;
			rect.x = 0;
			rect.y = (1.0f - scaleHeight) / 2.0f;
		}

		// Se il mio aspect ratio è maggiore del mio aspect ratio nativo, attivo le bande nere orizzontali
		else if (windowAspect > aspectRatioNative)
		{
			//sistemo la view tramite dei valori trovati via Internet
			float scaleWidth = 1.0f / scaleHeight; // valore che serve per sistemare il view port della camera
			
			rect.width = scaleWidth;
			rect.height = 1.0f;
			rect.x = (1.0f - scaleWidth) / 2.0f;
			rect.y = 0;
		}
		
		Camera.main.rect = rect; //aggiorno la view port
	}

    /*
     * Funzione che mi permette resettare la viewPort della camera
     * */
    private void ResetViewCamera()
    {
        Rect rect = Camera.main.rect;

        rect.width = 1;
        rect.height = 1;
        rect.y = 0;
        rect.x = 0;

        Camera.main.rect = rect; //aggiorno la view port
    }

	/*
	 * Controllo periodicamente se l'aspect ratio attuale è diverso da quello che ho registrato, se si, scalo la camera
	 * */
	void ControlScale()
	{
		windowActual = (float)Screen.width / (float)Screen.height; //la window attuale

        //se la window corrente è diversa dalla window precedente salvata, vuol dire che si deve riscalare la camera
        if (windowActual != windowAspect) 
		{
            windowAspect = windowActual;

            if (OtherMath.Similar(aspectRatioNative, windowAspect))
            {
                ResetViewCamera();
            }
            else
            {
                ScaleCamera();
            }
		}
	}
}
