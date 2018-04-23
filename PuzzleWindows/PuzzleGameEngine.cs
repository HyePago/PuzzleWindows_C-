using System;

namespace PuzzleWindows
{
    internal class PuzzleGameEngine
    {
        int[] theViewIndices;
        private int puzzleSize = 16;

        public PuzzleGameEngine()
        {
            // image index 가져오기
            theViewIndices = new int[puzzleSize];

            for(int i =0; i<16; i++)
            {
                theViewIndices[i] = i;
            }

            // image index 섞기
            Random rnd = new Random();

            /*for(int i = 0; i<100; i++)
            {
                int n = rnd.Next(0, 16);
                int m = rnd.Next(0, 16);

                Swap(ref theViewIndices[n], ref theViewIndices[m]);
            }*/

            for(int i = 0; i<1000; i++)
            {
                int n = rnd.Next(0, 16);
                //int m = rnd.Next(0, 16);

                Change(n);
                //Swap1(n, m);
            }
        }

        // Swap.version 1
        private void Swap(ref int n, ref int m)
        {
            int temp = n;
            n = m;
            m = temp;
        }

        // Swap.version 2
        private void Swap1(int i1, int i2)
        {
            int temp = theViewIndices[i1];
            theViewIndices[i1] = theViewIndices[i2];
            theViewIndices[i2] = temp;
        }

        internal int getViewIndex(int index)
        {
            return theViewIndices[index];
        }

        internal void Change(int touchindex)
        {
            // 터시한 인덱스 상하좌우에 빈 인덱스가 있다면 교환
            if (GetEmptyIndex()/4 == touchindex/4 &&(GetEmptyIndex() == touchindex - 1 || GetEmptyIndex() == touchindex + 1) || GetEmptyIndex() == touchindex - 4 || GetEmptyIndex() == touchindex + 4) 
            {
                Swap1(touchindex, GetEmptyIndex());
            }
        }

        private int GetEmptyIndex()
        {
            for(int i=0; i<puzzleSize; i++)
            {
                if(theViewIndices[i] == puzzleSize - 1)
                {
                    return i;
                }
            }

            return -1;
        }

        internal bool isEnd()
        {
            for(int i =0; i<puzzleSize; i++)
            {
                if(theViewIndices[i] != i)
                {
                    return false;
                }
            }

            return true;
        }
    }
}