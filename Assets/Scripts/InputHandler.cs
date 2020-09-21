using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler
{
    private List<KeyCommand> _keyCommands = new List<KeyCommand>();

    public void InputInit()
    {
        BindInputToCommand(KeyCode.LeftArrow, new LeftCommand());
        BindInputToCommand(KeyCode.RightArrow, new RightCommand());
        BindInputToCommand(KeyCode.UpArrow, new UpCommand());
        BindInputToCommand(KeyCode.DownArrow, new DownCommand());
    }

    public ICommand HandleInput()
    {
        foreach(KeyCommand keycommand in _keyCommands)
        {
            if (Input.GetKeyDown(keycommand.key))
            {
                return keycommand.command;
            }
        }

        return null;
    }

    public void BindInputToCommand(KeyCode keyCode, ICommand newCommand)
    {
        _keyCommands.Add(new KeyCommand()
        {
            key = keyCode,
            command = newCommand
        });
    }

    public void UnBindInput(KeyCode keyCode)
    {
        List<KeyCommand> commands = _keyCommands.FindAll(x => x.key == keyCode);

        foreach(KeyCommand command in commands)
        {
            _keyCommands.Remove(command);
        }
    }

    public class KeyCommand
    {
        public KeyCode key;
        public ICommand command;
    }
}