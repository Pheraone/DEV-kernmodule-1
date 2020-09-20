using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler
{
    private List<KeyCommand> keyCommands = new List<KeyCommand>();

    public void InputInit()
    {
        BindInputToCommand(KeyCode.LeftArrow, new LeftCommand());
        BindInputToCommand(KeyCode.RightArrow, new RightCommand());
        BindInputToCommand(KeyCode.UpArrow, new UpCommand());
        BindInputToCommand(KeyCode.DownArrow, new DownCommand());
    }

    public ICommand HandleInput()
    {
        foreach(KeyCommand keycommand in keyCommands)
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
        keyCommands.Add(new KeyCommand()
        {
            key = keyCode,
            command = newCommand
        });
    }

    public void UnBindInput(KeyCode keyCode)
    {
        //???
    }

    public class KeyCommand
    {
        public KeyCode key;
        public ICommand command;
    }
}