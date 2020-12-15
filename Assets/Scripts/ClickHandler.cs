using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ClickHandler : MonoBehaviour
{
    [Header("Please set up links to the object.")]
    [Tooltip("Link to the text of the score counter.")]
    [SerializeField] private Text scoreText;
    [Tooltip("Link to the text of the button shop panel.")]
    [SerializeField] private GameObject shopPannel;


    [Header("Магазин")]
    [Tooltip("Стоимость покупки улучшения.")]
    public int[] ShopCosts;
    [Tooltip("Бонус к кслику.")]
    public int[] ShopBonuses;
    [Tooltip("Изменение текста после покупки улучшения.")]
    public Text[] ShopButtonsText;
    public Button[] ShopButtons;

    public float[] bonusTime;      //Время задержки

    private int scorCount = 0;
    private int bonus = 1;
    private int workersCount, workersBonus = 1;

    private void Start()
    {
        if (scoreText == null) { Debug.LogError("Set a link to the (Score game object) the class Input Handler.."); }

        StartCoroutine(BounsPerSec());
    }

    private void Update()
    {
        scoreText.text = $"{scorCount}$";
    }

    /// <summary>
    /// Найм рабочего
    /// </summary>
    /// <param name="index"></param>
    public void HireWorker(int index)
    {
        if (scorCount >= ShopCosts[index])
        {
            workersCount++;

            scorCount -= ShopCosts[index];

            ShopCosts[index] *= 2;              // Увеличиваем след. стоимость покупки бонуса.

            // Обновляем текст стоимости покупки улучшения.
            ShopButtonsText[index].text = "Купить рабочего\n" + ShopCosts[index] + "$";

        }
        else Debug.Log("Не хвотает денег!!");
    }

    /// <summary>
    /// Автоклик, рабочие фармят деньги
    /// </summary>
    /// <returns></returns>
    private IEnumerator BounsPerSec()
    {
        while (true)
        {
            scorCount += (workersCount * workersBonus);

            yield return new WaitForSeconds(1);
        }
    }

    /// <summary>
    /// Активация бонуса рабочих
    /// </summary>
    /// <param name="index"></param>
    public void StartBonusTimer(int index)
    {
        int cost = 2 * workersCount;

        ShopButtonsText[2].text = "Купить пиво рабочему\n" + ShopCosts[cost] + "$";

        if (scorCount >= cost)
        {
            StartCoroutine(BonusTimer(bonusTime[index], index));
            scorCount -= cost;
        }
    }

    /// <summary>
    /// Бафф - увеличение скорости рабочих на time секунд
    /// </summary>
    /// <param name="time">Время работы бонуса</param>
    /// <param name="index">Индекс кнопки</param>
    /// <returns></returns>
    private IEnumerator BonusTimer(float time, int index)
    {
        ShopButtons[index].interactable = false;    // Деактивация кликабильности кнопки покупки пива.

        //TODO : Swith
        if (index == 0 && workersCount > 0)
        {
            workersBonus *= 2;

            yield return new WaitForSeconds(time);

            workersBonus /= 2;
        }

        ShopButtons[index].interactable = true;   // Активация кликабильности кнопки покупки пива.
    }

    /// <summary>
    /// Слушатель, который ловит клики.
    /// </summary>
    public void OnClick()
    {
        scorCount += bonus;
    }

    /// <summary>
    /// Открывает и закрывает магазин.
    /// </summary>
    public void ShopPannelShowAndHide()
    {
        shopPannel.SetActive(!shopPannel.activeSelf);
    }

    /// <summary>
    /// Добавляет бонус.
    /// </summary>
    /// <param name="index">Индекс бонуса</param>
    public void ShopButtonAddBonus(int index)
    {
        if (scorCount >= ShopCosts[index])      // Если кол-во очков больше или равно ценавому индексу.
        {
            bonus += ShopBonuses[index] - 1;    // В бонус сохраняем бонус хранящийся в массиве.

            scorCount -= ShopCosts[index];      // Из общего кол-ва очков отнимаем стоимось улучшения.

            ShopCosts[index] *= 2;              // Увеличиваем след. стоимость покупки бонуса.

            // Обновляем текст стоимости покупки улучшения.
            ShopButtonsText[index].text = "Купить улучшение\n" + ShopCosts[index] + "$";
        }
        else Debug.Log("Не хвотает денег!!");
    }
    #region Test
    public void DoubleScore()
    {
        scorCount *= 2;
    }

    public void DevideByScore()
    {
        if (scorCount <= 0) return;

        scorCount /= 2;
    }
    #endregion
}