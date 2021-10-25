using System;
using System.Diagnostics;

namespace YouAndCthulhu
{
    public class LexerParser
    {
        public enum CommandToken
        // Tokens used in parsing a function's command line.
              {
                  TOKEN_INCREMENT, //i
                  TOKEN_DECREMENT, //d
                  TOKEN_CALL,      //[
                  TOKEN_CALL_ACC,  //]
                  TOKEN_OUTPUT,    //o
                  TOKEN_INPUT,     //*
                  TOKEN_MOVE_OUT,  //E
                  TOKEN_MOVE_IN,    //e
                  TOKEN_DEFAULT    //error
              }

        public static Function LineToFunction(string s, FunctionTable ftable)
        {
            char[] delimiter = {' '};
            string[] parts = s.Trim().Split(delimiter);
            if (parts.Length != 2)
                throw new Exception("Line does not respect syntax");

            int index = 0;
            ulong idnum = GetIdNatural(parts[0], ref index);
            char idchar = GetIdLetter(parts[0], ref index);
            if (index != parts[0].Length)
                throw new Exception("Wrong line format !");
            Function f = new Function(idnum, idchar, parts[1], ftable);

            return f;
        }

        public static char GetIdLetter(string s, ref int index)
        // given a string and index pointing to the supposed place of an IdLetter, returns said letter.
        {
            char c = s[index++];
            if (c <= 'D' && c >= 'A')
                return c;

            throw new Exception("Letter is not between A and D");
        }

        public static UInt64 GetIdNatural(string s, ref int index)
        // given a string and index pointing to the supposed place of an Id's Natural, returns said natural.
        {
            string sub = "";
            while (Char.IsDigit(s[index]))
                sub += s[index++];

            if (sub.Length == 0)
                throw new Exception("Could not find a natural number");
            return Convert.ToUInt64(sub);
        }

        public static CommandToken CommandLexer(string s, int index)
        // returns CommandToken of the given index.
        {
            char c = s[index];
            switch (c)
            {
                case 'i':
                {
                    return CommandToken.TOKEN_INCREMENT;
                }
                case 'd':
                {
                    return CommandToken.TOKEN_DECREMENT;
                }
                case '*':
                {
                    return CommandToken.TOKEN_INPUT;
                }
                case 'o':
                {
                    return CommandToken.TOKEN_OUTPUT;
                }
                case '[':
                {
                    return CommandToken.TOKEN_CALL;
                }
                case ']':
                {
                    return CommandToken.TOKEN_CALL_ACC;
                }
                case 'E':
                {
                    return CommandToken.TOKEN_MOVE_OUT;
                }
                case 'e':
                {
                    return CommandToken.TOKEN_MOVE_IN;
                }
                default:
                {
                    return CommandToken.TOKEN_DEFAULT;
                }
            } 
        }
    }
}