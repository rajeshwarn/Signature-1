/* Copyright (C) 2017 ROM Knowledgeware. All rights reserved.
 * 
 * You can redistribute this program and/or modify it under the terms of
 * the GNU Lesser Public License as published by the Free Software Foundation,
 * either version 3 of the License, or (at your option) any later version.
 * 
 * Maintainer: Tal Aloni <tal@kmrom.com>
 */
using System;
using System.Collections.Generic;
using Signature.IPASign.Utilities;

namespace Signature.IPASign.Library.CodeSignature
{
    public class OrExpression : RequirementExpression
    {
        public RequirementExpression Expression1;
        public RequirementExpression Expression2;

        public OrExpression(RequirementExpression expression1, RequirementExpression expression2)
        {
            Expression1 = expression1;
            Expression2 = expression2;
        }

        public OrExpression(byte[] buffer, ref int offset)
        {
            Expression1 = ReadExpression(buffer, ref offset);
            Expression2 = ReadExpression(buffer, ref offset);
        }

        public override void WriteBytes(byte[] buffer, ref int offset)
        {
            BigEndianWriter.WriteUInt32(buffer, ref offset, (uint)RequirementOperatorName.Or);
            Expression1.WriteBytes(buffer, ref offset);
            Expression2.WriteBytes(buffer, ref offset);
        }

        public override int Length
        {
            get
            {
                return 4 + Expression1.Length + Expression2.Length;
            }
        }
    }
}
