using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;

namespace mono_house_defense.DTO
{
    public class AimState
    {
        public AimState(MouseState mouseState, bool isEligibleToShot)
        {
            MouseState = mouseState;
            IsEligibleToShot = isEligibleToShot;
        }

        public MouseState MouseState { get; set; }
        public bool IsEligibleToShot { get; set; }
    }
}
