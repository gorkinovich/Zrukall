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

namespace Engine.Data {
    /// <summary>
    /// This class represents the context in the game adventure.
    /// </summary>
    public class AdventureContext {
        //********************************************************************************
        // Constants
        //********************************************************************************

        #region string DEFAULT_ROOM
        /// <summary>
        /// The name of the default room when the game starts.
        /// </summary>
        public const string DEFAULT_ROOM = "main";
        #endregion

        #region string NO_ROOM
        /// <summary>
        /// The name of the "no room" in the game adventure.
        /// </summary>
        public const string NO_ROOM = "[nil]";
        #endregion

        //********************************************************************************
        // Properties
        //********************************************************************************

        #region Dictionary<string, IAdventureValue> _variables
        /// <summary>
        /// The variables inside the game adventure.
        /// </summary>
        private Dictionary<string, IAdventureValue> _variables;
        #endregion

        #region IAdventureValue _defaultValue
        /// <summary>
        /// The default value when a variable is undefined.
        /// </summary>
        private IAdventureValue _defaultValue;

        /// <summary>
        /// Gets the default value when a variable is undefined.
        /// </summary>
        public IAdventureValue DefaultValue {
            get { return _defaultValue; }
        }
        #endregion

        #region LinkedList<AdventureObject> _inventory
        /// <summary>
        /// The inventory inside the game adventure.
        /// </summary>
        private LinkedList<AdventureObject> _inventory;
        #endregion

        #region AdventureRoom Room
        /// <summary>
        /// Gets the current room inside the game adventure.
        /// </summary>
        public AdventureRoom Room { get; private set; }
        #endregion

        #region string NextRoom
        /// <summary>
        /// Gets the name of the next room to go.
        /// </summary>
        public string NextRoom { get; private set; }
        #endregion

        #region string LastRoom
        /// <summary>
        /// Gets the name of the last room in the game adventure.
        /// </summary>
        public string LastRoom { get; set; }
        #endregion

        #region bool Finished
        /// <summary>
        /// Gets or sets the finished flag of the game adventure
        /// </summary>
        public bool Finished { get; set; }
        #endregion

        #region bool Reset
        /// <summary>
        /// Gets or sets the reset flag of the game adventure
        /// </summary>
        public bool Reset { get; set; }
        #endregion

        #region string DefaultAnswer
        /// <summary>
        /// Gets the default message when no action event is found.
        /// </summary>
        public string DefaultMessage { get; set; }
        #endregion

        #region bool RunAll
        /// <summary>
        /// Gets or sets the run all actions flag of the game adventure
        /// </summary>
        public bool RunAll { get; set; }
        #endregion

        //********************************************************************************
        // Constructors
        //********************************************************************************

        #region AdventureContext ()
        /// <summary>
        /// Constructs a new object.
        /// </summary>
        public AdventureContext () {
            _variables = new Dictionary<string, IAdventureValue>();
            _defaultValue = new AdventureIntegerValue();
            _inventory = new LinkedList<AdventureObject>();
            Room = null;
            NextRoom = null;
            LastRoom = NO_ROOM;
            Finished = false;
            Reset = false;
            DefaultMessage = null;
            RunAll = true;
        }
        #endregion

        //********************************************************************************
        // Methods
        //********************************************************************************

        #region IAdventureValue GetVariable (string)
        /// <summary>
        /// Gets the value from a variable.
        /// </summary>
        /// <param name="name">The name of the variable.</param>
        /// <returns>The value of the variable.</returns>
        public IAdventureValue GetVariable (string name) {
            if (_variables.ContainsKey(name)) {
                return _variables[name];
            } else {
                return _defaultValue;
            }
        }
        #endregion

        #region void SetVariable (string, IAdventureValue)
        /// <summary>
        /// Sets a value to a variable.
        /// </summary>
        /// <param name="name">The name of the variable.</param>
        /// <param name="value">The value of the variable.</param>
        public void SetVariable (string name, IAdventureValue value) {
            if (value != null) {
                _variables[name] = value;
            }
        }
        #endregion

