using System;

/******************************************************
    雷塞运动控制卡PVTS运动(两轴)，实现线段上各点的计算
******************************************************/

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            MoveL(1, 1, 1, 1.1);
        }

        public static bool MoveL(double x1, double y1, double x2, double y2)
        {
            var distance = Math.Sqrt((x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2));

            ushort ConnectNo = 0;
            double dis = 1;
            ushort AxisX = 0;
            ushort AxisY = 1;
            double _time = 1;

            var ret = 0;

            uint count = distance % dis == 0 ? (uint)(distance / dis) + 1 : (uint)(distance / dis) + 2;

            double[] PosX = new double[count];
            double[] PosY = new double[count];
            double[] TimeX = new double[count];
            double[] TimeY = new double[count];
            ushort[] AxisList = new ushort[2];
            AxisList[0] = AxisX;
            AxisList[1] = AxisY;

            if (x1 == x2 && y1 == y2)
            {
                return false;
            }

            if (x1 == x2 && y1 != y2)
            {
                if (y1 < y2)
                {
                    for (int i = 0; i < count - 1; i++)
                    {
                        PosX[i] = x1;
                        PosY[i] = y1 + dis * i;
                    }

                    PosX[count - 1] = x2;
                    PosY[count - 1] = y2;
                }
                else
                {
                    for (int i = 0; i < count - 1; i++)
                    {
                        PosX[i] = x1;
                        PosY[i] = y1 - dis * i;
                    }

                    PosX[count - 1] = x2;
                    PosY[count - 1] = y2;
                }
            }

            if (x1 != x2 && y1 == y2)
            {
                if (x1 < x2)
                {
                    for (int i = 0; i < count - 1; i++)
                    {
                        PosX[i] = x1 + dis * i;
                        PosY[i] = y1;
                    }

                    PosX[count - 1] = x2;
                    PosY[count - 1] = y2;
                }
                else
                {
                    for (int i = 0; i < count - 1; i++)
                    {
                        PosX[i] = x1 - dis * i;
                        PosY[i] = y1;
                    }

                    PosX[count - 1] = x2;
                    PosY[count - 1] = y2;
                }
            }

            if (x1 != x2 && y1 != y2)
            {
                for (int i = 0; i < count - 1; i++)
                {
                    PosX[i] = x1 + (x2 - x1) / (count - 1) * i;
                    PosY[i] = y1 + (y2 - y1) / (count - 1) * i;
                }

                PosX[count - 1] = x2;
                PosY[count - 1] = y2;
            }

            for (int i = 0; i < count; i++)
            {
                TimeX[i] = i * _time;
                TimeY[i] = i * _time;
            }

            for (int i = 0; i < count; i++)
            {
                Console.Write("(" + PosX[i] + ", ");
                Console.WriteLine(PosY[i] + ")");
            }

            ret = LTSMC.smc_pvts_table_unit(ConnectNo, AxisX, count, TimeX, PosX, 0, 0);
            ret = LTSMC.smc_pvts_table_unit(ConnectNo, AxisY, count, TimeY, PosY, 0, 0);
            ret = LTSMC.smc_pvt_move(ConnectNo, 2, AxisList);

            return ret == 0 ? true : false;
        }
    }
}
