using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackBracketImplementation
{
    class StackBracketImplementation
    {
        char[] charArr;
        public int top = -1;
        public StackBracketImplementation(int len)
        {
            charArr = new char[len];
        }

        public bool push(char item)
        {
            if (top == charArr.Length)
            {
                Console.WriteLine("Stack full");
                return false;
            }
                
            else
            {
                charArr[++top] = item;
                return true;
            }
        }

        public char pop()
        {
            if (top == -1)
            {
                Console.WriteLine("Stack underflow");
                return 'a';
            }
                
            else
            {
                char stackTop = charArr[top];
                --top;
                return stackTop;
            }
        }

        static void Main(string[] args)
        {
            string bracketString = "{[{]()}";
            char[] ca = new char[bracketString.Length];
            StackBracketImplementation s = new StackBracketImplementation(bracketString.Length);
            for (int i = 0; i < bracketString.Length; i++)
            {
                ca[i] = bracketString[i];
            }

            char lastInsertedChar;
            bool isMatching = true;
            bool isStackFull = false;
            foreach(char c in ca)
            {
                if(!isMatching || !isStackFull)
                {
                    Console.WriteLine("Brackets not matching");
                    break;
                }
                if (c == '(' || c == '{' || c == '[')
                {
                    lastInsertedChar = c;
                    isStackFull = s.push(c);
                }
                else
                {
                    char popedChar = s.pop();
                    if((c == ')' && popedChar == '(') || (c == '}' && popedChar == '{') || (c == ']' && popedChar == '['))
                    {
                        isMatching = true;
                    }
                    else
                    {
                        isMatching = false;
                    }
                }
            }

            if(isMatching)
            {
                Console.WriteLine("Brackets matching");
            }
        }
    }
}
