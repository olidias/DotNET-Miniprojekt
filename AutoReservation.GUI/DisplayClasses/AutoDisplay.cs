using AutoReservation.Common.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoReservation.GUI.DisplayClasses
{
    public class AutoDisplay : AutoDto
    {
        public override string ToString()
        {
            return $"{Marke}, {AutoKlasse} {Tagestarif}.-";
        }
    }
}
