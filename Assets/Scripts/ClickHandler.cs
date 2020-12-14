using UnityEngine;
using UnityEngine.UI;

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

    private int scorCount = 0;
    private int bonus = 1;


    private void Start()
    {
        if (scoreText == null)
            Debug.LogError("Set a link to the (Score game object) the class Input Handler..");
    }

    private void Update()
    {
        // Обновление отображения счётчика очков.
        scoreText.text = $"{scorCount}$";
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