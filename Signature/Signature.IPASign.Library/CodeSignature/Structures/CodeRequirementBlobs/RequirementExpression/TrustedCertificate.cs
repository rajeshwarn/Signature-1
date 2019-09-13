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
    public class TrustedCertificate : RequirementExpression
    {
        public uint CertificateIndex;

        public TrustedCertificate()
        {
        }

        public TrustedCertificate(byte[] buffer, ref int offset)
        {
            CertificateIndex = BigEndianReader.ReadUInt32(buffer, ref offset);
        }

        public override void WriteBytes(byte[] buffer, ref int offset)
        {
            BigEndianWriter.WriteUInt32(buffer, ref offset, (uint)RequirementOperatorName.TrustedCertificate);
            BigEndianWriter.WriteUInt32(buffer, ref offset, CertificateIndex);
        }

        public override int Length
        {
            get
            {
                return 8;
            }
        }
    }
}
