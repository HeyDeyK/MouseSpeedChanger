using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Command;
using System.Runtime.InteropServices;


namespace MouseSpeed
{
    class AppViewModel : INotifyPropertyChanged
    {
        public bool SwapButtons = false;
        public const UInt32 SPI_SETMOUSESPEED = 0x0071;
        public const UInt32 SPI_SETWHEELSCROLLLINES = 0x0069;
        public const UInt32 SPI_SETMOUSEBUTTONSWAP = 0x0021;
        public const UInt32 SPI_SETDOUBLECLICKTIME = 0x0020;

        [DllImport("user32.dll")]
        static extern Boolean SystemParametersInfo(
            UInt32 uiAction,
            UInt32 uiParam,
            UInt32 pvParam,
            UInt32 fWinIni);   // Not used     
        



        private double _SliderPomerValue = 5;
        public double SliderPomerValue
        {
            get => _SliderPomerValue;
            set
            {
                double StepValue = 1;
                var newStep = Math.Round(value / StepValue);

                _SliderPomerValue = newStep * StepValue;
                Console.WriteLine((int)SliderPomerValue);
                ChangeMouseSpeed((int)SliderPomerValue);
                OnPropertyChanged("SliderPomerValue");
            }
        }
        private double _SliderWheelSpeed = 3;
        public double SliderWheelSpeed
        {
            get => _SliderWheelSpeed;
            set
            {
                double StepValue = 1;
                var newStep = Math.Round(value / StepValue);

                _SliderWheelSpeed = newStep * StepValue;
                Console.WriteLine((int)SliderWheelSpeed);
                ChangeWheelSpeed((int)SliderWheelSpeed);
                OnPropertyChanged("SliderWheelSpeed");
            }
        }
        private double _SliderDoubleClickSpeed = 5;
        public double SliderDoubleClickSpeed
        {
            get => _SliderDoubleClickSpeed;
            set
            {
                double StepValue = 1;
                var newStep = Math.Round(value / StepValue);

                _SliderDoubleClickSpeed = newStep * StepValue;
                Console.WriteLine((int)SliderDoubleClickSpeed);
                ChangeDoubleSpeed((int)SliderDoubleClickSpeed * 1000);
                OnPropertyChanged("SliderDoubleClickSpeed");
            }
        }
        private void ChangeMouseSpeed(int cislo)
        {
            const UInt32 SPI_SETMOUSESPEED = 0x0071;
            UInt32 MOUSESPEED = Convert.ToUInt32(cislo);

            SystemParametersInfo(
                SPI_SETMOUSESPEED,
                0,
                MOUSESPEED,
                0);
        }
        private void ChangeWheelSpeed(int cislo)
        {
            const UInt32 SPI_SETWHEELSCROLLLINES = 0x0069;
            UInt32 WHEELSPEED = Convert.ToUInt32(cislo);

            SystemParametersInfo(
                SPI_SETWHEELSCROLLLINES,
                WHEELSPEED,
                0,
                0);
        }
        private void ChangeSwapButtons(bool zmena)
        {
            const UInt32 SPI_SETMOUSEBUTTONSWAP = 0x0021;
            UInt32 SWAPBUTTONS = Convert.ToUInt32(zmena);


            SystemParametersInfo(
                SPI_SETMOUSEBUTTONSWAP,
                SWAPBUTTONS,
                0,
                0);
        }
        private void ChangeDoubleSpeed(int cislo)
        {
            const UInt32 SPI_SETDOUBLECLICKTIME = 0x0020;
            UInt32 DOUBLECLICKSPEED = Convert.ToUInt32(cislo);


            SystemParametersInfo(
                SPI_SETDOUBLECLICKTIME,
                DOUBLECLICKSPEED,
                0,
                0);
        }
        public RelayCommand ClickCommand { get; set; }
        public RelayCommand CmdSwapButtons { get; set; }
        public AppViewModel()
        {
            ClickCommand = new RelayCommand(ClickMethod);
            CmdSwapButtons = new RelayCommand(SwapMethod);

        }
        private void ClickMethod()
        {
            const UInt32 SPI_SETMOUSESPEED = 0x0071;
            const UInt32 MOUSESPEED = 5;

            SystemParametersInfo(
                SPI_SETMOUSESPEED,
                0,
                MOUSESPEED,
                0);
            SliderPomerValue = 5;
            OnPropertyChanged("SliderPomerValue");
        }
        public void SwapMethod()
        {
            Console.WriteLine("menim");
            if (SwapButtons)
            {
                SwapButtons = false;
                ChangeSwapButtons(SwapButtons);
                OnPropertyChanged("SwapButtons");
            }
            else
            {
                SwapButtons = true;
                ChangeSwapButtons(SwapButtons);
                OnPropertyChanged("SwapButtons");
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(name));
        }
    }
}
