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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Engine.Script {
    /// <summary>
    /// This class represents the script tokenizer.
    /// </summary>
    public class ScriptTokenizer {
        //********************************************************************************
        // Types
        //********************************************************************************

        #region enum TokenizerState
        /// <summary>
        /// This enumeration represents the states of the tokenizer state machine.
        /// </summary>
        private enum TokenizerState {
            Normal, String, StringEscape, String1, String1Escape, Comment
        }
        #endregion

        //********************************************************************************
        // Properties
        //********************************************************************************

        #region TokenizerState _state
        /// <summary>
        /// The state of the tokenizer.
        /// </summary>
        private TokenizerState _state = TokenizerState.Normal;
        #endregion

        #region StringBuilder _currentToken
        /// <summary>
        /// The current token in the tokenizer.
        /// </summary>
        private StringBuilder _currentToken = null;
        #endregion

        #region List<string> Tokens
        /// <summary>
        /// The generated tokens in the tokenizer.
        /// </summary>
        private List<string> _tokens = null;

        /// <summary>
        /// Gets the generated tokens in the tokenizer.
        /// </summary>
        public IEnumerable<string> Tokens { get { return _tokens; } }
        #endregion

        //********************************************************************************
        // Methods (General)
        //********************************************************************************

        #region string TokensToString ()
        /// <summary>
        /// Gets a string representation of the current generated tokens.
        /// </summary>
        /// <returns>The string representation of the tokens.</returns>
        public string TokensToString () {
            string msg = "[";
            if (_tokens != null && _tokens.Count > 0) {
                msg += _tokens.Select(x => "<: " + x + " :>")
                              .Aggregate((c, n) => c + "; " + n);
            }
            msg += "]";
            return msg;
        }
        #endregion

        #region IEnumerable<string> Split (string)
        /// <summary>
        /// Gets the tokens inside a source code string.
        /// </summary>
        /// <param name="input">The input string.</param>
        /// <returns>A collection of tokens.</returns>
        public IEnumerable<string> Split (string input) {
            _tokens = new List<string>();
            _state = TokenizerState.Normal;
            _currentToken = new StringBuilder("");
            foreach (var c in input) {
                if (!ignoreCharacter(c)) {
                    switch (_state) {
                        case TokenizerState.Normal: onNormalStep(c); break;
                        case TokenizerState.String: onStringStep(c); break;
                        case TokenizerState.StringEscape: onStringEscapeStep(c); break;
                        case TokenizerState.String1: onString1Step(c); break;
                        case TokenizerState.String1Escape: onString1EscapeStep(c); break;
                        case TokenizerState.Comment: onCommentStep(c); break;
                    }
                }
            }
            addToken();
            return _tokens;
        }
        #endregion

        #region bool ignoreCharacter (char)
        /// <summary>
        /// Checks if the character must be ignore by the tokenizer.
        /// </summary>
        /// <param name="victim">The character to check.</param>
        /// <returns>Returns true if the character must be ignored.</returns>
        private bool ignoreCharacter (char victim) {
            return (victim == '\r');
        }
        #endregion

        #region void addToken ()
        /// <summary>
        /// Adds a token inside the list of generated tokens.
        /// </summary>
        private void addToken () {
            if (_currentToken.Length > 0) {
                _tokens.Add(_currentToken.ToString());
                _currentToken.Remove(0, _currentToken.Length);
            }
        }
        #endregion

        //********************************************************************************
        // Methods (Validate)
        //********************************************************************************

        #region bool validate (string)
        /// <summary>
        /// Checks if a string is a valid token in the script.
        /// </summary>
        /// <param name="token">The string to check.</param>
        /// <returns>Returns true if the token is valid.</returns>
        private bool validate (string token) {
            return string.IsNullOrEmpty(token)
                || ScriptSymbol.IsIdentifier(token)
                || ScriptSymbol.IsIntegerNumber(token)
                || ScriptSymbol.IsFloatNumber(token)
                || ScriptSymbol.IsBracket(token)
                || ScriptSymbol.IsOperator(token)
                || ScriptSymbol.IsSeparator(token);
        }
        #endregion

        //********************************************************************************
        // Methods (Steps)
        //********************************************************************************

        #region void onNormalStep (char)
        /// <summary>
        /// Executes the next step in the normal state of the tokenizer.
        /// </summary>
        /// <param name="c">The character to process.</param>
        /// <exception cref="ScriptTokenizerException">
        /// This exception is thrown when an invalid token is founded.
        /// </exception>
        private void onNormalStep (char c) {
            Action<TokenizerState> changeState = ns => {
                addToken();
                _state = ns;
                _currentToken.Append(c);
            };
            if (ScriptSymbol.IsBlank(c)) {
                addToken();
            } else if (ScriptSymbol.IsComment(c)) {
                changeState(TokenizerState.Comment);
            } else if (ScriptSymbol.IsString2(c)) {
                changeState(TokenizerState.String);
            } else if (ScriptSymbol.IsString1(c)) {
                changeState(TokenizerState.String1);
            } else {
                var token = _currentToken.ToString();
                if (validate(token)) {
                    if (!validate(token + c)) {
                        addToken();
                    }
                    _currentToken.Append(c);
                } else {
                    throw new ScriptTokenizerException(token);
                }
            }
        }
        #endregion

        #region void onStringStep (char, TokenizerState)
        /// <summary>
        /// Executes the next step in the string state of the tokenizer.
        /// </summary>
        /// <param name="c">The character to process.</param>
        /// <param name="endOfString">The end of string checker.</param>
        /// <param name="escapeState">The escape state.</param>
        private void onStringStep (char c, Func<char, bool> endOfString,
            TokenizerState escapeState) {
            _currentToken.Append(c);
            if (endOfString(c)) {
                addToken();
                _state = TokenizerState.Normal;
            } else if (ScriptSymbol.IsEscape(c)) {
                _state = escapeState;
            }
        }
        #endregion

        #region void onStringEscapeStep (char, TokenizerState)
        /// <summary>
        /// Executes the next step in the string escape state of the tokenizer.
        /// </summary>
        /// <param name="c">The character to process.</param>
        /// <param name="nextState">The next state.</param>
        private void onStringEscapeStep (char c, TokenizerState nextState) {
            _currentToken.Append(c);
            _state = nextState;
        }
        #endregion

        #region void onStringStep (char)
        /// <summary>
        /// Executes the next step in the string state of the tokenizer.
        /// </summary>
        /// <param name="c">The character to process.</param>
        private void onStringStep (char c) {
            onStringStep(c, ScriptSymbol.IsString2, TokenizerState.StringEscape);
        }
        #endregion

        #region void onStringEscapeStep (char)
        /// <summary>
        /// Executes the next step in the string escape state of the tokenizer.
        /// </summary>
        /// <param name="c">The character to process.</param>
        private void onStringEscapeStep (char c) {
            onStringEscapeStep(c, TokenizerState.String);
        }
        #endregion

        #region void onString1Step (char)
        /// <summary>
        /// Executes the next step in the single quote state of the tokenizer.
        /// </summary>
        /// <param name="c">The character to process.</param>
        private void onString1Step (char c) {
            onStringStep(c, ScriptSymbol.IsString1, TokenizerState.String1Escape);
        }
        #endregion

        #region void onString1EscapeStep (char)
        /// <summary>
        /// Executes the next step in the single quote escape state of the tokenizer.
        /// </summary>
        /// <param name="c">The character to process.</param>
        private void onString1EscapeStep (char c) {
            onStringEscapeStep(c, TokenizerState.String1);
        }
        #endregion

        #region void onCommentStep (char)
        /// <summary>
        /// Executes the next step in the comment state of the tokenizer.
        /// </summary>
        /// <param name="c">The character to process.</param>
        private void onCommentStep (char c) {
            if (ScriptSymbol.NEW_LINE == c) {
                addToken();
                _state = TokenizerState.Normal;
            } else {
                _currentToken.Append(c);
            }
        }
        #endregion
    }
}
