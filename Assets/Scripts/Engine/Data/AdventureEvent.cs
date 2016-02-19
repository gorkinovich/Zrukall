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
using System.Collections.Generic;

namespace Engine.Data {
    /// <summary>
    /// This enumeration represents the different types of game events.
    /// </summary>
    public enum AdventureEventType {
        OnEnter, OnExit, OnAction
    }

    /// <summary>
    /// This class represents an event inside a room in the game adventure.
    /// </summary>
    public class AdventureEvent {
        //********************************************************************************
        // Properties
        //********************************************************************************

        #region AdventureEventType Type
        /// <summary>
        /// Gets the type of the event.
        /// </summary>
        public AdventureEventType Type { get; private set; }
        #endregion

        #region string ActionParam
        /// <summary>
        /// Gets the parameter of the action type event.
        /// </summary>
        public string ActionParam { get; private set; }
        #endregion

        #region IEnumerable<IAdventureExpression> Expressions
        /// <summary>
        /// Gets the expressions of the event.
        /// </summary>
        public IEnumerable<IAdventureExpression> Expressions { get; private set; }
        #endregion

        //********************************************************************************
        // Constructors
        //********************************************************************************

        #region AdventureEvent (AdventureEventType, string, IEnumerable<IAdventureExpression>)
        /// <summary>
        /// Constructs a new object.
        /// </summary>
        /// <param name="type">The type of the event.</param>
        /// <param name="actionParam">The parameter of the action type event.</param>
        /// <param name="expressions">The expressions of the event.</param>
        public AdventureEvent (AdventureEventType type, string actionParam,
            IEnumerable<IAdventureExpression> expressions) {
            Type = type;
            ActionParam = actionParam != null ? actionParam.ToLower() : "";
            Expressions = expressions;
        }
        #endregion

        //********************************************************************************
        // Methods
        //********************************************************************************

        #region bool ActionMatches (string)
        /// <summary>
        /// Checks if an input string from the user matches with the action.
        /// </summary>
        /// <param name="input">The user input string.</param>
        /// <returns>Returns true if the input matches with the action.</returns>
        public bool ActionMatches (string input) {
            if (input.ToLower() == ActionParam) {
                return true;
            }
            var inputTokens = input.Split(' ');
            if (inputTokens.Length <= 1) {
                // When there is only one token, you should has only one input:
                foreach (var action in ActionParam.Split('|')) {
                    if (input.ToLower() == action.ToLower()) {
                        return true;
                    }
                }
            } else {
                // When there is multiple tokens, you should check if there is
                // the same pattern in de input:
                foreach (var action in ActionParam.Split('|')) {
                    int index = 0;
                    var tokens = action.Split(' ');
                    foreach (var actionToken in inputTokens) {
                        if (actionToken.ToLower() == tokens[index].ToLower()) {
                            index++;
                            if (index >= tokens.Length) {
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }
        #endregion
    }
}
