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
using System;
using System.Linq;

namespace Engine.Script {
    /// <summary>
    /// This class represents the script language symbols.
    /// </summary>
    public static class ScriptSymbol {
        //********************************************************************************
        // Constants (Blank)
        //********************************************************************************

        #region char SPACE
        /// <summary>
        /// The space symbol in the source code.
        /// </summary>
        public const char SPACE = ' ';
        #endregion

        #region char TAB
        /// <summary>
        /// The tabulation symbol in the source code.
        /// </summary>
        public const char TAB = '\t';
        #endregion

        #region char NEW_LINE
        /// <summary>
        /// The new line symbol in the source code.
        /// </summary>
        public const char NEW_LINE = '\n';
        #endregion

        //********************************************************************************
        // Constants (Brackets)
        //********************************************************************************

        #region char PARENTHESIS_OPEN
        /// <summary>
        /// The parenthesis open symbol in the source code.
        /// </summary>
        public const char PARENTHESIS_OPEN = '(';
        #endregion

        #region char PARENTHESIS_CLOSE
        /// <summary>
        /// The parenthesis close symbol in the source code.
        /// </summary>
        public const char PARENTHESIS_CLOSE = ')';
        #endregion

        #region char BRACKET_OPEN
        /// <summary>
        /// The bracket open symbol in the source code.
        /// </summary>
        public const char BRACKET_OPEN = '[';
        #endregion

        #region char BRACKET_CLOSE
        /// <summary>
        /// The bracket close symbol in the source code.
        /// </summary>
        public const char BRACKET_CLOSE = ']';
        #endregion

        #region char CURLY_BRACKET_OPEN
        /// <summary>
        /// The curly bracket open symbol in the source code.
        /// </summary>
        public const char CURLY_BRACKET_OPEN = '{';
        #endregion

        #region char CURLY_BRACKET_CLOSE
        /// <summary>
        /// The curly bracket close symbol in the source code.
        /// </summary>
        public const char CURLY_BRACKET_CLOSE = '}';
        #endregion

        //********************************************************************************
        // Constants (Arithmetic)
        //********************************************************************************

        #region char PLUS
        /// <summary>
        /// The plus symbol in the source code.
        /// </summary>
        public const char PLUS = '+';
        #endregion

        #region char MINUS
        /// <summary>
        /// The minus symbol in the source code.
        /// </summary>
        public const char MINUS = '-';
        #endregion

        #region char ASTERISK
        /// <summary>
        /// The asterisk or multiply symbol in the source code.
        /// </summary>
        public const char ASTERISK = '*';
        #endregion

        #region char SLASH 
        /// <summary>
        /// The slash or divide symbol in the source code.
        /// </summary>
        public const char SLASH = '/';
        #endregion

        #region char CARET
        /// <summary>
        /// The caret or power symbol in the source code.
        /// </summary>
        public const char CARET = '^';
        #endregion

        #region char PERCENT
        /// <summary>
        /// The percent or module symbol in the source code.
        /// </summary>
        public const char PERCENT = '%';
        #endregion

        //********************************************************************************
        // Constants (Logical)
        //********************************************************************************

        #region char EQUALS
        /// <summary>
        /// The equals symbol in the source code.
        /// </summary>
        public const char EQUALS = '=';
        #endregion

        #region char LESS_THAN
        /// <summary>
        /// The less-than symbol in the source code.
        /// </summary>
        public const char LESS_THAN = '<';
        #endregion

        #region char GREATER_THAN
        /// <summary>
        /// The greater-than symbol in the source code.
        /// </summary>
        public const char GREATER_THAN = '>';
        #endregion

        #region string LESS_OR_EQUALS_THAN
        /// <summary>
        /// The less-or-equals-than symbol in the source code.
        /// </summary>
        public const string LESS_OR_EQUALS_THAN = "<=";
        #endregion

        #region string GREATER_OR_EQUALS_THAN
        /// <summary>
        /// The greater-or-equals-than symbol in the source code.
        /// </summary>
        public const string GREATER_OR_EQUALS_THAN = ">=";
        #endregion

