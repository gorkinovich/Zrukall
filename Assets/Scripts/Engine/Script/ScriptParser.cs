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
using Engine.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Engine.Script {
    /// <summary>
    /// This class represents the adventure engine parser.
    /// </summary>
    public static class ScriptParser {
        //********************************************************************************
        // Types
        //********************************************************************************

        #region struct OperatorInfo
        /// <summary>
        /// This structure represents the information about an operator.
        /// </summary>
        private struct OperatorInfo {
            public bool Binary;
            public string Symbol;
            public int Priority;
        }
        #endregion

        //********************************************************************************
        // Properties
        //********************************************************************************

        #region string _currentEntityName
        /// <summary>
        /// The current room or object name (used when an exception is thrown).
        /// </summary>
        private static string _lastEntityName = "";
        #endregion

        //********************************************************************************
        // Methods
        //********************************************************************************

        #region AdventureGame Parse (string)
        /// <summary>
        /// Parses a string to get an adventure game to use it with the engine.
        /// </summary>
        /// <param name="input">The string to parse.</param>
        /// <returns>Returns the loaded adventure if success; otherwise null.</returns>
        public static AdventureGame Parse (string input) {
            try {
                int index = 0;
                AdventureGame adventure = new AdventureGame();
                ScriptTokenizer tokenizer = new ScriptTokenizer();
                Func<string, string> toLowerTokens = (token) => {
                    if (ScriptSymbol.IsString(token) || ScriptSymbol.IsCommentLine(token)) {
                        return token;
                    } else {
                        return token.ToLower();
                    }
                };
                var tokens = tokenizer.Split(input).Select(toLowerTokens)
                    .Where(x => !ScriptSymbol.IsCommentLine(x)).ToArray();
                while (index < tokens.Length) {
                    var token = tokens[index];
                    if (ScriptSymbol.IsCommentLine(token)) {
                        index++;
                    } else if (token == ScriptKeywords.ROOM) {
                        var victim = parseRoom(tokens, ref index);
                        if (victim != null) {
                            adventure.AddRoom(victim);
                        }
                        index++;
                    } else if (token == ScriptKeywords.OBJECT) {
                        var victim = parseObject(tokens, ref index);
                        if (victim != null) {
                            adventure.AddObject(victim);
                        }
                        index++;
                    } else {
                        throw new Exception("[PARSER:Global] Invalid declaration: " + token);
                    }
                }
                return adventure;
            } catch (Exception exception) {
                var output = EngineBehaviour.Instance.Output;
                output.WriteLine(exception.Message);
                output.WriteLine(exception.Source);
                output.WriteLine(exception.StackTrace);
                Debug.LogException(exception);
                return null;
            }
        }
        #endregion

        #region AdventureRoom parseRoom (string[], ref int)
        /// <summary>
        /// Parses the next tokens to construct a new adventure room object.
        /// </summary>
        /// <param name="tokens">The tokens to use.</param>
        /// <param name="index">The current position in the tokens array.</param>
        /// <returns>A new adventure room object.</returns>
        private static AdventureRoom parseRoom (string[] tokens, ref int index) {
            string name = tokens[++index];
            if (!ScriptSymbol.IsIdentifier(name)) {
                throw new Exception("[PARSER:Room] Invalid name identifier: " + name);
            }
            string separator = tokens[++index];
            if (ScriptSymbol.COLON.ToString() != separator) {
                throw new Exception("[PARSER:Room(" + name + ")] No colon after the name.");
            }
            string token;
            List<AdventureEvent> events = new List<AdventureEvent>();
            _lastEntityName = name;
            while (true) {
                token = tokens[++index];
                if (token == ScriptKeywords.EVENT) {
                    var victim = parseEvent(tokens, ref index);
                    if (victim != null) {
                        events.Add(victim);
                    }
                } else if (token == ScriptKeywords.END) {
                    break;
                } else {
                    throw new Exception("[PARSER:Room(" + name + ")] Invalid declaration: " + token);
                }
            }
            return new AdventureRoom(name, events);
        }
        #endregion

        #region AdventureObject parseObject (string[], ref int)
        /// <summary>
        /// Parses the next tokens to construct a new adventure object object.
        /// </summary>
        /// <param name="tokens">The tokens to use.</param>
        /// <param name="index">The current position in the tokens array.</param>
        /// <returns>A new adventure object object.</returns>
        private static AdventureObject parseObject (string[] tokens, ref int index) {
            string name = tokens[++index];
            if (!ScriptSymbol.IsIdentifier(name)) {
                throw new Exception("[PARSER:Object] Invalid name identifier: " + name);
            }
            string separator = tokens[++index];
            if (ScriptSymbol.COLON.ToString() != separator) {
                throw new Exception("[PARSER:Object(" + name + ")] No colon after the name.");
            }
            string token;
            List<AdventureEvent> events = new List<AdventureEvent>();
            _lastEntityName = name;
            while (true) {
                token = tokens[++index];
                if (token == ScriptKeywords.EVENT) {
                    var victim = parseEvent(tokens, ref index);
                    if (victim != null) {
                        events.Add(victim);
                    }
                } else if (token == ScriptKeywords.END) {
                    break;
                } else {
                    throw new Exception("[PARSER:Object(" + name + ")] Invalid declaration: " + token);
                }
            }
            return new AdventureObject(name, events);
        }
        #endregion

        #region AdventureEvent parseEvent (string[], ref int)
        /// <summary>
        /// Parses the next tokens to construct a new adventure event object.
        /// </summary>
        /// <param name="tokens">The tokens to use.</param>
        /// <param name="index">The current position in the tokens array.</param>
        /// <returns>A new adventure event object.</returns>
        private static AdventureEvent parseEvent (string[] tokens, ref int index) {
            // Get the type of event:
            string token = tokens[++index];
            AdventureEventType type = AdventureEventType.OnAction;
            string actionParam = null;
            if (ScriptSymbol.IsString(token)) {
                actionParam = AdventureValue.Normalize(token);
            } else if (token == ScriptKeywords.ENTER) {
                type = AdventureEventType.OnEnter;
            } else if (token == ScriptKeywords.EXIT) {
                type = AdventureEventType.OnExit;
            } else {
                throw new Exception("[PARSER:Event] Invalid type: " +
                    token + " {" + _lastEntityName + "}");
            }
            // Get the separator of the event:
            token = tokens[++index];
            if (ScriptSymbol.COLON.ToString() != token) {
                throw new Exception("[PARSER:Event(" + type +
                    ")] No colon after the type. {" + _lastEntityName + "}");
            }
            // Get the expressions of the event:
            List<IAdventureExpression> expressions = parseExpressions(tokens, ref index);
            return new AdventureEvent(type, actionParam, expressions);
        }
        #endregion

        #region List<IAdventureExpression> parseExpressions (string[], ref int)
        /// <summary>
        /// Parses the next tokens to construct a list of expressions objects.
        /// </summary>
        /// <param name="tokens">The tokens to use.</param>
        /// <param name="index">The current position in the tokens array.</param>
        /// <returns>A new list of expressions objects.</returns>
        private static List<IAdventureExpression> parseExpressions (string[] tokens, ref int index) {
            string token;
            List<IAdventureExpression> expressions = new List<IAdventureExpression>();
            while (true) {
                token = tokens[++index];
                if (token == ScriptKeywords.IF) {
                    var expression = parseIfExpression(tokens, ref index);
                    if (expression != null) {
                        expressions.Add(expression);
                    }
                } else if (token == ScriptKeywords.ELSE) {
                    break;
                } else if (token == ScriptKeywords.WHILE) {
                    var expression = parseWhileExpression(tokens, ref index);
                    if (expression != null) {
                        expressions.Add(expression);
                    }
                } else if (token == ScriptKeywords.FOR) {
                    var expression = parseForExpression(tokens, ref index);
                    if (expression != null) {
                        expressions.Add(expression);
                    }
                } else if (token == ScriptKeywords.LET) {
                    var expression = parseLetExpression(tokens, ref index);
                    if (expression != null) {
                        expressions.Add(expression);
                    }
                } else if (ScriptSymbol.IsFunction(token)) {
                    var expression = parseCallExpression(tokens, ref index);
                    if (expression != null) {
                        expressions.Add(expression);
                    }
                } else if (token == ScriptKeywords.COMMA) {
                } else if (token == ScriptKeywords.END) {
                    break;
                } else {
                    throw new Exception("[PARSER:Expression] Invalid statement: " +
                        token + " {" + _lastEntityName + "}");
                }
            }
            return expressions;
        }
        #endregion

        #region IAdventureExpression parseIfExpression (string[], ref int)
        /// <summary>
        /// Parses the next tokens to construct an if expression object.
        /// </summary>
        /// <param name="tokens">The tokens to use.</param>
        /// <param name="index">The current position in the tokens array.</param>
        /// <returns>A new if expression object.</returns>
        private static IAdventureExpression parseIfExpression (string[] tokens, ref int index) {
            string token;
            List<string> expressionTokens = new List<string>();
            while (true) {
                token = tokens[++index];
                if (token == ScriptSymbol.COLON.ToString()) {
                    break;
                } else {
                    expressionTokens.Add(token);
                }
            }
            if (expressionTokens.Count <= 0) {
                throw new Exception("[PARSER:If] No condition: " +
                    tokens.Take(index + 1).Aggregate((c, n) => c + " " + n) +
                    " {" + _lastEntityName + "}");
            }
            IEnumerable<IAdventureExpression> elseExpressions = null;
            IAdventureExpression condition = parseExpression(expressionTokens.ToArray());
            IEnumerable<IAdventureExpression> expressions = parseExpressions(tokens, ref index);
            List<AdventureIfExpression.Block> blocks = new List<AdventureIfExpression.Block>();
            blocks.Add(new AdventureIfExpression.Block(condition, expressions));
            expressionTokens.Clear();
            while (tokens[index] == ScriptKeywords.ELSE) {
                token = tokens[++index];
                if (token == ScriptSymbol.COLON.ToString()) {
                    elseExpressions = parseExpressions(tokens, ref index);
                } else if (token == ScriptKeywords.IF) {
                    while (true) {
                        token = tokens[++index];
                        if (token == ScriptSymbol.COLON.ToString()) {
                            break;
                        } else {
                            expressionTokens.Add(token);
                        }
                    }
                    if (expressionTokens.Count <= 0) {
                        throw new Exception("[PARSER:If] No condition: " +
                            tokens.Take(index + 1).Aggregate((c, n) => c + " " + n) +
                            " {" + _lastEntityName + "}");
                    }
                    condition = parseExpression(expressionTokens.ToArray());
                    expressions = parseExpressions(tokens, ref index);
                    blocks.Add(new AdventureIfExpression.Block(condition, expressions));
                    expressionTokens.Clear();
                } else {
                    throw new Exception("[PARSER:If] Invalid else: " +
                        tokens.Take(index + 1).Aggregate((c, n) => c + " " + n) +
                        " {" + _lastEntityName + "}");
                }
            }
            return new AdventureIfExpression(blocks, elseExpressions);
        }
        #endregion

        #region IAdventureExpression parseWhileExpression (string[], ref int)
        /// <summary>
        /// Parses the next tokens to construct a while expression object.
        /// </summary>
        /// <param name="tokens">The tokens to use.</param>
        /// <param name="index">The current position in the tokens array.</param>
        /// <returns>A new while expression object.</returns>
        private static IAdventureExpression parseWhileExpression (string[] tokens, ref int index) {
            string token;
            List<string> expressionTokens = new List<string>();
            while (true) {
                token = tokens[++index];
                if (token == ScriptSymbol.COLON.ToString()) {
                    break;
                } else {
                    expressionTokens.Add(token);
                }
            }
            if (expressionTokens.Count <= 0) {
                throw new Exception("[PARSER:If] No condition: " +
                    tokens.Take(index + 1).Aggregate((c, n) => c + " " + n) +
                    " {" + _lastEntityName + "}");
            }
            IAdventureExpression condition = parseExpression(expressionTokens.ToArray());
            IEnumerable<IAdventureExpression> expressions = parseExpressions(tokens, ref index);
            return new AdventureWhileExpression(condition, expressions);
        }
        #endregion

        #region IAdventureExpression parseForExpression (string[], ref int)
        /// <summary>
        /// Parses the next tokens to construct a for expression object.
        /// </summary>
        /// <param name="tokens">The tokens to use.</param>
        /// <param name="index">The current position in the tokens array.</param>
        /// <returns>A new for expression object.</returns>
        private static IAdventureExpression parseForExpression (string[] tokens, ref int index) {
            string token = tokens[++index];
            if (!ScriptSymbol.IsIdentifier(token) || ScriptSymbol.IsReserved(token)) {
                throw new Exception("[PARSER:For] Invalid identifier: " +
                    token + " {" + _lastEntityName + "}");
            }
            var variable = new AdventureIdentifierExpression(token);
            token = tokens[++index];
            if (token != ScriptKeywords.IN) {
                throw new Exception("[PARSER:For] No in after the identifier: " +
                    token + " {" + _lastEntityName + "}");
            }
            bool ascending = true;
            List<string> expressionTokens = new List<string>();
            while (true) {
                token = tokens[++index];
                if (token == ScriptKeywords.TO || token == ScriptSymbol.RANGE) {
                    break;
                } else if (token == ScriptKeywords.DOWNTO) {
                    ascending = false;
                    break;
                } else {
                    expressionTokens.Add(token);
                }
            }
            if (expressionTokens.Count <= 0) {
                throw new Exception("[PARSER:For] No start expression: " +
                    tokens.Take(index + 1).Aggregate((c, n) => c + " " + n) +
                    " {" + _lastEntityName + "}");
            }
            var startExpression = parseExpression(expressionTokens.ToArray());
            while (true) {
                token = tokens[++index];
                if (token == ScriptSymbol.COLON.ToString()) {
                    break;
                } else {
                    expressionTokens.Add(token);
                }
            }
            if (expressionTokens.Count <= 0) {
                throw new Exception("[PARSER:For] No end expression: " +
                    tokens.Take(index + 1).Aggregate((c, n) => c + " " + n) +
                    " {" + _lastEntityName + "}");
            }
            var endExpression = parseExpression(expressionTokens.ToArray());
            IEnumerable<IAdventureExpression> expressions = parseExpressions(tokens, ref index);
            return new AdventureForExpression(variable, ascending, startExpression, endExpression, expressions);
        }
        #endregion

        #region IAdventureExpression parseLetExpression (string[], ref int)
        /// <summary>
        /// Parses the next tokens to construct a let expression object.
        /// </summary>
        /// <param name="tokens">The tokens to use.</param>
        /// <param name="index">The current position in the tokens array.</param>
        /// <returns>A new let expression object.</returns>
        private static IAdventureExpression parseLetExpression (string[] tokens, ref int index) {
            string token = tokens[++index];
            if (!ScriptSymbol.IsIdentifier(token) || ScriptSymbol.IsReserved(token)) {
                throw new Exception("[PARSER:Let] Invalid identifier: " +
                    token + " {" + _lastEntityName + "}");
            }
            var identifier = new AdventureIdentifierExpression(token);
            token = tokens[++index];
            if (token != ScriptSymbol.EQUALS.ToString()) {
                throw new Exception("[PARSER:Let] No equals after the identifier: " +
                    token + " {" + _lastEntityName + "}");
            }
            List<string> expressionTokens = new List<string>();
            while (true) {
                token = tokens[++index];
                if (isEndOfBlock(token)) {
                    checkGoBackInTime(token, ref index);
                    break;
                } else {
                    expressionTokens.Add(token);
                }
            }
            if (expressionTokens.Count <= 0) {
                throw new Exception("[PARSER:Let] No expresion to asign: " +
                    tokens.Take(index + 1).Aggregate((c, n) => c + " " + n) +
                    " {" + _lastEntityName + "}");
            }
            var expression = parseExpression(expressionTokens.ToArray());
            if (expression == null) {
                throw new Exception("[PARSER:Let] Invalid expresion: " +
                    tokens.Take(index + 1).Aggregate((c, n) => c + " " + n) +
                    " {" + _lastEntityName + "}");
            }
            return new AdventureLetExpression(identifier, expression);
        }
        #endregion

        #region IAdventureExpression parseCallExpression (string[], ref int)
        /// <summary>
        /// Parses the next tokens to construct a call expression object.
        /// </summary>
        /// <param name="tokens">The tokens to use.</param>
        /// <param name="index">The current position in the tokens array.</param>
        /// <returns>A new call expression object.</returns>
        private static IAdventureExpression parseCallExpression (string[] tokens, ref int index) {
            string name = tokens[index];
            string token;
            List<string> expressionTokens = new List<string>();
            List<IAdventureExpression> parameters = new List<IAdventureExpression>();
            while (true) {
                token = tokens[++index];
                if (isEndOfBlock(token)) {
                    if (expressionTokens.Count > 0) {
                        var expression = parseParameters(expressionTokens.ToArray());
                        if (expression != null) {
                            parameters = expression;
                        }
                    }
                    checkGoBackInTime(token, ref index);
                    break;
                } else {
                    expressionTokens.Add(token);
                }
            }
            return new AdventureCallExpression(name, parameters);
        }
        #endregion

        #region List<IAdventureExpression> parseParameters (string[])
        /// <summary>
        /// Parses the next tokens to construct a list of parameters objects.
        /// </summary>
        /// <param name="tokens">The tokens to use.</param>
        /// <returns>A new list of parameters objects.</returns>
        private static List<IAdventureExpression> parseParameters (string[] tokens) {
            int index = 0, parenthesis = 0;
            string token;
            List<string> expressionTokens = new List<string>();
            List<IAdventureExpression> parameters = new List<IAdventureExpression>();
            Action addExpTokens = () => {
                var expression = parseExpression(expressionTokens.ToArray());
                if (expression != null) {
                    parameters.Add(expression);
                }
                expressionTokens.Clear();
            };
            while (index < tokens.Length) {
                token = tokens[index++];
                if (parenthesis == 0) {
                    if (token == ScriptSymbol.PARENTHESIS_OPEN.ToString()) {
                        expressionTokens.Add(token);
                        parenthesis++;
                    } else if (token == ScriptSymbol.PARENTHESIS_CLOSE.ToString()) {
                        throw new Exception("[PARSER:Parameters] Invalid number of parenthesis: " +
                            tokens.Take(index + 1).Aggregate((c, n) => c + " " + n) + " {" +
                            _lastEntityName + "}");
                    } else {
                        var expression = parseExpression(token);
                        if (expression != null) {
                            parameters.Add(expression);
                        }
                    }
                } else {
                    expressionTokens.Add(token);
                    if (token == ScriptSymbol.PARENTHESIS_OPEN.ToString()) {
                        parenthesis++;
                    } else if (token == ScriptSymbol.PARENTHESIS_CLOSE.ToString()) {
                        parenthesis--;
                        if (parenthesis == 0) {
                            addExpTokens();
                        }
                    }
                }
            }
            return parameters;
        }
        #endregion

        #region IAdventureExpression parseExpression (string[])
        /// <summary>
        /// Parses the next tokens to construct an expression object.
        /// </summary>
        /// <param name="tokens">The tokens to use.</param>
        /// <returns>A new expression object.</returns>
        private static IAdventureExpression parseExpression (string[] tokens) {
            int index = 0;
            string token;
            Stack<OperatorInfo> operators = new Stack<OperatorInfo>();
            Stack<IAdventureExpression> values = new Stack<IAdventureExpression>();
            Action makeExpression = () => {
                var top = operators.Pop();
                IAdventureExpression nextValue;
                if (top.Binary) {
                    var rhs = values.Pop();
                    var lhs = values.Pop();
                    nextValue = new AdventureBinaryExpression(top.Symbol, lhs, rhs);
                } else {
                    var rhs = values.Pop();
                    nextValue = new AdventureUnaryExpression(top.Symbol, rhs);
                }
                values.Push(nextValue);
            };
            while (index < tokens.Length) {
                token = tokens[index];
                if (token == ScriptSymbol.PARENTHESIS_OPEN.ToString()) {
                    int parenthesis = 1;
                    List<string> subtokens = new List<string>();
                    var nextTokens = tokens.Skip(index + 1);
                    foreach (var item in nextTokens) {
                        index++;
                        if (item == ScriptSymbol.PARENTHESIS_CLOSE.ToString()) {
                            parenthesis--;
                            if (parenthesis == 0) {
                                values.Push(parseExpression(subtokens.ToArray()));
                                break;
                            } else {
                                subtokens.Add(item);
                            }
                        } else {
                            subtokens.Add(item);
                            if (item == ScriptSymbol.PARENTHESIS_OPEN.ToString()) {
                                parenthesis++;
                            }
                        }
                    }
                } else if (ScriptSymbol.IsFunction(token)) {
                    string name = token;
                    var subtokens = tokens.Skip(index + 1).ToArray();
                    List<IAdventureExpression> parameters = parseParameters(subtokens);
                    values.Push(new AdventureCallExpression(name, parameters));
                    index = tokens.Length;
                } else if (ScriptSymbol.IsOperator(token)) {
                    bool unary = (token == ScriptKeywords.NOT) ||
                        ((token == ScriptSymbol.PLUS.ToString() ||
                        token == ScriptSymbol.MINUS.ToString()) &&
                        (index == 0 || ScriptSymbol.IsOperator(tokens[index - 1])));
                    var info = getOperatorInfo(token, !unary);
                    if (operators.Count > 0) {
                        var top = operators.Peek();
                        if (top.Priority < info.Priority) {
                            makeExpression();
                        }
                    }
                    operators.Push(info);
                } else {
                    values.Push(parseExpression(token));
                }
                index++;
            }
            while (operators.Count > 0) {
                makeExpression();
            }
            return values.Peek();
        }
        #endregion

        #region IAdventureExpression parseExpression (string)
        /// <summary>
        /// Parses the next token to construct an expression object.
        /// </summary>
        /// <param name="token">The token to use.</param>
        /// <returns>A new expression object.</returns>
        private static IAdventureExpression parseExpression (string token) {
            if (ScriptSymbol.IsIdentifier(token)) {
                if (ScriptSymbol.IsBoolean(token)) {
                    var value = new AdventureBooleanValue(token);
                    return new AdventureLiteralExpression(value);
                } else if (ScriptSymbol.IsReserved(token)) {
                    throw new Exception("[PARSER:Expresion] Invalid identifier: " +
                        token + " {" + _lastEntityName + "}");
                } else {
                    return new AdventureIdentifierExpression(token);
                }
            } else if (ScriptSymbol.IsString(token)) {
                var normalized = AdventureValue.Normalize(token);
                var value = new AdventureStringValue(normalized);
                return new AdventureLiteralExpression(value);
            } else if (ScriptSymbol.IsIntegerNumber(token)) {
                var value = new AdventureIntegerValue(token);
                return new AdventureLiteralExpression(value);
            } else if (ScriptSymbol.IsFloatNumber(token)) {
                var value = new AdventureFloatValue(token);
                return new AdventureLiteralExpression(value);
            } else {
                throw new Exception("[PARSER:Expresion] Invalid expression: " +
                    token + " {" + _lastEntityName + "}");
            }
        }
        #endregion

        #region OperatorInfo getOperatorInfo (string, bool)
        /// <summary>
        /// Gets the operator information data.
        /// </summary>
        /// <param name="symbol">The symbol of the operator in lower case.</param>
        /// <param name="binary">The binary flag of the operator.</param>
        /// <returns>The operator information data.</returns>
        private static OperatorInfo getOperatorInfo (string symbol, bool binary) {
            OperatorInfo info = new OperatorInfo { Symbol = symbol, Binary = binary };
            if (binary) {
                if (symbol == ScriptSymbol.CARET.ToString()) {
                    info.Priority = 2;
                } else if ((symbol == ScriptSymbol.ASTERISK.ToString())
                    || (symbol == ScriptSymbol.SLASH.ToString())
                    || (symbol == ScriptSymbol.PERCENT.ToString())
                    || (symbol == ScriptKeywords.DIV)
                    || (symbol == ScriptKeywords.MOD)) {
                    info.Priority = 3;
                } else if ((symbol == ScriptSymbol.PLUS.ToString())
                    || (symbol == ScriptSymbol.MINUS.ToString())) {
                    info.Priority = 4;
                } else if (symbol == ScriptSymbol.RANGE) {
                    info.Priority = 5;
                } else if ((symbol == ScriptSymbol.LESS_THAN.ToString())
                    || (symbol == ScriptSymbol.GREATER_THAN.ToString())
                    || (symbol == ScriptSymbol.LESS_OR_EQUALS_THAN)
                    || (symbol == ScriptSymbol.GREATER_OR_EQUALS_THAN)) {
                    info.Priority = 6;
                } else if ((symbol == ScriptSymbol.EQUALS.ToString())
                    || (symbol == ScriptSymbol.NOT_EQUALS)) {
                    info.Priority = 7;
                } else if (symbol == ScriptKeywords.AND) {
                    info.Priority = 8;
                } else if (symbol == ScriptKeywords.XOR) {
                    info.Priority = 9;
                } else if (symbol == ScriptKeywords.OR) {
                    info.Priority = 10;
                } else {
                    throw new Exception("[PARSER:Operator] Invalid binary operator: " +
                        symbol + " {" + _lastEntityName + "}");
                }
            } else {
                if ((symbol == ScriptKeywords.NOT)
                    || (symbol == ScriptSymbol.PLUS.ToString())
                    || (symbol == ScriptSymbol.MINUS.ToString())) {
                    info.Priority = 1;
                } else {
                    throw new Exception("[PARSER:Operator] Invalid unary operator: " +
                        symbol + " {" + _lastEntityName + "}");
                }
            }
            return info;
        }
        #endregion

        #region bool isEndOfBlock (string)
        /// <summary>
        /// Gets if a token is the end of a block of code.
        /// </summary>
        /// <param name="token">The token to check.</param>
        /// <returns>Returns true if the token is an end of block.</returns>
        private static bool isEndOfBlock (string token) {
            return (token == ScriptKeywords.COMMA)
                || (token == ScriptKeywords.END)
                || (token == ScriptKeywords.ELSE);
        }
        #endregion

        #region void checkGoBackInTime (string, ref int)
        /// <summary>
        /// Checks if must go back in the parsing of expressions.
        /// </summary>
        /// <param name="token">The token to check.</param>
        /// <param name="index">The index of the parsing.</param>
        private static void checkGoBackInTime (string token, ref int index) {
            if ((token == ScriptKeywords.END) || (token == ScriptKeywords.ELSE)) {
                index--;
            }
        }
        #endregion
    }
}
