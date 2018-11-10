﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OpCode;

public class SVRule {

    public readonly List<IOpCode> OpCodes;

    public SVRule(List<IOpCode> opCodes)
    {
        OpCodes = opCodes;
    }

    public int Evaluate(SVDataPacket data)
    {
        Stack<IOpCode> operatorStack = new Stack<IOpCode>();
        Stack<IOpCode> operandStack = new Stack<IOpCode>();

        try
        {
            foreach (var Op in OpCodes)
            {
                if (Op.IsOperator())
                {
                    operatorStack.Push(Op);

                    if (operatorStack.Count > 0 && operandStack.Count >= operatorStack.Peek().OperandsRequired())
                    {
                        var Operands = new List<IOpCode>();
                        for (int i = 0; i < operatorStack.Peek().OperandsRequired(); i++)
                        {
                            Operands.Add(operandStack.Pop());
                        }
                        data.Result = operatorStack.Pop().Evaluate(data, Operands);
                        operandStack.Push(new Result());
                    }
                }
                else
                {
                    operandStack.Push(Op);

                    if (operatorStack.Count > 0 && (operandStack.Count >= operatorStack.Peek().OperandsRequired()))
                    {
                        var Operands = new List<IOpCode>();
                        for (int i = 0; i < operatorStack.Peek().OperandsRequired(); i++)
                        {
                            Operands.Add(operandStack.Pop());
                        }
                        data.Result = operatorStack.Pop().Evaluate(data, Operands);
                        operandStack.Push(new Result());
                    }
                }
            }
        }
        catch (EndNoValueException e)
        {
            return 0;
        }
        catch (EndException e)
        {
            return data.Result;
        }
        
        return data.Result;
    }
}
