using System;
using System.Collections;
using System.Collections.Generic;
using Script;
using TMPro;
using UnityEngine;

public class ScoreWriter : MonoBehaviour
{
     
    
    [SerializeField] private TMP_Text firstLetter;
    [SerializeField] private TMP_Text secondLetter;
    [SerializeField] private TMP_Text thirdLetter;

    public AlphabetList alphabetList;

    private string _firstL ;
    private string _secondL;
    private string _thirdL;
    
    private string _name ="";
    private string _currentLetter;
    private int _collumNumber;
    private int _letterNumber;

    private void Start()
    {
        _collumNumber = 0;
        _letterNumber = 0;

        firstLetter.text = "A";
        secondLetter.text = "A";
        thirdLetter.text = "A";
        
        name = firstLetter.text + secondLetter.text + thirdLetter.text;
    }

    public void NextLetterBlock()
    {
        _collumNumber++;
        if (_collumNumber ==3)
        {
            _collumNumber = 0;
        }
        
    }

    public void SwitchUpwardLetterBlock()
    {
        _letterNumber++;
        if (_letterNumber == 26)
        {
            _letterNumber = 0;
        }
        CallLetter(_letterNumber);
    }
    
    public void SwitchDownwardLetterBlock()
    {
        _letterNumber--;
        if (_letterNumber == -1)
        {
            _letterNumber = 25;
        }
        CallLetter(_letterNumber);
    }

    public void CallLetter(int letterNumber)
    {
        _currentLetter = alphabetList.GetLetterByNumber(letterNumber);

        switch (_collumNumber)
        {
            case 0:
                firstLetter.text = _currentLetter;
                break;
            case 1:
                secondLetter.text = _currentLetter;
                break;
            case 2:
                thirdLetter.text = _currentLetter;
                break;
        }

        name = firstLetter.text + secondLetter.text + thirdLetter.text;
       Debug.Log("bu isim : "+name);
    }

    public void SaveNewScore( )
    {
        LevelManager.Instance.AddCurrentLevelData(name);
        Debug.Log(""+name);
       
    }
    
}
