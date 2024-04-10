using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public enum CommandType
{
    None,
    Move,
    Attack,

}

public class Command
{
    public CommandType CommandType;
    public Vector2 Points;
    public List<IDamageable> Targets;
    public bool IsComplete;

    public Command(CommandType commandType, List<IDamageable> targets)
    {
        CommandType = commandType;
        Targets = targets;
    }

    public Command(CommandType commandType, Vector2 points)
    {
        CommandType = commandType; 
        Points = points;
    }
}


public class CommandHandler : MonoBehaviour
{

    public Command CurrentCommand;
    private ICommandHandler moveCommandHandler;
    private ICommandHandler attackCommandHandler;

    private void Awake()
    {
        moveCommandHandler = GetComponent<AgentMoveHandlerBase>();
        attackCommandHandler = GetComponent<AgentAttackHandler>();
    }

    public void SetCommand(Command newCommand)
    {
        CurrentCommand = newCommand;
    }

    private void Update()
    {
        if (CurrentCommand == null)
        {
            return;
        }

        ProcessCommand();
    }

    private void ProcessCommand()
    {
        switch (CurrentCommand.CommandType)
        {
            case CommandType.Move:
                ProcessMoveCommand();
                break;

            case CommandType.Attack:
                ProcessAttackCommand();
                break;
        }
        if (CurrentCommand.IsComplete)
        {
            CompleteComand();
        }
    }

    private void CompleteComand()
    {
        CurrentCommand = null;
    }

    private void ProcessAttackCommand()
    {
        attackCommandHandler.ProcessCommand(CurrentCommand);
    }

    private void ProcessMoveCommand()
    {
        moveCommandHandler.ProcessCommand(CurrentCommand);
    }

    public CommandType GetCurrentCommandType()
    {
        if (CurrentCommand == null)
        {
            return CommandType.None;
        }

        return CurrentCommand.CommandType;
    }
}
