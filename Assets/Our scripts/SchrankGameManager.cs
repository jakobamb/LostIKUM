using UnityEngine.Events;
using System.Collections.Generic;
using UnityEngine;

/*
 * Lost in Ikum VR Game (HCI Practical University of Hamburg)
 * 
 * @author Jakob Ambsdorf <jakob.ambsdorf@gmail.com>
 */

public class SchrankGameManager : MonoBehaviour
{
    public UnityEvent onCorrectAnswer = null;

    List<string> _tasks = new List<string>{ "C", "M", "Y", "K", "R", "G", "B" };
    public UnityEngine.UI.Text _screenText;
    private bool _rotationFinished;
    private bool _gameOver;

    private string _currentTask;
    private int _correctAnswCount = 0;
    private GameButtonType _lastButtonType = GameButtonType.undefined;
    private GameButtonType _currentButtonType = GameButtonType.undefined;

    // Start is called before the first frame update
    void Start()
    {
        displayNextTask();
    }

    public void onButtonPress(GameButtonType buttonType)
    {
        // store which buttons have been pushed
        _lastButtonType = _currentButtonType;
        _currentButtonType = buttonType;

        if (_gameOver)
        {
            // do nothing if the game has ended
            return;
        }

        if (checkAnswer())
        {
            _correctAnswCount++;
            onCorrectAnswer.Invoke();
            // answer is correct, flush button memory
            _lastButtonType = GameButtonType.undefined;
            _currentButtonType = GameButtonType.undefined;

            if (_rotationFinished && _correctAnswCount > 2)
            {
                // if the rotation is already finished, end the game
                endGame();
            }
            else
            {
                displayNextTask();
            }
        }
    }

    private bool checkAnswer()
    {
        // this is the "easy" task, where the button color is equal to the given color
        if (_currentButtonType.ToString() == _currentTask)
        {
            return true;
        }
        // Task: R = M + Y
        if (_currentTask == "R" && 
            (_currentButtonType.ToString() == "M" && _lastButtonType.ToString() == "Y") ||
             _currentButtonType.ToString() == "Y" && _lastButtonType.ToString() == "M")
        {
            return true;
        }
        // Task: G = C + Y
        if (_currentTask == "G" &&
            (_currentButtonType.ToString() == "C" && _lastButtonType.ToString() == "Y") ||
             _currentButtonType.ToString() == "Y" && _lastButtonType.ToString() == "C")
        {
            return true;
        }
        // Task: B = C + M
        if (_currentTask == "B" &&
            (_currentButtonType.ToString() == "C" && _lastButtonType.ToString() == "M") ||
             _currentButtonType.ToString() == "M" && _lastButtonType.ToString() == "C")
        {
            return true;
        }
        return false;
    }

    void displayNextTask()
    {
        int idx = Random.Range(0, _tasks.Count);

        // dont display the same task twice.
        while (_currentTask == _tasks[idx])
        {
            idx = Random.Range(0, _tasks.Count);
        }

        string newTask = _tasks[idx];
        _screenText.text = newTask;
        _currentTask = newTask;

    }

    // to be called from the rotation redirector
    public void rotationFinished()
    {
        _rotationFinished = true;
    }

    private void endGame()
    {
        _gameOver = true;
        _screenText.text = "Inverting completed.";
        _screenText.transform.localScale = new Vector3(-1,1,1);
    }

    public void resetGame()
    {
        _gameOver = false;
    }
}
