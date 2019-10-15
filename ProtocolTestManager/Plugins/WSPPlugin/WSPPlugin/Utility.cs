﻿// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using Microsoft.Protocols.TestTools.StackSdk.FileAccessService.WSP;

namespace Microsoft.Protocols.TestManager.WSPServerPlugin
{
    public enum NetError
    {
        NERR_Success = 0,
        NERR_BASE = 2100,
        NERR_UnknownDevDir = (NERR_BASE + 16),
        NERR_DuplicateShare = (NERR_BASE + 18),
        NERR_BufTooSmall = (NERR_BASE + 23),
    }

    public static class ServerHelper
    {
        #region Native API

        const uint MAX_PREFERRED_LENGTH = 0xFFFFFFFF;
        const int NERR_Success = 0;

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct SHARE_INFO_1
        {
            public string shi1_netname;
            public uint shi1_type;
            public string shi1_remark;
            public SHARE_INFO_1(string sharename, uint sharetype, string remark)
            {
                this.shi1_netname = sharename;
                this.shi1_type = sharetype;
                this.shi1_remark = remark;
            }
            public override string ToString()
            {
                return shi1_netname;
            }
        }

        [DllImport("Netapi32.dll", CharSet = CharSet.Unicode)]
        private static extern int NetShareEnum(
             StringBuilder ServerName,
             int level,
             ref IntPtr bufPtr,
             uint prefmaxlen,
             ref int entriesread,
             ref int totalentries,
             ref int resume_handle
             );

        [DllImport("Netapi32", CharSet = CharSet.Auto)]
        static extern int NetApiBufferFree(IntPtr Buffer);

        #endregion
        public static MessageBuilderParameter BuildParameter()
        {
            char[] delimiter = new char[] { ',' };

            Configs config = new Configs();
            config.LoadDefaultValues();

            var parameter = new MessageBuilderParameter();

            parameter.PropertySet_One_DBProperties = config.PropertySet_One_DBProperties.Split(delimiter);

            parameter.PropertySet_Two_DBProperties = config.PropertySet_Two_DBProperties.Split(delimiter);

            parameter.Array_PropertySet_One_Guid = new Guid(config.Array_PropertySet_One_Guid);

            parameter.Array_PropertySet_One_DBProperties = config.Array_PropertySet_One_DBProperties.Split(delimiter);

            parameter.Array_PropertySet_Two_Guid = new Guid(config.Array_PropertySet_Two_Guid);

            parameter.Array_PropertySet_Two_DBProperties = config.Array_PropertySet_Two_DBProperties.Split(delimiter);

            parameter.Array_PropertySet_Three_Guid = new Guid(config.Array_PropertySet_Three_Guid);

            parameter.Array_PropertySet_Three_DBProperties = config.Array_PropertySet_Three_DBProperties.Split(delimiter);

            parameter.Array_PropertySet_Four_Guid = new Guid(config.Array_PropertySet_Four_Guid);

            parameter.Array_PropertySet_Four_DBProperties = config.Array_PropertySet_Four_DBProperties.Split(delimiter);

            parameter.EachRowSize = MessageBuilder.rowWidth;

            parameter.EType = UInt32.Parse(config.EType);

            parameter.BufferSize = UInt32.Parse(config.BufferSize);

            parameter.LCID_VALUE = UInt32.Parse(config.LCID_VALUE);

            parameter.ClientBase = UInt32.Parse(config.ClientBase);

            parameter.RowsToTransfer = UInt32.Parse(config.RowsToTransfer);           
           
            return parameter;
        }
    }
}