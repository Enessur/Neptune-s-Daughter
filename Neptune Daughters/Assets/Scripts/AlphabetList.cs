using UnityEngine;

public class AlphabetList : MonoBehaviour
{
    public string GetLetterByNumber(int number)
    {
        int index = Mathf.Clamp(number, 0, 25);

        char letter = (char)(index + 65);

        return letter.ToString();
    }
}
