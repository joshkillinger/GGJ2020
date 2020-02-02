using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartScreenController : MonoBehaviour
{
	[SerializeField]
	private Sprite _unreadySprite = null;

	[SerializeField]
	private Sprite _readySprite = null;

	[SerializeField]
	private Image _player1Ready = null;

	[SerializeField]
	private Image _player2Ready = null;

	private bool _ready1 = false;

	private bool _ready2 = false;

	private Coroutine _startDelay;

	public bool ready1 {
		get => _ready1;
		set {
			Sprite sprt = value ? _readySprite : _unreadySprite;
			_player1Ready.sprite = sprt;
			_ready1 = value;
		}
	}
	public bool ready2 {
		get => _ready2;
		set {
			Sprite sprt = value ? _readySprite : _unreadySprite;
			_player2Ready.sprite = sprt;
			_ready2 = value;
		}
	}

	private void pauseGame() {

		// causes any passage of time in the simulation to halt
		Time.timeScale = 0;
	}

	private void resumeGame() {

		// resume the simulation
		Time.timeScale = 1;

		// hide the starat screen
		gameObject.SetActive(false);
	}
	
	private void handleInput() {

		if(Input.GetButton("Submit")) {
			ready1 = true;
			ready2 = true;
		}
	}

	private void Start() {
		pauseGame();
	}

	private void Update() {

		// if player 1 and player 2 have both pressed start
		if(_ready1 && _ready2) {

			// ensure coroutine hasn't already been started
			if(_startDelay == null) {

				// start the coroutine and set the _startDelay flag so the coroutine doesn't get initiated multiple times
				_startDelay = StartCoroutine(delaystart());

				// co routine that will wait for 0.5 seconds and then call resumeGame()
				IEnumerator delaystart() {
					yield return new WaitForSecondsRealtime(0.5f);
					resumeGame();
				}
			}
		}
		else {

			handleInput();
		}
	}
}