        #region string NOT_EQUALS
        /// <summary>
        /// The not equals symbol in the source code.
        /// </summary>
        public const string NOT_EQUALS = "<>";
        #endregion

        //********************************************************************************
        // Constants (String)
        //********************************************************************************

        #region char DOUBLE_QUOTES
        /// <summary>
        /// The double quote string symbol in the source code.
        /// </summary>
        public const char DOUBLE_QUOTES = '"';
        #endregion

        #region char SINGLE_QUOTE
        /// <summary>
        /// The single quote string symbol in the source code.
        /// </summary>
        public const char SINGLE_QUOTE = '\'';
        #endregion

        #region char BACKSLASH
        /// <summary>
        /// The backslash symbol in the source code.
        /// </summary>
        public const char BACKSLASH = '\\';
        #endregion

        //********************************************************************************
        // Constants (Special)
        //********************************************************************************

        #region char HASH
        /// <summary>
        /// The hash or comment symbol in the source code.
        /// </summary>
        public const char HASH = '#';
        #endregion

        #region char UNDERSCORE
        /// <summary>
        /// The underscore symbol in the source code.
        /// </summary>
        public const char UNDERSCORE = '_';
        #endregion

        #region char EXPONENTIAL
        /// <summary>
        /// The exponential symbol in the source code.
        /// </summary>
        public const char EXPONENTIAL = 'e';
        #endregion

        #region char PERIOD
        /// <summary>
        /// The period or point symbol in the source code.
        /// </summary>
        public const char PERIOD = '.';
        #endregion

        #region char COLON
        /// <summary>
        /// The colon symbol in the source code.
        /// </summary>
        public const char COLON = ':';
        #endregion

        #region char SEMICOLON
        /// <summary>
        /// The semicolon symbol in the source code.
        /// </summary>
        public const char SEMICOLON = ';';
        #endregion

        #region char COMMA
        /// <summary>
        /// The comma symbol in the source code.
        /// </summary>
        public const char COMMA = ',';
        #endregion

        #region string RANGE
        /// <summary>
        /// The range symbol in the source code.
        /// </summary>
        public const string RANGE = "..";
        #endregion

        //********************************************************************************
        // Constants (Other)
        //********************************************************************************

        #region char QUESTION_OPEN
        /// <summary>
        /// The question open symbol in the source code.
        /// </summary>
        public const char QUESTION_OPEN = '¿';
        #endregion

        #region char QUESTION_CLOSE
        /// <summary>
        /// The question close symbol in the source code.
        /// </summary>
        public const char QUESTION_CLOSE = '?';
        #endregion

        #region char EXCLAMATION_OPEN
        /// <summary>
        /// The exclamation open symbol in the source code.
        /// </summary>
        public const char EXCLAMATION_OPEN = '¡';
        #endregion

        #region char EXCLAMATION_CLOSE
        /// <summary>
        /// The exclamation close symbol in the source code.
        /// </summary>
        public const char EXCLAMATION_CLOSE = '!';
        #endregion

        #region char AT
        /// <summary>
        /// The at symbol in the source code.
        /// </summary>
        public const char AT = '@';
        #endregion

        #region char DOLLAR
        /// <summary>
        /// The dollar symbol in the source code.
        /// </summary>
        public const char DOLLAR = '$';
        #endregion

        #region char AMPERSAND
        /// <summary>
        /// The ampersand symbol in the source code.
        /// </summary>
        public const char AMPERSAND = '&';
        #endregion

        #region char PIPE
        /// <summary>
        /// The pipe symbol in the source code.
        /// </summary>
        public const char PIPE = '|';
        #endregion

        #region char TILDE
        /// <summary>
        /// The tilde symbol in the source code.
        /// </summary>
        public const char TILDE = '~';
        #endregion

        #region char NEGATION
        /// <summary>
        /// The negation symbol in the source code.
        /// </summary>
        public const char NEGATION = '¬';
        #endregion

        #region char INTERPUNCT
        /// <summary>
        /// The interpunct symbol in the source code.
        /// </summary>
        public const char INTERPUNCT = '·';
        #endregion

        #region char GRAVE_ACCENT
        /// <summary>
        /// The grave accent symbol in the source code.
        /// </summary>
        public const char GRAVE_ACCENT = '`';
        #endregion

