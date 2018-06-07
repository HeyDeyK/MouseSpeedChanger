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
        public const UInt32 SPI_SETMOUSESPEED = 0x0071;
        [DllImport("user32.dll")]
        static extern Boolean SystemParametersInfo(
            UInt32 uiAction,
            UInt32 uiParam,
            UInt32 pvParam,
            UInt32 fWinIni);   // Not used     

        private double _SliderPomerValue = 1;
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
        public RelayCommand ClickCommand { get; }
        public AppViewModel()
        {
            ClickCommand = new RelayCommand(ClickMethod);

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
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(name));
        }
    }
}
