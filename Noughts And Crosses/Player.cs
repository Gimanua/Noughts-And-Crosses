namespace Noughts_And_Crosses
{
    using Noughts_And_Crosses.Actions;
    using Noughts_And_Crosses.Actions.Spells;
    using System.Collections.Generic;
    using static Noughts_And_Crosses.GameObjects.Mark;

    sealed class Player
    {
        public Player(MarkType mark)
        {
            Mark = mark;
            
            Actions = new List<Action>
            {
                new Meditate(),
                new Trap(),
                new Wait(),
                new Explosion(),
                new Destroyer(),
                new Armageddon()
            };
        }

        public MarkType Mark { get; }
        public List<Action> Actions { get; private set; }
        public uint Mana { get; set; } = 20;
    }
}