        #region char ACUTE_ACCENT
        /// <summary>
        /// The acute accent symbol in the source code.
        /// </summary>
        public const char ACUTE_ACCENT = '´';
        #endregion

        #region char DIAERESIS
        /// <summary>
        /// The diaeresis symbol in the source code.
        /// </summary>
        public const char DIAERESIS = '¨';
        #endregion

        //********************************************************************************
        // Types
        //********************************************************************************

        #region enum FloatNumberState
        /// <summary>
        /// This enumeration represents the different sections in a float number.
        /// </summary>
        private enum FloatNumberState {
            Integer, Mantissa, Exponent1, Exponent
        }
        #endregion

        //********************************************************************************
        // Methods (Check)
        //********************************************************************************

        #region bool IsIdentifier (string)
        /// <summary>
        /// Checks if a string token is a valid identifier.
        /// </summary>
        /// <param name="token">The token to check.</param>
        /// <returns>Returns true if the token is an identifier.</returns>
        public static bool IsIdentifier (string token) {
            if (string.IsNullOrEmpty(token)) {
                return false;
            } else {
                foreach (var item in token) {
                    if (!((item == UNDERSCORE) || char.IsLetterOrDigit(item))) {
                        return false;
                    }
                }
                return !char.IsDigit(token, 0);
            }
        }
        #endregion

        #region bool IsBoolean (string)
        /// <summary>
        /// Checks if a string token is a valid booleam.
        /// </summary>
        /// <param name="token">The token to check.</param>
        /// <returns>Returns true if the token is a booleam.</returns>
        public static bool IsBoolean (string token) {
            return (token == ScriptKeywords.TRUE)
                || (token == ScriptKeywords.FALSE);
        }
        #endregion

        #region bool IsIntegerNumber (string)
        /// <summary>
        /// Checks if a string token is a valid integer number.
        /// </summary>
        /// <param name="token">The token to check.</param>
        /// <returns>Returns true if the token is an integer number.</returns>
        public static bool IsIntegerNumber (string token) {
            if (string.IsNullOrEmpty(token) || !IsIntegerFirstCharacter(token[0])) {
                return false;
            } else {
                for (int i = 1; i < token.Length; i++) {
                    if (!char.IsDigit(token, i)) {
                        return false;
                    }
                }
                return true;
            }
        }
        #endregion

        #region bool IsFloatNumber (string)
        /// <summary>
        /// Checks if a string token is a valid float number.
        /// </summary>
        /// <param name="token">The token to check.</param>
        /// <returns>Returns true if the token is a float number.</returns>
        public static bool IsFloatNumber (string token) {
            if (string.IsNullOrEmpty(token) || !IsIntegerFirstCharacter(token[0])) {
                return false;
            } else {
                FloatNumberState state = FloatNumberState.Integer;
                for (int i = 1; i < token.Length; i++) {
                    var item = token[i];
                    switch (state) {
                        // [<123.>]456e-789
                        case FloatNumberState.Integer:
                            if (item == PERIOD) {
                                state = FloatNumberState.Mantissa;
                            } else if (char.ToLower(item) == EXPONENTIAL) {
                                state = FloatNumberState.Exponent1;
                            } else if (!char.IsDigit(item)) {
                                return false;
                            }
                            break;
                        // 123.[<456e>]-789
                        case FloatNumberState.Mantissa:
                            if (char.ToLower(item) == EXPONENTIAL) {
                                state = FloatNumberState.Exponent1;
                            } else if (!char.IsDigit(item)) {
                                return false;
                            }
                            break;
                        // 123.456e[<->]789
                        case FloatNumberState.Exponent1:
                            if (!IsIntegerFirstCharacter(item)) {
                                return false;
                            }
                            break;
                        // 123.456e-[<789>]
                        case FloatNumberState.Exponent:
                            if (!char.IsDigit(item)) {
                                return false;
                            }
                            break;
                    }
                }
                // Avoid things like: 1.
                if (token.Last() == PERIOD) {
                    return false;
                }
                return true;
            }
        }
        #endregion

