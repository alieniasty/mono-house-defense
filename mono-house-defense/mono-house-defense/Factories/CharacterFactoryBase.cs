using System;
using System.Collections.Generic;
using mono_house_defense.Characters;
using Microsoft.Xna.Framework;

namespace mono_house_defense.Factories
{
    public abstract class CharacterFactoryBase
    {
        public static List<T> Create<T>(int numberOfCharacters, float millisecondsPerFrame, Vector2 initialPosition)
        {
            List<T> charactersList = new List<T>();

            Random random = new Random();

            for (int i = 0; i < numberOfCharacters; i++)
            {
                if (typeof(T) != typeof(Explosion))
                {
                    initialPosition.X += (float)(random.NextDouble() * (-100 - 300) + -100);
                }
                charactersList.Add((T)Activator.CreateInstance(typeof(T), millisecondsPerFrame, initialPosition, random.Next(3, 7)));
            }

            return charactersList;
        }
    }
}
