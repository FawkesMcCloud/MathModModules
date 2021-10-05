using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathModMudules.Logic
{
    class Module1 : BasicVM
    {
        private RelayCommand relayCommand;

        private float Flour1, Flour2, EggPowder;

        private float AProductFlour1, AProductFlour2, AProductEggPowder;
        private float BProductFlour1, BProductFlour2, BProductEggPowder;

        private float SellPriceProdA, SellPriceProdB, TotalSellPrice;
        private int TotalA, TotalB;

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

        public Module1()
        {
            RelayCommand = new RelayCommand(action => Calculate(), check => CanCalculate());

            Flour11 = 624;
            Flour21 = 541;
            EggPowder1 = 376;

            AProductFlour11 = 14;
            AProductFlour21 = 12;
            AProductEggPowder1 = 8;

            BProductEggPowder1 = 2;
            BProductFlour11 = 8;
            BProductFlour21 = 4;

            SellPriceProdA1 = 7;
            SellPriceProdB1 = 3;

            TotalSellPrice1 = 0;
            TotalA1 = 0;
            TotalB1 = 0;
        }

        public void Calculate()
        {
            int tempTotalA = 0;
            int tempTotalB = 0;

            while ((tempTotalA + 1) * AProductEggPowder1 <= EggPowder1 &&
                    (tempTotalA + 1) * AProductFlour11 <= Flour11 &&
                    (tempTotalA + 1) * AProductFlour21 <= Flour21)
            {
                tempTotalA++;
            }


            float currentMaxPrice = 0;
            for (; tempTotalA >= 0; tempTotalA--)
            {
                while ((tempTotalB + 1) * BProductFlour11 + tempTotalA * AProductFlour11 <= Flour11 &&
                        (tempTotalB + 1) * BProductFlour21 + tempTotalA * AProductFlour21 <= Flour21 &&
                        (tempTotalB + 1) * BProductEggPowder1 + tempTotalA * AProductEggPowder1 <= EggPowder1)
                {
                    tempTotalB++;
                }

                if (currentMaxPrice < tempTotalA * SellPriceProdA1 + tempTotalB * SellPriceProdB1)
                {
                    currentMaxPrice = tempTotalA * SellPriceProdA1 + tempTotalB * SellPriceProdB1;
                    TotalA1 = tempTotalA;
                    TotalB1 = tempTotalB;
                }
            }

            TotalSellPrice1 = SellPriceProdA1 * TotalA1 + SellPriceProdB1 * TotalB1;
        }

        public override string ToString()
        {
            return $"MaxSellPrice = {TotalSellPrice1} \nTotalA = {TotalA1} \nTotalB = {TotalB1}";
        }
    }
}