        #region bool IsIntegerFirstCharacter (char)
        /// <summary>
        /// Checks if a character is a valid first integer character.
        /// </summary>
        /// <param name="value">The character to check.</param>
        /// <returns>Returns true if the character is right.</returns>
        public static bool IsIntegerFirstCharacter (char value) {
            return char.IsDigit(value);
            //return char.IsDigit(value) || (value == PLUS) || (value == MINUS);
        }
        #endregion

        //********************************************************************************
        // Methods (Blank)
        //********************************************************************************

        #region bool IsBlank (char)
        /// <summary>
        /// Checks if the character is a blank character.
        /// </summary>
        /// <param name="victim">The character to check.</param>
        /// <returns>Returns true if it's a blank character.</returns>
        public static bool IsBlank (char victim) {
            return (victim == SPACE)
                || (victim == TAB)
                || (victim == NEW_LINE);
        }
        #endregion

        #region bool IsBlank (string)
        /// <summary>
        /// Checks if the string is a blank character.
        /// </summary>
        /// <param name="victim">The string to check.</param>
        /// <returns>Returns true if it's a blank character.</returns>
        public static bool IsBlank (string victim) {
            return testStringFirstChar(victim, IsBlank);
        }
        #endregion

        //********************************************************************************
        // Methods (Brackets)
        //********************************************************************************

        #region bool IsBracket (char)
        /// <summary>
        /// Checks if the character is a bracket character.
        /// </summary>
        /// <param name="victim">The character to check.</param>
        /// <returns>Returns true if it's a bracket character.</returns>
        public static bool IsBracket (char victim) {
            return IsOpenBracket(victim) || IsCloseBracket(victim);
        }
        #endregion

        #region bool IsOpenBracket (char)
        /// <summary>
        /// Checks if the character is an open bracket.
        /// </summary>
        /// <param name="victim">The character to check.</param>
        /// <returns>Returns true if it's an open bracket.</returns>
        public static bool IsOpenBracket (char victim) {
            return (victim == PARENTHESIS_OPEN)
                || (victim == BRACKET_OPEN)
                || (victim == CURLY_BRACKET_OPEN);
        }
        #endregion

        #region bool IsCloseBracket (char)
        /// <summary>
        /// Checks if the character is a close bracket.
        /// </summary>
        /// <param name="victim">The character to check.</param>
        /// <returns>Returns true if it's a close bracket.</returns>
        public static bool IsCloseBracket (char victim) {
            return (victim == PARENTHESIS_CLOSE)
                || (victim == BRACKET_CLOSE)
                || (victim == CURLY_BRACKET_CLOSE);
        }
        #endregion

        #region bool IsBracket (string)
        /// <summary>
        /// Checks if the string is a bracket character.
        /// </summary>
        /// <param name="victim">The string to check.</param>
        /// <returns>Returns true if it's a bracket character.</returns>
        public static bool IsBracket (string victim) {
            return testStringFirstChar(victim, IsBracket);
        }
        #endregion

        #region bool IsOpenBracket (string)
        /// <summary>
        /// Checks if the string is an open bracket.
        /// </summary>
        /// <param name="victim">The string to check.</param>
        /// <returns>Returns true if it's an open bracket.</returns>
        public static bool IsOpenBracket (string victim) {
            return testStringFirstChar(victim, IsOpenBracket);
        }
        #endregion

        #region bool IsCloseBracket (string)
        /// <summary>
        /// Checks if the string is a close bracket.
        /// </summary>
        /// <param name="victim">The string to check.</param>
        /// <returns>Returns true if it's a close bracket.</returns>
        public static bool IsCloseBracket (string victim) {
            return testStringFirstChar(victim, IsCloseBracket);
        }
        #endregion

        //********************************************************************************
        // Methods (Operators)
        //********************************************************************************

        #region bool IsOperator (char)
        /// <summary>
        /// Checks if the character is an operator character.
        /// </summary>
        /// <param name="victim">The character to check.</param>
        /// <returns>Returns true if it's an operator character.</returns>
        public static bool IsOperator (char victim) {
            return (victim == PLUS)
                || (victim == MINUS)
                || (victim == ASTERISK)
                || (victim == SLASH)
                || (victim == CARET)
                || (victim == PERCENT)
                || (victim == EQUALS)
                || (victim == LESS_THAN)
                || (victim == GREATER_THAN);
        }
        #endregion

