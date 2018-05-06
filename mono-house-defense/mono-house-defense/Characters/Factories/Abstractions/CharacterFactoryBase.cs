using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mono_house_defense.Characters.Abstractions;
using Microsoft.Xna.Framework;

namespace mono_house_defense.Characters.Factories.Astractions
{
    public abstract class CharacterFactoryBase
    {
        public static List<T> Create<T>(int numberOfCharacters, float millisecondsPerFrame, Vector2 initialPosition)
        {
            List<T> charactersList = new List<T>();

            Random random = new Random();

            for (int i = 0; i < numberOfCharacters; i++)
            {
                charactersList.Add((T)Activator.CreateInstance(typeof(T), millisecondsPerFrame, initialPosition, random.Next(20, 25)));
            }

            return charactersList;
        }
    }
}
