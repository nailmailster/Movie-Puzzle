using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SquareScript : MonoBehaviour
{
    private Vector3 mOffset;
    private float mZCoord;
    [SerializeField] GameObject squares;
    [SerializeField] GameObject fireworksVFX;
    [SerializeField] GameObject smokeVFX;
    GameObject fireworks, smoke, fireworks2, fireworks3, fireworks4, fireworks5, fireworks6, fireworks7;
    GameObject activeSquare;
    // [SerializeField] GameObject canvas;

    [SerializeField] int size;

    [SerializeField] Button skipButton;

    void OnMouseDown()
    {
        Vector2 scaleVector = new Vector2();
        if (size == 9)
        {
            scaleVector.x = 1.6f;
            scaleVector.y = 1.2f;
        }
        else if (size == 16)
        {
            scaleVector.x = 1.18f;
            scaleVector.y = 0.87f;
        }
        else if (size == 12)
        {
            scaleVector.x = 1.18f;
            scaleVector.y = 1.2f;
        }
        
        // if (canvas.activeInHierarchy)
        if (SettingsData.Win)
            return;
        Animator animator;
        Animator activeAnimator;

        animator = GetComponent<Animator>();

        //  если ни один фрагмент не выбран
        if (squares.GetComponent<Squares>().selectedCell == -1)
        {
            squares.GetComponent<Squares>().PlayAudio(1);
            //  запускаем анимацию
            animator.enabled = true;

            //  запоминаем выбранный фрагмент
            squares.GetComponent<Squares>().selectedCell = int.Parse(name.Substring(name.Length - 1));
            squares.GetComponent<Squares>().selectedSquare = gameObject;
        }
        //  если уже есть выбранный фрагмент
        else
        {
            squares.GetComponent<Squares>().PlayAudio(2);
            activeSquare =  squares.GetComponent<Squares>().selectedSquare;
            //  если выбран уже выбранный фрагмент
            if (activeSquare == gameObject)
            {
                //  останавливаем анимацию объекта
                animator.enabled = false;
                transform.localScale = scaleVector;

                squares.GetComponent<Squares>().selectedCell = -1;
                squares.GetComponent<Squares>().selectedSquare = null;
            }
            //  если выбран другой фрагмент
            else
            {
                //  останавливаем анимацию ранее выбранного
                activeAnimator = activeSquare.GetComponent<Animator>();
                activeAnimator.enabled = false;

                squares.GetComponent<Squares>().selectedSquare.transform.localScale = scaleVector;

                squares.GetComponent<Squares>().selectedCell = -1;
                squares.GetComponent<Squares>().selectedSquare = null;

                iTween.MoveTo(activeSquare, iTween.Hash(
                    "position", transform.position,
                    "time", .3f
                ));
                activeSquare.GetComponent<Renderer>().sortingOrder = 1;
                iTween.MoveTo(gameObject, iTween.Hash(
                    "position", activeSquare.transform.position,
                    "time", .3f,
                    "oncomplete", "WinCallback"
                ));

                smoke = Instantiate(smokeVFX, transform.position, transform.rotation);
                Destroy(smoke, 3f);

                //  activeSquare - ранее выбранный (пульсирующий) перемещаемый объект
                //  gameObject - замещаемый объект
                squares.GetComponent<Squares>().HeartIt(gameObject, activeSquare, gameObject.transform.position, activeSquare.transform.position);

                StartCoroutine("DegradeOrderLayer", activeSquare);
            }
        }
    }

    IEnumerator DegradeOrderLayer(GameObject activeSquare)
    {
        yield return new WaitForSeconds(3f);
        activeSquare.GetComponent<Renderer>().sortingOrder = 0;
    }

    void WinCallback()
    {
        if (squares.GetComponent<Squares>().CheckWin())
        {
            StartCoroutine(SleepToWinCoroutine());
        }
    }

    IEnumerator SleepToWinCoroutine()
    {
        skipButton.gameObject.SetActive(true);

        fireworks = Instantiate(fireworksVFX, new Vector2(Random.Range(-1.65f, 1.65f), Random.Range(-0.65f, 1.85f)), transform.rotation);
        Destroy(fireworks, 2f);
        yield return new WaitForSeconds(.5f);

        fireworks2 = Instantiate(fireworksVFX,  new Vector2(Random.Range(-1.65f, 1.65f), Random.Range(-0.65f, 1.85f)), transform.rotation);
        Destroy(fireworks2, 2f);
        yield return new WaitForSeconds(.5f);

        fireworks3 = Instantiate(fireworksVFX,  new Vector2(Random.Range(-1.65f, 1.65f), Random.Range(-0.65f, 1.85f)), transform.rotation);
        Destroy(fireworks3, 2f);
        yield return new WaitForSeconds(.5f);

        fireworks4 = Instantiate(fireworksVFX,  new Vector2(Random.Range(-1.65f, 1.65f), Random.Range(-0.65f, 1.85f)), transform.rotation);
        Destroy(fireworks4, 2f);
        yield return new WaitForSeconds(1f);

        fireworks5 = Instantiate(fireworksVFX,  new Vector2(Random.Range(-1.65f, 1.65f), Random.Range(-0.65f, 1.85f)), transform.rotation);
        Destroy(fireworks5, 2f);
        yield return new WaitForSeconds(1f);

        fireworks6 = Instantiate(fireworksVFX,  new Vector2(Random.Range(-1.65f, 1.65f), Random.Range(-0.65f, 1.85f)), transform.rotation);
        Destroy(fireworks6, 2f);
        yield return new WaitForSeconds(.5f);

        fireworks7 = Instantiate(fireworksVFX,  new Vector2(Random.Range(-1.65f, 1.65f), Random.Range(-0.65f, 1.85f)), transform.rotation);
        Destroy(fireworks7, 2f);
        yield return new WaitForSeconds(1f);

        yield return new WaitForSeconds(17f);

        if (SceneManager.GetActiveScene().buildIndex == 5)  //  Win scene
            // squares.GetComponent<Squares>().PlayAudio(3);
            squares.GetComponent<Squares>().PlayAudio(5);

        squares.GetComponent<Squares>().UnloadVideo();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Skip()
    {
        StopCoroutine("SleepToWinCoroutine");
        Destroy(fireworks, 2f);
        Destroy(fireworks2, 2f);
        Destroy(fireworks3, 2f);
        Destroy(fireworks4, 2f);
        Destroy(fireworks5, 2f);
        Destroy(fireworks6, 2f);
        Destroy(fireworks7, 2f);

        squares.GetComponent<Squares>().PlayAudio(5);
        // if (SceneManager.GetActiveScene().buildIndex == 5)  //  Win scene
        //     // squares.GetComponent<Squares>().PlayAudio(3);
        //     squares.GetComponent<Squares>().PlayAudio(4);
        // else
        //     squares.GetComponent<Squares>().PlayAudio(4);

        squares.GetComponent<Squares>().UnloadVideo();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
