using UnityEngine;

public class GameData : Singleton<GameData>
{
	public enum InputDevice
	{
		HUD,
		Keyboard,
	};

    public LevelManager LevelManager;
	public AbstractInput InputManager;
	public InputDevice inputDevice;

    public int MonsterCount { get; set; }

	void Awake()
	{
		var inputRoot = transform.Find("InputRoot");
		var hudInputManager = inputRoot.transform.Find("HUD").GetComponent<HUDInput>();
		var keyboardInputManager = inputRoot.transform.Find("Keyboard").GetComponent<VirtualInput>();
		InputManager = (inputDevice == InputDevice.HUD) ? hudInputManager as AbstractInput : keyboardInputManager as AbstractInput;
	}
}
