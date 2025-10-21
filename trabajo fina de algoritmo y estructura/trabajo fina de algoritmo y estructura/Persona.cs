using System;
using System.Collections.Generic;
// Se eliminan los 'using' innecesarios (Linq, Text, Threading.Tasks)

namespace trabajo_fina_de_algoritmo_y_estructura
{
   internal class Persona
    {
        // Propiedades que coinciden con las columnas de tu tabla 'Personas'
        public string Dni { get; set; }
        public string Apellido { get; set; }
        public string Nombre { get; set; }
        public string Telefono { get; set; }

        // Sobreescritura de ToString() para la visualización en el ListBox
        public override string ToString()
        {
            return $"DNI: {Dni} | Apellido: {Apellido}, {Nombre}";
        }
    }
}
