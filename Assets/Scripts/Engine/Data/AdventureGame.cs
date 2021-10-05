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
using System.Collections.Generic;

namespace Engine.Data {
    /// <summary>
    /// This class represents a room in the game adventure.
    /// </summary>
    public class AdventureGame {
        //********************************************************************************
        // Properties
        //********************************************************************************

        #region Dictionary<string, AdventureRoom> _rooms
        /// <summary>
        /// The rooms in the adventure.
        /// </summary>
        private Dictionary<string, AdventureRoom> _rooms;
        #endregion

        #region Dictionary<string, AdventureObject> _objects
        /// <summary>
        /// The objects in the adventure.
        /// </summary>
        private Dictionary<string, AdventureObject> _objects;
        #endregion

        //********************************************************************************
        // Constructors
        //********************************************************************************

        #region AdventureGame ()
        /// <summary>
        /// Constructs a new object.
        /// </summary>
        public AdventureGame () {
            _rooms = new Dictionary<string, AdventureRoom>();
            _objects = new Dictionary<string, AdventureObject>();
        }
        #endregion

        //********************************************************************************
        // Methods (General)
        //********************************************************************************

        #region void AddFrom (AdventureGame)
        /// <summary>
        /// Adds the data from another adventure game object.
        /// </summary>
        /// <param name="victim">The object with the data to add.</param>
        public void AddFrom (AdventureGame victim) {
            foreach (var item in victim._rooms) {
                AddRoom(item.Value);
            }
            foreach (var item in victim._objects) {
                AddObject(item.Value);
            }
        }
        #endregion

        //********************************************************************************
        // Methods (Rooms)
        //********************************************************************************

        #region void AddRoom (AdventureRoom)
        /// <summary>
        /// Adds a room in the adventure.
        /// </summary>
        /// <param name="victim">The room to add.</param>
        public void AddRoom (AdventureRoom victim) {
            addEntity(_rooms, victim);
        }
        #endregion

        #region AdventureRoom GetRoom (string)
        /// <summary>
        /// Gets a room from the adventure.
        /// </summary>
        /// <param name="victim">The name of the room.</param>
        /// <returns>The room if founded.</returns>
        public AdventureRoom GetRoom (string victim) {
            return getEntity(_rooms, victim);
        }
        #endregion

        //********************************************************************************
        // Methods (Objects)
        //********************************************************************************

        #region void AddObject (AdventureObject)
        /// <summary>
        /// Adds an object in the adventure.
        /// </summary>
        /// <param name="victim">The object to add.</param>
        public void AddObject (AdventureObject victim) {
            addEntity(_objects, victim);
        }
        #endregion

        #region AdventureObject GetObject (string)
        /// <summary>
        /// Gets an object from the adventure.
        /// </summary>
        /// <param name="victim">The name of the object.</param>
        /// <returns>The object if founded.</returns>
        public AdventureObject GetObject (string victim) {
            return getEntity(_objects, victim);
        }
        #endregion

        //********************************************************************************
        // Methods (Entities)
        //********************************************************************************

        #region void addEntity<T> (Dictionary<string, T>, T)
        /// <summary>
        /// Adds an entity in the adventure.
        /// </summary>
        /// <typeparam name="T">The type of the entity.</typeparam>
        /// <param name="container">The container.</param>
        /// <param name="victim">The entity to add.</param>
        private void addEntity<T>(Dictionary<string, T> container, T victim)
            where T : IAdventureEntity {
            if (victim != null && !container.ContainsKey(victim.Name)) {
                container[victim.Name] = victim;
            }
        }
        #endregion

        #region AdventureObject GetObject (string)
        /// <summary>
        /// Gets an entity from the adventure.
        /// </summary>
        /// <typeparam name="T">The type of the entity.</typeparam>
        /// <param name="container">The container.</param>
        /// <param name="victim">The name of the entity.</param>
        /// <returns>The entity if founded.</returns>
        public T getEntity<T> (Dictionary<string, T> container, string victim) {
            if (container.ContainsKey(victim)) {
                return container[victim];
            } else {
                return default(T);
            }
        }
        #endregion
    }
}
