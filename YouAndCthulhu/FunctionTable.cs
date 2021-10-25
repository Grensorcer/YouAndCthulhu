using System;
using System.Collections.Generic;

namespace YouAndCthulhu
{
    public class FunctionTable
    {
        // List of Function objects
        // A FunctionTable is always sorted. It has elements, each corresponding
        // to a List<Function> containing Function of IdLetter A, B, C or D,
        // respectively contained as elements 0, 1, 2, 3 of the table. These
        // lists are sorted in increasing order of the IdNatural.
        protected List<Function>[] ftable;

        // Constructor
        public FunctionTable()
        {
            this.ftable = new List<Function>[4];
            for (int i = 0; i < 4; i++)
            {
                ftable[i] = new List<Function>();
            }
        }

        // Used in order to keep the table sorted at all time
        private int BinarySearch(List<Function> l, ulong idnum)
        {
            int b = 0;
            int e = l.Count;
            while (b < e)
            {
                int m = b + (e - b) / 2;
                if (idnum == l[m].Idnum)
                    return m;
                else if (idnum < l[m].Idnum)
                    e = m;
                else
                    b = m + 1;
            }

            return b;
        }

        // Adds a function at the right place in the table
        public void Add(Function f)
        {
            int index = f.Idchar - 'A';
            int i = BinarySearch(ftable[index], f.Idnum);
            if (i < ftable[index].Count && ftable[index][i].Idnum == f.Idnum)
                throw new Exception($"The function {f.Idnum}{f.Idchar}" +
                                    "already exists !");
            
            ftable[index].Insert(i, f);
        }

        // Search method, returns the right function.
        public Function Search(ulong idnum, char idchar)
        {
            int index = idchar - 'A';
            int i = BinarySearch(ftable[index], idnum);

            if (i < ftable[index].Count && ftable[index][i].Idnum == idnum)
                return ftable[index][i];
            else if (i > 0)
                return ftable[index][i - 1];
            else if (ftable[index].Count > 0)
                return ftable[index][ftable[index].Count - 1];
            else
                return null;
        }

        // Overload of Search function, searching for a function called by ].
        // May need some change, maybe as another method instead.
        public Function SearchRegister(int register, char idchar)
        {
            int index = idchar - 'A';
            if (register < 0)
                return ftable[index].Count > 0 ? ftable[index][ftable[index]
                .Count - 1] : null;
            
            int i = BinarySearch(ftable[index], (ulong) register);
            if (i < ftable[index].Count && ftable[index][i].Idnum == 
            (ulong) register)
                return ftable[index][i];
            else if (i > 0)
                return ftable[index][i - 1];
            else if (ftable[index].Count > 0)
                return ftable[index][ftable[index].Count - 1];
            else
                return null;
        }

        // Executes the ftable (by executing 0A)
        public void Execute()
        {
            if (ftable[0].Count == 0 || ftable[0][0].Idnum != 0)
                return; 
            ftable[0][0].Execute();
        }
    }
}