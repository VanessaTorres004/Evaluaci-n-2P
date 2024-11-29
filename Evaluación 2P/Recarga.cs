using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evaluación_2P
{
   
    
        public class Recarga : BindableObject
        {
            private string _ultimaRecarga;

            public string UltimaRecarga
            {
                get => _ultimaRecarga;
                set
                {
                    _ultimaRecarga = value;
                    OnPropertyChanged();
                }
            }
        }
    }

