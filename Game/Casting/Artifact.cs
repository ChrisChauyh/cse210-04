using System.Collections.Generic;
using System.IO;
using System;

namespace unit04_greed.Game.Casting
{
    // TODO: Implement the Artifact class here

    // 1) Add the class declaration. Use the following class comment. Make sure you
    //    inherit from the Actor class.

        /// <summary>
        /// <para>An item of cultural or historical interest.</para>
        /// <para>
        /// The responsibility of an Artifact is to provide a message about itself.
        /// </para>
        /// </summary>
        public class Artifact:Actor{
            private int score = 0;

            public Artifact(){

            }
       
            public int GetScore(){
                
                return score;

            }
        

    // 4) Create the SetMessage(string message) method. Use the following method comment.
        
        /// <summary>
        /// Sets the artifact's message to the given value.
        /// </summary>
        /// <param name="message">The given message.</param>
            public void SetScore(int score){
                this.score = score;

            }
        }
}