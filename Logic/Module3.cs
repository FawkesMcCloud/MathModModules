using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MathModMudules.Logic
{
    class Module3 : BasicVM
    {
        private TransportModul a = new TransportModul();

        private RelayCommand buttonFindFirst;
        private RelayCommand buttonFindOptimal;
        private List<MatrixTable> listShipping;
        private List<MatrixTable> listCost;
        private int cost;

        public Module3()
        {
            buttonFindFirst = new RelayCommand(a => CalculateFirst(), f => CanCalculate());
            buttonFindOptimal = new RelayCommand(a => CalculateOptimal(), f => CanCalculate());

            List<MatrixTable> initListShipment = new List<MatrixTable>();
            for (int i = 0; i < a.costMatrix.GetLength(0); i++)
            {
                initListShipment.Add(new MatrixTable(Shipment[i, 0], Shipment[i, 1], Shipment[i, 2], Shipment[i, 3], Shipment[i, 4]));
            }

            List<MatrixTable> initListCost = new List<MatrixTable>();
            for (int i = 0; i < a.shipmentMatrix.GetLength(0); i++)
            {
                initListCost.Add(new MatrixTable(CostMatrix[i, 0], CostMatrix[i, 1], CostMatrix[i, 2], CostMatrix[i, 3], CostMatrix[i, 4]));
            }

            ListCost = initListCost;
            ListShipping = initListShipment;

            cost = 0;
        }

        private void CalculateOptimal()
        {
            setCostMatrix();

            a.calcualte2();
            for (int i = 0; i < a.shipmentMatrix.GetLength(0); i++)
                for (int j = 0; j < a.shipmentMatrix.GetLength(1); j++)
                    Shipment[i, j] = a.shipmentMatrix[i, j];

            List<MatrixTable> newlist = new List<MatrixTable>();
            for (int i = 0; i < a.costMatrix.GetLength(0); i++)
            {
                newlist.Add(new MatrixTable(Shipment[i, 0], Shipment[i, 1], Shipment[i, 2], Shipment[i, 3], Shipment[i, 4]));
            }
            ListShipping = newlist;
            Cost = a.cost;
        }

        private void setCostMatrix()
        {
            foreach (var item in ListCost.Select((value, i) => new { i, value }))
            {
                CostMatrix[item.i, 0] = item.value.Consumer1;
                CostMatrix[item.i, 1] = item.value.Consumer2;
                CostMatrix[item.i, 2] = item.value.Consumer3;
                CostMatrix[item.i, 3] = item.value.Consumer4;
                CostMatrix[item.i, 4] = item.value.Consumer5;
            }
        }

        private void CalculateFirst()
        {
            setCostMatrix();

            Shipment = a.calculate1();
            List<MatrixTable> newlist = new List<MatrixTable>();
            for (int i = 0; i < a.shipmentMatrix.GetLength(0); i++)
            {
                newlist.Add(new MatrixTable(Shipment[i, 0], Shipment[i, 1], Shipment[i, 2], Shipment[i, 3], Shipment[i, 4]));
            }
            ListShipping = newlist;
            Cost = a.cost;
        }

        public RelayCommand ButtonFindFirst { get => buttonFindFirst; set { SetField(ref buttonFindFirst, value); } }
        public RelayCommand ButtonFindOptimal { get => buttonFindOptimal; set { SetField(ref buttonFindOptimal, value); } }

        private bool CanCalculate()
        {
            return a.consumers.Sum() == a.manufacturers.Sum();
        }

        public int Cost { get => cost; set => SetField(ref cost, value); }
        public int[,] Shipment { get => a.shipmentMatrix; set => SetField(ref a.shipmentMatrix, value); }

        public int[,] CostMatrix { get => a.costMatrix; set => SetField(ref a.costMatrix, value); }
        public int Manufacturers0 { get => a.manufacturers[0]; set { SetField(ref a.manufacturers[0], value); OnPropertyChanged("TotalManufcaturer"); } }
        public int Manufacturers1 { get => a.manufacturers[1]; set { SetField(ref a.manufacturers[1], value); OnPropertyChanged("TotalManufcaturer"); } }
        public int Manufacturers2 { get => a.manufacturers[2]; set { SetField(ref a.manufacturers[2], value); OnPropertyChanged("TotalManufcaturer"); } }
        public int Manufacturers3 { get => a.manufacturers[3]; set { SetField(ref a.manufacturers[3], value); OnPropertyChanged("TotalManufcaturer"); } }
        public int Consumers0 { get => a.consumers[0]; set { SetField(ref a.consumers[0], value); OnPropertyChanged("TotalConsumers"); } }
        public int Consumers1 { get => a.consumers[1]; set { SetField(ref a.consumers[1], value); OnPropertyChanged("TotalConsumers"); } }
        public int Consumers2 { get => a.consumers[2]; set { SetField(ref a.consumers[2], value); OnPropertyChanged("TotalConsumers"); } }
        public int Consumers3 { get => a.consumers[3]; set { SetField(ref a.consumers[3], value); OnPropertyChanged("TotalConsumers"); } }
        public int Consumers4 { get => a.consumers[4]; set { SetField(ref a.consumers[4], value); OnPropertyChanged("TotalConsumers"); } }

        public List<MatrixTable> ListShipping { get => listShipping; set => SetField(ref listShipping, value); }
        public List<MatrixTable> ListCost { get => listCost; set => SetField(ref listCost, value); }
        public int TotalConsumer { get => Consumers0 + Consumers1 + Consumers2 + Consumers3 + Consumers4; }
        public int TotalManufcaturer { get => Manufacturers0 + Manufacturers1 + Manufacturers2 + Manufacturers3;}

        public class MatrixTable
        {

            public MatrixTable(int consumer1, int consumer2, int consumer3, int consumer4, int consumer5)
            {
                this.Consumer1 = consumer1;
                this.Consumer2 = consumer2;
                this.Consumer3 = consumer3;
                this.Consumer4 = consumer4;
                this.Consumer5 = consumer5;
            }

            public int Consumer1 { get; set; }
            public int Consumer2 { get; set; }
            public int Consumer3 { get; set; }
            public int Consumer4 { get; set; }
            public int Consumer5 { get; set; }
        }

    }

    class TransportModul
    {

        public int[,] costMatrix = new int[,] {
            {   4, 4,  3,   5,   4},
            {   4, 3,  3,   4,   4},
            {   4, 3,  3,   4,   4},
            {   5, 3,  3,   5,   4},
        };
        public int[] manufacturers = new int[] { 246, 186, 196, 197 };
        public int[] consumers = new int[] { 136, 171, 71, 261, 186 };
        public int index_count_warehouse = 4;
        public int index_count_consumer = 5;

        public int[,] shipmentMatrix;
        public int cost = 0;

        public TransportModul()
        {
            index_count_consumer = costMatrix.GetLength(1);
            index_count_warehouse = costMatrix.GetLength(0);

            shipmentMatrix = new int[index_count_warehouse, index_count_consumer];
        }

        public int[,] calculate1()
        {
            return StartFillMatrix(manufacturers, consumers);
        }
        public void calcualte2()
        {
            cost = SearchPotential(ref shipmentMatrix);
        }

        public int[,] StartFillMatrix(int[] mP, int[] mC)//северо-западное заполнение матрицы
        {
            cost = 0;
            int[] massProduction = mP.ToArray();
            int[] massConsumer = mC.ToArray();
            int[,] matrix = new int[massProduction.Length, massConsumer.Length];
            for (int i = 0; i < massProduction.Length; i++)
            {
                for (int j = 0; j < massConsumer.Length; j++)
                {
                    if (massProduction[i] != 0 && massConsumer[j] != 0)
                    {
                        int min = Math.Min(massProduction[i], massConsumer[j]);
                        massProduction[i] -= min;
                        massConsumer[j] -= min;
                        matrix[i, j] += min;
                    }
                }
            }

            for (int i = 0; i < massProduction.Length; i++)
                for (int j = 0; j < massConsumer.Length; j++)
                {
                    cost += costMatrix[i, j] * matrix[i, j];
                }


            return matrix;
        }

        public int SearchPotential(ref int[,] matrix)//работа с потенциалами
        {
            int coordU = 1;
            int coordV = 1;
            int income = 0;

            while (coordU >= 0 && coordV >= 0)
            {
                points.Clear();
                var productPotentials = new int?[index_count_warehouse];
                productPotentials[0] = 0;
                var consumerPotentials = new int?[index_count_consumer];
                for (int i = 0; i < productPotentials.Length; i++)//поиск потенциалов производ. и потреб.
                {
                    for (int j = 0; j < consumerPotentials.Length; j++)
                    {
                        if (matrix[i, j] != 0)
                        {
                            points.Add(new Point(i, j));
                            if (productPotentials[i] != null && consumerPotentials[j] == null)
                                consumerPotentials[j] = costMatrix[i, j] - productPotentials[i];

                            if (consumerPotentials[j] != null && productPotentials[i] == null)
                                productPotentials[i] = costMatrix[i, j] - consumerPotentials[j];

                        }
                    }
                }
                coordU = -1;
                coordV = -1;
                int violation = 0;
                for (int i = 0; i < matrix.GetLength(0); i++)//нахождение максимального +
                {
                    for (int j = 0; j < matrix.GetLength(1); j++)
                    {
                        if (productPotentials[i] + consumerPotentials[j] > costMatrix[i, j] && matrix[i, j] == 0)
                        {
                            if (violation > (int)(costMatrix[i, j] - productPotentials[i] - consumerPotentials[j]))
                            {
                                violation = (int)(costMatrix[i, j] - productPotentials[i] - consumerPotentials[j]);
                                coordU = i;
                                coordV = j;
                            }
                        }
                    }
                }
                //violation = Math.Abs(violation);
                if (coordU >= 0 && coordV >= 0)
                {
                    Point startPoint = new Point(coordU, coordV);
                    startPoint.sign = 1;
                    points.Add(startPoint);

                    //<-----Ваневская\Ванешная\Ванчевская\Ивэновская\И-Van-овская Функция

                    bool left = true;
                    while (left)
                    {
                        var rows = points.FindAll(p => points.Count(g => g.i == p.i) == 1);
                        var cols = points.FindAll(p => points.Count(g => g.j == p.j) == 1);
                        points.RemoveAll(p => rows.Contains(p) || cols.Contains(p));
                        left = rows.Count != 0 || cols.Count != 0;
                    }

                    for (int i = points.Count - 1; i >= 0; i--)//расставляет знаки
                    {
                        foreach (Point point in points)
                        {
                            if ((points[i].i == point.i || points[i].j == point.j) && point.sign == 0)
                                point.sign = -points[i].sign;
                        }
                    }
                    int minShipping = 999999999;
                    foreach (Point point in points)//ищет минимальную перевозку из минусов
                    {
                        if (minShipping > matrix[point.i, point.j] && point.sign < 0)
                            minShipping = matrix[point.i, point.j];
                    }

                    foreach (Point point in points)//изменяет перевозки
                    {
                        if (point.sign > 0)
                            matrix[point.i, point.j] += minShipping;
                        else
                            matrix[point.i, point.j] -= minShipping;
                    }
                }
            }

            for (int i = 0; i < matrix.GetLength(0); i++)//сумма затрат
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    income += matrix[i, j] * costMatrix[i, j];
                }
            }
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                    Console.Write($"{matrix[i, j]} ");
                Console.Write("\n");
            }
            return income;
        }
        static List<Point> points = new List<Point>();//класс точки
        public class Point
        {
            public int i;
            public int j;
            public int sign;
            public Point(int a, int b)
            {
                i = a;
                j = b;
                sign = 0;
            }
        }

        public int[] SetMassProduction(int a, int b, int c)//массив производителей
        {
            int[] massProduction = new int[] { a, b, c };
            return massProduction;
        }
        public int[] SetMassProduction(int a, int b, int c, int d)//массив производителей
        {
            int[] massProduction = new int[] { a, b, c, d };
            return massProduction;
        }
        public int[] SetMassConsumer(int a, int b, int c, int d, int e)//массив потребителей
        {
            int[] massConsumer = new int[] { a, b, c, d, e };
            return massConsumer;
        }

    }

}
public static class ArrayExt
{
    public static T[] GetRow<T>(this T[,] array, int row)
    {
        if (!typeof(T).IsPrimitive)
            throw new InvalidOperationException("Not supported for managed types.");

        if (array == null)
            throw new ArgumentNullException("array");

        int cols = array.GetUpperBound(1) + 1;
        T[] result = new T[cols];

        int size;

        if (typeof(T) == typeof(bool))
            size = 1;
        else if (typeof(T) == typeof(char))
            size = 2;
        else
            size = Marshal.SizeOf<T>();

        Buffer.BlockCopy(array, row * cols * size, result, 0, cols * size);

        return result;
    }
}


