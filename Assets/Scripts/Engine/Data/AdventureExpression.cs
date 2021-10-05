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
using Console;
using Engine.Script;
using Engine.Util;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Engine.Data {
    //|==================================================================================|
    //|                               IAdventureExpression                               |
    //|==================================================================================|

    #region interface IAdventureExpression
    /// <summary>
    /// This interface represents an expression inside the game adventure.
    /// </summary>
    public interface IAdventureExpression {
        //********************************************************************************
        // Methods
        //********************************************************************************

        #region IAdventureValue Execute ()
        /// <summary>
        /// Executes the expression.
        /// </summary>
        IAdventureValue Execute ();
        #endregion
    }
    #endregion

    //|==================================================================================|
    //|                            AdventureLiteralExpression                            |
    //|==================================================================================|

    #region class AdventureLiteralExpression
    /// <summary>
    /// This class represents a literal expresion inside the game adventure.
    /// </summary>
    public class AdventureLiteralExpression : IAdventureExpression {
        //********************************************************************************
        // Properties
        //********************************************************************************

        #region IAdventureValue _value
        /// <summary>
        /// The value of the literal.
        /// </summary>
        private IAdventureValue _value;
        #endregion

        //********************************************************************************
        // Constructors
        //********************************************************************************

        #region AdventureLiteralExpression (string)
        /// <summary>
        /// Constructs a new object.
        /// </summary>
        /// <param name="value">The value of the literal.</param>
        public AdventureLiteralExpression (string value) {
            _value = AdventureValue.Create(value);
        }
        #endregion

        #region AdventureLiteralExpression (IAdventureValue)
        /// <summary>
        /// Constructs a new object.
        /// </summary>
        /// <param name="value">The value of the literal.</param>
        public AdventureLiteralExpression (IAdventureValue value) {
            _value = value;
        }
        #endregion

        //********************************************************************************
        // Methods
        //********************************************************************************

        #region IAdventureValue Execute ()
        /// <summary>
        /// Executes the expression.
        /// </summary>
        public IAdventureValue Execute () {
            return _value;
        }
        #endregion
    }
    #endregion

    //|==================================================================================|
    //|                          AdventureIdentifierExpression                           |
    //|==================================================================================|

    #region class AdventureIdentifierExpression
    /// <summary>
    /// This class represents a variable expresion inside the game adventure.
    /// </summary>
    public class AdventureIdentifierExpression : IAdventureExpression {
        //********************************************************************************
        // Properties
        //********************************************************************************

        #region string Identifier
        /// <summary>
        /// Gets the identifier name of the variable.
        /// </summary>
        public string Identifier { get; private set; }
        #endregion

        //********************************************************************************
        // Constructors
        //********************************************************************************

        #region AdventureIdentifierExpression (string)
        /// <summary>
        /// Constructs a new object.
        /// </summary>
        /// <param name="identifier">The identifier name of the variable.</param>
        public AdventureIdentifierExpression (string identifier) {
            Identifier = identifier;
        }
        #endregion

        //********************************************************************************
        // Methods
        //********************************************************************************

        #region IAdventureValue Execute ()
        /// <summary>
        /// Executes the expression.
        /// </summary>
        public IAdventureValue Execute () {
            return EngineBehaviour.Instance.Context.GetVariable(Identifier);
        }
        #endregion
    }
    #endregion

    //|==================================================================================|
    //|                             AdventureCallExpression                              |
    //|==================================================================================|

    #region class AdventureCallExpression
    /// <summary>
    /// This class represents a function call expression inside the game adventure.
    /// </summary>
    public class AdventureCallExpression : IAdventureExpression {
        //********************************************************************************
        // Properties
        //********************************************************************************

        #region string Name
        /// <summary>
        /// Gets the name of the function to call.
        /// </summary>
        public string Name { get; private set; }
        #endregion

        #region IEnumerable<IAdventureExpression> Parameters
        /// <summary>
        /// Gets the parameters of the function to call.
        /// </summary>
        public IEnumerable<IAdventureExpression> Parameters { get; private set; }
        #endregion

        //********************************************************************************
        // Constructors
        //********************************************************************************

        #region AdventureCallExpression (string, IEnumerable<IAdventureExpression>)
        /// <summary>
        /// Constructs a new object.
        /// </summary>
        /// <param name="name">The name of the function to call.</param>
        /// <param name="events">The parameters of the function to call.</param>
        public AdventureCallExpression (string name,
            IEnumerable<IAdventureExpression> parameters) {
            Name = name;
            Parameters = parameters;
        }
        #endregion

        //********************************************************************************
        // Methods
        //********************************************************************************

        #region IAdventureValue Execute ()
        /// <summary>
        /// Executes the expression.
        /// </summary>
        public IAdventureValue Execute () {
            var core = EngineBehaviour.Instance;
            if (Name == ScriptFunctions.WRITELN) {
                // The "write line" function:
                core.WriteLine(paramsToConsoleStrings());
            } else if (Name == ScriptFunctions.WRITE) {
                // The "write" function:
                core.Write(paramsToConsoleStrings());
            } else if (Name == ScriptFunctions.GOTO) {
                // The "goto" function:
                var name = firstParamToString();
                if (name != null) {
                    core.Context.SetNextRoom(name);
                }
            } else if (Name == ScriptFunctions.FINISH) {
                // The "finish" function:
                core.Context.Finished = true;
            } else if (Name == ScriptFunctions.SETDEFMSG) {
                // The "set default message" function:
                if (Parameters.Count() > 0) {
                    var message = Parameters.First().Execute().GetString();
                    core.Context.DefaultMessage = message;
                }
            } else if (Name == ScriptFunctions.INVADD) {
                // The "inventory add" function:
                var name = firstParamToString();
                if (name != null) {
                    core.Context.AddToInventory(name);
                }
            } else if (Name == ScriptFunctions.INVREM) {
                // The "inventory remove" function:
                var name = firstParamToString();
                if (name != null) {
                    core.Context.RemoveFromInventory(name);
                }
            } else if (Name == ScriptFunctions.INVHAS) {
                // The "inventory has" function:
                bool result = false;
                var name = firstParamToString();
                if (name != null) {
                    result = core.Context.HasInInventory(name);
                }
                return new AdventureBooleanValue(result);
            } else if (Name == ScriptFunctions.RESET) {
                // The "reset" function:
                core.Context.Reset = true;
            } else if (Name == ScriptFunctions.ROOMNAME) {
                // The "room name" function:
                return new AdventureStringValue((core.Context.Room != null) ?
                    core.Context.Room.Name : AdventureContext.NO_ROOM);
            } else if (Name == ScriptFunctions.LASTROOM) {
                // The "last room" function:
                return new AdventureStringValue(core.Context.LastRoom);
            } else if (Name == ScriptFunctions.SETRUNFST) {
                // The "set run first" function:
                core.Context.RunAll = false;
            } else if (Name == ScriptFunctions.SETRUNALL) {
                // The "set run all" function:
                core.Context.RunAll = true;
            } else if (Name == ScriptFunctions.SETFGCOLOR) {
                // The "set foreground color" function:
                if (Parameters.Count() > 0) {
                    var color = Parameters.First().Execute().GetInteger();
                    core.CurrentForegroundColor = EgaPalette.GetColorByIndex(color, EgaPalette.WhiteIndex);
                }
            } else if (Name == ScriptFunctions.SETBGCOLOR) {
                // The "set background color" function:
                if (Parameters.Count() > 0) {
                    var color = Parameters.First().Execute().GetInteger();
                    core.CurrentBackgroundColor = EgaPalette.GetColorByIndex(color, EgaPalette.BlackIndex);
                }
            } else if (Name == ScriptFunctions.CLEAR) {
                // The "clear screen" function:
                core.Clear();
            } else if (Name == ScriptFunctions.EXIT) {
                // The "exit" function:
                core.Exit();
            }
            return core.Context.DefaultValue;
        }
        #endregion

        #region string paramsToStringBlocks ()
        /// <summary>
        /// Converts the parameters into a string blocks.
        /// </summary>
        /// <returns>The string blocks representation of the parameters.</returns>
        private IEnumerable<ConsoleString> paramsToConsoleStrings () {
            if (Parameters.Count() > 0) {
                int position = 0;
                int foreground = EgaPalette.WhiteIndex;
                int background = EgaPalette.BlackIndex;
                LinkedList<ConsoleString> messages = new LinkedList<ConsoleString>();
                foreach (var item in Parameters) {
                    var value = item.Execute();
                    if (value is AdventureStringValue) {
                        messages.AddLast(new ConsoleString(
                            value.GetString(),
                            EgaPalette.GetColorByIndex(foreground),
                            EgaPalette.GetColorByIndex(background)
                        ));
                        position = 0;
                        foreground = EgaPalette.WhiteIndex;
                        background = EgaPalette.BlackIndex;
                    } else {
                        if (position == 0) {
                            foreground = value.GetInteger();
                            ++position;
                        } else {
                            background = value.GetInteger();
                        }
                    }
                }
                return messages;
            } else {
                return null;
            }
        }
        #endregion

        #region string paramsToString ()
        /// <summary>
        /// Converts the parameters into a string.
        /// </summary>
        /// <returns>The string representation of the parameters.</returns>
        private string paramsToString () {
            string message = "";
            if (Parameters.Count() > 0) {
                message = Parameters.Select(x => x.Execute())
                                    .Select(x => x.GetString())
                                    .Aggregate((cur, nxt) => cur + nxt);
            }
            return message;
        }
        #endregion

        #region string firstParamToString ()
        /// <summary>
        /// Gets the first parameter as an string.
        /// </summary>
        /// <returns>The first parameter as an string.</returns>
        private string firstParamToString () {
            if (Parameters.Count() > 0) {
                var first = Parameters.First();
                if (first is AdventureIdentifierExpression) {
                    return ((AdventureIdentifierExpression) first).Identifier;
                } else if (first is AdventureLiteralExpression) {
                    return first.Execute().GetString();
                }
            }
            return null;
        }
        #endregion
    }
    #endregion

    //|==================================================================================|
    //|                             AdventureUnaryExpression                             |
    //|==================================================================================|

    #region class AdventureUnaryExpression
    /// <summary>
    /// This class represents an unary operator expresion inside the game adventure.
    /// </summary>
    public class AdventureUnaryExpression : IAdventureExpression {
        //********************************************************************************
        // Properties
        //********************************************************************************

        #region string _operator
        /// <summary>
        /// The operator symbol.
        /// </summary>
        private string _operator;
        #endregion

        #region IAdventureExpression _rhs
        /// <summary>
        /// The right handed side.
        /// </summary>
        private IAdventureExpression _rhs;
        #endregion

        //********************************************************************************
        // Constructors
        //********************************************************************************

        #region AdventureUnaryExpression (string, IAdventureExpression)
        /// <summary>
        /// Constructs a new object.
        /// </summary>
        /// <param name="op">The operator symbol.</param>
        /// <param name="rhs">The right handed side.</param>
        public AdventureUnaryExpression (string op, IAdventureExpression rhs) {
            _operator = op;
            _rhs = rhs;
        }
        #endregion

        //********************************************************************************
        // Methods
        //********************************************************************************

        #region IAdventureValue Execute ()
        /// <summary>
        /// Executes the expression.
        /// </summary>
        public IAdventureValue Execute () {
            if (_operator == ScriptSymbol.PLUS.ToString()) {
                return _rhs.Execute();
            } else if (_operator == ScriptSymbol.MINUS.ToString()) {
                var value = _rhs.Execute();
                if (value is AdventureFloatValue) {
                    return new AdventureFloatValue(-value.GetFloat());
                } else {
                    return new AdventureIntegerValue(-value.GetInteger());
                }
            } else if (_operator == ScriptKeywords.NOT) {
                var value = _rhs.Execute().GetBoolean();
                return new AdventureBooleanValue(!value);
            } else {
                throw new Exception("Invalid operator: " + _operator);
            }
        }
        #endregion
    }
    #endregion

    //|==================================================================================|
    //|                            AdventureBinaryExpression                             |
    //|==================================================================================|

    #region class AdventureBinaryExpression
    /// <summary>
    /// This class represents a binary operator expresion inside the game adventure.
    /// </summary>
    public class AdventureBinaryExpression : IAdventureExpression {
        //********************************************************************************
        // Properties
        //********************************************************************************

        #region string _operator
        /// <summary>
        /// The operator symbol.
        /// </summary>
        private string _operator;
        #endregion

        #region IAdventureExpression _lhs
        /// <summary>
        /// The left handed side.
        /// </summary>
        private IAdventureExpression _lhs;
        #endregion

        #region IAdventureExpression _rhs
        /// <summary>
        /// The right handed side.
        /// </summary>
        private IAdventureExpression _rhs;
        #endregion

        //********************************************************************************
        // Constructors
        //********************************************************************************

        #region AdventureBinaryExpression (string, IAdventureExpression, IAdventureExpression)
        /// <summary>
        /// Constructs a new object.
        /// </summary>
        /// <param name="op">The operator symbol.</param>
        /// <param name="lhs">The left handed side.</param>
        /// <param name="rhs">The right handed side.</param>
        public AdventureBinaryExpression (string op, IAdventureExpression lhs,
            IAdventureExpression rhs) {
            _operator = op;
            _rhs = rhs;
            _lhs = lhs;
        }
        #endregion

        //********************************************************************************
        // Methods
        //********************************************************************************

        #region IAdventureValue Execute ()
        /// <summary>
        /// Executes the expression.
        /// </summary>
        public IAdventureValue Execute () {
            if (_operator == ScriptSymbol.CARET.ToString()) {
                var lvalue = _lhs.Execute();
                var rvalue = _rhs.Execute();
                if (lvalue is AdventureFloatValue || rvalue is AdventureFloatValue) {
                    float value = (float) Math.Pow(lvalue.GetFloat(), rvalue.GetFloat());
                    return new AdventureFloatValue(value);
                } else {
                    int value = (int) Math.Pow(lvalue.GetInteger(), rvalue.GetInteger());
                    return new AdventureIntegerValue(value);
                }
            } else if (_operator == ScriptSymbol.ASTERISK.ToString()) {
                var lvalue = _lhs.Execute();
                var rvalue = _rhs.Execute();
                if (lvalue is AdventureFloatValue || rvalue is AdventureFloatValue) {
                    return new AdventureFloatValue(lvalue.GetFloat() * rvalue.GetFloat());
                } else {
                    return new AdventureIntegerValue(lvalue.GetInteger() * rvalue.GetInteger());
                }
            } else if (_operator == ScriptSymbol.SLASH.ToString()) {
                var lvalue = _lhs.Execute();
                var rvalue = _rhs.Execute();
                if (lvalue is AdventureFloatValue || rvalue is AdventureFloatValue) {
                    return new AdventureFloatValue(lvalue.GetFloat() / rvalue.GetFloat());
                } else {
                    return new AdventureIntegerValue(lvalue.GetInteger() / rvalue.GetInteger());
                }
            } else if (_operator == ScriptKeywords.DIV) {
                var lvalue = _lhs.Execute().GetInteger();
                var rvalue = _rhs.Execute().GetInteger();
                return new AdventureIntegerValue(lvalue / rvalue);
            } else if ((_operator == ScriptSymbol.PERCENT.ToString())
                || (_operator == ScriptKeywords.MOD)) {
                var lvalue = _lhs.Execute();
                var rvalue = _rhs.Execute();
                if (lvalue is AdventureFloatValue || rvalue is AdventureFloatValue) {
                    return new AdventureFloatValue(lvalue.GetFloat() % rvalue.GetFloat());
                } else {
                    return new AdventureIntegerValue(lvalue.GetInteger() % rvalue.GetInteger());
                }
            } else if (_operator == ScriptSymbol.PLUS.ToString()) {
                var lvalue = _lhs.Execute();
                var rvalue = _rhs.Execute();
                if (lvalue is AdventureStringValue || rvalue is AdventureStringValue) {
                    return new AdventureStringValue(lvalue.GetString() + rvalue.GetString());
                } else if (lvalue is AdventureFloatValue || rvalue is AdventureFloatValue) {
                    return new AdventureFloatValue(lvalue.GetFloat() + rvalue.GetFloat());
                } else {
                    return new AdventureIntegerValue(lvalue.GetInteger() + rvalue.GetInteger());
                }
            } else if (_operator == ScriptSymbol.MINUS.ToString()) {
                var lvalue = _lhs.Execute();
                var rvalue = _rhs.Execute();
                if (lvalue is AdventureFloatValue || rvalue is AdventureFloatValue) {
                    return new AdventureFloatValue(lvalue.GetFloat() - rvalue.GetFloat());
                } else {
                    return new AdventureIntegerValue(lvalue.GetInteger() - rvalue.GetInteger());
                }
            } else if (_operator == ScriptSymbol.RANGE) {
                //TODO: Complete this operator...
                return EngineBehaviour.Instance.Context.DefaultValue;
                //...
            } else if (_operator == ScriptSymbol.LESS_THAN.ToString()) {
                var lvalue = _lhs.Execute();
                var rvalue = _rhs.Execute();
                if (lvalue is AdventureStringValue || rvalue is AdventureStringValue) {
                    return new AdventureBooleanValue(lvalue.GetString().CompareTo(rvalue.GetString()) < 0);
                } else if (lvalue is AdventureFloatValue || rvalue is AdventureFloatValue) {
                    return new AdventureBooleanValue(lvalue.GetFloat() < rvalue.GetFloat());
                } else {
                    return new AdventureBooleanValue(lvalue.GetInteger() < rvalue.GetInteger());
                }
            } else if (_operator == ScriptSymbol.GREATER_THAN.ToString()) {
                var lvalue = _lhs.Execute();
                var rvalue = _rhs.Execute();
                if (lvalue is AdventureStringValue || rvalue is AdventureStringValue) {
                    return new AdventureBooleanValue(lvalue.GetString().CompareTo(rvalue.GetString()) > 0);
                } else if (lvalue is AdventureFloatValue || rvalue is AdventureFloatValue) {
                    return new AdventureBooleanValue(lvalue.GetFloat() > rvalue.GetFloat());
                } else {
                    return new AdventureBooleanValue(lvalue.GetInteger() > rvalue.GetInteger());
                }
            } else if (_operator == ScriptSymbol.LESS_OR_EQUALS_THAN) {
                var lvalue = _lhs.Execute();
                var rvalue = _rhs.Execute();
                if (lvalue is AdventureStringValue || rvalue is AdventureStringValue) {
                    return new AdventureBooleanValue(lvalue.GetString().CompareTo(rvalue.GetString()) <= 0);
                } else if (lvalue is AdventureFloatValue || rvalue is AdventureFloatValue) {
                    return new AdventureBooleanValue(lvalue.GetFloat() <= rvalue.GetFloat());
                } else {
                    return new AdventureBooleanValue(lvalue.GetInteger() <= rvalue.GetInteger());
                }
            } else if (_operator == ScriptSymbol.GREATER_OR_EQUALS_THAN) {
                var lvalue = _lhs.Execute();
                var rvalue = _rhs.Execute();
                if (lvalue is AdventureStringValue || rvalue is AdventureStringValue) {
                    return new AdventureBooleanValue(lvalue.GetString().CompareTo(rvalue.GetString()) >= 0);
                } else if (lvalue is AdventureFloatValue || rvalue is AdventureFloatValue) {
                    return new AdventureBooleanValue(lvalue.GetFloat() >= rvalue.GetFloat());
                } else {
                    return new AdventureBooleanValue(lvalue.GetInteger() >= rvalue.GetInteger());
                }
            } else if (_operator == ScriptSymbol.EQUALS.ToString()) {
                var lvalue = _lhs.Execute();
                var rvalue = _rhs.Execute();
                if (lvalue is AdventureStringValue || rvalue is AdventureStringValue) {
                    return new AdventureBooleanValue(lvalue.GetString() == rvalue.GetString());
                } else if (lvalue is AdventureFloatValue || rvalue is AdventureFloatValue) {
                    return new AdventureBooleanValue(lvalue.GetFloat() == rvalue.GetFloat());
                } else {
                    return new AdventureBooleanValue(lvalue.GetInteger() == rvalue.GetInteger());
                }
            } else if (_operator == ScriptSymbol.NOT_EQUALS) {
                var lvalue = _lhs.Execute();
                var rvalue = _rhs.Execute();
                if (lvalue is AdventureStringValue || rvalue is AdventureStringValue) {
                    return new AdventureBooleanValue(lvalue.GetString() != rvalue.GetString());
                } else if (lvalue is AdventureFloatValue || rvalue is AdventureFloatValue) {
                    return new AdventureBooleanValue(lvalue.GetFloat() != rvalue.GetFloat());
                } else {
                    return new AdventureBooleanValue(lvalue.GetInteger() != rvalue.GetInteger());
                }
            } else if (_operator == ScriptKeywords.AND) {
                var lvalue = _lhs.Execute().GetBoolean();
                var rvalue = _rhs.Execute().GetBoolean();
                return new AdventureBooleanValue(lvalue && rvalue);
            } else if (_operator == ScriptKeywords.XOR) {
                var lvalue = _lhs.Execute().GetBoolean();
                var rvalue = _rhs.Execute().GetBoolean();
                return new AdventureBooleanValue(lvalue ^ rvalue);
            } else if (_operator == ScriptKeywords.OR) {
                var lvalue = _lhs.Execute().GetBoolean();
                var rvalue = _rhs.Execute().GetBoolean();
                return new AdventureBooleanValue(lvalue || rvalue);
            } else {
                throw new Exception("Invalid operator: " + _operator);
            }
        }
        #endregion
    }
    #endregion

    //|==================================================================================|
    //|                              AdventureLetExpression                              |
    //|==================================================================================|

    #region class AdventureLetExpression
    /// <summary>
    /// This class represents a let statement expression inside the game adventure.
    /// </summary>
    public class AdventureLetExpression : IAdventureExpression {
        //********************************************************************************
        // Properties
        //********************************************************************************

        #region AdventureIdentifierExpression _variable
        /// <summary>
        /// The variable of the statement.
        /// </summary>
        private AdventureIdentifierExpression _variable;
        #endregion

        #region IAdventureExpression _expression
        /// <summary>
        /// The expression of the statement.
        /// </summary>
        private IAdventureExpression _expression;
        #endregion

        //********************************************************************************
        // Constructors
        //********************************************************************************

        #region AdventureLetExpression (AdventureIdentifierExpression, IAdventureExpression)
        /// <summary>
        /// Constructs a new object.
        /// </summary>
        /// <param name="variable">The variable of the statement.</param>
        /// <param name="expression">The expression of the statement.</param>
        public AdventureLetExpression (AdventureIdentifierExpression variable,
            IAdventureExpression expression) {
            _variable = variable;
            _expression = expression;
        }
        #endregion

        //********************************************************************************
        // Methods
        //********************************************************************************

        #region IAdventureValue Execute ()
        /// <summary>
        /// Executes the expression.
        /// </summary>
        public IAdventureValue Execute () {
            var value = _expression.Execute();
            EngineBehaviour.Instance.Context.SetVariable(_variable.Identifier, value);
            return value;
        }
        #endregion
    }
    #endregion

    //|==================================================================================|
    //|                              AdventureIfExpression                               |
    //|==================================================================================|

    #region class AdventureIfExpression
    /// <summary>
    /// This class represents an if statement expression inside the game adventure.
    /// </summary>
    public class AdventureIfExpression : IAdventureExpression {
        //********************************************************************************
        // Types
        //********************************************************************************

        #region struct Block
        /// <summary>
        /// This class represents a block in the if statement.
        /// </summary>
        public struct Block {
            public IAdventureExpression Condition;
            public IEnumerable<IAdventureExpression> Expressions;
            public Block (IAdventureExpression condition,
                IEnumerable<IAdventureExpression> expressions) {
                Condition = condition;
                Expressions = expressions;
            }
        }
        #endregion

        //********************************************************************************
        // Properties
        //********************************************************************************

        #region IEnumerable<Block> _blocks
        /// <summary>
        /// The conditional blocks of the if statement.
        /// </summary>
        private IEnumerable<Block> _blocks;
        #endregion

        #region IEnumerable<IAdventureExpression> _elseExpressions
        /// <summary>
        /// The else block of the if statement.
        /// </summary>
        private IEnumerable<IAdventureExpression> _elseExpressions;
        #endregion

        //********************************************************************************
        // Constructors
        //********************************************************************************

        #region AdventureIfExpression (IEnumerable<Block>, IEnumerable<IAdventureExpression>)
        /// <summary>
        /// Constructs a new object.
        /// </summary>
        /// <param name="blocks">The conditional blocks of the if statement.</param>
        /// <param name="elseExpressions">The else block of the if statement.</param>
        public AdventureIfExpression (IEnumerable<Block> blocks,
            IEnumerable<IAdventureExpression> elseExpressions) {
            _blocks = blocks;
            _elseExpressions = elseExpressions;
        }
        #endregion

        //********************************************************************************
        // Methods
        //********************************************************************************

        #region IAdventureValue Execute ()
        /// <summary>
        /// Executes the expression.
        /// </summary>
        public IAdventureValue Execute () {
            IAdventureValue lastValue = EngineBehaviour.Instance.Context.DefaultValue;
            foreach (var item in _blocks) {
                if (item.Condition.Execute().GetBoolean()) {
                    foreach (var expr in item.Expressions) {
                        lastValue = expr.Execute();
                    }
                    return lastValue;
                }
            }
            if (_elseExpressions != null) {
                foreach (var expr in _elseExpressions) {
                    lastValue = expr.Execute();
                }
            }
            return lastValue;
        }
        #endregion
    }
    #endregion

    //|==================================================================================|
    //|                             AdventureWhileExpression                             |
    //|==================================================================================|

    #region class AdventureWhileExpression
    /// <summary>
    /// This class represents a while statement expression inside the game adventure.
    /// </summary>
    public class AdventureWhileExpression : IAdventureExpression {
        //********************************************************************************
        // Properties
        //********************************************************************************

        #region IAdventureExpression _condition
        /// <summary>
        /// The condition of the while statement.
        /// </summary>
        private IAdventureExpression _condition;
        #endregion

        #region IEnumerable<IAdventureExpression> _expressions
        /// <summary>
        /// The block of the while statement.
        /// </summary>
        private IEnumerable<IAdventureExpression> _expressions;
        #endregion

        //********************************************************************************
        // Constructors
        //********************************************************************************

        #region AdventureWhileExpression (IAdventureExpression, IEnumerable<IAdventureExpression>)
        /// <summary>
        /// Constructs a new object.
        /// </summary>
        /// <param name="condition">The condition of the while statement.</param>
        /// <param name="expressions">The block of the while statement.</param>
        public AdventureWhileExpression (IAdventureExpression condition,
            IEnumerable<IAdventureExpression> expressions) {
            _condition = condition;
            _expressions = expressions;
        }
        #endregion

        //********************************************************************************
        // Methods
        //********************************************************************************

        #region IAdventureValue Execute ()
        /// <summary>
        /// Executes the expression.
        /// </summary>
        public IAdventureValue Execute () {
            while (_condition.Execute().GetBoolean()) {
                foreach (var expr in _expressions) {
                    expr.Execute();
                }
            }
            return EngineBehaviour.Instance.Context.DefaultValue;
        }
        #endregion
    }
    #endregion

    //|==================================================================================|
    //|                              AdventureForExpression                              |
    //|==================================================================================|

    #region class AdventureForExpression
    /// <summary>
    /// This class represents a for statement expression inside the game adventure.
    /// </summary>
    public class AdventureForExpression : IAdventureExpression {
        //********************************************************************************
        // Properties
        //********************************************************************************

        #region AdventureIdentifierExpression _variable
        /// <summary>
        /// The variable of the statement.
        /// </summary>
        private AdventureIdentifierExpression _variable;
        #endregion

        #region bool _ascending
        /// <summary>
        /// The ascending flag of the statement.
        /// </summary>
        private bool _ascending;
        #endregion

        #region IAdventureExpression _startExpression
        /// <summary>
        /// The start expression of the statement.
        /// </summary>
        private IAdventureExpression _startExpression;
        #endregion

        #region IAdventureExpression _endExpression
        /// <summary>
        /// The end expression of the statement.
        /// </summary>
        private IAdventureExpression _endExpression;
        #endregion

        #region IEnumerable<IAdventureExpression> _expressions
        /// <summary>
        /// The block of the statement.
        /// </summary>
        private IEnumerable<IAdventureExpression> _expressions;
        #endregion

        //********************************************************************************
        // Constructors
        //********************************************************************************

        #region AdventureForExpression (AdventureIdentifierExpression, bool, IAdventureExpression, IAdventureExpression, IEnumerable<IAdventureExpression>)
        /// <summary>
        /// Constructs a new object.
        /// </summary>
        /// <param name="variable">The variable of the statement.</param>
        /// <param name="ascending">The ascending flag of the statement.</param>
        /// <param name="startExpression">The start expression of the statement.</param>
        /// <param name="endExpression">The end expression of the statement.</param>
        /// <param name="expressions">The block of the statement.</param>
        public AdventureForExpression (AdventureIdentifierExpression variable, bool ascending,
            IAdventureExpression startExpression, IAdventureExpression endExpression,
            IEnumerable<IAdventureExpression> expressions) {
            _variable = variable;
            _ascending = ascending;
            _startExpression = startExpression;
            _endExpression = endExpression;
            _expressions = expressions;
        }
        #endregion

        //********************************************************************************
        // Methods
        //********************************************************************************

        #region IAdventureValue Execute ()
        /// <summary>
        /// Executes the expression.
        /// </summary>
        public IAdventureValue Execute () {
            var context = EngineBehaviour.Instance.Context;
            var start = _startExpression.Execute().GetInteger();
            var end = _endExpression.Execute().GetInteger();
            context.SetVariable(_variable.Identifier, new AdventureIntegerValue(start));
            var current = context.GetVariable(_variable.Identifier);
            if (_ascending) {
                while (current.GetInteger() <= end) {
                    foreach (var expr in _expressions) {
                        expr.Execute();
                    }
                    current.SetInteger(current.GetInteger() + 1);
                }
            } else {
                while (current.GetInteger() >= end) {
                    foreach (var expr in _expressions) {
                        expr.Execute();
                    }
                    current.SetInteger(current.GetInteger() - 1);
                }
            }
            return EngineBehaviour.Instance.Context.DefaultValue;
        }
        #endregion
    }
    #endregion
}
