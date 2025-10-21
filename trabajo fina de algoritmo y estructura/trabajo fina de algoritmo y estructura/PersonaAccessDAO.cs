using System;
using System.Collections.Generic;
using System.Data; // Añadido para DBNull.Value
using System.Data.OleDb;
using System.Windows.Forms;
using trabajo_fina_de_algoritmo_y_estructura;
internal class PersonaAccessDAO
{// =========================================================================
 // CONSTANTES Y CADENA DE CONEXIÓN
 // =========================================================================

    // CORRECCIÓN: Definición de la constante dentro de la clase para evitar el error CS0103.
    private const string TABLA_PERSONA = "Personas";

    // **IMPORTANTE:** Verifica que esta ruta y el proveedor sean correctos.
    // CADENA DE CONEXIÓN CORREGIDA (usando el nombre de usuario "Nano" para el ejemplo)
    private readonly string connectionString =
        "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\Users\\Nano\\Desktop\\base de datos prueba\\base de datos prueba.accdb";

    // =========================================================================
    // 1. OBTENER / BUSCAR
    // =========================================================================

    public List<Persona> ObtenerPersonas(string dniFiltro, string apellidoFiltro)
    {
        List<Persona> personas = new List<Persona>();
        string query = $"SELECT Dni, Apellido, Nombre, Telefono FROM {TABLA_PERSONA} WHERE 1=1";

        // Usamos '?' en la consulta, y los agregaremos en orden
        if (!string.IsNullOrEmpty(dniFiltro))
        {
            query += " AND Dni LIKE ?";
        }

        if (!string.IsNullOrEmpty(apellidoFiltro))
        {
            query += " AND Apellido LIKE ?";
        }

        query += " ORDER BY Apellido, Nombre";

        try
        {
            using (var connection = new OleDbConnection(connectionString))
            using (var command = new OleDbCommand(query, connection))
            {
                connection.Open();

                // IMPORTANTE: Solo importa el orden en que se agregan.
                if (!string.IsNullOrEmpty(dniFiltro))
                {
                    // Usamos * para comodín en Access
                    command.Parameters.AddWithValue("?", $"*{dniFiltro}*");
                }
                if (!string.IsNullOrEmpty(apellidoFiltro))
                {
                    command.Parameters.AddWithValue("?", $"*{apellidoFiltro}*");
                }

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        personas.Add(new Persona
                        {
                            Dni = reader["Dni"].ToString(),
                            Apellido = reader["Apellido"].ToString(),
                            // Manejo seguro de nulos de base de datos
                            Nombre = reader["Nombre"] is DBNull ? string.Empty : reader["Nombre"].ToString(),
                            Telefono = reader["Telefono"] is DBNull ? string.Empty : reader["Telefono"].ToString()
                        });
                    }
                }
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error al cargar datos: {ex.Message}", "Error de Base de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        return personas;
    }

    // =========================================================================
    // 2. AGREGAR, 3. MODIFICAR, 4. ELIMINAR
    // =========================================================================

    public bool AgregarPersona(Persona persona)
    {
        string query = $"INSERT INTO {TABLA_PERSONA} (Dni, Apellido, Nombre, Telefono) VALUES (?, ?, ?, ?)";
        return EjecutarComando(query, persona);
    }

    public bool ModificarPersona(Persona persona)
    {
        string query = $"UPDATE {TABLA_PERSONA} SET Apellido = ?, Nombre = ?, Telefono = ? WHERE Dni = ?";
        return EjecutarComando(query, persona);
    }

    public bool EliminarPersona(string dni)
    {
        string query = $"DELETE FROM {TABLA_PERSONA} WHERE Dni = ?";
        Persona personaTemp = new Persona { Dni = dni };
        return EjecutarComando(query, personaTemp);
    }

    // =========================================================================
    // MÉTODO AUXILIAR PARA INSERT/UPDATE/DELETE
    // =========================================================================

    private bool EjecutarComando(string query, Persona persona)
    {
        try
        {
            using (var connection = new OleDbConnection(connectionString))
            using (var command = new OleDbCommand(query, connection))
            {
                connection.Open();

                // CORRECCIÓN CRÍTICA: Se eliminan los nombres de parámetros (@...) y se usa solo el orden para OLEDB.

                if (query.StartsWith($"INSERT INTO {TABLA_PERSONA}", StringComparison.OrdinalIgnoreCase))
                {
                    // INSERT: (1)Dni, (2)Apellido, (3)Nombre, (4)Telefono
                    command.Parameters.AddWithValue("?", persona.Dni);
                    command.Parameters.AddWithValue("?", persona.Apellido);
                    command.Parameters.AddWithValue("?", (object)persona.Nombre ?? DBNull.Value);
                    command.Parameters.AddWithValue("?", (object)persona.Telefono ?? DBNull.Value);
                }
                else if (query.StartsWith($"UPDATE {TABLA_PERSONA}", StringComparison.OrdinalIgnoreCase))
                {
                    // UPDATE: (1)Apellido, (2)Nombre, (3)Telefono, (4)Dni (Condición WHERE)
                    command.Parameters.AddWithValue("?", persona.Apellido);
                    command.Parameters.AddWithValue("?", (object)persona.Nombre ?? DBNull.Value);
                    command.Parameters.AddWithValue("?", (object)persona.Telefono ?? DBNull.Value);
                    command.Parameters.AddWithValue("?", persona.Dni);
                }
                else if (query.StartsWith($"DELETE FROM {TABLA_PERSONA}", StringComparison.OrdinalIgnoreCase))
                {
                    // DELETE: (1)Dni (Condición WHERE)
                    command.Parameters.AddWithValue("?", persona.Dni);
                }

                int filasAfectadas = command.ExecuteNonQuery();

                return filasAfectadas > 0;
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error en la operación ({query}): {ex.Message}", "Error de Base de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return false;
        }
    }
}
   
