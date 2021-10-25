using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading;

namespace YouAndCthulhu
{
    public class Function
    {
        // A Function contains an id, composed of a natural and a character,
        // a list of Actions, which are kinds of lambda expressions, and a
        // register.
        private UInt64 idnum;
        private char idchar;
        private List<Action> execution;
        private int accumulator;

        public Function(UInt64 idnum, char idchar, string commands, 
                        FunctionTable ftable)
        {
            this.idnum = idnum;
            this.idchar = idchar;
            this.accumulator = 0;
            this.execution = new List<Action>();
            
            ParseCommands(commands, ftable);
        }

        // Getters
        public ulong Idnum => idnum;
        public char Idchar => idchar;
        public int Accumulator => accumulator;

        // Methods implements Function Commands as seen on Esolang.
        public void Increment()
        {
            ++accumulator;
        }

        public void Decrement()
        {
            --accumulator;
        }

        public void MoveOut(Function f)
        {
            if (f != null)
                f.accumulator = accumulator;
        }

        public void MoveIn(Function f)
        {
            if (f != null)
                accumulator = f.accumulator;
        }

        public void Output()
        {
            Console.WriteLine(accumulator);
        }

        public void Input()
        {
            Console.Write("Waiting for integer input:");
            string input = Console.ReadLine();
            Console.WriteLine("");
            try
            {
                int iinput = Convert.ToInt32(input);
                accumulator = iinput;
            }
            catch
            {
                throw new ArgumentException("Expecting integer input");
            }
        }
        
        public void Execute()
        {
            foreach (Action act in execution)
                act();
        }
        
        private void ParseCommands(string commands, FunctionTable ftable)
        // Given the command string and the ftable, parses commands by creating
        // a lambda expression and adding it to the list every time.
        {
            int index = 0;
            
            while (index < commands.Length)
            {
                Action act;
                switch (LexerParser.CommandLexer(commands, index++))
                {
                    case LexerParser.CommandToken.TOKEN_INCREMENT:
                    {
                        act = () => { this.Increment(); };
                        break;
                    }
                    case LexerParser.CommandToken.TOKEN_DECREMENT:
                    {
                        act = () => { this.Decrement(); };
                        break;
                    }
                    case LexerParser.CommandToken.TOKEN_OUTPUT:
                    {
                        act = () => { this.Output(); };
                        break;
                    }
                    case LexerParser.CommandToken.TOKEN_INPUT:
                    {
                        act = () => { this.Input(); };
                        break;
                    }
                    case LexerParser.CommandToken.TOKEN_MOVE_IN:
                    {
                        ulong _idnum =
                            LexerParser.GetIdNatural(commands, ref index);
                        char _idchar =
                            LexerParser.GetIdLetter(commands, ref index);
                        act = () =>
                        {
                            Function f = ftable.Search(_idnum, _idchar);
                            this.MoveIn(f);
                        };
                        break;
                    }
                    case LexerParser.CommandToken.TOKEN_MOVE_OUT:
                    {
                        ulong _idnum =
                            LexerParser.GetIdNatural(commands, ref index);
                        char _idchar =
                            LexerParser.GetIdLetter(commands, ref index);
                        act = () =>
                        {
                            Function f = ftable.Search(_idnum, _idchar);
                            this.MoveOut(f);
                        };
                        break;
                    }
                    case LexerParser.CommandToken.TOKEN_CALL:
                    {
                        // calling a function simply consists in executing it...
                        ulong _idnum =
                            LexerParser.GetIdNatural(commands, ref index);
                        char _idchar =
                            LexerParser.GetIdLetter(commands, ref index);
                        act = () =>
                        {
                            Function f = ftable.Search(_idnum, _idchar);
                            if (f != null)
                                f.Execute();
                        };
                        break;
                    }
                    case LexerParser.CommandToken.TOKEN_CALL_ACC:
                    {
                        char _idchar =
                            LexerParser.GetIdLetter(commands, ref index);
                        act = () =>
                        {
                            Function f = ftable.SearchRegister(this.Accumulator,
                                                                _idchar);
                            if (f != null)
                                f.Execute();
                        };
                        break;
                    }
                    default:
                    {
                        throw new Exception("Unexpected token in parsing");
                    }
                }

                execution.Add(act);
            }
        }
        
    }
}