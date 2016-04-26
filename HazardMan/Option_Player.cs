using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HazardMan
{
    class Option_Player
    {
        public ConsoleKey up;
        public ConsoleKey left;
        public ConsoleKey right;
        private string name;
        private string UUID;
        private ConsoleColor color;

        public Option_Player(string name, ConsoleKey up, ConsoleKey left, ConsoleKey right, ConsoleColor color)
        {
            this.name = name;
            this.UUID = name.ToUpper();
            this.color = color;

            this.up = up;
            this.left = left;
            this.right = right;
        }

        public ConsoleColor getColor()
        {
            return color;
        }

        public string getUUID()
        {
            return UUID;
        }

        public string getName()
        {
            return name;
        }

        public void setName(string name)
        {
            this.name = name;
            this.UUID = name.ToUpper();
        }

        public ConsoleKey getUpKey()
        {
            return up;
        }

        public ConsoleKey getLeftKey()
        {
            return left;
        }

        public ConsoleKey getRightKey()
        {
            return right;
        }

        public void setConsoleKeys(ConsoleKey up, ConsoleKey left, ConsoleKey right) 
        {
            this.up = up;
            this.left = left;
            this.right = right;
        }

    }
}
