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
using System.Linq;

namespace Engine.Data {
    //|==================================================================================|
    //|                                 IAdventureEntity                                 |
    //|==================================================================================|

    #region interface IAdventureEntity
    /// <summary>
    /// This interface represents an adventure entity.
    /// </summary>
    public interface IAdventureEntity {
        //********************************************************************************
        // Properties
        //********************************************************************************

        #region string Name
        /// <summary>
        /// Gets the name of the entity.
        /// </summary>
        string Name { get; }
        #endregion

        #region IEnumerable<AdventureEvent> Events
        /// <summary>
        /// Gets the events of the entity.
        /// </summary>
        IEnumerable<AdventureEvent> Events { get; }
        #endregion
    }
    #endregion

    //|==================================================================================|
    //|                                 AdventureEntity                                  |
    //|==================================================================================|

    #region class AdventureEntity
    /// <summary>
    /// This class represents an entity in the game adventure.
    /// </summary>
    public abstract class AdventureEntity : IAdventureEntity {
        //********************************************************************************
        // Properties
        //********************************************************************************

        #region string Name
        /// <summary>
        /// Gets the name of the entity.
        /// </summary>
        public string Name { get; private set; }
        #endregion

        #region IEnumerable<AdventureEvent> Events
        /// <summary>
        /// Gets the events of the entity.
        /// </summary>
        public IEnumerable<AdventureEvent> Events { get; private set; }
        #endregion

        //********************************************************************************
        // Constructors
        //********************************************************************************

        #region AdventureEntity ()
        /// <summary>
        /// Constructs a new object.
        /// </summary>
        /// <param name="name">The name of the entity.</param>
        /// <param name="events">The events of the entity.</param>
        public AdventureEntity (string name, IEnumerable<AdventureEvent> events) {
            Name = name;
            Events = events;
        }
        #endregion

        //********************************************************************************
        // Methods
        //********************************************************************************

        #region IEnumerable<AdventureEvent> GetOnEnterEvents ()
        /// <summary>
        /// Gets all the on enter events.
        /// </summary>
        /// <returns>The selected events.</returns>
        public IEnumerable<AdventureEvent> GetOnEnterEvents () {
            return Events.Where(item => item.Type == AdventureEventType.OnEnter);
        }
        #endregion

        #region IEnumerable<AdventureEvent> GetOnExitEvents ()
        /// <summary>
        /// Gets all the on exit events.
        /// </summary>
        /// <returns>The selected events.</returns>
        public IEnumerable<AdventureEvent> GetOnExitEvents () {
            return Events.Where(item => item.Type == AdventureEventType.OnExit);
        }
        #endregion

        #region IEnumerable<AdventureEvent> GetOnActionEvents ()
        /// <summary>
        /// Gets all the on action events.
        /// </summary>
        /// <param name="action">The action to find.</param>
        /// <returns>The selected events.</returns>
        public IEnumerable<AdventureEvent> GetOnActionEvents (string action) {
            return Events.Where(item => item.Type == AdventureEventType.OnAction
                && item.ActionMatches(action));
        }
        #endregion
    }
    #endregion

    //|==================================================================================|
    //|                                  AdventureRoom                                   |
    //|==================================================================================|

    #region class AdventureRoom
    /// <summary>
    /// This class represents a room in the game adventure.
    /// </summary>
    public class AdventureRoom : AdventureEntity {
        //********************************************************************************
        // Constructors
        //********************************************************************************

        #region AdventureRoom (string, IEnumerable<AdventureEvent>)
        /// <summary>
        /// Constructs a new object.
        /// </summary>
        /// <param name="name">The name of the room.</param>
        /// <param name="events">The events of the room.</param>
        public AdventureRoom (string name, IEnumerable<AdventureEvent> events)
            : base(name, events) { }
        #endregion
    }
    #endregion

    //|==================================================================================|
    //|                                 AdventureObject                                  |
    //|==================================================================================|

    #region class AdventureObject
    /// <summary>
    /// This class represents an object in the game adventure.
    /// </summary>
    public class AdventureObject : AdventureEntity {
        //********************************************************************************
        // Constructors
        //********************************************************************************

        #region AdventureObject (string, IEnumerable<AdventureEvent>)
        /// <summary>
        /// Constructs a new object.
        /// </summary>
        /// <param name="name">The name of the object.</param>
        /// <param name="events">The events of the object.</param>
        public AdventureObject (string name, IEnumerable<AdventureEvent> events)
            : base(name, events) { }
        #endregion
    }
    #endregion
}