        #region bool IsOperator (string)
        /// <summary>
        /// Checks if the character is an operator string.
        /// </summary>
        /// <param name="victim">The string to check.</param>
        /// <returns>Returns true if it's an operator string.</returns>
        public static bool IsOperator (string victim) {
            return testStringFirstChar(victim, IsOperator)
                || (victim == LESS_OR_EQUALS_THAN)
                || (victim == GREATER_OR_EQUALS_THAN)
                || (victim == NOT_EQUALS)
                || (victim == RANGE)
                || (victim == ScriptKeywords.NOT)
                || (victim == ScriptKeywords.AND)
                || (victim == ScriptKeywords.OR)
                || (victim == ScriptKeywords.XOR)
                || (victim == ScriptKeywords.DIV)
                || (victim == ScriptKeywords.MOD);
        }
        #endregion

        //********************************************************************************
        // Methods (String)
        //********************************************************************************

        #region bool IsString2 (char)
        /// <summary>
        /// Checks if the character is a double quotes string character.
        /// </summary>
        /// <param name="victim">The character to check.</param>
        /// <returns>Returns true if it's a double quotes character.</returns>
        public static bool IsString2 (char victim) {
            return (victim == DOUBLE_QUOTES);
        }
        #endregion

        #region bool IsString1 (char)
        /// <summary>
        /// Checks if the character is a single quote string character.
        /// </summary>
        /// <param name="victim">The character to check.</param>
        /// <returns>Returns true if it's a single quote character.</returns>
        public static bool IsString1 (char victim) {
            return (victim == SINGLE_QUOTE);
        }
        #endregion

        #region bool IsEscape (char)
        /// <summary>
        /// Checks if the character is an escape character.
        /// </summary>
        /// <param name="victim">The character to check.</param>
        /// <returns>Returns true if it's an escape character.</returns>
        public static bool IsEscape (char victim) {
            return (victim == BACKSLASH);
        }
        #endregion

        #region bool IsString (string)
        /// <summary>
        /// Checks if the string is a string element.
        /// </summary>
        /// <param name="victim">The string to check.</param>
        /// <returns>Returns true if it's a string.</returns>
        public static bool IsString (string victim) {
            if (string.IsNullOrEmpty(victim) || victim.Length < 2) {
                return false;
            } else {
                string DOUBLE_QUOTES_STRING = DOUBLE_QUOTES.ToString();
                string SINGLE_QUOTE_STRING = SINGLE_QUOTE.ToString();
                return (victim.StartsWith(DOUBLE_QUOTES_STRING)
                    && victim.EndsWith(DOUBLE_QUOTES_STRING))
                    || (victim.StartsWith(SINGLE_QUOTE_STRING)
                    && victim.EndsWith(SINGLE_QUOTE_STRING));
            }
        }
        #endregion

        //********************************************************************************
        // Methods (Special)
        //********************************************************************************

        #region bool IsComment (char)
        /// <summary>
        /// Checks if the character is a comment character.
        /// </summary>
        /// <param name="victim">The character to check.</param>
        /// <returns>Returns true if it's a comment character.</returns>
        public static bool IsComment (char victim) {
            return (victim == HASH);
        }
        #endregion

        #region bool IsSeparator (char)
        /// <summary>
        /// Checks if the character is a separator character.
        /// </summary>
        /// <param name="victim">The character to check.</param>
        /// <returns>Returns true if it's a separator character.</returns>
        public static bool IsSeparator (char victim) {
            return (victim == PERIOD)
                || (victim == COLON)
                || (victim == SEMICOLON)
                || (victim == COMMA);
        }
        #endregion

        #region bool IsComment (string)
        /// <summary>
        /// Checks if the character is a comment string.
        /// </summary>
        /// <param name="victim">The string to check.</param>
        /// <returns>Returns true if it's a comment string.</returns>
        public static bool IsComment (string victim) {
            return testStringFirstChar(victim, IsComment);
        }
        #endregion

