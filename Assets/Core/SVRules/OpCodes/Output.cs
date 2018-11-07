﻿using System.Collections.Generic;

public class Output : OpCode
{

    public int Evaluate(SVDataPacket data, List<OpCode> operands)
    {
        return data.NeuronOutput;
    }

    public bool IsOperator()
    {
        return false;
    }

    public int OperandsRequired()
    {
        return 0;
    }
}