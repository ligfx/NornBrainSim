﻿using System.Collections.Generic;

namespace OpCode
{
    public class Random : IOpCode
    {

        public int Evaluate(SVDataPacket data, List<IOpCode> operands)
        {
            return UnityEngine.Random.Range(operands[0].Evaluate(data, null), operands[1].Evaluate(data, null));
        }

        public bool IsOperator()
        {
            return true;
        }

        public int OperandsRequired()
        {
            return 2;
        }
    }
}