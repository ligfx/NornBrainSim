﻿using System.Collections.Generic;

namespace OpCode
{
    public class True : IOpCode
    {

        public int Evaluate(SVDataPacket data, List<IOpCode> operands)
        {
            if (data.Result > 0)
            {
                return operands[0].Evaluate(data, null);
            }
            else
            {
                throw new EndNoValueException();
            }
        }

        public bool IsOperator()
        {
            return true;
        }

        public int OperandsRequired()
        {
            return 1;
        }
    }
}