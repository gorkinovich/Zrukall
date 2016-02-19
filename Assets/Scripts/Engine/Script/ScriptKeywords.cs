//******************************************************************************************
// Copyright (c) 2016 Gorka Suárez García
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
    /// This class represents the script language keywords.
    /// </summary>
    public static class ScriptKeywords {
        //********************************************************************************
        // Constants
        //********************************************************************************

        #region string ROOM
        /// <summary>
        /// The keyword in the script language to declare rooms.
        /// </summary>
        public const string ROOM = "room";
        #endregion

        #region string OBJECT
        /// <summary>
        /// The keyword in the script language to declare objects.
        /// </summary>
        public const string OBJECT = "object";
        #endregion

        #region string EVENT
        /// <summary>
        /// The keyword in the script language to declare events.
        /// </summary>
        public const string EVENT = "on";
        #endregion

        #region string ENTER
        /// <summary>
        /// The keyword in the script language to declare an enter event.
        /// </summary>
        public const string ENTER = "enter";
        #endregion

        #region string EXIT
        /// <summary>
        /// The keyword in the script language to declare an exit event.
        /// </summary>
        public const string EXIT = "exit";
        #endregion

        #region string IF
        /// <summary>
        /// The keyword in the script language to declare an if statement.
        /// </summary>
        public const string IF = "if";
        #endregion

        #region string ELSE
        /// <summary>
        /// The keyword in the script language to declare an else statement.
        /// </summary>
        public const string ELSE = "else";
        #endregion

        #region string WHILE
        /// <summary>
        /// The keyword in the script language to declare a while statement.
        /// </summary>
        public const string WHILE = "while";
        #endregion

        #region string FOR
        /// <summary>
        /// The keyword in the script language to declare a for statement.
        /// </summary>
        public const string FOR = "for";
        #endregion

        #region string IN
        /// <summary>
        /// The keyword in the script language to declare a for statement.
        /// </summary>
        public const string IN = "in";
        #endregion

        #region string TO
        /// <summary>
        /// The keyword in the script language to declare a for statement.
        /// </summary>
        public const string TO = "to";
        #endregion

        #region string DOWNTO
        /// <summary>
        /// The keyword in the script language to declare a for statement.
        /// </summary>
        public const string DOWNTO = "downto";
        #endregion

        #region string LET
        /// <summary>
        /// The keyword in the script language to declare a let statement.
        /// </summary>
        public const string LET = "let";
        #endregion

        #region string COMMA
        /// <summary>
        /// The keyword in the script language to separate statements.
        /// </summary>
        public const string COMMA = ",";
        #endregion

        #region string END
        /// <summary>
        /// The keyword in the script language to finish statements.
        /// </summary>
        public const string END = "end";
        #endregion

        #region string NOT
        /// <summary>
        /// The not symbol in the source code.
        /// </summary>
        public const string NOT = "not";
        #endregion

        #region string AND
        /// <summary>
        /// The and symbol in the source code.
        /// </summary>
        public const string AND = "and";
        #endregion

        #region string OR
        /// <summary>
        /// The or symbol in the source code.
        /// </summary>
        public const string OR = "or";
        #endregion

        #region string XOR
        /// <summary>
        /// The xor symbol in the source code.
        /// </summary>
        public const string XOR = "xor";
        #endregion

        #region string DIV
        /// <summary>
        /// The div symbol in the source code.
        /// </summary>
        public const string DIV = "div";
        #endregion

        #region string MOD
        /// <summary>
        /// The mod symbol in the source code.
        /// </summary>
        public const string MOD = "mod";
        #endregion

        #region string TRUE
        /// <summary>
        /// The true symbol in the source code.
        /// </summary>
        public const string TRUE = "true";
        #endregion

        #region string FALSE
        /// <summary>
        /// The false symbol in the source code.
        /// </summary>
        public const string FALSE = "false";
        #endregion
    }
}