        #region bool IsCommentLine (string)
        /// <summary>
        /// Checks if the string is a comment line.
        /// </summary>
        /// <param name="victim">The string to check.</param>
        /// <returns>Returns true if it's a comment line.</returns>
        public static bool IsCommentLine (string victim) {
            return victim.StartsWith(HASH.ToString());
        }
        #endregion

        #region bool IsSeparator (string)
        /// <summary>
        /// Checks if the character is a separator string.
        /// </summary>
        /// <param name="victim">The string to check.</param>
        /// <returns>Returns true if it's a separator string.</returns>
        public static bool IsSeparator (string victim) {
            return testStringFirstChar(victim, IsSeparator);
        }
        #endregion

        //********************************************************************************
        // Methods (Reserved words)
        //********************************************************************************

        #region bool IsReserved (string)
        /// <summary>
        /// Checks if the string is a reserved name.
        /// </summary>
        /// <param name="victim">The string to check.</param>
        /// <returns>Returns true if it's a reserved name.</returns>
        public static bool IsReserved (string victim) {
            return IsFunction(victim) || IsKeyword(victim);
        }
        #endregion

        #region bool IsNotReserved (string)
        /// <summary>
        /// Checks if the string is not a reserved name.
        /// </summary>
        /// <param name="victim">The string to check.</param>
        /// <returns>Returns true if it's not a reserved name.</returns>
        public static bool IsNotReserved (string victim) {
            return !IsReserved(victim);
        }
        #endregion

        #region bool IsFunction (string)
        /// <summary>
        /// Checks if the string is a function name.
        /// </summary>
        /// <param name="victim">The string to check.</param>
        /// <returns>Returns true if it's a function name.</returns>
        public static bool IsFunction (string victim) {
            return (victim == ScriptFunctions.WRITELN)
                || (victim == ScriptFunctions.WRITE)
                || (victim == ScriptFunctions.GOTO)
                || (victim == ScriptFunctions.FINISH)
                || (victim == ScriptFunctions.SETDEFMSG)
                || (victim == ScriptFunctions.INVADD)
                || (victim == ScriptFunctions.INVREM)
                || (victim == ScriptFunctions.INVHAS)
                || (victim == ScriptFunctions.RESET)
                || (victim == ScriptFunctions.ROOMNAME)
                || (victim == ScriptFunctions.LASTROOM)
                || (victim == ScriptFunctions.SETRUNFST)
                || (victim == ScriptFunctions.SETRUNALL);
        }
        #endregion

        #region bool IsKeyword (string)
        /// <summary>
        /// Checks if the string is a keyword.
        /// </summary>
        /// <param name="victim">The string to check.</param>
        /// <returns>Returns true if it's a keyword.</returns>
        public static bool IsKeyword (string victim) {
            return (victim == ScriptKeywords.ROOM)
                || (victim == ScriptKeywords.OBJECT)
                || (victim == ScriptKeywords.EVENT)
                || (victim == ScriptKeywords.IF)
                || (victim == ScriptKeywords.ELSE)
                || (victim == ScriptKeywords.WHILE)
                || (victim == ScriptKeywords.FOR)
                || (victim == ScriptKeywords.IN)
                || (victim == ScriptKeywords.TO)
                || (victim == ScriptKeywords.DOWNTO)
                || (victim == ScriptKeywords.LET)
                || (victim == ScriptKeywords.END)
                || (victim == ScriptKeywords.TRUE)
                || (victim == ScriptKeywords.FALSE);
        }
        #endregion

        //********************************************************************************
        // Methods (Util)
        //********************************************************************************

        #region bool testStringFirstChar (string, Func<char, bool>)
        /// <summary>
        /// Checks if a string contains only one character and if that
        /// character matches with some defined type.
        /// </summary>
        /// <param name="victim">The string to check.</param>
        /// <param name="operation">The function that checks.</param>
        /// <returns>Returns true if it's matches the type character.</returns>
        private static bool testStringFirstChar (string victim, Func<char, bool> operation) {
            return (string.IsNullOrEmpty(victim) || (victim.Length != 1)) ?
                false : operation(victim.First());
        }
        #endregion
    }
}
