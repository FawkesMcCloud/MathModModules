using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathModMudules.Logic
{
    class Module2 : BasicVM
    {
        private RelayCommand relayCommand;

        private float Flour1, Flour2, EggPowder;

        private float AProductFlour1, AProductFlour2, AProductEggPowder;
        private float BProductFlour1, BProductFlour2, BProductEggPowder;

        private float SellPriceProdA, SellPriceProdB, TotalSellPrice;
        private int TotalA, TotalB;

        private float Flour1Left, Flour2Left, EggPowderLeft;

        private float[,] SimplexMat = new float[4, 4];
        private float[,] tempSimplexMat = new float[4, 4];

        #region Properties
        public RelayCommand RelayCommand { get => relayCommand; set { SetField(ref relayCommand, value); } }
        public float Flour11 { get => Flour1; set => SetField(ref Flour1, value); }
        public float Flour21 { get => Flour2; set => SetField(ref Flour2, value); }
        public float EggPowder1 { get => EggPowder; set => SetField(ref EggPowder, value); }
        public float AProductFlour11 { get => AProductFlour1; set => SetField(ref AProductFlour1, value); }
        public float AProductFlour21 { get => AProductFlour2; set => SetField(ref AProductFlour2, value); }
        public float AProductEggPowder1 { get => AProductEggPowder; set => SetField(ref AProductEggPowder, value); }
        public float BProductFlour11 { get => BProductFlour1; set => SetField(ref BProductFlour1, value); }
        public float BProductFlour21 { get => BProductFlour2; set => SetField(ref BProductFlour2, value); }
        public float BProductEggPowder1 { get => BProductEggPowder; set => SetField(ref BProductEggPowder, value); }
        public float SellPriceProdA1 { get => SellPriceProdA; set => SetField(ref SellPriceProdA, value); }
        public float SellPriceProdB1 { get => SellPriceProdB; set => SetField(ref SellPriceProdB, value); }
        public float TotalSellPrice1 { get => TotalSellPrice; set => SetField(ref TotalSellPrice, value); }
        public int TotalA1 { get => TotalA; set => SetField(ref TotalA, value); }
        public int TotalB1 { get => TotalB; set => SetField(ref TotalB, value); }
        public float Flour1Left1 { get => Flour1Left; set => SetField(ref Flour1Left, value); }
        public float Flour2Left1 { get => Flour2Left; set => SetField(ref Flour2Left, value); }
        public float EggPowderLeft1 { get => EggPowderLeft; set => SetField(ref EggPowderLeft, value); }
        #endregion

        private bool CanCalculate()
        {
            return Flour11 >= 0 && Flour21 >= 0 && EggPowder1 >= 0 &&
                    BProductFlour11 >= 0 && BProductFlour21 >= 0 && BProductEggPowder1 >= 0 &&
                    BProductFlour11 >= 0 && BProductFlour21 >= 0 && BProductEggPowder1 >= 0 &&
                    SellPriceProdA1 > 0 && SellPriceProdB1 > 0 &&
                    !(BProductFlour11 == 0 && BProductFlour21 == 0 && BProductEggPowder1 == 0) &&
                    !(AProductFlour11 == 0 && AProductFlour21 == 0 && AProductEggPowder1 == 0);
        }

        public Module2()
        {
            RelayCommand = new RelayCommand(action => Calculate(), check => CanCalculate());

            Flour11 = 100;
            Flour21 = 60;
            EggPowder1 = 180;

            AProductFlour11 = 2;
            AProductFlour21 = 1;
            AProductEggPowder1 = 1;

            BProductFlour11 = 1;
            BProductFlour21 = 1;
            BProductEggPowder1 = 4;

            SellPriceProdA1 = 30;
            SellPriceProdB1 = 20;

            TotalSellPrice1 = 0;
            TotalA1 = 0;
            TotalB1 = 0;
        }

        public void Calculate()
        {
            //Свободные пременные x1 первый столбец
            SimplexMat[0, 0] = AProductFlour11;
            SimplexMat[1, 0] = AProductFlour21;
            SimplexMat[2, 0] = AProductEggPowder1;
            //Свободные пременные x2 второй столбец
            SimplexMat[0, 1] = BProductFlour11;
            SimplexMat[1, 1] = BProductFlour21;
            SimplexMat[2, 1] = BProductEggPowder1;
            //Свободные члены третий столбец
            SimplexMat[0, 2] = Flour11;
            SimplexMat[1, 2] = Flour21;
            SimplexMat[2, 2] = EggPowder1;
            //F строка
            SimplexMat[3, 0] = -SellPriceProdA1;
            SimplexMat[3, 1] = -SellPriceProdB1;
            SimplexMat[3, 2] = 0;


            int Col = -1; //Выбранный столбец
            int Row = -1; //Выбранная строка

            int loopCounter = 0;
            int[] order = new int[5] { 1, 2, 3, 4, 5 };

            while (SimplexMat[3, 0] < 0.0f || SimplexMat[3, 1] < 0.0f)
            {
                loopCounter++;
                //Выбор столбца (абс минимум F-Строки)
                Col = SimplexMat[3, 0] <= SimplexMat[3, 1] ? 0 : 1;

                //Построение симлексного отношения
                for (int i = 0; i < 3; i++)
                    SimplexMat[i, 3] = SimplexMat[i, 2] / SimplexMat[i, Col];

                //Выбор строки (миниму симплекс)
                if (SimplexMat[0, 3] > SimplexMat[1, 3])
                    Row = SimplexMat[1, 3] > SimplexMat[2, 3] ? 2 : 1;
                else
                    Row = SimplexMat[0, 3] > SimplexMat[2, 3] ? 2 : 0;

                int temp = order[Row + 2];
                order[Row + 2] = order[Col];
                order[Col] = temp;

                //Пересчет
                //Все элементы
                for (int i = 0; i < 4; i++)
                    for (int j = 0; j < 3; j++)
                        tempSimplexMat[i, j] = (SimplexMat[i, j] * SimplexMat[Row, Col] - SimplexMat[Row, j] * SimplexMat[i, Col])
                                                                            / SimplexMat[Row, Col];

                //Разрешающий элемент
                tempSimplexMat[Row, Col] = 1 / SimplexMat[Row, Col];
                //Элементы столбца
                for (int i = 0; i < 4; i++)
                    if (i != Row)
                        tempSimplexMat[i, Col] = -(SimplexMat[i, Col] / SimplexMat[Row, Col]);
                //Элементы строки
                for (int j = 0; j < 3; j++)
                    if (j != Col)
                        tempSimplexMat[Row, j] = SimplexMat[Row, j] / SimplexMat[Row, Col];


                for (int i = 0; i < 4; i++)
                    for (int j = 0; j < 3; j++)
                    {
                        SimplexMat[i, j] = tempSimplexMat[i, j];
                        tempSimplexMat[i, j] = 0;
                    }
            }

            TotalA = TotalB = 0;
            Flour1Left1 = Flour2Left1 = EggPowderLeft1 = 0;

            for (int i = 0; i < 3; i++)
            {
                switch (order[i + 2])
                {
                    case 1: TotalA1 = (int)SimplexMat[i, 2]; break;
                    case 2: TotalB1 = (int)SimplexMat[i, 2]; break;
                    case 3: Flour1Left1 = SimplexMat[i, 2]; break;
                    case 4: Flour2Left1 = SimplexMat[i, 2]; break;
                    case 5: EggPowderLeft1 = SimplexMat[i, 2]; break;
                    default: break;
                }
            }
            TotalSellPrice1 = SimplexMat[3, 2];
        }
    }
}
