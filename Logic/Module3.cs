using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathModMudules.Logic
{
    class Module3 : BasicVM
    {   
        // Тут указывается количество товара (потребность и наличие)
        // Фактически производителей всего 4, 5ый на случай не сбалансированной задачи
        private int[] manufacturers = new int[5] { 246, 186,196, 197, 0};
        // Фактических потребителей всего 5, 6ой на случай не сбалансированной задачи
        private int[] consumers = new int[6] { 136, 171, 71, 261, 186, 0 };

        // Матрица стоимости доставки и матрица планов доставки
        private float[,] costMatrix = new float[5, 6] { 
            {   4.20f, 4.00f,  3.35f,   5.00f,   4.65f,  0.00f },
            {   4.00f, 3.85f,  3.50f,   4.90f,   4.55f,  0.00f },
            {   4.75f, 3.50f,  3.40f,   4.50f,   4.40f,  0.00f },
            {   5.00f, 3.00f,  3.10f,   5.10f,   4.40f,  0.00f },
            {   0.00f, 0.00f,  0.00f,   0.00f,   0.00f,  0.00f }
        };
        private int[,] shippingMatrix = new int[5, 6];

        #region CustomerProperties
        //Продсиб
        public int ConsumerOne { get => consumers[0]; set => SetField(ref consumers[0], value); }
        //Добрянка
        public int ConsumerTwo { get => consumers[1]; set => SetField(ref consumers[1], value); }
        //Ашан
        public int ConsumerThree { get => consumers[2]; set => SetField(ref consumers[2], value); }
        //ФСИН России
        public int ConsumerFour { get => consumers[3]; set => SetField(ref consumers[3], value); }
        //Ярче
        public int ConsumerFive { get => consumers[4]; set => SetField(ref consumers[4], value); }
        //При решении не сбалансированной транспортной задачи
        public int ConsumerUnbalanced { get => consumers[0]; set => SetField(ref consumers[0], value); }
        #endregion
        #region Manufacturers
        public int ManufacturerOne { get => manufacturers[0]; set => SetField(ref manufacturers[0], value); }
        public int ManufacturerTwo { get => manufacturers[0]; set => SetField(ref manufacturers[0], value); }
        public int ManufacturerThree { get => manufacturers[0]; set => SetField(ref manufacturers[0], value); }
        public int ManufacturerFour { get => manufacturers[0]; set => SetField(ref manufacturers[0], value); }
        public int ManufacturerUnbalanced { get => manufacturers[0]; set => SetField(ref manufacturers[0], value); }
        #endregion

        public void calculate()
        {
            int maxI = 4, maxJ = 5;
            int sumConsumers = consumers.Sum();
            int sumManufacturers = manufacturers.Sum();

            int[,] tempMatrix = new int[5, 6];

            //Потребность больше производства вводим фиктивного поставщика
            if (sumConsumers - sumManufacturers > 0)
            {
                maxI += 1;
                manufacturers[4] = sumConsumers - sumManufacturers;
            }
            //Потребность меньше производства вводим фиктивного покупателся
            if (sumConsumers - sumManufacturers < 0)
            {
                maxJ += 1;
                consumers[5] = sumManufacturers - sumConsumers;
            }
            int[] tempCons = consumers.ToArray();
            int[] tempManuf = manufacturers.ToArray();

            // Подсчет начального плана методом северо-западного угла
            for(int i = 0; i < maxI; i++)
                for(int j = 0; j < maxJ; j++)
                    if (tempCons[j] != 0 && tempManuf[i] != 0)
                    {
                        int min = Math.Min(tempCons[j], tempManuf[i]);
                        tempCons[j] -= min;
                        tempManuf[i] -= min;
                        tempMatrix[i, j] += min;
                    }

            bool optimal = false;
            while (!optimal)
            {
                List<Point> points = new List<Point>();
                var potentialC = new float?[maxJ];
                var potentialM = new float?[maxI];
                potentialM[0] = 0.0f;

                for (int i = 0; i < potentialM.Length; i++)
                    for (int j = 0; j < potentialC.Length; j++)
                        if (tempMatrix[i, j] != 0)
                        {
                            if (potentialM[i] != null && potentialC[j] == null)
                                potentialC[j] = costMatrix[i, j] - potentialM[i];

                            if (potentialC[j] != null && potentialM[i] == null)
                                potentialM[i] = costMatrix[i, j] - potentialC[j];
                            points.Add(new Point(i, j));
                        }
                // Поиск максимального нарушения (по модулю)
                int minDeltaI = -1, minDeltaJ = -1;
                float minDelta = 0.0f;
                for (int i = 0; i < potentialM.Length; i++)
                    for (int j = 0; j < potentialC.Length; j++)
                    {
                        float delta = (float) (costMatrix[i, j] - (potentialC[j] + potentialM[i]) );
                        if(minDelta > delta)
                        {
                            minDelta = delta;
                            minDeltaI = i;
                            minDeltaJ = j;
                        }
                    }
                minDelta = Math.Abs(minDelta);

                // СМОТРИ ПО СТОЛБЦУ И СТРОКЕ ПАРЫ +-
                points.Add(new Point(minDeltaI, minDeltaJ));

                LinkedList<Point> cycle = new LinkedList<Point>();
                bool left = true;
                while(left){
                    var rows = points.FindAll(p => points.Count(g => g.i == p.i) == 1);
                    var cols = points.FindAll(p => points.Count(g => g.j == p.j) == 1);
                    points.RemoveAll(p => rows.Contains(p) || cols.Contains(p));
                    left = rows.Count != 0 || cols.Count != 0;
                }

            }

        }

        class Point
        {
            public int i, j;
            public bool sign;
            public Point(int i, int j)
            {
                this.i = i;
                this.j = j;
            }
        }
    }

    static public class Test
    {
        static Module3 testing = new Module3();
        static public void startTest()
        {
            testing.calculate();
        }
    }
}
