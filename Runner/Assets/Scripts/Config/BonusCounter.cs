using UnityEngine.UI;
public class BonusCounter
{
    public int BonusesValue { get; private set; } = 0;

    private Text _bonusText;

    public BonusCounter(Text bonusText)
    {
        _bonusText = bonusText;
    }

    public void AddBonus(int value)
    {
        if (value > 0)
        {
            BonusesValue += value;
            _bonusText.text = BonusesValue.ToString();
        }
    }
}
