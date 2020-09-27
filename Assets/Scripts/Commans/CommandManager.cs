using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System.Linq;

public class CommandManager : Singleton<CommandManager>
{
    private List<ICommand> _commandList = new List<ICommand>();

    public void AddCommand(ICommand command)
    {
        _commandList.Add(command);
    }

    public void ClearCommandList()
    {
        _commandList.Clear();
    }

    public IEnumerator PlayCommandList()
    {
        foreach (ICommand command in Enumerable.Reverse(_commandList))
        {
            command.Undo();
            yield return new WaitForSeconds(command.GetLag());
        }
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.C))
        {
            StartCoroutine( PlayCommandList());
        }
    }
}