        #region void ChangeRoom (string)
        /// <summary>
        /// Changes the room of the game adventure.
        /// </summary>
        /// <param name="name">The name of the room.</param>
        public void ChangeRoom (string name) {
            var game = EngineBehaviour.Instance.Game;
            var room = game.GetRoom(name.ToLower());
            if (room != null) {
                if (Room != null) {
                    LastRoom = Room.Name;
                    execute(Room.GetOnExitEvents());
                } else {
                    LastRoom = NO_ROOM;
                }
                Room = room;
                if (Room != null) {
                    execute(Room.GetOnEnterEvents());
                }
            }
        }
        #endregion

        #region void execute (IEnumerable<AdventureEvent>)
        /// <summary>
        /// Executes a collection of game events.
        /// </summary>
        /// <param name="victims">The collection of events.</param>
        private void execute (IEnumerable<AdventureEvent> victims) {
            if (RunAll) {
                foreach (var item in victims) {
                    foreach (var expr in item.Expressions) {
                        expr.Execute();
                    }
                }
            } else if (victims.Count() > 0) {
                var item = victims.First();
                foreach (var expr in item.Expressions) {
                    expr.Execute();
                }
            }
        }
        #endregion

        #region void SetNextRoom (string)
        /// <summary>
        /// Sets the name of the next room to go.
        /// </summary>
        /// <param name="name">The name of the next room.</param>
        public void SetNextRoom (string name) {
            var game = EngineBehaviour.Instance.Game;
            var room = game.GetRoom(name.ToLower());
            if (room != null) {
                NextRoom = name;
            } else {
                NextRoom = null;
            }
        }
        #endregion

        #region void UpdateChangeRoom ()
        /// <summary>
        /// Goes to the next room if needed.
        /// </summary>
        public bool UpdateChangeRoom () {
            if (NextRoom != null) {
                ChangeRoom(NextRoom);
                NextRoom = null;
                return true;
            } else {
                return false;
            }
        }
        #endregion

        #region void ExecuteAction (string)
        /// <summary>
        /// Executes an action event in the game.
        /// </summary>
        /// <param name="line">The readed line.</param>
        public void ExecuteAction (string line) {
            if (Room != null) {
                bool noAnswer = true;
                Action<AdventureEntity> checkAndExecute = (item) => {
                    var events = item.GetOnActionEvents(line);
                    if (events != null && events.Count() > 0) {
                        execute(events);
                        noAnswer = false;
                    }
                };
                foreach (var item in _inventory.ToArray()) {
                    checkAndExecute(item);
                    if (!noAnswer) break;
                }
                if (noAnswer) {
                    checkAndExecute(Room);
                }
                if (noAnswer && DefaultMessage != null) {
                    EngineBehaviour.Instance.Output.WriteLine(DefaultMessage);
                }
            }
        }
        #endregion

        #region void AddToInventory (string)
        /// <summary>
        /// Adds an object to the inventory.
        /// </summary>
        /// <param name="name">The name of the object.</param>
        public void AddToInventory (string name) {
            var game = EngineBehaviour.Instance.Game;
            var item = game.GetObject(name.ToLower());
            if (item != null) {
                _inventory.AddLast(item);
            }
        }
        #endregion

        #region void RemoveFromInventory (string)
        /// <summary>
        /// Removes an object from the inventory.
        /// </summary>
        /// <param name="name">The name of the object.</param>
        public void RemoveFromInventory (string name) {
            var item = _inventory.Where(x => x.Name == name).FirstOrDefault();
            if (item != null) {
                _inventory.Remove(item);
            }
        }
        #endregion

        #region bool IsInInventory (string)
        /// <summary>
        /// Checks if an object is in the inventory.
        /// </summary>
        /// <param name="name">The name of the object.</param>
        /// <returns>Returns true if the object is found.</returns>
        public bool HasInInventory (string name) {
            return _inventory.Where(x => x.Name == name).FirstOrDefault() != null;
        }
        #endregion
    }
}
