//******************************************************************************************
// Copyright (c) 2021 Gorka Suárez García
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
// DEALINGS IN THE SOFTWARE.
//******************************************************************************************

namespace Engine.Script {
    /// <summary>
    /// This class represents the script language functions.
    /// </summary>
    public static class ScriptFunctions {
        //********************************************************************************
        // Constants
        //********************************************************************************

        #region string WRITELN
        /// <summary>
        /// The "write line" function name in the source code.
        /// </summary>
        public const string WRITELN = "writeln";
        #endregion

        #region string WRITE
        /// <summary>
        /// The "write" function name in the source code.
        /// </summary>
        public const string WRITE = "write";
        #endregion

        #region string GOTO
        /// <summary>
        /// The "goto" function name in the source code.
        /// </summary>
        public const string GOTO = "goto";
        #endregion

        #region string FINISH
        /// <summary>
        /// The "finish" function name in the source code.
        /// </summary>
        public const string FINISH = "finish";
        #endregion

        #region string SETDEFMSG
        /// <summary>
        /// The "set default message" function name in the source code.
        /// </summary>
        public const string SETDEFMSG = "setdefmsg";
        #endregion

        #region string INVADD
        /// <summary>
        /// The "inventory add" function name in the source code.
        /// </summary>
        public const string INVADD = "invadd";
        #endregion

        #region string INVREM
        /// <summary>
        /// The "inventory remove" function name in the source code.
        /// </summary>
        public const string INVREM = "invrem";
        #endregion

        #region string INVHAS
        /// <summary>
        /// The "inventory has" function name in the source code.
        /// </summary>
        public const string INVHAS = "invhas";
        #endregion

        #region string RESET
        /// <summary>
        /// The "reset" function name in the source code.
        /// </summary>
        public const string RESET = "reset";
        #endregion

        #region string ROOMNAME
        /// <summary>
        /// The "room name" function name in the source code.
        /// </summary>
        public const string ROOMNAME = "roomname";
        #endregion

        #region string LASTROOM
        /// <summary>
        /// The "last room" function name in the source code.
        /// </summary>
        public const string LASTROOM = "lastroom";
        #endregion

        #region string SETRUNFST
        /// <summary>
        /// The "set run first" function name in the source code.
        /// </summary>
        public const string SETRUNFST = "setrunfst";
        #endregion

        #region string SETRUNALL
        /// <summary>
        /// The "set run all" function name in the source code.
        /// </summary>
        public const string SETRUNALL = "setrunall";
        #endregion

        #region string SETFGCOLOR
        /// <summary>
        /// The "set foreground color" function name in the source code.
        /// </summary>
        public const string SETFGCOLOR = "setfgcolor";
        #endregion

        #region string SETBGCOLOR
        /// <summary>
        /// The "set background color" function name in the source code.
        /// </summary>
        public const string SETBGCOLOR = "setbgcolor";
        #endregion

        #region string CLEAR
        /// <summary>
        /// The "clear screen" function name in the source code.
        /// </summary>
        public const string CLEAR = "clear";
        #endregion

        #region string EXIT
        /// <summary>
        /// The "exit" function name in the source code.
        /// </summary>
        public const string EXIT = "exit";
        #endregion
    }
}
